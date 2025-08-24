using FrEee.Objects.Civilization;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FrEee.Objects.GameState;
using FrEee.Modding.Abilities;
using FrEee.Objects.Technology;
using FrEee.Processes.Construction;

namespace FrEee.Modding.Templates;

/// <summary>
/// A template for a facility.
/// </summary>
[Serializable]
public class FacilityTemplate : IModObject, IResearchable, IAbilityContainer, ITemplate<Facility>, IConstructionTemplate, IUpgradeable<FacilityTemplate>
{
	public FacilityTemplate()
	{
		Abilities = new List<Ability>();
		UnlockRequirements = new List<Requirement<Empire>>();
		Cost = new ResourceFormula(this);
	}

	/// <summary>
	/// Abilities possessed by this facility.
	/// </summary>
	public IList<Ability> Abilities { get; private set; }

	public AbilityTargets AbilityTarget
	{
		get { return AbilityTargets.Facility; }
	}

	public IEnumerable<IAbilityObject> Children
	{
		get { yield break; }
	}

	/// <summary>
	/// The cost to build the facility.
	/// </summary>
	public ResourceFormula Cost { get; set; }

	ResourceQuantity IConstructionTemplate.Cost
	{
		get
		{
			return Cost;
		}
	}

	/// <summary>
	/// A description of the facility.
	/// </summary>
	public Formula<string> Description { get; set; }

	/// <summary>
	/// The family that the facility belongs to. Used for "Only Latest" on the construction queue screen.
	/// </summary>
	public Formula<string> Family { get; set; }

	/// <summary>
	/// The group that the facility belongs to. Used for grouping on the construction queue screen.
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

	public bool IsObsolescent
	{
		get
		{
			return this != LatestVersion;
		}
	}

	/// <summary>
	/// Facility templates cannot be manually obsoleted; they are obsoleted automatically when new ones are unlocked.
	/// </summary>
	public bool IsObsolete
	{
		get { return IsObsolescent; }
	}

	/// <summary>
	/// The latest upgraded version of this component template.
	/// </summary>
	public FacilityTemplate LatestVersion
	{
		get
		{
			return NewerVersions.Where(t => Empire.Current.HasUnlocked(t)).LastOrDefault() ?? this;
		}
	}

	public string ModID
	{
		get;
		set;
	}

	/// <summary>
	/// The name of the facility.
	/// </summary>
	public Formula<string> Name { get; set; }

	string INamed.Name
	{
		get { return Name; }
	}

	public IEnumerable<FacilityTemplate> NewerVersions
	{
		get
		{
			return Mod.Current.FacilityTemplates.NewerVersions(this, t => t.Family);
		}
	}

	public IEnumerable<FacilityTemplate> OlderVersions
	{
		get
		{
			return Mod.Current.FacilityTemplates.OlderVersions(this, t => t.Family);
		}
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
	/// Name of the picture used to represent this facility, excluding the file extension.
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
				yield return Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Facilities", PictureName);
			yield return Path.Combine("Pictures", "Facilities", PictureName);
		}
	}

	/// <summary>
	/// Facilities must be built on a colony.
	/// </summary>
	public bool RequiresColonyQueue
	{
		get { return true; }
	}

	/// <summary>
	/// Facilities do not require a space yard.
	/// </summary>
	public bool RequiresSpaceYardQueue
	{
		get { return false; }
	}

	public string ResearchGroup
	{
		get { return "Facility"; }
	}

	/// <summary>
	/// The value of the Roman numeral that should be displayed on the facility's icon.
	/// </summary>
	public Formula<int> RomanNumeral { get; set; }

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
	/// The technology requirements for this facility.
	/// </summary>
	public IList<Requirement<Empire>> UnlockRequirements { get; private set; }
	public IEnumerable<IReferrable> Referrables => [];

	public static readonly FacilityTemplate Unknown = new FacilityTemplate { Name = "Unknown", ModID = "*UNKNOWN*" };

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
		Game.Current.UnassignID(this);
		if (Mod.Current != null)
			Mod.Current.FacilityTemplates.Remove(this);
	}

	public bool HasBeenUnlockedBy(Empire emp)
	{
		return emp.HasUnlocked(this);
	}

	/// <summary>
	/// Creates a facility from the template.
	/// </summary>
	/// <returns></returns>
	public Facility Instantiate()
	{
		return new Facility(this);
	}

	public override string ToString()
	{
		return Name;
	}
}