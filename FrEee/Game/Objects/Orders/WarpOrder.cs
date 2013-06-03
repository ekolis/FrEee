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
	/// An order to warp a mobile space object via a warp point.
	/// </summary>
	[Serializable]
	public class WarpOrder<T> : IMobileSpaceObjectOrder<T>
		where T : IMobileSpaceObject<T>, IReferrable<object>
	{
		public WarpOrder(T target, WarpPoint warpPoint, bool avoidEnemies)
		{
			Target = target;
			WarpPoint = warpPoint;
			AvoidEnemies = avoidEnemies;
			// TODO - add flag for "avoid damaging sectors"? but how to specify in UI?
		}

		/// <summary>
		/// The object that is warping.
		/// </summary>
		public T Target { get { return target; } set { target = value; } }

		private Reference<T> target;

		/// <summary>
		/// The warp point we are using.
		/// </summary>
		public WarpPoint WarpPoint { get { return warpPoint; } set { warpPoint = value; } }

		private Reference<WarpPoint> warpPoint;

		/// <summary>
		/// Should pathfinding avoid enemies?
		/// </summary>
		public bool AvoidEnemies { get; set; }

		public void Execute()
		{
			var here = Target.FindSector();
			var hereSys = here.FindStarSystem();
			Sector gotoSector = WarpPoint.FindSector();
			if (here == gotoSector)
			{
				// warp now!!!
				here.SpaceObjects.Remove(Target);
				WarpPoint.Target.SpaceObjects.Add(Target);

				// spend time
				Target.TimeToNextMove += Target.TimePerMove;

				// done warping
				IsComplete = true;
			}
			else if (hereSys == gotoSector.FindStarSystem())
			{
				// pathfind within system
				var hereCoords = here.Coordinates;
				var thereCoords = gotoSector.Coordinates;
				var dx = Math.Sign(thereCoords.X - hereCoords.X);
				var dy = Math.Sign(thereCoords.Y - hereCoords.Y);
				// TODO - smart pathfinding that can avoid obstacles and enemies
				gotoSector = hereSys.GetSector(hereCoords.X + dx, hereCoords.Y + dy);
			}
			else
			{
				// TODO - pathfind via warp points
				gotoSector = null;
			}

			// TODO - movement logs
			if (!IsComplete && gotoSector != null)
			{
				// move toward warp point
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
			get;
			private set;
		}

		public override string ToString()
		{
			return "Warp via " + WarpPoint.Name + " in " + WarpPoint.FindStarSystem();
		}
	}
}
