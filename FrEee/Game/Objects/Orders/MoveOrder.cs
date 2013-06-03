using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility.Extensions;

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
		public T Target { get { return target; } set { target = value; } }

		private Reference<T> target;

		/// <summary>
		/// The sector we are moving to.
		/// </summary>
		public Sector Destination { get { return destination; } set { destination = value; } }

		private Reference<Sector> destination;

		/// <summary>
		/// Should pathfinding avoid enemies?
		/// </summary>
		public bool AvoidEnemies { get; set; }

		public void Execute()
		{
			var here = Target.FindSector();
			var hereSys = here.FindStarSystem();
			Sector gotoSector = null;
			if (hereSys == Destination.FindStarSystem())
			{
				// pathfind within system
				var hereCoords = here.Coordinates;
				var thereCoords = Destination.Coordinates;
				var dx = Math.Sign(thereCoords.X - hereCoords.X);
				var dy = Math.Sign(thereCoords.Y - hereCoords.Y);
				// TODO - smart pathfinding that can avoid obstacles and enemies
				gotoSector = hereSys.GetSector(hereCoords.X + dx, hereCoords.Y + dy);
			}
			else
			{
				// TODO - pathfind via warp points
			}

			// TODO - movement logs
			if (gotoSector != null)
			{
				// move
				here.SpaceObjects.Remove(Target);
				gotoSector.SpaceObjects.Add(Target);

				// spend time
				Target.TimeToNextMove += Target.TimePerMove;
			}
			else
			{
				// TODO - log a message for the player that pathfinding failed, but only once per space object per turn
			}
		}

		public bool IsComplete
		{
			get { return Target.FindSector() == Destination; }
		}

		public override string ToString()
		{
			var coords = Destination.Coordinates;
			return "Move to " + Destination.FindStarSystem().Name + " (" + coords.X + ", " + coords.Y + ")";
		}
	}
}
