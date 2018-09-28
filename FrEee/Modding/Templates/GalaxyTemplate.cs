using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Game.Setup;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FrEee.Modding.Templates
{
	/// <summary>
	/// A template for creating galaxies
	/// Maps to a record in QuadrantTypes.txt.
	/// </summary>
	[Serializable]
	public class GalaxyTemplate : ITemplate<Galaxy>, IModObject
	{
		public GalaxyTemplate()
		{
			StarSystemTemplateChances = new Dictionary<StarSystemTemplate, int>();
		}

		/// <summary>
		/// A description of this galaxy template.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Setup parameters for the game.
		/// </summary>
		public GameSetup GameSetup { get; set; }

		public bool IsDisposed
		{
			get; private set;
		}

		/// <summary>
		/// Preferred maximum warp points per system. May be overridden for connectivity purposes.
		/// </summary>
		public int MaxWarpPointsPerSystem { get; set; }

		/// <summary>
		/// Minimum number of empty squares between star systems.
		/// (Zero, not one, means systems can be adjacent.)
		/// </summary>
		public int MinimumStarSystemDistance { get; set; }

		/// <summary>
		/// Preferred minimum angle between warp points in a system. May be overridden for connectivity purposes.
		/// </summary>
		public int MinWarpPointAngle { get; set; }

		public string ModID
		{
			get;
			set;
		}

		/// <summary>
		/// The name of this galaxy template.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Algorithm for placing stars on the galaxy map.
		/// </summary>
		public IStarSystemPlacementStrategy StarSystemPlacementStrategy { get; set; }

		/// <summary>
		/// Chances for each type of star system to appear.
		/// </summary>
		public IDictionary<StarSystemTemplate, int> StarSystemTemplateChances { get; set; }

		/// <summary>
		/// Parameters from the mod meta templates.
		/// </summary>
		public IDictionary<string, object> TemplateParameters { get; set; }

		public void Dispose()
		{
			// TODO - remove it from somewhere?
			IsDisposed = true;
		}

		public Galaxy Instantiate()
		{
			return Instantiate(null, 1.0);
		}

		/// <param name="status">A status object to report status back to the GUI.</param>
		/// <param name="desiredProgress">How much progress should we report back to the GUI when we're done initializing the galaxy? 1.0 means all done with everything that needs to be done.</param>
		public Galaxy Instantiate(Status status, double desiredProgress)
		{
			var gal = new Galaxy(Mod.Current);
			gal.Width = GameSetup.GalaxySize.Width;
			gal.Height = GameSetup.GalaxySize.Height;
			gal.MinPlanetValue = GameSetup.MinPlanetValue;
			gal.MinSpawnedPlanetValue = GameSetup.MinSpawnedPlanetValue;
			gal.MaxSpawnedPlanetValue = GameSetup.MaxSpawnedPlanetValue;
			gal.MaxPlanetValue = GameSetup.MaxPlanetValue;
			gal.MinAsteroidValue = GameSetup.MinAsteroidValue;
			gal.MinSpawnedAsteroidValue = GameSetup.MinSpawnedAsteroidValue;
			gal.MaxSpawnedAsteroidValue = GameSetup.MaxSpawnedAsteroidValue;
			gal.EventFrequency = GameSetup.EventFrequency;
			gal.MaximumEventSeverity = GameSetup.MaximumEventSeverity;
			var bounds = new Rectangle(-GameSetup.GalaxySize.Width / 2, -GameSetup.GalaxySize.Height / 2, GameSetup.GalaxySize.Width, GameSetup.GalaxySize.Height);

			var unusedNames = new List<string>(Mod.Current.StarSystemNames);

			// create star systems
			if (status != null)
				status.Message = "Creating star systems";
			var progressPerStarSystem = (desiredProgress - (status == null ? 0 : status.Progress)) / GameSetup.StarSystemCount / 2d;
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

			var doneCreatingStarSystemsProgress = status == null ? 0.5 : status.Progress;

			// create warp points
			if (status != null)
				status.Message = "Creating warp points";
			var graph = new ConnectivityGraph<ObjectLocation<StarSystem>>();
			foreach (var ssl in gal.StarSystemLocations)
				graph.Add(ssl);
			bool triedEverything = false;
			while (graph.Subgraphs.Count() > GameSetup.StarSystemGroups)
			{
				// pick 2 systems
				ObjectLocation<StarSystem> startLocation = null, endLocation = null;
				var ssls = gal.StarSystemLocations;
				var fewest = ssls.Min(ssl => GetWarpPointCount(ssl.Item));
				if (fewest < GameSetup.GalaxyTemplate.MaxWarpPointsPerSystem && !triedEverything)
				{
					// place warp points where there aren't many to begin with
					var candidates = ssls.Where(ssl => GetWarpPointCount(ssl.Item) == fewest);
					foreach (var candidate in candidates.Shuffle())
					{
						// pick a nearby star system to create a warp point to
						for (int dist = 1; dist < gal.Width + gal.Height; dist++)
						{
							var nearby = gal.StarSystemLocations.Where(ssl => ssl.Location.ManhattanDistance(candidate.Location) == dist);
							nearby = nearby.Where(ssl => GetWarpPointCount(ssl.Item) < GameSetup.GalaxyTemplate.MaxWarpPointsPerSystem);
							nearby = nearby.Where(ssl => AreWarpPointAnglesOk(candidate, ssl, gal, GameSetup.GalaxyTemplate.MinWarpPointAngle));
							nearby = nearby.Where(ssl => !graph.GetExits(candidate).Contains(ssl));
							if (nearby.Any())
							{
								startLocation = candidate;
								endLocation = nearby.PickRandom();
								break;
							}
						}
					}

					// time to give up and place warp points willy nilly (or give up for disconnected maps)?
					if (startLocation == null || endLocation == null)
					{
						triedEverything = true;
						continue;
					}
				}
				else
				{
					// systems are full of warp points - need to connect systems that are not very connected yet
					var subgraphs = graph.Subgraphs;
					var smallest = subgraphs.Min(sg => sg.Count);
					if (status != null)
					{
						var largest = subgraphs.Max(sg => sg.Count);
						status.Message = "Creating warp points (" + largest + " systems connected)";
						status.Progress = doneCreatingStarSystemsProgress + largest * progressPerStarSystem;
					}
					var candidates = subgraphs.Where(sg => sg.Count == smallest);
					var subgraph1 = candidates.PickRandom();
					var subgraph2 = subgraphs.Where(sg => sg != subgraph1).PickRandom();
					if (subgraph2 == null)
						break; // no more subgraphs to merge!

					// try to pick systems that are nearby but not already connected
					var crosstable = subgraph1.Join(subgraph2, ssl => 0, ssl => 0, (ssl1, ssl2) => Tuple.Create(ssl1, ssl2));
					crosstable = crosstable.Where(t => !graph.GetExits(t.Item1).Contains(t.Item2));
					var mindist = crosstable.Min(tuple => tuple.Item1.Location.ManhattanDistance(tuple.Item2.Location));
					var pair = crosstable.Where(tuple => tuple.Item1.Location.ManhattanDistance(tuple.Item2.Location) == mindist).PickRandom();
					startLocation = pair.Item1;
					endLocation = pair.Item2;
				}

				// create the warp points
				if (startLocation != null && endLocation != null)
				{
					GameSetup.WarpPointPlacementStrategy.PlaceWarpPoints(startLocation, endLocation);

					// mark systems connected
					graph.Connect(startLocation, endLocation, true);
				}
				else
					break;
			}

			return gal;
		}

		private bool AreWarpPointAnglesOk(ObjectLocation<StarSystem> start, ObjectLocation<StarSystem> end, Galaxy gal, int minAngle)
		{
			var angleOut = NormalizeAngle(start.Location.AngleTo(end.Location));
			var angleBack = NormalizeAngle(angleOut + 180d);

			// test warp points going out
			foreach (var angle in GetWarpPointAngles(start, gal))
			{
				if (IsInRangeExclusive(angleOut, angle, minAngle, 360))
					return false;
			}

			// test warp points coming back
			foreach (var angle in GetWarpPointAngles(end, gal))
			{
				if (IsInRangeExclusive(angleBack, angle, minAngle, 360))
					return false;
			}

			return true;
		}

		private IEnumerable<double> GetWarpPointAngles(ObjectLocation<StarSystem> ssl, Galaxy gal)
		{
			foreach (var wp in ssl.Item.FindSpaceObjects<WarpPoint>())
			{
				var target = wp.TargetStarSystemLocation;
				var offset = target.Location.AngleTo(ssl.Location);
				yield return offset;
			}
		}

		private int GetWarpPointCount(StarSystem sys)
		{
			return sys.FindSpaceObjects<WarpPoint>().Count();
		}

		private bool IsInRangeExclusive(double d, double middle, double range, double modulo = 0)
		{
			if (modulo == 0)
				return d > (middle - range) && d < (middle + range);
			else
				return d % modulo > (middle - range) % modulo && d % modulo < (middle + range) % modulo;
		}

		private void NameStellarObjects(StarSystem sys)
		{
			int index;

			index = 1;
			var stars = sys.FindSpaceObjects<Star>().ToArray();
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
			var planets = sys.FindSpaceObjects<Planet>().ToArray();
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
			var asteroids = sys.FindSpaceObjects<AsteroidField>().ToArray();
			foreach (var asteroid in asteroids)
			{
				asteroid.Name = sys.Name + " Asteroid Field " + index.ToRomanNumeral();
				index++;
			}

			index = 1;
			var storms = sys.FindSpaceObjects<Storm>().ToArray();
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

		private double NormalizeAngle(double angle)
		{
			angle %= 360d;
			if (angle < 0)
				angle += 360d;
			return angle;
		}
	}
}