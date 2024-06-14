using FrEee.Objects.Space;

namespace FrEee.Processes.Setup.WarpPointPlacementStrategies;

/// <summary>
/// Places warp points at the center of the system. Exploration is easy, but so is setting up chokepoints.
/// </summary>
public class CenterWarpPointPlacementStrategy : WarpPointPlacementStrategy
{
    static CenterWarpPointPlacementStrategy()
    {
        Instance = new CenterWarpPointPlacementStrategy();
    }

    private CenterWarpPointPlacementStrategy()
        : base("Center", "Places warp points at the center of the system. Exploration is easy, but so is setting up chokepoints.")
    {
    }

    public static CenterWarpPointPlacementStrategy Instance { get; private set; }

    public override Sector GetWarpPointSector(ObjectLocation<StarSystem> here, ObjectLocation<StarSystem> there)
    {
        return here.Item.GetSector(0, 0);
    }
}