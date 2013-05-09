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
		/// Setup parameters for the game.
		/// </summary>
		public GameSetup GameSetup { get; set; }

		public Galaxy Instantiate()
		{
			var gal = new Galaxy();
			var bounds = new Rectangle(-GameSetup.GalaxySize.Width / 2, -GameSetup.GalaxySize.Height / 2, GameSetup.GalaxySize.Width, GameSetup.GalaxySize.Height);

			// create star systems
			for (int i = 0; i < GameSetup.StarSystemCount; i++)
			{
				var p = StarSystemPlacementStrategy.PlaceStarSystem(gal, MinimumStarSystemDistance, bounds, GameSetup.StarSystemCount - i);
				if (p == null)
					break; // no more locations available

				gal.StarSystemLocations.Add(new ObjectLocation<StarSystem> { Location = p.Value, Item = StarSystemTemplateChances.PickWeighted().Instantiate()});
			}

			// TODO - create warp points

			return gal;
		}
	}
}
