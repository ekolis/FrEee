using FrEee.Game.Objects.Space;
using FrEee.Utility;

namespace FrEee.Game.Setup.WarpPointPlacementStrategies
{
    /// <summary>
    /// Places warp points randomly within a system.
    /// </summary>
    public class RandomWarpPointPlacementStrategy : WarpPointPlacementStrategy
    {
        #region Public Constructors

        static RandomWarpPointPlacementStrategy()
        {
            Instance = new RandomWarpPointPlacementStrategy();
        }

        #endregion Public Constructors

        #region Private Constructors

        private RandomWarpPointPlacementStrategy()
            : base("Random", "Places warp points randomly within a system.")
        {
        }

        #endregion Private Constructors

        #region Public Properties

        public static RandomWarpPointPlacementStrategy Instance { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public override Sector GetWarpPointSector(ObjectLocation<StarSystem> here, ObjectLocation<StarSystem> there)
        {
            var x = RandomHelper.Range(-here.Item.Radius, here.Item.Radius);
            var y = RandomHelper.Range(-here.Item.Radius, here.Item.Radius);
            return here.Item.GetSector(x, y);
        }

        #endregion Public Methods
    }
}
