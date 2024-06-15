using FrEee.Extensions;
using System.Collections.Generic;

namespace FrEee.Modding;

/// <summary>
/// A type of damage that can be inflicted on a component, seeker, population, etc.
/// Most of these formulas, in addition to their stated parameters, can also take
/// "attacker", "defender", and "weapon" parameters, which should be fairly self explanatory.
/// </summary>
public class DamageType : IModObject
{
	public DamageType()
	{
		Name = "Normal";
		Description = "Default damage type.";
		NormalShieldDamage = 100;
		NormalShieldPiercing = 0;
		PhasedShieldDamage = 100;
		PhasedShieldPiercing = 0;
		ComponentDamage = 100;
		ComponentPiercing = 0;
		SeekerDamage = ComponentDamage;
		PopulationDamage = 100;
		ConditionsDamage = 0;
		FacilityDamage = 100;
		FacilityPiercing = 0;
		PlagueLevel = 0;
		TargetPush = 0;
		TargetTeleport = 0;
		IncreaseReload = 0;
		DisruptReload = 0;
		ShipCapture = 0;
		EmissiveArmor = 100;
		ShieldGenerationFromDamage = 100;
	}

	/// <summary>
	/// The default "Normal" damage type.
	/// </summary>
	public static DamageType Normal
	{
		get
		{
			return Mod.Current.DamageTypes.FindByName("Normal") ?? new DamageType();
		}
	}

	/// <summary>
	/// Percentage of nominal damage inflicted to components.
	/// This formula can take a "target" parameter which is the component being damaged.
	/// </summary>
	public Formula<int> ComponentDamage { get; set; }

	/// <summary>
	/// Percentage of damage that pierces a component, instead of being inflicted to the component.
	/// This formula can take a "target" parameter which is the component being damaged.
	/// </summary>
	public Formula<int> ComponentPiercing { get; set; }

	/// <summary>
	/// Percentage of nominal damage inflicted to planetary conditions.
	/// </summary>
	public Formula<int> ConditionsDamage { get; set; }

	/// <summary>
	/// A description of the effects of this damage type.
	/// </summary>
	public Formula<string> Description
	{
		get;
		set;
	}

	/// <summary>
	/// Percentage of nominal damage applied as "disrupt reload" effect.
	/// This disrupts the reload time of a ship's weapons up to their maximum reload time, and is effective even against ships with master computers.
	/// </summary>
	public Formula<int> DisruptReload { get; set; }

	/// <summary>
	/// Percentage effectiveness of the target's emissive armor against this damage type.
	/// </summary>
	public Formula<int> EmissiveArmor { get; set; }

	/// <summary>
	/// Percentage of nominal damage inflicted to facilities.
	/// This formula can take a "target" parameter which is the facility being damaged.
	/// </summary>
	public Formula<int> FacilityDamage { get; set; }

	/// <summary>
	/// Percentage of damage that pierces a facility, instead of being inflicted to the facility.
	/// This formula can take a "target" parameter which is the facility being damaged.
	/// </summary>
	public Formula<int> FacilityPiercing { get; set; }

	/// <summary>
	/// Percentage of nominal damage applied as "increase reload" effect.
	/// This increases the reload time of a ship's weapons with no limit. Ineffective against ships with master computers.
	/// </summary>
	public Formula<int> IncreaseReload { get; set; }

	public bool IsDisposed
	{
		// TODO - make it disposable?
		get { return false; }
	}

	/// <summary>
	/// A unique ID to reference this damage type in a mod.
	/// </summary>
	public string ModID
	{
		get;
		set;
	}

	/// <summary>
	/// The name of this damage type.
	/// </summary>
	public string Name
	{
		get;
		set;
	}

	/// <summary>
	/// Percentage of nominal damage inflicted to normal shields.
	/// </summary>
	public Formula<int> NormalShieldDamage { get; set; }

	/// <summary>
	/// Percentage of damage that pierces normal shields, instead of being inflicted to the shields.
	/// Actual shield piercing is the weighted average of this and the phased shield piercing, if the vehicle has both types of shields.
	/// </summary>
	public Formula<int> NormalShieldPiercing { get; set; }

	/// <summary>
	/// Percentage of nominal damage inflicted to phased shields.
	/// </summary>
	public Formula<int> PhasedShieldDamage { get; set; }

	/// <summary>
	/// Percentage of damage that pierces phased shields, instead of being inflicted to the shields.
	/// Actual shield piercing is the weighted average of this and the normal shield piercing, if the vehicle has both types of shields.
	/// </summary>
	public Formula<int> PhasedShieldPiercing { get; set; }

	/// <summary>
	/// Level of plague inflicted on a colony.
	/// This formula is NOT a percentage! It's a raw plague level number.
	/// This formula can take a "target" parameter which is the colony being plagued.
	/// </summary>
	public Formula<int> PlagueLevel { get; set; }

	/// <summary>
	/// Percentage of nominal damage inflicted to population.
	/// This formula can take a "target" parameter which is the race of the population being damaged.
	/// </summary>
	public Formula<int> PopulationDamage { get; set; }

	/// <summary>
	/// Percentage of nominal damage inflicted to seekers.
	/// Defaults to the component damage value.
	/// </summary>
	public Formula<int> SeekerDamage { get; set; }

	/// <summary>
	/// Percentage effectiveness of the target's "shield generation from damage" ability against this damage type.
	/// </summary>
	public Formula<int> ShieldGenerationFromDamage { get; set; }

	/// <summary>
	/// Percentage of nominal damage applied as a boarding attempt.
	/// If successful, the ship's security stations will be destroyed, but otherwise no damage is inflicted.
	/// </summary>
	public Formula<int> ShipCapture { get; set; }

	/// <summary>
	/// Percentage of nominal damage applied as acceleration away from the attacker, or toward him if the value is negative.
	/// </summary>
	public Formula<int> TargetPush { get; set; }

	/// <summary>
	/// Percentage of nominal damage applied as random teleportation within a specified radius. Does not affect velocity.
	/// </summary>
	public Formula<int> TargetTeleport { get; set; }

	/// <summary>
	/// Parameters from the mod meta templates.
	/// </summary>
	public IDictionary<string, object> TemplateParameters { get; set; }

	public override string ToString()
	{
		return Name;
	}
}