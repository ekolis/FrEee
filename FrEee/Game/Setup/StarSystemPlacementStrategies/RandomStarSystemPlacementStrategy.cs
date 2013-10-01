using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using FrEee.Game.Objects.Space;
using FrEee.Utility; using FrEee.Utility.Serialization;
using FrEee.Utility.Extensions;
using FrEee.Modding.Interfaces;
using FrEee.Game.Interfaces;

namespace FrEee.Game.Setup.StarSystemPlacementStrategies
{
	/// <summary>
	/// Places stars randomly.
	/// </summary>
	 [Serializable] public class RandomStarSystemPlacementStrategy : IStarSystemPlacementStrategy
	{
		public Point? PlaceStarSystem(Galaxy galaxy, int buffer, Rectangle bounds, int starsLeft)
		{
			var openPositions = bounds.GetAllPoints();
			foreach (var sspos in galaxy.StarSystemLocations)
				openPositions = openPositions.BlockOut(sspos.Location, buffer);
			if (!openPositions.Any())
				return null;
			return openPositions.PickRandom();
		}
	}
}
