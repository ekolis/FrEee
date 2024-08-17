using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FrEee.Objects.Civilization;
using FrEee.Modding;
using FrEee.Modding.Templates;
using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Extensions;
using FrEee.Objects.GameState;
using FrEee.Modding.Abilities;

namespace FrEee.Objects.Space;

/// <summary>
/// A star system containing a grid of sectors.
/// Is always square and always has an odd number of sectors across.
/// </summary>
[Serializable]
public class StarSystem : IReferrable, IPictorial, IFoggable, ICommonAbilityObject, IAbilityContainer
{
	/// <summary>
	/// Creates a star system.
	/// </summary>
	/// <param name="radius">The number of sectors counting outward from the center to the edge.</param>
	public StarSystem(int radius)
	{
		Radius = radius;
		Abilities = new List<Ability>();
		SpaceObjectLocations = new HashSet<ObjectLocation<ISpaceObject>>();
		ExploredByEmpires = new HashSet<Empire>();
	}

	/// <summary>
	/// Any special abilities possessed by this star system.
	/// </summary>
	public IList<Ability> Abilities { get; private set; }

	public AbilityTargets AbilityTarget
	{
		get { return AbilityTargets.StarSystem; }
	}

	public Image BackgroundImage
	{
		get
		{
			if (BackgroundImagePath == null)
				return null;
			return Pictures.GetModImage(
				Path.Combine("Pictures", "Systems", "1024x768", BackgroundImagePath),
				Path.Combine("Pictures", "Systems", "800x600", BackgroundImagePath),
				Path.Combine("Pictures", "Systems", BackgroundImagePath)
				);
		}
	}

	/// <summary>
	/// The path to the background image, relative to Pictures/Systems.
	/// </summary>
	public string BackgroundImagePath { get; set; }

	public IEnumerable<IAbilityObject> Children
	{
		get
		{
			foreach (var l in SpaceObjectLocations)
				yield return l.Item;
		}
	}

	public Point Coordinates
	{
		get
		{
			return Location.Location;
		}
	}

	/// <summary>
	/// The description of this star system.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// The number of sectors across the star system.
	/// </summary>
	public int Diameter
	{
		get { return Math.Max(0, Radius * 2 + 1); }
	}

	/// <summary>
	/// If true, empire homeworlds can be located in this system.
	/// </summary>
	public bool EmpiresCanStartIn { get; set; }

	/// <summary>
	/// Empires which have explored this star system.
	/// </summary>
	public ICollection<Empire> ExploredByEmpires { get; private set; }

	public Image Icon
	{
		get { return BackgroundImage; }
	}

	public Image Icon32 => Icon.Resize(32);

	public IEnumerable<string> IconPaths
	{
		get
		{
			return PortraitPaths;
		}
	}

	public long ID
	{
		get;
		set;
	}

	public IEnumerable<Ability> IntrinsicAbilities
	{
		get { return Abilities; }
	}

	public bool IsDisposed { get; set; }

	public bool IsMemory
	{
		get;
		set;
	}

	public ObjectLocation<StarSystem> Location
	{
		get
		{
			try
			{
				return Galaxy.Current.StarSystemLocations.Single(l => l.Item == this);
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidOperationException(this + " does not appear to be located anywhere on the galaxy map, or it has multiple locations.", ex);
			}
		}
	}

	/// <summary>
	/// The name of this star system.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// If true, the background image for this system will be centered, not tiled, in combat.
	/// </summary>
	public bool NonTiledCenterCombatImage { get; set; }

	/// <summary>
	/// Star systems are not owned, per se.
	/// </summary>
	public Empire Owner
	{
		get { return null; }
	}

	public IEnumerable<IAbilityObject> Parents
	{
		get
		{
			yield return Galaxy.Current;
		}
	}

	public Image Portrait
	{
		get { return BackgroundImage; }
	}

	public IEnumerable<string> PortraitPaths
	{
		get
		{
			yield return Path.Combine("Systems", BackgroundImagePath);
		}
	}

	/// <summary>
	/// The number of sectors counting outward from the center to the edge.
	/// </summary>
	public int Radius { get; private set; }

	public IEnumerable<Sector> Sectors
	{
		get
		{
			for (var x = -Radius; x <= Radius; x++)
			{
				for (var y = -Radius; y <= Radius; y++)
				{
					yield return new Sector(this, new Point(x, y));
				}
			}
		}
	}

	/// <summary>
	/// The space objects contained in this star system.
	/// </summary>
	public ICollection<ObjectLocation<ISpaceObject>> SpaceObjectLocations { get; private set; }

	public IEnumerable<ISpaceObject> SpaceObjects { get { return FindSpaceObjects<ISpaceObject>(); } }

	public double Timestamp { get; set; }

	/// <summary>
	/// Abilities for random warp points that appear in this system.
	/// </summary>
	[DoNotSerialize]
	public RandomAbilityTemplate WarpPointAbilities { get { return warpPointAbilities; } set { warpPointAbilities = value; } }

	private ModReference<RandomAbilityTemplate> warpPointAbilities { get; set; }

	public bool AreCoordsInBounds(int x, int y)
	{
		return x >= -Radius && x <= Radius && y >= -Radius && y <= Radius;
	}

	public bool AreCoordsInBounds(Point p)
	{
		return AreCoordsInBounds(p.X, p.Y);
	}

	public Visibility CheckVisibility(Empire emp)
	{
		if (FindSpaceObjects<ISpaceObject>(sobj => sobj.Owner == emp && !sobj.IsMemory).Any())
			return Visibility.Visible;
		else if (emp.ExploredStarSystems.Contains(this))
			return Visibility.Fogged;
		return Visibility.Unknown;
	}

