using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;
using System;

namespace FrEee.Game.Setup.WarpPointPlacementStrategies
{
    /// <summary>
    /// Places warp points along the edge of the system, aligned with the star systems they lead to.
    /// </summary>
    public class EdgeAlignedWarpPointPlacementStrategy : WarpPointPlacementStrategy
    {
        #region Public Constructors

        static EdgeAlignedWarpPointPlacementStrategy()
        {
            Instance = new EdgeAlignedWarpPointPlacementStrategy();
        }

        #endregion Public Constructors

        #region Private Constructors

        private EdgeAlignedWarpPointPlacementStrategy()
            : base("Edge Aligned", "Places warp points along the edge of the system, aligned with the star systems they lead to.")
        {
        }

        #endregion Private Constructors

        #region Public Properties

        public static EdgeAlignedWarpPointPlacementStrategy Instance { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public override Sector GetWarpPointSector(ObjectLocation<StarSystem> here, ObjectLocation<StarSystem> there)
        {
            var angle = here.Location.AngleTo(there.Location);
            var y = Math.Sin(angle / 180d * Math.PI) * here.Item.Radius;
            var x = Math.Cos(angle / 180d * Math.PI) * here.Item.Radius;
            var multiplier = here.Item.Radius / Math.Max(Math.Abs(x), Math.Abs(y));
            x *= multiplier;
            y *= multiplier;
            return here.Item.GetSector((int)Math.Round(x), (int)Math.Round(y));
        }

        #endregion Public Methods
    }
}
