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
	public class Sector : IPromotable, ICargoContainer, ICommonAbilityObject, IOwnable
	{
		public Sector(StarSystem starSystem, Point coordinates)
		{
			StarSystem = starSystem;
			Coordinates = coordinates;
		}

		private GalaxyReference<StarSystem> starSystem { get; set; }

		[DoNotSerialize]
		public StarSystem StarSystem { get { return starSystem; } set { starSystem = value; } }

		public Point Coordinates { get; set; }

		public IEnumerable<ISpaceObject> SpaceObjects
		{
			get
			{
				if (StarSystem == null)
					return Enumerable.Empty<ISpaceObject>();
				return StarSystem.SpaceObjectLocations.Where(l => l.Location == Coordinates).Select(l => l.Item).Where(sobj => !(sobj is IContainable<Fleet>) || ((IContainable<Fleet>)sobj).Container == null).ToList();
			}
		}

		public void Place(ISpaceObject sobj, bool removeFromFleet = true)
		{
			if (removeFromFleet)
			{
				// remove from fleet
				if (sobj is IMobileSpaceObject)
				{
					var v = (IMobileSpaceObject)sobj;
					if (v.Container != null)
						v.Container.Vehicles.Remove(v);
				}
			}

			// remove from cargo
			if (sobj is IUnit)
			{
				var u = (IUnit)sobj;
				u.Container.RemoveUnit(u);
			}

			// place in space if it's actually in space
			if (StarSystem != null)
				StarSystem.Place(sobj, Coordinates);
		}

		public void Remove(ISpaceObject sobj)
		{
			// remove from fleet
			if (sobj is IMobileSpaceObject)
			{
				var v = (IMobileSpaceObject)sobj;
				if (v.Container != null)
					v.Container.Vehicles.Remove(v);
			}

			// remove from cargo
			if (sobj is IUnit)
			{
				var u = (IUnit)sobj;
				if (!u.Container.Equals(this))
					u.Container.RemoveUnit(u);
			}

			if (SpaceObjects.Contains(sobj))
				StarSystem.Remove(sobj);
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			if (done == null)
				done = new HashSet<IPromotable>();
			if (!done.Contains(this))
			{
				done.Add(this);
				starSystem.ReplaceClientIDs(idmap, done);
			}
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
			return HashCodeMasher.Mash(starSystem, Coordinates);
		}

		public override bool Equals(object obj)
		{
			// TODO - upgrade equals to use "as" operator
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

		public bool AddUnit(IUnit unit)
		{
			// can't place a unit that can't move about in space, in space!
			if (!(unit is IMobileSpaceObject))
				return false;

			// TODO - limit number of units in space per empire as specified in Settings.txt

			// place this unit in a fleet with other similar units
			var fleet = this.SpaceObjects.OfType<Fleet>().SelectMany(f => f.SubfleetsWithNonFleetChildren()).Where(
				f => f.Vehicles.OfType<IUnit>().Where(u => u.Design == unit.Design).Any()).FirstOrDefault();
			var v = (IMobileSpaceObject)unit;
			if (fleet == null)
			{
				// create a new fleet, there's no fleet with similar units
				fleet = new Fleet();
				fleet.Name = unit.Design.Name + " Group";
				Place(fleet);
			}
			Place(v);
			fleet.Vehicles.Add(v);
			return true;
		}

		public bool RemoveUnit(IUnit unit)
		{
			if (unit is IMobileSpaceObject)
			{
				Remove((IMobileSpaceObject)unit);
			}
			return false;
		}

		public Image Icon
		{
			get
			{
				return SpaceObjects.Largest()?.Icon ?? StarSystem?.Icon;
			}
		}

		public Image Portrait
		{
			get
			{
				return SpaceObjects.Largest()?.Portrait ?? StarSystem?.Portrait;
			}
		}

		public IEnumerable<string> PortraitPaths
		{
			get
			{
				return SpaceObjects.Largest()?.PortraitPaths ?? StarSystem?.PortraitPaths;
			}
		}

		public IEnumerable<string> IconPaths
		{
			get
			{
				return SpaceObjects.Largest()?.IconPaths ?? StarSystem?.IconPaths;
			}
		}

		public string Name
		{
			get
			{
				if (StarSystem == null)
					return "(Unexplored)";
				return StarSystem + " (" + Coordinates.X + ", " + Coordinates.Y + ")";
			}
		}

		public override string ToString()
		{
			return Name;
		}

		[DoNotSerialize(false)]
		Sector ILocated.Sector
		{
			get { return this; }
			set { throw new NotSupportedException("Cannot set the sector of a sector."); }
		}

		/// <summary>
		/// Sectors don't contain population. (They kind of die in space...)
		/// </summary>
		public IDictionary<Race, long> AllPopulation
		{
			get { return new Dictionary<Race, long>(); }
		}


		public IEnumerable<IUnit> AllUnits
		{
			get { return StarSystem.SpaceObjectLocations.Where(l => l.Location == Coordinates).Select(l => l.Item).OfType<IUnit>().ToList(); }
		}

		/// <summary>
		/// Has this sector's star system been explored by an empire?
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public bool IsExploredBy(Empire emp)
		{
			if (StarSystem == null)
				return false;
			return StarSystem.ExploredByEmpires.Contains(emp);
		}

		public Visibility CheckVisibility(Empire emp)
		{
			throw new NotImplementedException();
		}

		public AbilityTargets AbilityTarget
		{
			get { return AbilityTargets.Sector; }
		}

		public IEnumerable<IAbilityObject> GetContainedAbilityObjects(Empire emp)
		{
			return SpaceObjects.Where(sobj => sobj.Owner == emp).OfType<IAbilityObject>();
		}

		public IEnumerable<Ability> IntrinsicAbilities
		{
			get { yield break; }
		}

		public IEnumerable<IAbilityObject> Children
		{
			get { return SpaceObjects; }
		}

		public IEnumerable<IAbilityObject> Parents
		{
			get
			{
				yield return StarSystem;
			}
		}

		public Empire Owner
		{
			get
			{
				var owned = SpaceObjects.Where(sobj => sobj.Owner != null);
				if (!owned.Any())
					return null;
				return owned.Largest().Owner;
			}
		}

		public bool IsContested
		{
			get
			{
				return SpaceObjects.Select(sobj => sobj.Owner).Distinct().ExceptSingle((Empire)null).Count() > 1;
			}
		}
	}
}
