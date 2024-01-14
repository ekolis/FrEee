using FrEee.Enumerations;
using FrEee.Interfaces;
using FrEee.Objects.Abilities;
using FrEee.Objects.Civilization;
using FrEee.Utility; using FrEee.Serialization;
using FrEee.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace FrEee.Objects.Space;

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

	public AbilityTargets AbilityTarget
	{
		get { return AbilityTargets.Sector; }
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

	public IEnumerable<IAbilityObject> Children
	{
		get { return SpaceObjects; }
	}

	public Point Coordinates { get; set; }

	public Image Icon
	{
		get
		{
			return SpaceObjects.Largest()?.Icon ?? StarSystem?.Icon;
		}
	}

	public Image Icon32 => Icon.Resize(32);

	public IEnumerable<string> IconPaths
	{
		get
		{
			return SpaceObjects.Largest()?.IconPaths ?? StarSystem?.IconPaths;
		}
	}

	public IEnumerable<Ability> IntrinsicAbilities
	{
		get { yield break; }
	}

	public bool IsContested
	{
		get
		{
			return SpaceObjects.Select(sobj => sobj.Owner).Distinct().ExceptSingle((Empire)null).Count() > 1;
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

	public IEnumerable<IAbilityObject> Parents
	{
		get
		{
			yield return StarSystem;
		}
	}

	public long PopulationStorageFree
	{
		get { return 0; }
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

	[DoNotSerialize(false)]
	Sector ILocated.Sector
	{
		get { return this; }
		set { throw new NotSupportedException("Cannot set the sector of a sector."); }
	}

	public IEnumerable<ISpaceObject> SpaceObjects
	{
		get
		{
			if (StarSystem == null)
				return Enumerable.Empty<ISpaceObject>();
			var result = StarSystem.SpaceObjectLocations.Where(l => l.Location == Coordinates).Select(l => l.Item).ExceptSingle(null).Where(sobj => !(sobj is IContainable<Fleet>) || ((IContainable<Fleet>)sobj).Container == null);

			// on the server we don't want to count memories as physical space objects
			if (Empire.Current == null)
				result = result.Where(x => !x.IsMemory);

			return result.ExceptNull().ToArray();
		}
	}

	[DoNotSerialize]
	public StarSystem StarSystem { get { return starSystem; } set { starSystem = value; } }

	private GalaxyReference<StarSystem> starSystem { get; set; }

	public static bool operator !=(Sector s1, Sector s2)
	{
		return !(s1 == s2);
	}

	public static bool operator ==(Sector s1, Sector s2)
	{
		if (s1 is null && s2 is null)
			return true;
		if (s1 is null || s2 is null)
			return false;
		return s1.starSystem == s2.starSystem && s1.Coordinates == s2.Coordinates;
	}

	public long AddPopulation(Race race, long amount)
	{
		// population jettisoned into space just disappears without a trace...
		return 0;
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

	public Visibility CheckVisibility(Empire emp)
	{
		throw new NotImplementedException();
	}

	public override bool Equals(object? obj)
	{
		if (obj is Sector s)
			return this == s;
		return false;
	}

	public IEnumerable<IAbilityObject> GetContainedAbilityObjects(Empire emp)
	{
		return SpaceObjects.Where(sobj => sobj?.Owner == emp).OfType<IAbilityObject>();
	}

	public override int GetHashCode()
	{
		return HashCodeMasher.Mash(starSystem, Coordinates);
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
			u.Container?.RemoveUnit(u);
		}

		// place in space if it's actually in space
		if (StarSystem != null)
			StarSystem.Place(sobj, Coordinates);
	}

	public void Remove(ISpaceObject sobj)
	{
		// remove from fleet if necessary
		if (sobj is IMobileSpaceObject v)
		{
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

	public long RemovePopulation(Race race, long amount)
	{
		// population cannot be recovered from space!
		return amount;
	}

	public bool RemoveUnit(IUnit unit)
	{
		if (unit is IMobileSpaceObject)
		{
			Remove((IMobileSpaceObject)unit);
		}
		return false;
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

	public override string ToString()
	{
		var largestObject = SpaceObjects.Largest();

		// If sector doesn't have space objects, or the star system is unexplored, early return just the sector name
		if (largestObject == null || StarSystem == null)
			return Name;

		// From here on, largestObject and StarSystem are defined

		// Don't display the star system name, if the space object's name contains it, to avoid repetition
		bool objectNameContainsStarName = Regex.IsMatch(largestObject.Name, string.Format(@"\b{0}\b", Regex.Escape(StarSystem.Name ?? "(unexplored)")));
		if (objectNameContainsStarName)
			return largestObject + " (" + Coordinates.X + ", " + Coordinates.Y + ")";
		else
			return largestObject + " at " + Name;
	}
}