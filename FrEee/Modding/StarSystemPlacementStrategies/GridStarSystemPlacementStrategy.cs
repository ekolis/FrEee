using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.Modding.Interfaces;

namespace FrEee.Modding.StarSystemPlacementStrategies
{
	/// <summary>
	/// Places stars in a grid.
	/// </summary>
	 [Serializable] public class GridStarSystemPlacementStrategy : IStarSystemPlacementStrategy
	{
		public Point? PlaceStarSystem(Galaxy galaxy, int buffer, Rectangle bounds, int starsLeft)
		{
			var openPositions = bounds.GetAllPoints();
			foreach (var sspos in galaxy.StarSystemLocations.Select(sspos => sspos.Location))
				openPositions = openPositions.BlockOut(sspos, buffer);
			if (!openPositions.Any())
				return null;

			int totalStars = starsLeft + galaxy.StarSystemLocations.Count;
			double xfactor = Math.Sqrt(totalStars) * (double)bounds.Width / (double)bounds.Height;
			double yfactor = Math.Sqrt(totalStars) * (double)bounds.Height / (double)bounds.Width;
			int xstars = (int)(totalStars / xfactor);
			int ystars = (int)(totalStars / yfactor);

			if (xstars * ystars <= galaxy.StarSystemLocations.Count)
				return null;

			int row = galaxy.StarSystemLocations.Count % xstars;
			int col = galaxy.StarSystemLocations.Count / xstars;
			int rowsize = bounds.Width / (xstars - 1);
			int colsize = bounds.Height / (ystars - 1);

			var idealPos = new Point(row * rowsize + bounds.Left, col * colsize + bounds.Top);

			return openPositions.OrderBy(p => p.ManhattanDistance(idealPos)).First();
		}
	}
}
