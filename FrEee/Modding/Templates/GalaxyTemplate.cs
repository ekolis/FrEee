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
using FrEee.Game.Enumerations;

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
			return Instantiate(null, 1.0);
		}

		/// <param name="status">A status object to report status back to the GUI.</param>
		/// <param name="desiredProgress">How much progress should we report back to the GUI when we're done initializing the galaxy? 1.0 means all done with everything that needs to be done.</param>
		public Galaxy Instantiate(Status status, double desiredProgress)
		{
			var gal = new Galaxy(Mod.Current);
			var bounds = new Rectangle(-GameSetup.GalaxySize.Width / 2, -GameSetup.GalaxySize.Height / 2, GameSetup.GalaxySize.Width, GameSetup.GalaxySize.Height);

			var unusedNames = new List<string>(Mod.Current.StarSystemNames);

			// create star systems
			if (status != null)
			status.Message = "Creating star systems";
			var progressPerStarSystem = (desiredProgress - (status == null ? 0 : status.Progress)) / GameSetup.StarSystemCount;
			for (int i = 0; i < GameSetup.StarSystemCount; i++)
			{
				if (status != null)
					status.Message = "Creating star system " + (i + 1) + " of " + GameSetup.StarSystemCount;
				var p = StarSystemPlacementStrategy.PlaceStarSystem(gal, MinimumStarSystemDistance, bounds, GameSetup.StarSystemCount - i);
				if (p == null)
					break; // no more locations available

				var sys = StarSystemTemplateChances.PickWeighted().Instantiate();
				sys.Name = unusedNames.PickRandom();
				unusedNames.Remove(sys.Name);
				NameStellarObjects(sys);
				gal.StarSystemLocations.Add(new ObjectLocation<StarSystem> { Location = p.Value, Item = sys });
				if (status != null)
					status.Progress += progressPerStarSystem;
			}

			// create warp points
			var graph = new ConnectivityGraph<ObjectLocation<StarSystem>>();
			foreach (var ssl in gal.StarSystemLocations)
				graph.Add(ssl);
			bool triedEverything = false;
			while (!graph.IsConnected)
			{
				// pick 2 systems
				ObjectLocation<StarSystem> startLocation, endLocation = null;
				var ssls = gal.StarSystemLocations;
				var fewest = ssls.Min(ssl => GetWarpPointCount(ssl.Item));
				if (fewest < GameSetup.GalaxyTemplate.MaxWarpPointsPerSystem && !triedEverything)
				{
					// place warp points where there aren't many to begin with
					var candidates = ssls.Where(ssl => GetWarpPointCount(ssl.Item) == fewest);
					startLocation = candidates.PickRandom();

					// pick a nearby star system to create a warp point to
					for (int dist = 1; dist < gal.Width + gal.Height; dist++)
					{
						var nearby = gal.StarSystemLocations.Where(ssl => ssl.Location.ManhattanDistance(startLocation.Location) == dist);
						nearby = nearby.Where(ssl => GetWarpPointCount(ssl.Item) < GameSetup.GalaxyTemplate.MaxWarpPointsPerSystem);
						nearby = nearby.Where(ssl => AreWarpPointAnglesOk(startLocation, ssl, gal, GameSetup.GalaxyTemplate.MinWarpPointAngle));
						if (nearby.Any())
						{
							endLocation = nearby.PickRandom();
							break;
						}
					}

					// time to give up and place warp points willy nilly?
					if (endLocation == null)
					{
						triedEverything = true;
						continue;
					}
				}
				else
				{
					// systems are full of warp points - need to connect systems that are not very connected yet
					var subgraphs = graph.Subdivide();
					var smallest = subgraphs.Min(sg => sg.Count);
					var candidates = subgraphs.Where(sg => sg.Count == smallest);
					var subgraph1 = candidates.PickRandom();
					var subgraph2 = subgraphs.Where(sg => sg != subgraph1).PickRandom();
					
					// try to pick systems that are nearby
					var crosstable = subgraph1.Join(subgraph2, ssl => 0, ssl => 0, (ssl1, ssl2) => Tuple.Create(ssl1, ssl2));
					var mindist = crosstable.Min(tuple => tuple.Item1.Location.ManhattanDistance(tuple.Item2.Location));
					var pair = crosstable.Where(tuple => tuple.Item1.Location.ManhattanDistance(tuple.Item2.Location) == mindist).PickRandom();
					startLocation = pair.Item1;
					endLocation = pair.Item2;
				}

				// create the warp points
				// TODO - use mod files data for warp points
				var angleOut = startLocation.Location.AngleTo(endLocation.Location);
				var angleBack = angleOut + 180d;
				var sector1 = GetWarpPointSector(startLocation.Item, angleOut);
				var sector2 = GetWarpPointSector(endLocation.Item, angleBack);
				var wp1 = new WarpPoint
				{
					Description = "A warp point connecting two star systems.",
					IsOneWay = false,
					Name = "Warp Point to " + endLocation.Item.Name,
					StellarSize = StellarSize.Medium,
					Target = sector2,
				};
				sector1.SpaceObjects.Add(wp1);
				var wp2 = new WarpPoint
				{
					Description = "A warp point connecting two star systems.",
					IsOneWay = false,
					Name = "Warp Point to " + startLocation.Item.Name,
					StellarSize = StellarSize.Medium,
					Target = sector1,
				};
				sector2.SpaceObjects.Add(wp2);
				foreach (var abil in startLocation.Item.WarpPointAbilities.Concat(endLocation.Item.WarpPointAbilities))
				{
					wp1.IntrinsicAbilities.Add(abil);
					wp2.IntrinsicAbilities.Add(abil);
				}

				// mark systems connected
				graph.Connect(startLocation, endLocation, true);
			}

			return gal;
		}

