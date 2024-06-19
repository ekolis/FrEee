using FrEee.Objects.Civilization;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Extensions;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using FrEee.Objects.GameState;
using FrEee.Objects.Vehicles;
using FrEee.Processes.Combat;
using FrEee.Ecs.Abilities.Utility;

namespace FrEee.Objects.Technology;

/// <summary>
/// A mount that can be applied to a component.
/// </summary>
public class Mount : IResearchable, IModObject, IUpgradeable<Mount>
{
	public Mount()
	{
		AbilityPercentages = new Dictionary<AbilityRule, IDictionary<int, Formula<int>>>();
		AbilityModifiers = new Dictionary<AbilityRule, IDictionary<int, Formula<int>>>();
		UnlockRequirements = new List<Requirement<Empire>>();
	}

	/// <summary>
	/// Additive modifiers for abilities.
	/// </summary>
	public IDictionary<AbilityRule, IDictionary<int, Formula<int>>> AbilityModifiers
	{
		get;
		set;
	}

	/// <summary>
	/// Percentage factors for abilities.
	/// </summary>
	public IDictionary<AbilityRule, IDictionary<int, Formula<int>>> AbilityPercentages
	{
		get;
		set;
	}

	/// <summary>
	/// An abbreviation for this mount.
	/// </summary>
	public Formula<string> Code { get; set; }

	/// <summary>
	/// Percentage of normal component cost.
	/// </summary>
	public Formula<int> CostPercent { get; set; }

	/// <summary>
	/// A description of this mount.
	/// </summary>
	public Formula<string> Description { get; set; }

	/// <summary>
	/// Percentage of normal component hitpoints.
	/// </summary>
	public Formula<int> DurabilityPercent { get; set; }

	public Image Icon
	{
		get { return Pictures.GetIcon(this); }
	}

	public Image Icon32 => Icon.Resize(32);

	public IEnumerable<string> IconPaths
	{
		get
		{
			return PortraitPaths;
		}
	}

	public bool IsDisposed { get; set; }

	public bool IsMemory
	{
		get;
		set;
	}

	/// <summary>
	/// Mounts cannot currently be obsoleted.
	/// TODO - add family and roman numeral properties to mounts
	/// </summary>
	public bool IsObsolescent
	{
		get { return false; }
	}

	/// <summary>
	/// Mounts cannot currently be manually obsoleted.
	/// </summary>
	public bool IsObsolete
	{
		get { return false; }
	}

	/// <summary>
	/// Mounts cannot currently be obsoleted.
	/// TODO - add family and roman numeral properties to mounts
	/// </summary>
	public Mount LatestVersion
	{
		get { return this; }
	}

	/// <summary>
	/// Maximum vehicle size to use this mount. Null means no restriction.
	/// </summary>
	public Formula<int> MaximumVehicleSize { get; set; }

	/// <summary>
	/// Minimum vehicle size to use this mount. Null means no restriction.
	/// </summary>
	public Formula<int> MinimumVehicleSize { get; set; }

	public string ModID { get; set; }

	/// <summary>
	/// The full name of this mount.
	/// </summary>
	public Formula<string> Name { get; set; }

	string INamed.Name
	{
		get { return Name; }
	}

	/// <summary>
	/// Mounts cannot currently be obsoleted.
	/// TODO - add family and roman numeral properties to mounts
	/// </summary>
	public IEnumerable<Mount> NewerVersions
	{
		get { yield break; }
	}

	/// <summary>
	/// Mounts cannot currently be obsoleted.
	/// TODO - add family and roman numeral properties to mounts
	/// </summary>
	public IEnumerable<Mount> OlderVersions
	{
		get { yield break; }
	}

	/// <summary>
	/// The name of the picture to use for this mount.
	/// </summary>
	public Formula<string> PictureName { get; set; }

	public System.Drawing.Image Portrait
	{
		get { return Pictures.GetPortrait(this); }
	}

	public IEnumerable<string> PortraitPaths
	{
		get
		{
			if (Mod.Current?.RootPath != null)
				yield return Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Mounts", PictureName);
			yield return Path.Combine("Pictures", "Mounts", PictureName);
		}
	}

	/// <summary>
	/// Required component families, comma delimited list. Null means no restriction.
	/// </summary>
	public Formula<string> RequiredComponentFamily { get; set; }

	public string ResearchGroup
	{
		get { return "Mount"; }
	}

	/// <summary>
	/// A shorter name for this mount.
	/// </summary>
	public Formula<string> ShortName { get; set; }

	/// <summary>
	/// Percentage of normal component size.
	/// </summary>
	public Formula<int> SizePercent { get; set; }

	/// <summary>
	/// Percentage of normal component supply usage.
	/// </summary>
	public Formula<int> SupplyUsagePercent { get; set; }

	/// <summary>
	/// Parameters from the mod meta templates.
	/// </summary>
	public IDictionary<string, object> TemplateParameters { get; set; }

	public double Timestamp
	{
		get;
		set;
	}

	/// <summary>
	/// Requirements to unlock this mount.
	/// </summary>
	public IList<Requirement<Empire>> UnlockRequirements
	{
		get;
		set;
	}

	/// <summary>
	/// Vehicle types which can use this mount.
	/// </summary>
	public VehicleTypes VehicleTypes { get; set; }

	/// <summary>
	/// Accuracy modifier for weapons.
	/// </summary>
	public Formula<int> WeaponAccuracyModifier { get; set; }

	/// <summary>
	/// Percentage of normal weapon damage.
	/// </summary>
	public Formula<int> WeaponDamagePercent { get; set; }

	/// <summary>
	/// Range modifier for weapons.
	/// </summary>
	public Formula<int> WeaponRangeModifier { get; set; }

	/// <summary>
	/// Weapon types which can use this mount.
	/// </summary>
	public Formula<WeaponTypes> WeaponTypes { get; set; }

	/// <summary>
	/// Mod objects are fully known to everyone.
	/// </summary>
	/// <param name="emp"></param>
	/// <returns></returns>
	public Visibility CheckVisibility(Empire emp)
	{
		return Visibility.Scanned;
	}

	public void Dispose()
	{
		if (IsDisposed)
			return;
		if (Mod.Current != null)
			Mod.Current.Mounts.Remove(this);
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
}
