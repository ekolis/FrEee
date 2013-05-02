using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Modding.StarSystemPlacementStrategies
{
	/// <summary>
	/// Places stars randomly.
	/// </summary>
	public class RandomStarSystemPlacementStrategy : IStarSystemPlacementStrategy
	{
		public Point? PlaceStarSystem(Galaxy galaxy, int buffer, Rectangle bounds, int starsLeft)
		{
			var openPositions = bounds.GetAllPoints();
			foreach (var sspos in galaxy.StarSystemLocations.Keys)
				openPositions = openPositions.BlockOut(sspos, buffer);
			if (!openPositions.Any())
				return null;
			return openPositions.PickRandom();
		}
	}
}
