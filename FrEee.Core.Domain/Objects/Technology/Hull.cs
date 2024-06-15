using FrEee.Objects.Civilization;
using FrEee.Objects.Vehicles;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using FrEee.Objects.GameState;
using FrEee.Serialization;
using FrEee.Ecs;

namespace FrEee.Objects.Technology;

/// <summary>
/// A vehicle hull.
/// </summary>
/// <typeparam name="T">The type of vehicle.</typeparam>
[Serializable]
public class Hull<T> : IHull<T> where T : IVehicle
{
	public Hull()
	{
		PictureNames = new List<string>();
		UnlockRequirements = new List<Requirement<Empire>>();
		Abilities = new List<Ability>();
		Cost = new ResourceQuantity();
	}

	public IEnumerable<Ability> Abilities { get; set; }

	public AbilityTargets AbilityTarget
	{
		get
		{
			if (VehicleType == VehicleTypes.Base)
				return AbilityTargets.Base;
			if (VehicleType == VehicleTypes.Ship)
				return AbilityTargets.Ship;
			if (VehicleType == VehicleTypes.Fighter)
				return AbilityTargets.Fighter;
			if (VehicleType == VehicleTypes.Satellite)
				return AbilityTargets.Satellite;
			if (VehicleType == VehicleTypes.Mine)
				return AbilityTargets.Mine;
			if (VehicleType == VehicleTypes.Drone)
				return AbilityTargets.Drone;
			if (VehicleType == VehicleTypes.Troop)
				return AbilityTargets.Troop;
			if (VehicleType == VehicleTypes.WeaponPlatform)
				return AbilityTargets.WeaponPlatform;
			return AbilityTargets.Invalid;
		}
	}

	/// <summary>
	/// Can this hull use components with the Ship Auxiliary Control ability?
	/// </summary>
	public bool CanUseAuxiliaryControl { get; set; }

	public IEnumerable<IAbilityObject> Children
	{
		get { yield break; }
	}

	/// <summary>
	/// An abbeviation for the ship's name.
	/// </summary>
	public string Code { get; set; }

	/// <summary>
	/// The cost to build this hull.
	/// </summary>
	public ResourceQuantity Cost { get; set; }

	/// <summary>
	/// A description of the hull.
	/// </summary>
	public string Description { get; set; }

	[DoNotSerialize]
	public Image Icon
	{
		get
		{
			if (Empire.Current == null)
				return null;
			return GetIcon(Empire.Current.ShipsetPath);
		}
	}

	public Image Icon32 => Icon.Resize(32);

