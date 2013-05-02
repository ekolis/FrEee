using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Modding.Templates
{
	/// <summary>
	/// A template for creating galaxies
	/// Maps to a record in QuadrantTypes.txt.
	/// </summary>
	public class GalaxyTemplate : ITemplate<Galaxy>, INamed
	{
		public GalaxyTemplate()
		{
			StarSystemTemplateChances = new Dictionary<StarSystemTemplate, int>();

			// TODO - let mods specify galaxy size?
			Bounds = new Rectangle(-30, -20, 60, 40);

			// TODO - let game setup specify star system count
			StarSystemCount = 50;
		}

		/// <summary>
		/// The name of this galaxy template.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// A description of this galaxy template.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Minimum number of empty squares between star systems.
		/// (Zero, not one, means systems can be adjacent.)
		/// </summary>
		public int MinimumStarSystemDistance { get; set; }

		/// <summary>
		/// Algorithm for placing stars on the galaxy map.
		/// </summary>
		public IStarSystemPlacementStrategy StarSystemPlacementStrategy { get; set; }

		/// <summary>
		/// Preferred maximum warp points per system. May be overridden for connectivity purposes.
		/// </summary>
		public int MaxWarpPointsPerSystem { get; set; }

		/// <summary>
		/// Preferred minimum angle between warp points in a system. May be overridden for connectivity purposes.
		/// </summary>
		public int MinWarpPointAngle { get; set; }

		/// <summary>
		/// Chances for each type of star system to appear.
		/// </summary>
		public IDictionary<StarSystemTemplate, int> StarSystemTemplateChances { get; set; }

		/// <summary>
		/// Where are we allowed to place star systems?
		/// </summary>
		public Rectangle Bounds { get; set; }

		/// <summary>
		/// How many star systems will we place?
		/// </summary>
		public int StarSystemCount { get; set; }

		public Galaxy Instantiate()
		{
			var gal = new Galaxy();

			// create star systems
			for (int i = 0; i < StarSystemCount; i++)
			{
				var p = StarSystemPlacementStrategy.PlaceStarSystem(gal, MinimumStarSystemDistance, Bounds, StarSystemCount - i);
				if (p == null)
					break; // no more locations available

				gal.StarSystemLocations[p.Value] = StarSystemTemplateChances.PickWeighted().Instantiate();
			}

			// TODO - create warp points

			return gal;
		}
	}
}
