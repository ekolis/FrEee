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
	/// Places warp points at the location of stars. If there are no stars, warp points will be placed randomly. Exploration is easy, but so is setting up chokepoints.
	/// </summary>
	public class StarWarpPointPlacementStrategy : WarpPointPlacementStrategy
	{
		static StarWarpPointPlacementStrategy()
		{
			Instance = new StarWarpPointPlacementStrategy();
		}

		public static StarWarpPointPlacementStrategy Instance { get; private set; }

		private StarWarpPointPlacementStrategy()
			: base("Star", "Places warp points at the location of stars. If there are no stars, warp points will be placed randomly. Exploration is easy, but so is setting up chokepoints.")
		{
		}

		public override Sector GetWarpPointSector(ObjectLocation<StarSystem> here, ObjectLocation<StarSystem> there)
		{
			var stars = here.Item.FindSpaceObjects<Star>();
			if (stars.Any())
			{
				var star = stars.PickRandom();
				return star.FindSector();
			}
			else
				return RandomWarpPointPlacementStrategy.Instance.GetWarpPointSector(here, there);
		}
	}
}
