using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;
using System.Linq;

namespace FrEee.Game.Setup.WarpPointPlacementStrategies
{
    /// <summary>
    /// Places warp points at the location of planets. If there are no planets, warp points will be placed randomly. Planets on warp points will be particularly vulnerable to attack.
    /// </summary>
    public class PlanetWarpPointPlacementStrategy : WarpPointPlacementStrategy
    {
        #region Public Constructors

        static PlanetWarpPointPlacementStrategy()
        {
            Instance = new PlanetWarpPointPlacementStrategy();
        }

        #endregion Public Constructors

        #region Private Constructors

        private PlanetWarpPointPlacementStrategy()
            : base("Planet", "Places warp points at the location of planets. If there are no planets, warp points will be placed randomly. Planets on warp points will be particularly vulnerable to attack.")
        {
        }

        #endregion Private Constructors

        #region Public Properties

        public static PlanetWarpPointPlacementStrategy Instance { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public override Sector GetWarpPointSector(ObjectLocation<StarSystem> here, ObjectLocation<StarSystem> there)
        {
            var planets = here.Item.FindSpaceObjects<Planet>();
            if (planets.Any())
            {
                var planet = planets.PickRandom();
                return planet.Sector;
            }
            else
                return RandomWarpPointPlacementStrategy.Instance.GetWarpPointSector(here, there);
        }

        #endregion Public Methods
    }
}
