using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility.Extensions;
using System.Drawing;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Enumerations;
using FrEee.Game.Objects.LogMessages;

namespace FrEee.Game.Objects.Orders
{
	/// <summary>
	/// An order to move a mobile space object to a new location.
	/// </summary>
	[Serializable]
	public class MoveOrder<T> : IMovementOrder<T>
		where T : IMobileSpaceObject, IReferrable
	{
		public MoveOrder(Sector destination, bool avoidEnemies)
		{
			Owner = Empire.Current;
			Destination = destination;
			AvoidEnemies = avoidEnemies;
			// TODO - add flag for "avoid damaging sectors"? but how to specify in UI?
		}

		/// <summary>
		/// The sector we are moving to.
		/// </summary>
		public Sector Destination { get; set; }

		/// <summary>
		/// Should pathfinding avoid enemies?
		/// </summary>
		public bool AvoidEnemies { get; set; }

		/// <summary>
		/// Finds the path for executing this order.
		/// </summary>
		/// <param name="sobj">The space object executing the order.</param>
		/// <returns></returns>
		public IEnumerable<Sector> Pathfind(IMobileSpaceObject me, Sector start)
		{
			return Pathfinder.Pathfind(me, start, Destination, AvoidEnemies, true, me.DijkstraMap);
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

		public void Execute(T sobj)
		{
			// TODO - movement logs
			if (sobj.FindSector() == Destination)
			{
				IsComplete = true;
				return;
			}
			else
			{
				var gotoSector = Pathfind(sobj, sobj.FindSector()).FirstOrDefault();
				if (gotoSector != null)
				{
					// move
					sobj.FindStarSystem().Remove(sobj);
					gotoSector.Place(sobj);
					sobj.RefreshDijkstraMap();

					// is it done?
					if (gotoSector == Destination)
						IsComplete = true;

					// apply damage from damaging sectors
					foreach (var damager in gotoSector.SpaceObjects.Where(dsobj => dsobj.HasAbility("Sector - Damage")))
					{
						var damage = damager.GetAbilityValue("Sector - Damage").ToInt();
						// TODO - let sector damage have special damage types?
						// TODO - use a static damage type instance for normal damage
						sobj.TakeDamage(new Combat.DamageType { Name = "Normal" }, damage, null);
						sobj.Owner.Log.Add(sobj.CreateLogMessage(sobj + " took " + damage + " damage from entering " + damager + "'s sector."));
						sobj.ReplenishShields();
					}
				}
				else if (!LoggedPathfindingError)
				{
					// log pathfinding error
					PathfindingError = sobj.CreateLogMessage(sobj + " could not move to " + Destination + " because there is no available path leading toward " + Destination + " or " + sobj + " is immobile.");
					sobj.Owner.Log.Add(PathfindingError);
					LoggedPathfindingError = true;
				}
			}

			// spend time
			sobj.SpendTime(sobj.TimePerMove);
		}

		public bool IsComplete
		{
			get;
			private set;
		}

		public override string ToString()
		{
			var coords = Destination.Coordinates;
			if (AvoidEnemies)
				return "Move to " + Destination.StarSystem.Name + " (" + coords.X + ", " + coords.Y + ")";
			else
				return "Attack " + Destination.StarSystem.Name + " (" + coords.X + ", " + coords.Y + ")";
		}

		/// <summary>
		/// Did we already log a pathfinding error this turn?
		/// </summary>
		[DoNotSerialize]
		public bool LoggedPathfindingError { get; private set; }

		/// <summary>
		/// Any pathfinding error that we might have found.
		/// </summary>
		[DoNotSerialize]
		public LogMessage PathfindingError { get; private set; }

		public void Dispose()
		{
			if (IsDisposed)
				return;
			foreach (var v in Galaxy.Current.Referrables.OfType<IMobileSpaceObject>())
				v.RemoveOrder(this);
			Galaxy.Current.UnassignID(this);
		}

		private Reference<Empire> owner { get; set; }

		/// <summary>
		/// The empire which issued the order.
		/// </summary>
		[DoNotSerialize]
		public Empire Owner { get { return owner; } set { owner = value; } }

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

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done)
		{
			// This type does not use client objects, so nothing to do here.
		}

		public long ID { get; set; }

		public bool CheckCompletion(T v)
		{
			return IsComplete;
		}

		public IEnumerable<LogMessage> GetErrors(T v)
		{
			if (PathfindingError != null)
				yield return PathfindingError;
		}

		public bool IsDisposed { get; set; }
	}
}
