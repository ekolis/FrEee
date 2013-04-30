using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game
{
	/// <summary>
	/// A galaxy in which the game is played.
	/// </summary>
	public class Galaxy
	{
		public Galaxy()
		{
			StarSystemLocations = new Dictionary<Point, StarSystem>();
		}

		/// <summary>
		/// The locations of the star systems in the galaxy.
		/// </summary>
		public IDictionary<Point, StarSystem> StarSystemLocations { get; private set; }

		public int MinX { get { return StarSystemLocations.MinOrDefault(kvp => kvp.Key.X); } }

		public int MinY { get { return StarSystemLocations.MinOrDefault(kvp => kvp.Key.Y); } }

		public int MaxX { get { return StarSystemLocations.MaxOrDefault(kvp => kvp.Key.X); } }

		public int MaxY { get { return StarSystemLocations.MaxOrDefault(kvp => kvp.Key.Y); } }

		public int Width { get { return MaxX - MinX + 1; } }

		public int Height { get { return MaxY - MinY + 1; } }
	}
}
