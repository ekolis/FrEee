﻿using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;
using System.Linq;

namespace FrEee.Game.Setup.WarpPointPlacementStrategies
{
    /// <summary>
    /// Places warp points at the location of stars. If there are no stars, warp points will be placed randomly. Exploration is easy, but so is setting up chokepoints.
    /// </summary>
    public class StarWarpPointPlacementStrategy : WarpPointPlacementStrategy
    {
        #region Public Constructors

        static StarWarpPointPlacementStrategy()
        {
            Instance = new StarWarpPointPlacementStrategy();
        }

        #endregion Public Constructors

        #region Private Constructors

        private StarWarpPointPlacementStrategy()
            : base("Star", "Places warp points at the location of stars. If there are no stars, warp points will be placed randomly. Exploration is easy, but so is setting up chokepoints.")
        {
        }

        #endregion Private Constructors

        #region Public Properties

        public static StarWarpPointPlacementStrategy Instance { get; private set; }

        #endregion Public Properties

        #region Public Methods

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

        #endregion Public Methods
    }
}
