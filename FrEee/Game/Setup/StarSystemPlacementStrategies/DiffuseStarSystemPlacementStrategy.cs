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
	/// Places stars spaced roughly evenly.
	/// </summary>
	 [Serializable] public class DiffuseStarSystemPlacementStrategy : IStarSystemPlacementStrategy
	{
		public Point? PlaceStarSystem(Galaxy galaxy, int buffer, Rectangle bounds, int starsLeft)
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
}