#region Helper methods for greating warp points

		private int GetWarpPointCount(StarSystem sys)
		{
			return sys.FindSpaceObjects<WarpPoint>().Flatten().Count();
		}

		private IEnumerable<double> GetWarpPointAngles(ObjectLocation<StarSystem> ssl, Galaxy gal)
		{
			foreach (var wp in ssl.Item.FindSpaceObjects<WarpPoint>().Flatten())
			{
				var target = wp.TargetStarSystemLocation;
				var offset = target.Location.AngleTo(ssl.Location);
				yield return offset;
			}
		}

		private bool AreWarpPointAnglesOk(ObjectLocation<StarSystem> start, ObjectLocation<StarSystem> end, Galaxy gal, int minAngle)
		{
			var angleOut = NormalizeAngle(start.Location.AngleTo(end.Location));
			var angleBack = NormalizeAngle(angleOut + 180d);

			// test warp points going out
			foreach (var angle in GetWarpPointAngles(start, gal))
			{
				if (IsInRangeExclusive(angleOut, angle, minAngle))
					return false;
			}

			// test warp points coming back
			foreach (var angle in GetWarpPointAngles(end, gal))
			{
				if (IsInRangeExclusive(angleBack, angle, minAngle))
					return false;
			}

			return true;
		}

		private double NormalizeAngle(double angle)
		{
			angle %= 360d;
			if (angle < 0)
				angle += 360d;
			return angle;
		}

		private bool IsInRangeExclusive(double d, double middle, double range)
		{
			return d > middle - range && d < middle + range;
		}

		private Sector GetWarpPointSector(StarSystem sys, double angle)
		{
			var x = Math.Sin(angle / 180d * Math.PI) * sys.Radius;
			var y = Math.Cos(angle / 180d * Math.PI) * sys.Radius;
			var multiplier = sys.Radius / Math.Max(Math.Abs(x), Math.Abs(y));
			x *= multiplier;
			y *= multiplier;
			return sys.GetSector((int)Math.Round(x), (int)Math.Round(y));
		}

#endregion

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
