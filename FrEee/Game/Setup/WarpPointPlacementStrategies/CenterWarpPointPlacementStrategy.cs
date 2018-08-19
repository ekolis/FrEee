using FrEee.Game.Objects.Space;

namespace FrEee.Game.Setup.WarpPointPlacementStrategies
{
    /// <summary>
    /// Places warp points at the center of the system. Exploration is easy, but so is setting up chokepoints.
    /// </summary>
    public class CenterWarpPointPlacementStrategy : WarpPointPlacementStrategy
    {
        #region Public Constructors

        static CenterWarpPointPlacementStrategy()
        {
            Instance = new CenterWarpPointPlacementStrategy();
        }

        #endregion Public Constructors

        #region Private Constructors

        private CenterWarpPointPlacementStrategy()
            : base("Center", "Places warp points at the center of the system. Exploration is easy, but so is setting up chokepoints.")
        {
        }

        #endregion Private Constructors

        #region Public Properties

        public static CenterWarpPointPlacementStrategy Instance { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public override Sector GetWarpPointSector(ObjectLocation<StarSystem> here, ObjectLocation<StarSystem> there)
        {
            return here.Item.GetSector(0, 0);
        }

        #endregion Public Methods
    }
}
