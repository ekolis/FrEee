using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Orders
{
	/// <summary>
	/// An order to pathfind relative to a target.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class PathfindingOrder<T>
		: IMovementOrder<T>, IPathfindingOrder where T : IMobileSpaceObject
	{
		protected PathfindingOrder(ISpaceObject target, bool avoidEnemies)
		{
			Owner = Empire.Current;
			Target = target;
			AvoidEnemies = avoidEnemies;
			// TODO - add flag for "avoid damaging sectors"? but how to specify in UI?
		}

		/// <summary>
		/// The target we are pursuing.
		/// </summary>
		[DoNotSerialize]
		public ISpaceObject Target { get { return target.Value; } set { target = value.ReferViaGalaxy(); } }

		private GalaxyReference<ISpaceObject> target { get; set; }

		/// <summary>
		/// Either the target itself, or the memory of the target, if it's not visible.
		/// </summary>
		public ISpaceObject KnownTarget
		{
			get
			{
				if (Target == null)
					return null;
				if (Target.CheckVisibility(Owner) >= Visibility.Visible)
					return Target;
				return Owner.Recall(Target);
			}
		}

		/// <summary>
		/// Alternate target. This should be the largest ship in a fleet when a fleet is being pursued.
		/// </summary>
		[DoNotSerialize]
		public ISpaceObject AlternateTarget
		{
			get;
			private set;
		}

		/// <summary>
		/// Call this when calling UpdateMemory on the target.
		/// Sets the alternate target to the largest ship in a fleet, if the target is a fleet.
		/// If the fleet is destroyed, sets the target to the alternate target.
		/// If the target is a ship, etc., and it is destroyed, sets the target to the memory of the target, or deletes the order if there is no memory.
		/// If the target is a memory, and the original object is sighted again, sets the target to the original object.
		/// Otherwise sets the alternate target to the target.
		/// </summary>
		public void UpdateAlternateTarget()
		{
			if (Target is Fleet)
			{
				var f = (Fleet)Target;
				if (!f.IsDestroyed)
					AlternateTarget = f.LeafVehicles.Largest();
				else
					Target = AlternateTarget;
			}
			else if (Target is IMobileSpaceObject)
			{
				var sobj = (IMobileSpaceObject)Target;
				if (sobj.IsMemory && sobj.FindOriginalObject(Owner) != null)
					Target = (ISpaceObject)sobj.FindOriginalObject(Owner);
				if (!sobj.IsDestroyed)
					AlternateTarget = Target;
				else if (Owner.Memory[Target.ID] != null)
					Target = (ISpaceObject)Owner.Memory[Target.ID];
				else
					Dispose();
			}
			else
				AlternateTarget = Target;
		}

		/// <summary>
		/// Should pathfinding avoid enemies?
		/// </summary>
		public bool AvoidEnemies { get; set; }

		/// <summary>
		/// Finds the path for executing this order.
		/// </summary>
		/// <param name="sobj">The space object executing the order.</param>
		/// <param name="start">The start location (need not be the current location, in case there are prior orders queued).</param>
		/// <returns></returns>
		public abstract IEnumerable<Sector> Pathfind(IMobileSpaceObject me, Sector start);

		public bool IsComplete
		{
			get;
			set;
		}

		public void Dispose()
		{
			if (IsDisposed)
				return;
			foreach (var v in Galaxy.Current.FindSpaceObjects<IMobileSpaceObject>())
				v.RemoveOrder(this);
			Galaxy.Current.UnassignID(this);
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
		
		private GalaxyReference<Empire> owner { get; set; }

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

		public Sector Destination
		{
			get { return KnownTarget.FindSector(); }
		}

		public IDictionary<PathfinderNode<Sector>, ISet<PathfinderNode<Sector>>> CreateDijkstraMap(T me, Sector start)
		{
			return Pathfinder.CreateDijkstraMap(me, start, Destination, AvoidEnemies, true);
		}

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

		/// <summary>
		/// A verb used to describe this order.
		/// </summary>
		public abstract string Verb { get; }

		public void Execute(T sobj)
		{
			// TODO - movement logs
			if (KnownTarget == null)
				IsComplete = true; // target is known to be dead
			else if (sobj.FindSector() == KnownTarget.FindSector())
				IsComplete = true; // we've arrived at the target
			else
			{
				var gotoSector = Pathfind(sobj, sobj.FindSector()).FirstOrDefault();
				if (gotoSector != null)
				{
					// move
					if (gotoSector == null)
					{
						// try to warp through an unexplored warp point
						var wps = sobj.Sector.SpaceObjects.OfType<WarpPoint>().Where(w => !w.TargetStarSystemLocation.Item.ExploredByEmpires.Contains(sobj.Owner));
						var wp = wps.PickRandom();
						if (wp != null)
						{
							// warp through the unexplored warp point
							sobj.Sector = wp.Target;
						}
						else if (!LoggedPathfindingError)
						{
							// no warp points to explore and we haven't told the player yet
							PathfindingError = sobj.CreateLogMessage("{0} found no unexplored warp points at {1} to enter.".F(sobj, sobj.Sector));
							sobj.Owner.Log.Add(PathfindingError);
							LoggedPathfindingError = true;
						}
					}
					else
					{
						sobj.Sector = gotoSector;
						sobj.RefreshDijkstraMap();

						// consume supplies
						sobj.BurnMovementSupplies();

						// resupply space vehicles
						// either this vehicle from other space objects, or other vehicles from this one
						// TODO - this should really be done AFTER battles...
						if (gotoSector.HasAbility("Supply Generation", sobj.Owner))
						{
							foreach (var v in gotoSector.SpaceObjects.OfType<SpaceVehicle>().Where(v => v.Owner == sobj.Owner))
								v.SupplyRemaining = v.SupplyStorage;
						}
						if (gotoSector.StarSystem.HasAbility("Supply Generation - System", sobj.Owner) || gotoSector.StarSystem.HasAbility("Supply Generation - System"))
						{
							foreach (var v in gotoSector.StarSystem.FindSpaceObjects<SpaceVehicle>().Where(v => v.Owner == sobj.Owner))
								v.SupplyRemaining = v.SupplyStorage;
						}
					}
				}
				else if (!LoggedPathfindingError)
				{
					// log pathfinding error
					string reason;
					if (sobj.Speed <= 0)
						reason = sobj + " is immobile";
					else
						reason = "there is no available path leading toward " + Destination;
					PathfindingError = sobj.CreateLogMessage(sobj + " could not " + Verb + " " + KnownTarget + " because " + reason + ".");
					sobj.Owner.Log.Add(PathfindingError);
					LoggedPathfindingError = true;
				}
			}

			// spend time
			sobj.SpendTime(sobj.TimePerMove);
		}

		public IDictionary<PathfinderNode<Sector>, ISet<PathfinderNode<Sector>>> CreateDijkstraMap(IMobileSpaceObject me, Sector start)
		{
			return Pathfinder.CreateDijkstraMap(me, start, Destination, AvoidEnemies, true);
		}

		public override string ToString()
		{
			if (KnownTarget == null)
				return "Unknown " + Verb + " order";
			return Verb.Capitalize() + " " + KnownTarget;
		}

		public bool ConsumesMovement
		{
			get { return true; }
		}
	}
}
