using FrEee.Modding;
using Newtonsoft.Json;
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
			Abilities = new List<Ability>();
			WarpPointAbilities = new List<Ability>();
			ExploredByEmpires = new HashSet<Empire>();
		}

		/// <summary>
		/// The name of this star system.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The number of sectors counting outward from the center to the edge.
		/// </summary>
		public int Radius { get; private set; }

		/// <summary>
		/// The description of this star system.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// The path to the background image.
		/// </summary>
		public string BackgroundImagePath { get; set; }

		/// <summary>
		/// If true, empire homeworlds can be located in this system.
		/// </summary>
		public bool EmpiresCanStartIn { get; set; }

		/// <summary>
		/// If true, the background image for this system will be centered, not tiled, in combat.
		/// </summary>
		public bool NonTiledCenterCombatImage { get; set; }

		/// <summary>
		/// Any special abilities possessed by this star system.
		/// </summary>
		public IList<Ability> Abilities { get; private set; }

		/// <summary>
		/// Abilities for random warp points that appear in this system.
		/// </summary>
		public IList<Ability> WarpPointAbilities { get; set; }

		/// <summary>
		/// The number of sectors across the star system.
		/// </summary>
		public int Diameter
		{
			get { return Math.Max(0, Radius * 2 + 1); }
		}

		public bool AreCoordsInBounds(int x, int y)
		{
			return x >= -Radius && x <= Radius && y >= -Radius && y <= Radius;
		}

		public bool AreCoordsInBounds(Point p)
		{
			return AreCoordsInBounds(p.X, p.Y);
		}

		[JsonIgnore]
		private Sector[,] sectors;

		/// <summary>
		/// For serialization purposes mostly.
		/// </summary>
		private ICollection<ObjectLocation<Sector>> Sectors
		{
			get
			{
				var list = new List<ObjectLocation<Sector>>();
				for (int x = -Radius; x <= Radius; x++)
				{
					for (int y = -Radius; y <= Radius; y++)
						list.Add(new ObjectLocation<Sector> { Location = new Point(x, y), Item = GetSector(x, y) });
				}
				return list;
			}
			set
			{
				sectors = new Sector[Diameter, Diameter];
				foreach (var loc in value)
					SetSector(loc.Location, loc.Item);
			}
		}

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

		/// <summary>
		/// Searches for space objects matching criteria.
		/// </summary>
		/// <typeparam name="T">The type of space object.</typeparam>
		/// <param name="criteria">The criteria.</param>
		/// <returns>The matching space objects, grouped by location.</returns>
		public ILookup<Point, T> FindSpaceObjects<T>(Func<T, bool> criteria = null) where T : ISpaceObject
		{
			var list = new List<Tuple<Point, T>>();
			for (int x = -Radius; x <= Radius; x++)
			{
				for (int y = -Radius; y <= Radius; y++)
				{
					var sector = GetSector(x, y);
					var coords = new Point(x, y);
					foreach (var sobj in sector.SpaceObjects)
					{
						if (sobj is T && (criteria == null || criteria((T)sobj)))
							list.Add(Tuple.Create(coords, (T)sobj));
					}
				}
			}
			return list.ToLookup(t => t.Item1, t => t.Item2);
		}

		/// <summary>
		/// Empires which have explored this star system.
		/// </summary>
		public ICollection<Empire> ExploredByEmpires { get; private set; }

		/// <summary>
		/// Removes any space objects, etc. that the current empire cannot see.
		/// </summary>
		/// <param name="galaxy">The galaxy, for context.</param>
		public void Redact(Galaxy galaxy)
		{
			// hide space objects
			var toRemove = new List<Tuple<Point, ISpaceObject>>();
			foreach (var group in FindSpaceObjects<ISpaceObject>().ToArray())
			{
				foreach (var sobj in group)
				{
					var vis = sobj.CheckVisibility(galaxy, this);
					if (vis != Visibility.Unknown)
						sobj.Redact(galaxy, this, vis);
					else
						toRemove.Add(Tuple.Create(group.Key, sobj));
				}
			}
			foreach (var t in toRemove)
				GetSector(t.Item1).SpaceObjects.Remove(t.Item2);

			// hide explored-by empires
			foreach (var emp in ExploredByEmpires.Where(emp => emp != galaxy.CurrentEmpire).ToArray())
				ExploredByEmpires.Remove(emp);
		}
	}
}