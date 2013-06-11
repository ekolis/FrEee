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
		public WarpOrder(WarpPoint warpPoint)
		{
			WarpPoint = warpPoint;
		}

		/// <summary>
		/// The warp point we are using.
		/// </summary>
		[DoNotSerialize]
		public WarpPoint WarpPoint { get { return warpPoint; } set { warpPoint = value; } }

		private Reference<WarpPoint> warpPoint {get; set;}

		public void Execute(T sobj)
		{
			var here = sobj.FindSector();
			if (here == WarpPoint.FindSector())
			{
				// warp now!!!
				here.SpaceObjects.Remove(sobj);
				WarpPoint.Target.SpaceObjects.Add(sobj);
				// mark system explored
				if (!WarpPoint.TargetStarSystemLocation.Item.ExploredByEmpires.Contains(((ISpaceObject)sobj).Owner))
					WarpPoint.TargetStarSystemLocation.Item.ExploredByEmpires.Add(((ISpaceObject)sobj).Owner);

				// done warping
				IsComplete = true;
			}
			else
			{
				// can't warp here, maybe the GUI should have issued a move order?
				((ISpaceObject)sobj).Owner.Log.Add(sobj.CreateLogMessage(sobj + " cannot warp via " + WarpPoint + " because it is not currently located at the warp point."));
			}

			// spend time
			sobj.TimeToNextMove += sobj.TimePerMove;
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