	public bool Contains(ISpaceObject sobj)
	{
		return SpaceObjectLocations.Any(l => l.Item == sobj);
	}

	public void Dispose()
	{
		if (IsDisposed)
			return;
		if (IsDisposed)
			return;
		Galaxy.Current.UnassignID(this);
		if (!IsMemory)
			this.UpdateEmpireMemories();
	}

	/// <summary>
	/// Do any of the empire's space objects in a sector have an ability?
	/// </summary>
	/// <param name="emp"></param>
	/// <param name="name"></param>
	/// <param name="index"></param>
	/// <param name="filter"></param>
	/// <returns></returns>
	public bool DoesSectorHaveAbility(Point coords, Empire emp, string name, int index = 1, Func<Ability, bool> filter = null)
	{
		var sobjs = FindSpaceObjects<ISpaceObject>().Where(o => o.Owner == emp && o.FindCoordinates() == coords);
		return sobjs.SelectMany(o => o.UnstackedAbilities(true)).Where(a => a.Rule.Matches(name) && (filter == null || filter(a))).Any();
	}

	public Point FindCoordinates(ISpaceObject sobj)
	{
		try
		{
			return SpaceObjectLocations.Single(l => l.Item == sobj).Location;
		}
		catch (Exception ex)
		{
			throw new Exception("Can't find coordinates of " + sobj + " in " + this + ".", ex);
		}
	}

	/// <summary>
	/// Searches for space objects matching criteria.
	/// </summary>
	/// <typeparam name="T">The type of space object.</typeparam>
	/// <param name="criteria">The criteria.</param>
	/// <returns>The matching space objects.</returns>
	public IEnumerable<T> FindSpaceObjects<T>(Func<T, bool> criteria = null)
	{
		return SpaceObjectLocations.Select(l => l.Item).OfType<T>().Where(l => criteria == null || criteria(l));
	}

	public IEnumerable<IAbilityObject> GetContainedAbilityObjects(Empire emp)
	{
		return SpaceObjectLocations.Select(l => l.Item).Where(sobj => sobj?.Owner == emp).OfType<IAbilityObject>();
	}

	public Sector GetSector(int x, int y)
	{
		return GetSector(new Point(x, y));
	}

	public Sector GetSector(Point p)
	{
		if (!AreCoordsInBounds(p))
			throw new Exception("Sector coordinates (" + p.X + ", " + p.Y + ") are out of bounds for star system of radius " + Radius + ".");
		return new Sector(this, p);
	}

	/// <summary>
	/// Do any of the empire's space objects in this system have an ability?
	/// </summary>
	/// <param name="emp"></param>
	/// <param name="name"></param>
	/// <param name="index"></param>
	/// <param name="filter"></param>
	/// <returns></returns>
	public bool HasAbility(Empire emp, string name, int index = 1, Func<Ability, bool> filter = null)
	{
		return FindSpaceObjects<ISpaceObject>(o => o.Owner == emp).SelectMany(o => o.UnstackedAbilities(true)).Where(a => a.Rule.Matches(name) && (filter == null || filter(a))).Any();
	}

	public bool IsObsoleteMemory(Empire emp)
	{
		return CheckVisibility(emp) >= Visibility.Visible && Timestamp < Galaxy.Current.Timestamp - 1;
	}

	public Sector PickRandomSector(PRNG prng = null)
	{
		return new Sector(this, new Point(RandomHelper.Range(-Radius, Radius, prng), RandomHelper.Range(-Radius, Radius, prng)));
	}

	public void Place(ISpaceObject sobj, Point coords)
	{
		var sys = sobj.FindStarSystem();
		if (sys != null)
		{
			sys.Remove(sobj);
			sobj.Sector = null;
		}
		else
			sobj.Sector = new Sector(this, coords);

		SpaceObjectLocations.Add(new ObjectLocation<ISpaceObject>(sobj, coords));

		MarkAsExploredBy(sobj.Owner);

		// see if we got hit by a minefield
		if (!Serializer.IsDeserializing)
			sobj.DealWithMines();
	}

	/// <summary>
	/// Removes any space objects, etc. that the current empire cannot see.
	/// </summary>
	/// <param name="galaxy">The galaxy, for context.</param>
	public void Redact(Empire emp)
	{
		// hide explored-by empires
		foreach (var e in ExploredByEmpires.Where(e => e != emp).ToArray())
			ExploredByEmpires.Remove(e);

		// hide background image and description (so player can't see what kind of system it is) and name and abilities
		if (!ExploredByEmpires.Contains(emp))
		{
			BackgroundImagePath = null;
			Name = "(Unexplored)";
			Description = "An unexplored star system. Who knows what lies in wait here?";
			Abilities.Clear();
		}
	}

	public void Remove(ISpaceObject sobj)
	{
		foreach (var l in SpaceObjectLocations.ToArray())
		{
			if (l.Item == sobj)
				SpaceObjectLocations.Remove(l);
		}
	}

	public override string ToString()
	{
		return Name;
	}

	/// <summary>
	/// Marks this system as explored by a particular empire and adds an appropriate log entry for the player.
	/// Does nothing if already explored by that player.
	/// </summary>
	/// <param name="e"></param>
	public void MarkAsExploredBy(Empire e)
	{
		if (e != null && !ExploredByEmpires.Contains(e))
		{
			ExploredByEmpires.Add(e);
			e.RecordLog(this, $"We have explored the {Name} system.", LogMessages.LogMessageType.Generic);
		}
	}
}