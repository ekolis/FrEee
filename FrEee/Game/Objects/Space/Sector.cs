using System;
using System.Collections.Generic;
using System.Linq;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using System.Drawing;
using FrEee.Game.Objects.Abilities;
using FrEee.Utility.Extensions;
using FrEee.Game.Enumerations;
using FrEee.Utility;
using FrEee.Game.Objects.Vehicles;

namespace FrEee.Game.Objects.Space
{
	/// <summary>
	/// A sector in a star system.
	/// </summary>
	[Serializable]
	public class Sector : IPromotable, ICargoContainer
	{
		public Sector(StarSystem starSystem, Point coordinates)
		{
			StarSystem = starSystem;
			Coordinates = coordinates;
		}

		private Reference<StarSystem> starSystem { get; set; }

		[DoNotSerialize]
		public StarSystem StarSystem { get { return starSystem; } set { starSystem = value; } }

		public Point Coordinates { get; set; }

		public IEnumerable<ISpaceObject> SpaceObjects
		{
			get
			{
				return StarSystem.SpaceObjectLocations.Where(l => l.Location == Coordinates).Select(l => l.Item).ToList();
			}
		}

		public void Place(ISpaceObject sobj)
		{
			StarSystem.Place(sobj, Coordinates);
		}

		public void Remove(ISpaceObject sobj)
		{
			if (SpaceObjects.Contains(sobj))
				StarSystem.Remove(sobj);
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap)
		{
			starSystem.ReplaceClientIDs(idmap);
		}

		public static bool operator ==(Sector s1, Sector s2)
		{
			if (s1.IsNull() && s2.IsNull())
				return true;
			if (s1.IsNull() || s2.IsNull())
				return false;
			return s1.starSystem == s2.starSystem && s1.Coordinates == s2.Coordinates;
		}

		public static bool operator !=(Sector s1, Sector s2)
		{
			return !(s1 == s2);
		}

		public override int GetHashCode()
		{
			if (starSystem == null)
				return Coordinates.GetHashCode();
			return starSystem.GetHashCode() ^ Coordinates.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj is Sector)
				return this == (Sector)obj;
			return false;
		}

		public Cargo Cargo
		{
			get
			{
				// TODO - implement sector cargo once we have unit groups
				return new Cargo();
			}
		}

		/// <summary>
		/// Sectors can contain practically infinite cargo.
		/// </summary>
		public int CargoStorage
		{
			get { return int.MaxValue; }
		}

		public long PopulationStorageFree
		{
			get { return 0; }
		}

		public long AddPopulation(Race race, long amount)
		{
			// population jettisoned into space just disappears without a trace...
			return 0;
		}

		public long RemovePopulation(Race race, long amount)
		{
			// population cannot be recovered from space!
			return amount;
		}

		public bool AddUnit(Unit unit)
		{
			// TODO - allow launching of units
			return false;
		}

		public bool RemoveUnit(Unit unit)
		{
			// TODO - allow recovery of units
			return false;
		}

		public Image Icon
		{
			get { return StarSystem.Icon; }
		}

		public Image Portrait
		{
			get { return StarSystem.Portrait; }
		}

		public string Name
		{
			get { return StarSystem + " (" + Coordinates.X + ", " + Coordinates.Y + ")"; }
		}

		public override string ToString()
		{
			return Name;
		}

		Sector ILocated.Sector
		{
			get { return this; }
		}
	}
}
