using FrEee.Utility;
using FrEee.Extensions;
using System;
using System.Drawing;
using System.Linq;
using FrEee.Objects.GameState;
using FrEee.Objects.Space;

namespace FrEee.Processes.Setup.StarSystemPlacementStrategies;

/// <summary>
/// Places stars randomly.
/// </summary>
[Serializable]
public class RandomStarSystemPlacementStrategy : IStarSystemPlacementStrategy
{
    public Point? PlaceStarSystem(Galaxy galaxy, int buffer, Rectangle bounds, int starsLeft, PRNG dice)
    {
        var openPositions = bounds.GetAllPoints();
        foreach (var sspos in galaxy.StarSystemLocations)
            openPositions = openPositions.BlockOut(sspos.Location, buffer);
        if (!openPositions.Any())
            return null;
        return openPositions.PickRandom(dice);
    }
}