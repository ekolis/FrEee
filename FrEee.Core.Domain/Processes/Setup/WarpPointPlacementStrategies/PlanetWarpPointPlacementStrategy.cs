using FrEee.Objects.Space;
using FrEee.Extensions;
using System.Linq;
using FrEee.Extensions;

namespace FrEee.Processes.Setup.WarpPointPlacementStrategies;

/// <summary>
/// Places warp points at the location of planets. If there are no planets, warp points will be placed randomly. Planets on warp points will be particularly vulnerable to attack.
/// </summary>
public class PlanetWarpPointPlacementStrategy : WarpPointPlacementStrategy
{
    static PlanetWarpPointPlacementStrategy()
    {
        Instance = new PlanetWarpPointPlacementStrategy();
    }

    private PlanetWarpPointPlacementStrategy()
        : base("Planet", "Places warp points at the location of planets. If there are no planets, warp points will be placed randomly. Planets on warp points will be particularly vulnerable to attack.")
    {
    }

    public static PlanetWarpPointPlacementStrategy Instance { get; private set; }

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
}