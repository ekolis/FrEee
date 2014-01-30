﻿using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using FrEee.Game.Objects.Space;
using FrEee.Utility; using Newtonsoft.Json;
using FrEee.Utility.Extensions;
using FrEee.Modding.Interfaces;
using FrEee.Game.Interfaces;

namespace FrEee.Game.Setup.StarSystemPlacementStrategies
{
	/// <summary>
	/// Places stars clustered around the center of the galaxy.
	/// </summary>
	 [Serializable] public class SpiralStarSystemPlacementStrategy : IStarSystemPlacementStrategy
	{
		public Point? PlaceStarSystem(Galaxy galaxy, int buffer, Rectangle bounds, int starsLeft)
		{
			var openPositions = bounds.GetAllPoints();
			foreach (var sspos in galaxy.StarSystemLocations.Select(sspos => sspos.Location))
				openPositions = openPositions.BlockOut(sspos, buffer);
			if (!openPositions.Any())
				return null;

			// weight locations based on gradient from distance to center
			var center = new Point(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2);
			var ordered = openPositions.Select(p => new KeyValuePair<Point, double>(p, Math.Pow(p.ManhattanDistance(center), 2))).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
			var max = ordered.Max(kvp => kvp.Value);
			foreach (var p in ordered.Keys.ToArray())
				ordered[p] = max / ordered[p];
			return ordered.PickWeighted();

		}
	}
}
