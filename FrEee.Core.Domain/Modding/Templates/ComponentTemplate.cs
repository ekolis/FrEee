using FrEee.Objects.Civilization;
using FrEee.Objects.Technology;
using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FrEee.Objects.GameState;
using FrEee.Processes.Combat;
using FrEee.Modding.Abilities;
using FrEee.Vehicles.Types;

namespace FrEee.Modding.Templates;

/// <summary>
/// A template for a vehicle component.
/// </summary>
[Serializable]
public class ComponentTemplate : IModObject, IResearchable, IAbilityContainer, ITemplate<Component>, IUpgradeable<ComponentTemplate>
{
	public ComponentTemplate()
	{
		Abilities = new List<Ability>();
		UnlockRequirements = new List<Requirement<Empire>>();
		Cost = new ResourceFormula(this);
	}

	/// <summary>
	/// Abilities possessed by this component.
	/// </summary>
	public IList<Ability> Abilities { get; private set; }

	public AbilityTargets AbilityTarget
	{
		get { return AbilityTargets.Component; }
	}

	public IEnumerable<IAbilityObject> Children
	{
		get { yield break; }
	}

	/// <summary>
	/// The cost to build the component.
	/// </summary>
	public ResourceFormula Cost { get; set; }

	/// <summary>
	/// A description of the component.
	/// </summary>
	public Formula<string> Description { get; set; }

	/// <summary>
	/// The durability of the component, in kilotons. (Yes, kilotons.)
	/// </summary>
	public Formula<int> Durability { get; set; }

	/// <summary>
	/// The family that the component belongs to. Used for "Only Latest" on the design screen.
	/// </summary>
	public Formula<string> Family { get; set; }

	/// <summary>
	/// The group that the component belongs to. Used for grouping on the design screen.
	/// </summary>
	public Formula<string> Group { get; set; }

	[DoNotSerialize]
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

	public long ID { get; set; }

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

	public bool IsObsolescent
	{
		get { return this != LatestVersion; }
	}

	/// <summary>
	/// Is this component template obsolete for the current empire?
	/// </summary>
	/// <remarks>
	/// Components are obsolete if and only if they are obsolescent; they cannot currently be "marked" obsolete by the player.
	/// </remarks>
	public bool IsObsolete
	{
		get
		{
			return this.IsObsolescent;
		}
	}

	/// <summary>
	/// The latest upgraded version of this component template.
	/// </summary>
	public ComponentTemplate LatestVersion
	{
		get
		{
			var latest = OlderVersions.UnionSingle(this).Union(NewerVersions).Where(t => Empire.Current.HasUnlocked(t)).LastOrDefault();
			if (latest != null)
			{
				System.Diagnostics.Debug.WriteLine($"{this} is obsoleted by {latest}");
				return latest;
			}
			return this;
		}
	}

	/// <summary>
	/// The maximum number of this component family that can be installed on a vehicle, or null for no limit.
	/// </summary>
	public Formula<int> MaxPerVehicle { get; set; }

	public string ModID
	{
		get;
		set;
	}

	/// <summary>
	/// The name of the component.
	/// </summary>
	public Formula<string> Name { get; set; }

	string INamed.Name
	{
		get { return Name; }
	}

	public IEnumerable<ComponentTemplate> NewerVersions
	{
		get
		{
			return Mod.Current.ComponentTemplates.NewerVersions(this, ct => ct.Family);
		}
	}

	public IEnumerable<ComponentTemplate> OlderVersions
	{
		get
		{
			return Mod.Current.ComponentTemplates.OlderVersions(this, ct => ct.Family);
		}
	}

	/// <summary>
	/// No one owns component templates; they are shared.
	/// </summary>
	public Empire Owner { get { return null; } }

	public IEnumerable<IAbilityObject> Parents
	{
		get
		{
			yield break;
		}
	}

	/// <summary>
	/// Name of the picture used to represent this component, excluding the file extension.
	/// PNG files will be searched first, then BMP.
	/// </summary>
	public Formula<string> PictureName { get; set; }

	[DoNotSerialize]
	public Image Portrait
	{
		get { return Pictures.GetPortrait(this); }
	}

	public IEnumerable<string> PortraitPaths
	{
		get
		{
			if (Mod.Current.RootPath != null)
				yield return Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Components", PictureName);
			yield return Path.Combine("Pictures", "Components", PictureName);
		}
	}

	public string ResearchGroup
	{
		get { return "Component"; }
	}

	/// <summary>
	/// The value of the Roman numeral that should be displayed on the component's icon.
	/// </summary>
	public Formula<int> RomanNumeral { get; set; }

	/// <summary>
	/// The size of the component, in kilotons.
	/// </summary>
	public Formula<int> Size { get; set; }

	/// <summary>
	/// Used by artificial world construction abilities.
	/// </summary>
	public Formula<string> StellarConstructionGroup { get; set; }

	/// <summary>
	/// Amount of supply consumed when this component is "used". (What "usage" means depends on the component's abilities.)
	/// </summary>
	public Formula<int> SupplyUsage { get; set; }

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
	/// The requirements to unlock this component.
	/// </summary>
	public IList<Requirement<Empire>> UnlockRequirements { get; private set; }

	/// <summary>
	/// The vehicle types on which this component can be installed.
	/// </summary>
	public VehicleTypes VehicleTypes { get; set; }

	/// <summary>
	/// If this component is a weapon, info about it will be stored here.
	/// </summary>
	public WeaponInfo WeaponInfo { get; set; }

	public WeaponTypes WeaponType
	{
		get { return WeaponInfo == null ? WeaponTypes.NotAWeapon : WeaponInfo.WeaponType; }
	}

	/// <summary>
	/// Can this component use a mount?
	/// </summary>
	/// <param name="m"></param>
	/// <returns></returns>
	public bool CanUseMount(Mount m)
	{
		return m == null || (
		   (m.RequiredComponentFamily == null || m.RequiredComponentFamily.Value.Split(',').Select(q => q.Trim()).Contains(Family.Value)) &&
		   m.WeaponTypes.Value.HasFlag(WeaponType)
		   );
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

	public void Dispose()
	{
		if (IsDisposed)
			return;
		if (Mod.Current != null)
			Mod.Current.ComponentTemplates.Remove(this);
	}

	/// <summary>
	/// Creates a component from this template.
	/// </summary>
	/// <returns></returns>
	public Component Instantiate()
	{
		return new Component(null, new MountedComponentTemplate(null, this, null));
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

	public override bool Equals(object? obj)
	{
		return obj is ComponentTemplate t && t == this;
	}

	public override int GetHashCode()
	{
		if (ModID is null)
			return base.GetHashCode();
		return ModID.GetHashCode();
	}

	public static bool operator ==(ComponentTemplate? t1, ComponentTemplate? t2)
	{
		if (t1 is null && t2 is null)
			return true;
		if (t1 is null || t2 is null)
			return false;
		// treat component templates with the same mod ID as duplicates
		return t1.ModID == t2.ModID;
	}

	public static bool operator !=(ComponentTemplate t1, ComponentTemplate t2)
	{
		return !(t1 == t2);
	}
}