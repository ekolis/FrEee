using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;
using System;
using System.Drawing;
using System.Linq;

namespace FrEee.Game.Setup.StarSystemPlacementStrategies
{
    /// <summary>
    /// Places stars randomly.
    /// </summary>
    [Serializable]
    public class RandomStarSystemPlacementStrategy : IStarSystemPlacementStrategy
    {
        #region Public Methods

        public Point? PlaceStarSystem(Galaxy galaxy, int buffer, Rectangle bounds, int starsLeft)
        {
            var openPositions = bounds.GetAllPoints();
            foreach (var sspos in galaxy.StarSystemLocations)
                openPositions = openPositions.BlockOut(sspos.Location, buffer);
            if (!openPositions.Any())
                return null;
            return openPositions.PickRandom();
        }

        #endregion Public Methods
    }
}
