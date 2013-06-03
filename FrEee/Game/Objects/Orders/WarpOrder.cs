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

		/// <summary>
		/// Finds the path for executing this order.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Sector> Pathfind()
		{
			return Pathfinder.Pathfind(Target, WarpPoint.FindSector(), AvoidEnemies);
		}

		public void Execute()
		{
			var here = Target.FindSector();
			if (here == WarpPoint.FindSector())
			{
				// warp now!!!
				here.SpaceObjects.Remove(Target);
				WarpPoint.Target.SpaceObjects.Add(Target);
				// mark system explored
				if (!WarpPoint.TargetStarSystemLocation.Item.ExploredByEmpires.Contains(((ISpaceObject)Target).Owner))
					WarpPoint.TargetStarSystemLocation.Item.ExploredByEmpires.Add(((ISpaceObject)Target).Owner);

				// done warping
				IsComplete = true;
			}
			else
			{
				// move toward warp point
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

			// spend time
			Target.TimeToNextMove += Target.TimePerMove;
		}

		public bool IsComplete
		{
			get;
			private set;
		}

		public override string ToString()
		{
			if (AvoidEnemies)
				return "Warp via " + WarpPoint.Name + " in " + WarpPoint.FindStarSystem();
			else
				return "Attack and Warp via " + WarpPoint.Name + " in " + WarpPoint.FindStarSystem();
		}
	}
}
