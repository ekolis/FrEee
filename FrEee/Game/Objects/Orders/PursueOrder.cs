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
	/// An order to move a mobile space object toward another space object.
	/// </summary>
	[Serializable]
	public class PursueOrder<T> : IMovementOrder<T>
		where T : IMobileSpaceObject, IReferrable
	{
		public PursueOrder(ISpaceObject target, bool avoidEnemies)
		{
			Owner = Empire.Current;
			Target = target;
			AvoidEnemies = avoidEnemies;
			// TODO - add flag for "avoid damaging sectors"? but how to specify in UI?
		}

		/// <summary>
		/// The target we are pursuing.
		/// </summary>
		public ISpaceObject Target { get { return target.Value; } set { target = value.Reference(); } }

		private Reference<ISpaceObject> target {get; set;}

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
		/// <returns></returns>
		public IEnumerable<Sector> Pathfind(IMobileSpaceObject me, Sector start)
		{
			return Pathfinder.Pathfind(me, start, Target.FindSector(), AvoidEnemies, true, me.DijkstraMap);
		}

		public void Execute(T sobj)
		{
			// TODO - movement logs
			if (sobj.FindSector() == Target.FindSector())
				IsComplete = true;
			else
			{
				var gotoSector = Pathfind(sobj, sobj.FindSector()).FirstOrDefault();
				if (gotoSector != null)
				{
					// move
					sobj.FindStarSystem().Remove(sobj);
					gotoSector.Place(sobj);
					sobj.RefreshDijkstraMap();
				}
				else if (!LoggedPathfindingError)
				{
					// log pathfinding error
					PathfindingError = sobj.CreateLogMessage(sobj + " could not pursue " + Target + " because there is no available path available leading toward " + Target + " or " + sobj + " is immobile.");
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
			if (AvoidEnemies && Target.Owner != null && !Target.IsHostileTo(Owner))
				return "Escort " + Target;
			else
				return "Pursue " + Target;
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

		public Sector Destination
		{
			get { return Target.FindSector(); }
		}

		public IDictionary<PathfinderNode<Sector>, ISet<PathfinderNode<Sector>>> CreateDijkstraMap(IMobileSpaceObject me, Sector start)
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
	}
}
