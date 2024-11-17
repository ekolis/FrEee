using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FrEee.Objects.GameState;
using FrEee.Processes.Combat;
using FrEee.Modding.Abilities;
using FrEee.Vehicles;

namespace FrEee.Objects.Technology;

/// <summary>
/// A component of a vehicle.
/// TODO - should Component implement IOwnable like Facility does?
/// </summary>
[Serializable]
public class Component : IAbilityObject, INamed, IPictorial, IDamageable, IContainable<IVehicle>, IFormulaHost, IUpgradeable<Component>
{
	public Component(IVehicle container, MountedComponentTemplate template)
	{
		Container = container;
		Template = template;
		Hitpoints = template.Durability;
	}

	public IEnumerable<Ability> Abilities
	{
		get
		{
			if (IsDestroyed)
				return Enumerable.Empty<Ability>();
			else
				return Template.Abilities;
		}
	}

	public AbilityTargets AbilityTarget
	{
		get { return AbilityTargets.Component; }
	}

	public int ArmorHitpoints
	{
		get
		{
			return this.HasAbility("Armor") ? Hitpoints : 0;
		}
	}

	public IEnumerable<IAbilityObject> Children
	{
		get
		{
			// The mounted component template might seem like a descendant, but it can't be,
			// because its abilities shouldn't be passed up when the component is destroyed.
			yield break;
		}
	}

	[DoNotSerialize]
	public IVehicle Container
	{
		get
		{
			if (container == null)
				container = Galaxy.Current.FindSpaceObjects<IVehicle>().SingleOrDefault(q => q.Components.Contains(this)).ReferViaGalaxy();
			return container?.Value;
		}
		set
		{
			container = value.ReferViaGalaxy();
		}
	}

	/// <summary>
	/// Component hit chances are normally determined by their maximum hitpoints.
	/// This is what makes leaky armor work.
	/// </summary>
	public int HitChance
	{
		// TODO - moddable hit chance
		get { return MaxHitpoints; }
	}

	/// <summary>
	/// The current hitpoints of this component.
	/// </summary>
	public int Hitpoints { get; set; }

	public int HullHitpoints
	{
		get
		{
			return this.HasAbility("Armor") ? 0 : Hitpoints;
		}
	}

	public Image Icon
	{
		get { return Template.Icon; }
	}

	public Image Icon32 => Icon.Resize(32);

	public IEnumerable<string> IconPaths
	{
		get
		{
			return Template.IconPaths;
		}
	}

	public IEnumerable<Ability> IntrinsicAbilities
	{
		get
		{
			// Need to treat the template's abilities as intrinsic because they can be switched on or off
			// based on the damage state of the component.
			return Abilities;
		}
	}

	/// <summary>
	/// Is this component out of commission?
	/// </summary>
	public bool IsDestroyed { get { return Hitpoints <= 0; } }

	public bool IsObsolescent
	{
		get { return Template.IsObsolescent; }
	}

	/// <summary>
	/// Is this component obsolete (can be upgraded to a newer component)?
	/// </summary>
	public bool IsObsolete
	{
		get
		{
			return Template.IsObsolete;
		}
	}

	public Component LatestVersion
	{
		get
		{
			if (IsObsolescent)
				return Template.LatestVersion.Instantiate();
			else
				return this;
		}
	}

	public int MaxArmorHitpoints
	{
		get
		{
			return this.HasAbility("Armor") ? MaxHitpoints : 0;
		}
	}

	public int MaxHitpoints
	{
		get { return Template.Durability; }
	}

	public int MaxHullHitpoints
	{
		get
		{
			return this.HasAbility("Armor") ? 0 : MaxHitpoints;
		}
	}

	public int MaxNormalShields
	{
		get { return 0; }
	}

	public int MaxPhasedShields
	{
		get { return 0; }
	}

	public int MaxShieldHitpoints
	{
		get { return MaxNormalShields + MaxPhasedShields; }
	}

	public string Name { get { return Template.Name; } }

	public IEnumerable<Component> NewerVersions
	{
		get { return Galaxy.Current.FindSpaceObjects<IVehicle>().SelectMany(v => v.Components).Where(c => Template.UpgradesTo(c.Template)); }
	}

	/// <summary>
	/// Components don't actually have shields; they just generate them for the vehicle.
	/// </summary>
	[DoNotSerialize(false)]
	public int NormalShields
	{
		get
		{
			return 0;
		}
		set
		{
			throw new NotSupportedException("Components don't actually have shields; they just generate them for the vehicle.");
		}
	}

	public IEnumerable<Component> OlderVersions
	{
		get { return Galaxy.Current.FindSpaceObjects<IVehicle>().SelectMany(v => v.Components).Where(c => c.Template.UpgradesTo(Template)); }
	}

	public Empire Owner
	{
		get { return Container == null ? null : Container.Owner; }
	}

