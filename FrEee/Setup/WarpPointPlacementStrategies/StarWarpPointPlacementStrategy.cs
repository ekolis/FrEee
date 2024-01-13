using FrEee.Objects.Space;
using FrEee.Extensions;
using System.Linq;

namespace FrEee.Setup.WarpPointPlacementStrategies
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

		private StarWarpPointPlacementStrategy()
			: base("Star", "Places warp points at the location of stars. If there are no stars, warp points will be placed randomly. Exploration is easy, but so is setting up chokepoints.")
		{
		}

		public static StarWarpPointPlacementStrategy Instance { get; private set; }

		public override Sector GetWarpPointSector(ObjectLocation<StarSystem> here, ObjectLocation<StarSystem> there)
		{
			var stars = here.Item.FindSpaceObjects<Star>();
			if (stars.Any())
			{
				var star = stars.PickRandom();
				return star.Sector;
			}
			else
				return RandomWarpPointPlacementStrategy.Instance.GetWarpPointSector(here, there);
		}
	}
}