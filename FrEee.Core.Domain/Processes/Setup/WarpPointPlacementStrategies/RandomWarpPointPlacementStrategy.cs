using FrEee.Objects.Space;
using FrEee.Utility;
using FrEee.Utility;
namespace FrEee.Processes.Setup.WarpPointPlacementStrategies;

/// <summary>
/// Places warp points randomly within a system.
/// </summary>
public class RandomWarpPointPlacementStrategy : WarpPointPlacementStrategy
{
    static RandomWarpPointPlacementStrategy()
    {
        Instance = new RandomWarpPointPlacementStrategy();
    }

    private RandomWarpPointPlacementStrategy()
        : base("Random", "Places warp points randomly within a system.")
    {
    }

    public static RandomWarpPointPlacementStrategy Instance { get; private set; }

    public override Sector GetWarpPointSector(ObjectLocation<StarSystem> here, ObjectLocation<StarSystem> there)
    {
        var x = RandomHelper.Range(-here.Item.Radius, here.Item.Radius);
        var y = RandomHelper.Range(-here.Item.Radius, here.Item.Radius);
        return here.Item.GetSector(x, y);
    }
}