	public IEnumerable<IAbilityObject> Parents
	{
		get
		{
			if (Container != null)
				yield return Container;
		}
	}

	/// <summary>
	/// Components don't actually have shields; they just generate them for the vehicle.
	/// </summary>
	[DoNotSerialize(false)]
	public int PhasedShields
	{
		get
		{
			return 0;
		}
		set
		{
			throw new NotSupportedException("Components don't actually have shields; they just generate them for the vehicle.");
		}
	}

	public System.Drawing.Image Portrait
	{
		get { return Template.Portrait; }
	}

	public IEnumerable<string> PortraitPaths
	{
		get
		{
			return Template.PortraitPaths;
		}
	}

	public int ShieldHitpoints
	{
		get { return NormalShields + PhasedShields; }
	}

	/// <summary>
	/// The template for this component.
	/// Specifies the basic stats of the component and its abilities.
	/// </summary>
	public MountedComponentTemplate Template { get; private set; }

	public IEnumerable<Ability> UnstackedAbilities
	{
		get { return Abilities; }
	}

	public IDictionary<string, object> Variables
	{
		get
		{
			return new Dictionary<string, object>
			{
				{"component", Template.ComponentTemplate},
				{"mount", Template.Mount},
				{"vehicle", Container},
				{"design", Container.Design},
				{"empire", Container.Owner}
			};
		}
	}

	[DoNotCopy]
	private GameReference<IVehicle> container { get; set; }

	/// <summary>
	/// If this is a weapon, returns true if this weapon can target an object at a particular range.
	/// If not a weapon, always returns false.
	/// </summary>
	/// <param name="target"></param>
	/// <returns></returns>
	public bool CanTarget(ITargetable target)
	{
		if (IsDestroyed)
			return false; // damaged weapons can't fire!
		if (Template.ComponentTemplate.WeaponInfo == null)
			return false; // not a weapon!
		return Template.ComponentTemplate.WeaponInfo.Targets.HasFlag(target.WeaponTargetType);
	}

	/*/// <summary>
	/// If this is a weapon, attempts to attack the target.
	/// If not a weapon, does nothing.
	/// </summary>
	/// <param name="target"></param>
	public void Attack(ICombatant defender, int range, Battle battle)
	{
		if (!CanTarget(defender))
			return;

		// TODO - check range too
		var tohit = Mod.Current.Settings.WeaponAccuracyPointBlank + Template.WeaponAccuracy + Container.Accuracy - defender.Evasion;
		// TODO - moddable min/max hit chances with per-weapon overrides
		if (tohit > 99)
			tohit = 99;
		if (tohit < 1)
			tohit = 1;
		var hit = RandomHelper.Range(0, 99) < tohit;
		battle.LogShot(this, hit);
		if (hit)
		{
			var shot = new Shot(this, defender, range);
			defender.TakeDamage(Template.ComponentTemplate.WeaponInfo.DamageType, shot.Damage, null);
			if (defender.MaxNormalShields < defender.NormalShields)
				defender.NormalShields = defender.MaxNormalShields;
			if (defender.MaxPhasedShields < defender.PhasedShields)
				defender.PhasedShields = defender.MaxPhasedShields;
			if (defender.IsDestroyed)
				battle.LogTargetDeath(defender);
		}
	}*/

	public void Dispose()
	{
		Hitpoints = 0;
	}

	public int? Repair(int? amount = null)
	{
		if (this.HasAbility("Component Destroyed On Use") || this.HasAbility("Space Object Destroyed On Use"))
		{
			// if component is destroyed on use, it can only be repaired at a friendly colony
			// SE4 said a friendly spaceyard, but that's still exploitable by using mobile SY ships
			if (!Container.Sector.SpaceObjects.OfType<Planet>().Any(p => p.Owner == Owner))
				return amount;
		}

		if (amount == null)
		{
			Hitpoints = MaxHitpoints;
			return amount;
		}
		else
		{
			var actual = Math.Min(MaxHitpoints - Hitpoints, amount.Value);
			Hitpoints += actual;
			return amount.Value - actual;
		}
	}

	public void ReplenishShields(int? amount = null)
	{
		// nothing to do
	}

	public int TakeDamage(Hit hit, PRNG dice = null)
	{
		int damage = hit.NominalDamage;
		var realhit = new Hit(hit.Shot, this, damage);
		var dt = realhit.Shot?.DamageType ?? DamageType.Normal;
		var df = dt.ComponentDamage.Evaluate(realhit);
		var dp = dt.ComponentPiercing.Evaluate(realhit);
		var factoredDmg = df.PercentOfRounded(damage);
		var piercing = dp.PercentOfRounded(damage);
		var realdmg = Math.Min(Hitpoints, factoredDmg);
		var nominalDamageSpent = realdmg / ((df + dp) / 100);
		Hitpoints -= realdmg;
		return damage - nominalDamageSpent;
	}

	public override string ToString()
	{
		return Name;
	}
}