	public IEnumerable<string> IconPaths
	{
		get
		{
			return GetPaths("Mini");
		}
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

	/// <summary>
	/// Hulls cannot currently be obsoleted.
	/// TODO - hull family field
	/// </summary>
	public bool IsObsolescent
	{
		get { return false; }
	}

	/// <summary>
	/// Hulls cannot manually be obsoleted.
	/// </summary>
	public bool IsObsolete
	{
		get { return false; }
	}

	/// <summary>
	/// Hulls cannot currently be obsoleted.
	/// TODO - hull family field
	/// </summary>
	public IHull<T> LatestVersion
	{
		get { return this; }
	}

	IHull IUpgradeable<IHull>.LatestVersion
	{
		get { return LatestVersion; }
	}

	/// <summary>
	/// Maximum number of engines allowed.
	/// </summary>
	public int MaxEngines { get; set; }

	/// <summary>
	/// Required number of crew quarters components.
	/// </summary>
	public int MinCrewQuarters { get; set; }

	/// <summary>
	/// Required number of life support components.
	/// </summary>
	public int MinLifeSupport { get; set; }

	/// <summary>
	/// Minimum percentage of space required to be used for cargo-storage components.
	/// </summary>
	public int MinPercentCargoBays { get; set; }

	/// <summary>
	/// Minimum percentage of space required to be used for colonizing components.
	/// </summary>
	public int MinPercentColonyModules { get; set; }

	/// <summary>
	/// Minimum percentage of space required to be used for fighter-launching components.
	/// </summary>
	public int MinPercentFighterBays { get; set; }

	public string ModID { get; set; }

	/// <summary>
	/// The name of the hull.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Does this hull need a component with the Ship Bridge ability?
	/// </summary>
	public bool NeedsBridge { get; set; }

	/// <summary>
	/// Mounts cannot currently be obsoleted.
	/// TODO - add family and roman numeral properties to mounts
	/// </summary>
	public IEnumerable<IHull<T>> NewerVersions
	{
		get { yield break; }
	}

	IEnumerable<IHull> IUpgradeable<IHull>.NewerVersions
	{
		get { return NewerVersions; }
	}

	/// <summary>
	/// Mounts cannot currently be obsoleted.
	/// TODO - add family and roman numeral properties to mounts
	/// </summary>
	public IEnumerable<IHull<T>> OlderVersions
	{
		get { yield break; }
	}

	IEnumerable<IHull> IUpgradeable<IHull>.OlderVersions
	{
		get { return OlderVersions; }
	}

	public Empire Owner
	{
		get { return null; }
	}

	public IEnumerable<IAbilityObject> Parents
	{
		get
		{
			yield break;
		}
	}

	/// <summary>
	/// Names of pictures from the approrpriate shipset to use for the hull.
	/// </summary>
	public IList<string> PictureNames { get; private set; }

	[DoNotSerialize]
	public Image Portrait
	{
		get
		{
			if (Empire.Current == null)
				return null;
			return GetPortrait(Empire.Current.ShipsetPath);
		}
	}

	public IEnumerable<string> PortraitPaths
	{
		get
		{
			return GetPaths("Portrait");
		}
	}

	public string ResearchGroup
	{
		get { return "Hull"; }
	}

	/// <summary>
	/// A shorter name for use in some places.
	/// </summary>
	public string ShortName { get; set; }

	/// <summary>
	/// The amount of space available for components.
	/// </summary>
	public int Size { get; set; }

	/// <summary>
	/// Parameters from the mod meta templates.
	/// </summary>
	public IDictionary<string, object> TemplateParameters { get; set; }

	/// <summary>
	/// The number of thrust points required to generate 1 movement point.
	/// Also known as Engines Per Move, though technically engines can generate more than 1 thrust point.
	/// </summary>
	public int ThrustPerMove { get; set; }

	public double Timestamp
	{
		get;
		set;
	}

	/// <summary>
	/// Requirements to unlock this hull.
	/// </summary>
	public IList<Requirement<Empire>> UnlockRequirements
	{
		get;
		private set;
	}

	public VehicleTypes VehicleType
	{
		get
		{
			if (typeof(T) == typeof(Ship))
				return VehicleTypes.Ship;
			if (typeof(T) == typeof(Base))
				return VehicleTypes.Base;
			if (typeof(T) == typeof(Fighter))
				return VehicleTypes.Fighter;
			if (typeof(T) == typeof(Troop))
				return VehicleTypes.Troop;
			if (typeof(T) == typeof(Satellite))
				return VehicleTypes.Satellite;
			if (typeof(T) == typeof(Drone))
				return VehicleTypes.Drone;
			if (typeof(T) == typeof(Mine))
				return VehicleTypes.Mine;
			if (typeof(T) == typeof(WeaponPlatform))
				return VehicleTypes.WeaponPlatform;
			return VehicleTypes.Invalid;
		}
	}

	public string VehicleTypeName
	{
		get
		{
			return VehicleType.ToSpacedString();
		}
	}

	public bool CanUseMount(Mount m)
	{
		if (m == null)
			return true;
		if (m.MinimumVehicleSize != null && m.MinimumVehicleSize > Size)
			return false;
		if (m.MaximumVehicleSize != null && m.MaximumVehicleSize < Size)
			return false;
		if (!m.VehicleTypes.HasFlag(VehicleType))
			return false;
		return true;
	}

	/// <summary>
	/// Mod objects are fully known to everyone.
	/// </summary>
	/// <param name="emp"></param>
	/// <returns></returns>
	public Visibility CheckVisibility(Empire emp)
	{
		return Visibility.Scanned;
	}

	public IDesign<T> CreateDesign()
	{
		return new Design<T> { Hull = this };
	}

	public void Dispose()
	{
		if (IsDisposed)
			return;
		if (Mod.Current != null)
		{
			var h = (IHull<T>)this;
			Mod.Current.Hulls.Remove(h);
		}
	}

	/// <summary>
	/// The icon for this hull.
	/// </summary>
	public Image GetIcon(string shipsetPath)
	{
		if (shipsetPath == null)
			return null;
		return Pictures.GetIcon(this, shipsetPath);
	}

	/// <summary>
	/// The portrait for this hull.
	/// </summary>
	public Image GetPortrait(string shipsetPath)
	{
		if (shipsetPath == null)
			return null;
		return Pictures.GetPortrait(this, shipsetPath);
	}

	public bool IsObsoleteMemory(Empire emp)
	{
		return false;
	}

	public void Redact(Empire emp)
	{
		// TODO - tech items that aren't visible until some requirements are met
	}

	public override string ToString()
	{
		return Name;
	}

	private IEnumerable<string> GetPaths(string pathtype)
	{
		var paths = new List<string>();

		var shipsetPath = "Default";
		if (Empire.Current != null)
			shipsetPath = Empire.Current.ShipsetPath;

		foreach (var s in PictureNames)
		{
			if (Mod.Current?.RootPath != null)
			{
				paths.Add(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, pathtype + "_" + s));
				paths.Add(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, shipsetPath + "_" + pathtype + "_" + s)); // for SE4 shipset compatibility
			}
			paths.Add(Path.Combine("Pictures", "Races", shipsetPath, pathtype + "_" + s));
			paths.Add(Path.Combine("Pictures", "Races", shipsetPath, shipsetPath + "_" + pathtype + "_" + s)); // for SE4 shipset compatibility
		}
		return paths;
	}
}