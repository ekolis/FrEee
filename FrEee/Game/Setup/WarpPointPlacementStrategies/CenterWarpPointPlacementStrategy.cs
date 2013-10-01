using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility; using FrEee.Utility.Serialization;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Setup.WarpPointPlacementStrategies
{
	/// <summary>
	/// Places warp points at the center of the system. Exploration is easy, but so is setting up chokepoints.
	/// </summary>
	public class CenterWarpPointPlacementStrategy : WarpPointPlacementStrategy
	{
		static CenterWarpPointPlacementStrategy()
		{
			Instance = new CenterWarpPointPlacementStrategy();
		}

		public static CenterWarpPointPlacementStrategy Instance { get; private set; }

		private CenterWarpPointPlacementStrategy()
			: base("Center", "Places warp points at the center of the system. Exploration is easy, but so is setting up chokepoints.")
		{
		}

		public override Sector GetWarpPointSector(ObjectLocation<StarSystem> here, ObjectLocation<StarSystem> there)
		{
			return here.Item.GetSector(0, 0);
		}
	}
}
