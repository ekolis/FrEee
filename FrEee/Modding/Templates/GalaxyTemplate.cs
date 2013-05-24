using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;

namespace FrEee.Modding.Templates
{
	/// <summary>
	/// A template for creating galaxies
	/// Maps to a record in QuadrantTypes.txt.
	/// </summary>
	[Serializable]
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
			var gal = new Galaxy(Mod.Current);
			var bounds = new Rectangle(-GameSetup.GalaxySize.Width / 2, -GameSetup.GalaxySize.Height / 2, GameSetup.GalaxySize.Width, GameSetup.GalaxySize.Height);

			var unusedNames = new List<string>(Mod.Current.StarSystemNames);

			// create star systems
			for (int i = 0; i < GameSetup.StarSystemCount; i++)
			{
				var p = StarSystemPlacementStrategy.PlaceStarSystem(gal, MinimumStarSystemDistance, bounds, GameSetup.StarSystemCount - i);
				if (p == null)
					break; // no more locations available

				var sys = StarSystemTemplateChances.PickWeighted().Instantiate();
				sys.Name = unusedNames.PickRandom();
				unusedNames.Remove(sys.Name);
				NameStellarObjects(sys);
				gal.StarSystemLocations.Add(new ObjectLocation<StarSystem> { Location = p.Value, Item = sys });
			}

			// TODO - create warp points

			return gal;
		}

		private void NameStellarObjects(StarSystem sys)
		{
			int index;

			index = 1;
			var stars = sys.FindSpaceObjects<Star>().Flatten().ToArray();
			foreach (var star in stars)
			{
				if (stars.Count() == 1)
					star.Name = sys.Name; // just name star after system
				else
				{
					try
					{
						star.Name = sys.Name + " " + index.ToLetter();
					}
					catch (ArgumentException)
					{
						// seriously, 27 stars? fine, just name it after system again!
						star.Name = sys.Name;
					}
				}
				index++;
			}

			index = 1;
			var planets = sys.FindSpaceObjects<Planet>().Flatten().ToArray();
			foreach (var planet in planets.Where(p => p.MoonOf == null))
			{
				planet.Name = sys.Name + " " + index.ToRomanNumeral();
				index++;
			}
			var moonIndices = new Dictionary<Planet, int>();
			foreach (var moon in planets.Where(p => p.MoonOf != null))
			{
				if (moonIndices.ContainsKey(moon.MoonOf))
					moonIndices[moon.MoonOf]++;
				else
					moonIndices.Add(moon.MoonOf, 1);
				try
				{
					moon.Name = moon.MoonOf.Name + " " + moonIndices[moon.MoonOf].ToLetter();
				}
				catch (ArgumentException)
				{
					// seriously, 27 moons? just call it a moon
					moon.Name = moon.MoonOf.Name + " Moon";
				}
			}

			index = 1;
			var asteroids = sys.FindSpaceObjects<AsteroidField>().Flatten().ToArray();
			foreach (var asteroid in asteroids)
			{
				asteroid.Name = sys.Name + " Asteroid Field " + index.ToRomanNumeral();
				index++;
			}

			index = 1;
			var storms = sys.FindSpaceObjects<Storm>().Flatten().ToArray();
			foreach (var storm in storms)
			{
				if (storms.Count() == 1)
					storm.Name = sys.Name + " Storm";
				else
				{
					try
					{
						storm.Name = sys.Name + " Storm " + index.ToLetter();
					}
					catch (ArgumentException)
					{
						// seriously, 27 storms? fine, just call it a storm!
						storm.Name = sys.Name + " Storm";
					}
				}
				index++;
			}
		}
	}
}
