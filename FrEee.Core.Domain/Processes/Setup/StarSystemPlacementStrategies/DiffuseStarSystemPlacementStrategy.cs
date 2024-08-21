using FrEee.Utility;
using FrEee.Extensions;
using System;
using System.Drawing;
using System.Linq;
using FrEee.Objects.GameState;

namespace FrEee.Processes.Setup.StarSystemPlacementStrategies;

/// <summary>
/// Places stars spaced roughly evenly.
/// </summary>
[Serializable]
public class DiffuseStarSystemPlacementStrategy : IStarSystemPlacementStrategy
{
    public Point? PlaceStarSystem(Game galaxy, int buffer, Rectangle bounds, int starsLeft, PRNG dice)
    {
        var openPositions = bounds.GetAllPoints();
        foreach (var sspos in galaxy.StarSystemLocations.Select(sspos => sspos.Location))
            openPositions = openPositions.BlockOut(sspos, buffer);
        if (!openPositions.Any())
            return null;

        // sort positions by distance to nearest star
        var ordered = openPositions.OrderBy(p => galaxy.StarSystemLocations.Select(sspos => sspos.Location).MinOrDefault(p2 => p2.ManhattanDistance(p)));

        // place a star off in the middle of nowhere
        return ordered.Last();
    }
}