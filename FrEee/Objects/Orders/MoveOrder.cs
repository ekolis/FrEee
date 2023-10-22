using FrEee.Enumerations;
using FrEee.Extensions;
using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Objects.Combat;
using FrEee.Objects.LogMessages;
using FrEee.Objects.Space;
using FrEee.Serialization;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Objects.Orders
{
	/// <summary>
	/// An order to move a mobile space object to a new location.
	/// </summary>
	[Serializable]
	public class MoveOrder : IMovementOrder
	{
		public MoveOrder(Sector destination, bool avoidEnemies)
		{
			Owner = Empire.Current;
			Destination = destination;
			AvoidEnemies = avoidEnemies;
			// TODO - add flag for "avoid damaging sectors"? but how to specify in UI?
		}

		/// <summary>
		/// Should pathfinding avoid enemies?
		/// </summary>
		public bool AvoidEnemies { get; set; }

		public bool ConsumesMovement
		{
			get { return true; }
		}

		/// <summary>
		/// The sector we are moving to.
		/// </summary>
		public Sector Destination { get; set; }

		public long ID { get; set; }

		public bool IsComplete
		{
			get;
			set;
		}

		public bool IsDisposed { get; set; }

		/// <summary>
		/// Did we already log a pathfinding error this turn?
		/// </summary>
		[DoNotSerialize]
		public bool LoggedPathfindingError { get; private set; }

		/// <summary>
		/// The empire which issued the order.
		/// </summary>
		[GameReference]
		public Empire Owner { get; set; }

		/// <summary>
		/// Any pathfinding error that we might have found.
		/// </summary>
		[DoNotSerialize]
		public LogMessage PathfindingError { get; private set; }

		public bool CheckCompletion(IOrderable v)
		{
			return IsComplete;
		}

		/// <summary>
		/// Orders are visible only to their owners.
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public Visibility CheckVisibility(Empire emp)
		{
			if (emp == Owner)
				return Visibility.Visible;
			return Visibility.Unknown;
		}

		/// <summary>
		/// Creates a Dijkstra map for this order's movement.
		/// </summary>
		/// <param name="me"></param>
		/// <param name="start"></param>
		/// <returns></returns>
		public IDictionary<PathfinderNode<Sector>, ISet<PathfinderNode<Sector>>> CreateDijkstraMap(IMobileSpaceObject me, Sector start)
		{
			return Pathfinder.CreateDijkstraMap(me, start, Destination, AvoidEnemies, true);
		}

		public void Dispose()
		{
			if (IsDisposed)
				return;
			foreach (var v in The.Game.Referrables.OfType<IMobileSpaceObject>())
				v.RemoveOrder(this);
			The.ReferrableRepository.Remove(this);
		}

		public void Execute(IOrderable ord)
		{
			if (ord is IMobileSpaceObject sobj)
			{
				// TODO - movement logs
				if (sobj.Sector == Destination)
				{
					IsComplete = true;
					return;
				}
				else
				{
					var gotoSector = Pathfind(sobj, sobj.Sector).FirstOrDefault();
					if (gotoSector != null)
					{
						// move
						sobj.Sector = gotoSector;
						sobj.RefreshDijkstraMap();

						// consume supplies
						sobj.BurnMovementSupplies();

						// resupply space vehicles
						// either this vehicle from other space objects, or other vehicles from this one
						// TODO - this should really be done AFTER battles...
						if (gotoSector.HasAbility("Supply Generation", sobj.Owner))
						{
							foreach (var v in gotoSector.SpaceObjects.OfType<IMobileSpaceObject>().Where(v => v.Owner == sobj.Owner))
								v.SupplyRemaining = v.SupplyStorage;
						}
						if (gotoSector.StarSystem.HasAbility("Supply Generation - System", sobj.Owner) || gotoSector.StarSystem.HasAbility("Supply Generation - System"))
						{
							foreach (var v in gotoSector.StarSystem.FindSpaceObjects<IMobileSpaceObject>().Where(v => v.Owner == sobj.Owner))
								v.SupplyRemaining = v.SupplyStorage;
						}

						// is it done?
						if (gotoSector == Destination)
							IsComplete = true;

						// apply damage from damaging sectors
						// TODO - apply damage from damaging systems too
						// TODO - move this out into the Place method so it applies to all movement-type orders and newly constructed vehicles
						foreach (var damager in gotoSector.SpaceObjects.Where(dsobj => dsobj.HasAbility("Sector - Damage")))
						{
							var damage = damager.GetAbilityValue("Sector - Damage").ToInt();
							// TODO - let sector damage have special damage types?
							var shot = new Shot(null, null, sobj, 0);
							var hit = new Hit(shot, sobj, damage);
							sobj.TakeDamage(hit, null);
							sobj.Owner.Log.Add(sobj.CreateLogMessage(sobj + " took " + damage + " damage from entering " + damager + "'s sector.", LogMessageType.Generic));
							sobj.ReplenishShields();
						}
					}
					else if (!LoggedPathfindingError)
					{
						// log pathfinding error
						string reason;
						if (sobj.StrategicSpeed <= 0)
							reason = sobj + " is immobile";
						else
							reason = "there is no available path leading toward " + Destination;
						PathfindingError = sobj.CreateLogMessage(sobj + " could not move to " + Destination + " because " + reason + ".", LogMessageType.Warning);
						sobj.Owner?.Log.Add(PathfindingError);
						LoggedPathfindingError = true;
					}
				}

				// spend time
				sobj.SpendTime(sobj.TimePerMove);
			}
			else
				ord.Owner.RecordLog(ord, $"{ord} cannot be ordered to move because it is not a mobile space object.", LogMessageType.Error);
		}

		public IEnumerable<LogMessage> GetErrors(IOrderable v)
		{
			if (PathfindingError != null)
				yield return PathfindingError;
		}

		/// <summary>
		/// Finds the path for executing this order.
		/// </summary>
		/// <param name="sobj">The space object executing the order.</param>
		/// <returns></returns>
		public IEnumerable<Sector> Pathfind(IMobileSpaceObject me, Sector start)
		{
			return Pathfinder.Pathfind(me, start, Destination, AvoidEnemies, true, me.DijkstraMap);
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done)
		{
			// This type does not use client objects, so nothing to do here.
		}

		public override string ToString()
		{
			var coords = Destination.Coordinates;
			if (Destination == null || Destination.StarSystem == null)
				return "(Unknown Move Order)";
			if (AvoidEnemies)
				return "Move to " + Destination.StarSystem.Name + " (" + coords.X + ", " + coords.Y + ")";
			else
				return "Attack " + Destination.StarSystem.Name + " (" + coords.X + ", " + coords.Y + ")";
		}
	}
}
