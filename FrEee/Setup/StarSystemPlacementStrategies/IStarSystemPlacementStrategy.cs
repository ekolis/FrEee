using FrEee.Objects.GameState;
using FrEee.Utility;
using System.Drawing;

namespace FrEee.Setup.StarSystemPlacementStrategies;

/// <summary>
/// Algorithm for placing star systems on the galaxy map.
/// </summary>
public interface IStarSystemPlacementStrategy
{
    /// <summary>
    /// Places a star system.
    /// </summary>
    /// <param name="galaxy">The galaxy.</param>
    /// <param name="buffer">The minimum number of buffer squares between any two star systems.</param>
    /// <param name="bounds">Where are we allowed to place star systems?</param>
    /// <param name="starsLeft">How many more stars to place?</param>
    /// <returns>The location of the star system, or null if a location could not be found.</returns>
    Point? PlaceStarSystem(Galaxy galaxy, int buffer, Rectangle bounds, int starsLeft, PRNG dice);
}