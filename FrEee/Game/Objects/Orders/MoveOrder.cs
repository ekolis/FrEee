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

namespace FrEee.Game.Objects.Orders
{
	/// <summary>
	/// An order to move a mobile space object to a new location.
	/// </summary>
	[Serializable]
	public class MoveOrder<T> : IMobileSpaceObjectOrder<T>
		where T : IMobileSpaceObject<T>, IReferrable<object>
	{
		public MoveOrder(T target, Sector destination, bool avoidEnemies)
		{
			Target = target;
			Destination = destination;
			AvoidEnemies = avoidEnemies;
			// TODO - add flag for "avoid damaging sectors"? but how to specify in UI?
		}

		/// <summary>
		/// The object that is moving.
		/// </summary>
		[DoNotSerialize]
		public T Target { get { return target; } set { target = value; } }

		private Reference<T> target {get; set;}

		/// <summary>
		/// The sector we are moving to.
		/// </summary>
		[DoNotSerialize]
		public Sector Destination { get { return destination; } set { destination = value; } }

		private Reference<Sector> destination {get; set;}

		/// <summary>
		/// Should pathfinding avoid enemies?
		/// </summary>
		public bool AvoidEnemies { get; set; }

		/// <summary>
		/// Finds the path for executing this order.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Sector> Pathfind()
		{
			return Pathfinder.Pathfind(Target, Destination, AvoidEnemies);
		}

		public void Execute()
		{
			var gotoSector = Pathfind().FirstOrDefault();

			// TODO - movement logs
			if (gotoSector != null)
			{
				// move
				Target.FindSector().SpaceObjects.Remove(Target);
				gotoSector.SpaceObjects.Add(Target);
			}
			else
			{
				// TODO - log a message for the player that pathfinding failed, but only once per space object per turn
			}

			// spend time
			Target.TimeToNextMove += Target.TimePerMove;
		}

		public bool IsComplete
		{
			get { return Target.FindSector() == Destination; }
		}

		public override string ToString()
		{
			var coords = Destination.Coordinates;
			if (AvoidEnemies)
				return "Move to " + Destination.FindStarSystem().Name + " (" + coords.X + ", " + coords.Y + ")";
			else
				return "Attack " + Destination.FindStarSystem().Name + " (" + coords.X + ", " + coords.Y + ")";
		}
	}
}
