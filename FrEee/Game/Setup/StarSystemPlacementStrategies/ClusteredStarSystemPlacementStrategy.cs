using FrEee.Game;
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
	/// Places stars grouped together in tight clusters separated by long distances.
	/// </summary>
	 [Serializable] public class ClusteredStarSystemPlacementStrategy : IStarSystemPlacementStrategy
	{
		public Point? PlaceStarSystem(Galaxy galaxy, int buffer, Rectangle bounds, int starsLeft)
		{
			var openPositions = bounds.GetAllPoints();
			foreach (var sspos in galaxy.StarSystemLocations.Select(sspos => sspos.Location))
				openPositions = openPositions.BlockOut(sspos, buffer);
			if (!openPositions.Any())
				return null;

			// sort positions by distance to nearest star
			var ordered = openPositions.Select(p => new {
				Position = p, 
				Distances = galaxy.StarSystemLocations.Select(sspos => sspos.Location.ManhattanDistance(p)).OrderBy(dist => dist)
			}).OrderBy(p => p.Distances.MinOrDefault());
			var minDist = ordered.SelectMany(p => p.Distances).MinOrDefault();

			if (RandomHelper.Next(2) == 0)
			{
				// place a star near other stars, but not near TOO many other stars
				var ok = ordered.Where(item => item.Distances.FirstOrDefault() == minDist);
				Dictionary<Point, double> dict;
				if (ok.Any())
				{
					dict = new Dictionary<Point, double>();
					foreach (var p in ok)
						dict.Add(p.Position, 1d / p.Distances.Sum(d => Math.Pow(d, 3)));
				}
				else
				{
					// place a star off in the middle of nowhere
					dict = new Dictionary<Point, double>();
					foreach (var p in ordered)
						dict.Add(p.Position, p.Distances.Sum(d => Math.Pow(d, 3)));
				}
				return dict.PickWeighted();
			}
			else
			{
				// place a star off in the middle of nowhere
				var dict = new Dictionary<Point, double>();
				foreach (var p in ordered)
					dict.Add(p.Position, p.Distances.Sum(d => Math.Pow(d, 3)));
				return dict.PickWeighted();
			}
		}
	}
}
