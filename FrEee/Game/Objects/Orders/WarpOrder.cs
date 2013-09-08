using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility.Extensions;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Enumerations;

namespace FrEee.Game.Objects.Orders
{
	/// <summary>
	/// An order to warp a mobile space object via a warp point.
	/// </summary>
	[Serializable]
	public class WarpOrder<T> : IMobileSpaceObjectOrder<T>
		where T : IMobileSpaceObject<T>, IReferrable
	{
		public WarpOrder(WarpPoint warpPoint)
		{
			Owner = Empire.Current;
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
			if (sobj.Owner.IsMinorEmpire)
			{
				sobj.Owner.Log.Add(sobj.CreateLogMessage(sobj + " cannot warp because minor empires cannot use warp points."));
				return;
			}

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

		public void Dispose()
		{
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

		public void ReplaceClientIDs(IDictionary<long, long> idmap)
		{
			// This type does not use client objects, so nothing to do here.
		}

		public long ID { get; set; }
	}
}
