using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility; using Newtonsoft.Json;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Setup.WarpPointPlacementStrategies
{
	/// <summary>
	/// Places warp points randomly within a system.
	/// </summary>
	public class RandomWarpPointPlacementStrategy : WarpPointPlacementStrategy
	{
		static RandomWarpPointPlacementStrategy()
		{
			Instance = new RandomWarpPointPlacementStrategy();
		}

		public static RandomWarpPointPlacementStrategy Instance { get; private set; }

		private RandomWarpPointPlacementStrategy()
			: base("Random", "Places warp points randomly within a system.")
		{
		}

		public override Sector GetWarpPointSector(ObjectLocation<StarSystem> here, ObjectLocation<StarSystem> there)
		{
			var x = RandomHelper.Range(-here.Item.Radius, here.Item.Radius);
			var y = RandomHelper.Range(-here.Item.Radius, here.Item.Radius);
			return here.Item.GetSector(x, y);
		}
	}
}
