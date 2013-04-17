using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game
{
	/// <summary>
	/// A star system containing a grid of sectors.
	/// Is always square and always has an odd number of sectors across.
	/// </summary>
	public class StarSystem
	{
		/// <summary>
		/// Creates a star system.
		/// </summary>
		/// <param name="radius">The number of sectors counting outward from the center to the edge.</param>
		public StarSystem(int radius)
		{
			Radius = radius;
			sectors = new Sector[Diameter, Diameter];
			for (int x = -radius; x <= radius; x++)
			{
				for (int y = -radius; y <= radius; y++)
					SetSector(x, y, new Sector());
			}
		}

		/// <summary>
		/// The number of sectors counting outward from the center to the edge.
		/// </summary>
		public int Radius { get; private set; }

		/// <summary>
		/// The number of sectors across the star system.
		/// </summary>
		public int Diameter
		{
			get { return Math.Max(0, Radius * 2 + 1); }
		}

		private Sector[,] sectors;

		public Sector GetSector(int x, int y)
		{
			return sectors[x + Radius, y + Radius];
		}

		public Sector GetSector(Point p)
		{
			return GetSector(p.X, p.Y);
		}

		public void SetSector(int x, int y, Sector sector)
		{
			sectors[x + Radius, y + Radius] = sector;
		}

		public void SetSector(Point p, Sector sector)
		{
			SetSector(p.X, p.Y, sector);
		}

		/// <summary>
		/// Finds the coordinates of a sector.
		/// </summary>
		/// <param name="sector">The sector to search for.</param>
		/// <exception cref="ArgumentException">if the specified sector is not present in this star system.</exception>
		/// <returns>The coordinates of the sector.</returns>
		public Point FindSector(Sector sector)
		{
			for (int x = -Radius; x <= Radius; x++)
			{
				for (int y = -Radius; y <= Radius; y++)
				{
					if (GetSector(x, y) == sector)
						return new Point(x, y);
				}
			}

			throw new ArgumentException("The specified sector was not found in this star system.");
		}
	}
}