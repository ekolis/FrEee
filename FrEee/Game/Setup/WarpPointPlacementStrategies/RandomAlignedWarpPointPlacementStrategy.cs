using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;

namespace FrEee.Game.Setup.WarpPointPlacementStrategies
{
    /// <summary>
    /// Places warp points randomly within a system, but aligned with the star system they lead to. Exploration is difficult due to needing to traverse entire systems.
    /// </summary>
    public class RandomAlignedWarpPointPlacementStrategy : WarpPointPlacementStrategy
    {
        #region Public Constructors

        static RandomAlignedWarpPointPlacementStrategy()
        {
            Instance = new RandomAlignedWarpPointPlacementStrategy();
        }

        #endregion Public Constructors

        #region Private Constructors

        private RandomAlignedWarpPointPlacementStrategy()
            : base("Random Aligned", "Places warp points randomly within a system, but aligned with the star system they lead to. Exploration is difficult due to needing to traverse entire systems.")
        {
        }

        #endregion Private Constructors

        #region Public Properties

        public static RandomAlignedWarpPointPlacementStrategy Instance { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public override Sector GetWarpPointSector(ObjectLocation<StarSystem> here, ObjectLocation<StarSystem> there)
        {
            var angle = here.Location.AngleTo(there.Location);
            var y = Math.Sin(angle / 180d * Math.PI) * here.Item.Radius;
            var x = Math.Cos(angle / 180d * Math.PI) * here.Item.Radius;
            var multiplier = RandomHelper.Next(here.Item.Radius / Math.Max(Math.Abs(x), Math.Abs(y)));
            x *= multiplier;
            y *= multiplier;
            return here.Item.GetSector((int)Math.Round(x), (int)Math.Round(y));
        }

        #endregion Public Methods
    }
}
