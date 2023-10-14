using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FrEee.Interfaces;
using FrEee.Objects.Space;
using FrEee.Setup;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;

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
			return Instantiate(null, 1.0, new PRNG(RandomSeed));
		}

		public int RandomSeed { get; set; } = DateTime.Now.Millisecond + 1000 * DateTime.Now.Second + 60000 * DateTime.Now.Minute;

		/// <param name="status">A status object to report status back to the GUI.</param>
		/// <param name="desiredProgress">How much progress should we report back to the GUI when we're done initializing the galaxy? 1.0 means all done with everything that needs to be done.</param>
		public Galaxy Instantiate(Status? status, double desiredProgress, PRNG dice)
		{
			var gal = new Galaxy();
			gal.Width = GameSetup.GalaxySize.Width;
			gal.Height = GameSetup.GalaxySize.Height;
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
				var p = StarSystemPlacementStrategy.PlaceStarSystem(gal, MinimumStarSystemDistance, bounds, GameSetup.StarSystemCount - i, dice);
				if (p == null)
					break; // no more locations available

				var sst = StarSystemTemplateChances.PickWeighted(dice);
				sst.Dice = dice;
				var sys = sst.Instantiate();
				sys.Name = unusedNames.PickRandom(dice);
				unusedNames.Remove(sys.Name);
				NameStellarObjects(sys);
				gal.StarSystemLocations.Add(new ObjectLocation<StarSystem> { Location = p.Value, Item = sys });
				if (status != null)
					status.Progress += progressPerStarSystem;
			}

			var progressPerWarp = (desiredProgress - (status == null ? 0 : status.Progress)) / (gal.StarSystemLocations.Count * 2);

			// create warp points
			if (status != null)
				status.Message = "Creating warp points";
			var graph = new ConnectivityGraph<ObjectLocation<StarSystem>>();
			foreach (var ssl in gal.StarSystemLocations)
				graph.Add(ssl);
			int wpsGenerated = 0;
			while (wpsGenerated < gal.StarSystemLocations.Count * 2)
			{
				// pick 2 systems
				ObjectLocation<StarSystem> startLocation = null, endLocation = null;
				(startLocation, endLocation) = MinDistanceDisconnectedSystemPair(graph);

				// create the warp points
				if (startLocation != null && endLocation != null)
				{
					GameSetup.WarpPointPlacementStrategy.PlaceWarpPoints(startLocation, endLocation);

					// mark systems connected
					graph.Connect(startLocation, endLocation, true);

					wpsGenerated++;
				}
				else
					break;
				if (status != null)
					status.Progress += progressPerWarp;
			}

			// TODO - delete excess warp points if system groups count is >1

			if (status != null)
				status.Progress = desiredProgress;

			return gal;
		}

		private (ObjectLocation<StarSystem>, ObjectLocation<StarSystem>) MinDistanceDisconnectedSystemPair(ConnectivityGraph<ObjectLocation<StarSystem>> graph)
		{
			if (graph.Subgraphs.Count() == 0)
				return (null, null);

			(ObjectLocation<StarSystem>, ObjectLocation<StarSystem>) best = (null, null);
			int bestDistance = int.MaxValue;

			if (graph.Subgraphs.Count() == 1)
			{
				foreach (var l1 in graph)
				{
					foreach (var l2 in graph)
					{
						if (graph.AreDirectlyConnected(l1, l2))
							continue;
						if (IntersectsExceptAtEnds(l1.Location, l2.Location, graph))
							continue;
						var dist = l1.Location.ManhattanDistance(l2.Location);
						if (dist < bestDistance && AreWarpPointAnglesOk(l1, l2, The.Galaxy, MinWarpPointAngle))
						{
							bestDistance = dist;
							best = (l1, l2);
						}
					}
				}
				return best;
			}

			foreach (var g1 in graph.Subgraphs)
			{
				foreach (var g2 in graph.Subgraphs.ExceptSingle(g1))
				{
					foreach (var l1 in g1)
					{
						foreach (var l2 in g2)
						{
							if (graph.AreDirectlyConnected(l1, l2))
								continue;
							var dist = l1.Location.ManhattanDistance(l2.Location);
							if (dist < bestDistance && AreWarpPointAnglesOk(l1, l2, The.Galaxy, MinWarpPointAngle))
							{
								bestDistance = dist;
								best = (l1, l2);
							}
						}
					}
				}
			}
			return best;
		}

		/// <summary>
		/// Determines if a number is between two bounds (inclusive).
		/// </summary>
		/// <param name="num"></param>
		/// <param name="bound1"></param>
		/// <param name="bound2"></param>
		/// <returns></returns>
		private static bool IsBetween(double num, double bound1, double bound2)
		{
			return num >= bound1 && num <= bound2 || num >= bound2 && num <= bound1;
		}

		private static bool IntersectsExceptAtEnds(Point p1, Point p2, ConnectivityGraph<ObjectLocation<StarSystem>> graph)
		{
			foreach (var kvp in graph.Connections)
			{
				var q1 = kvp.Key.Location;
				foreach (var q2 in kvp.Value.Select(l => l.Location))
				{
					if (IntersectsExceptAtEnds(p1, p2, q1, q2))
						return true;
					// else no intersection
				}
			}
			return false;
		}

		/// <summary>
		/// Public for unit tests.
		/// </summary>
		/// <param name="p1"></param>
		/// <param name="p2"></param>
		/// <param name="q1"></param>
		/// <param name="q2"></param>
		/// <returns></returns>
		public static bool IntersectsExceptAtEnds(Point p1, Point p2, Point q1, Point q2)
		{
			if (p1 == q1 && p2 == q2)
				return true;
			if (p1 == q2 && p2 == q1)
				return true;
			if (p1 == q1 || p1 == q2 || p2 == q1 || p2 == q2)
				return false; // intersects only on endpoints
			var slope1 = (double)(p2.Y - p1.Y) / (double)(p2.X - p1.X);
			var slope2 = (double)(q2.Y - q1.Y) / (double)(q2.X - q1.X);
			if (slope1 != slope2)
			{
				// intersect in infinite space! but do the line segments actually intersect?
				double intercept1, intercept2, intersectX, intersectY;
				if ((slope1 == double.PositiveInfinity || slope1 == double.NegativeInfinity) && (slope2 == double.PositiveInfinity || slope2 == double.NegativeInfinity))
				{
					if (IsBetween(q1.Y, p1.Y, p2.Y) || IsBetween(q2.Y, p1.Y, p2.Y))
						return true;
				}
				else if (slope1 == double.PositiveInfinity || slope1 == double.NegativeInfinity)
				{
					// x is a constant for a vertical line
					intersectX = p1.X;

					// y = m*x+b
					intercept2 = q1.Y - slope2 * q1.X;
					intersectY = slope2 * intersectX + intercept2;

					if (IsBetween(intersectY, p1.Y, p2.Y))
						return true;
				}
				else if (slope2 == double.PositiveInfinity || slope2 == double.NegativeInfinity)
				{
					// x is a constant for a vertical line
					intersectX = q1.X;

					// y = m*x+b
					intercept1 = p1.Y - slope1 * p1.X;
					intersectY = slope1 * intersectX + intercept1;

					if (IsBetween(intersectY, p1.Y, p2.Y))
						return true;
				}
				else
				{
					// y = m*x+b
					// b = y - m*x
					intercept1 = p1.Y - slope1 * p1.X;
					intercept2 = q1.Y - slope2 * q1.X;
					// m1*x+b1 = m2*x+b2
					// m1*x-m2*x = b2-b1
					// x*(m1-m2) = b2-b1
					// x = (b2-b1)/(m1-m2)
					intersectX = (intercept2 - intercept1) / (slope1 - slope2);
					intersectY = slope1 * intersectX + intercept1;

					if (IsBetween(intersectX, p1.X, p2.X) && IsBetween(intersectX, q1.X, q2.X))
						return true;
				}
			}
			else
			{
				if (IsBetween(p1.X, q1.X, q2.X) || IsBetween(p2.X, q1.X, q2.X)
					|| IsBetween(q1.X, p1.X, p2.X) || IsBetween(q2.X, p1.X, p2.X)
					|| IsBetween(p1.Y, q1.Y, q2.Y) || IsBetween(p2.Y, q1.Y, q2.Y)
					|| IsBetween(q1.Y, p1.Y, p2.Y) || IsBetween(q2.Y, p1.Y, p2.Y))
					return true;
			}
			return false;
		}

		private bool AreWarpPointAnglesOk(ObjectLocation<StarSystem> start, ObjectLocation<StarSystem> end, Galaxy gal, int minAngle)
		{
			var angleOut = NormalizeAngle(start.Location.AngleTo(end.Location));
			var angleBack = NormalizeAngle(angleOut + 180d);

			// test warp points going out
			foreach (var angle in GetWarpPointAngles(start, gal))
			{
				if (AngleIsInRangeExclusive(angleOut, angle, minAngle))
					return false;
			}

			// test warp points coming back
			foreach (var angle in GetWarpPointAngles(end, gal))
			{
				if (AngleIsInRangeExclusive(angleBack, angle, minAngle))
					return false;
			}

			return true;
		}

		private IEnumerable<double> GetWarpPointAngles(ObjectLocation<StarSystem> ssl, Galaxy gal)
		{
			foreach (var wp in ssl.Item.FindSpaceObjects<WarpPoint>())
			{
				var target = wp.TargetStarSystemLocation;
				var offset = ssl.Location.AngleTo(target.Location);
				yield return offset;
			}
		}

		private int GetWarpPointCount(StarSystem sys)
		{
			return sys.FindSpaceObjects<WarpPoint>().Count();
		}

		/// <summary>
		/// Public for unit tests.
		/// </summary>
		/// <param name="d"></param>
		/// <param name="middle"></param>
		/// <param name="range"></param>
		/// <returns></returns>
		public static bool AngleIsInRangeExclusive(double d, double middle, double range)
		{
			return Math.Abs(NormalizeAngle(d - middle)) < range;
		}

		internal static void NameStellarObjects(StarSystem sys)
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

		private static double NormalizeAngle(double angle)
		{
			angle %= 360d;
			if (angle < -180d)
				angle += 360d;
			else if (angle > 180d)
				angle -= 360d;
			return angle;
		}
	}
}
