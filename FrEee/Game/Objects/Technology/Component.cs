
using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Vehicles;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using FrEee.Modding.Templates;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Technology
{
	/// <summary>
	/// A component of a vehicle.
	/// TODO - should Component implement IOwnable like Facility does?
	/// </summary>
	[Serializable]
	public class Component : IAbilityObject, INamed, IPictorial, IDamageable, IContainable<IVehicle>, IFormulaHost, IReferrable
	{
		public Component(IVehicle container, MountedComponentTemplate template)
		{
			Container = container;
			Template = template;
			Hitpoints = template.Durability;
		}

		/// <summary>
		/// The template for this component.
		/// Specifies the basic stats of the component and its abilities.
		/// </summary>
		public MountedComponentTemplate Template { get; private set; }

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

		public IEnumerable<Ability> UnstackedAbilities
		{
			get { return Abilities; }
		}

		public string Name { get { return Template.Name; } }

		/// <summary>
		/// Is this component out of commission?
		/// </summary>
		public bool IsDestroyed { get { return Hitpoints <= 0; } }

		/// <summary>
		/// The current hitpoints of this component.
		/// </summary>
		public int Hitpoints { get; set; }

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

		public System.Drawing.Image Icon
		{
			get { return Template.Icon; }
		}

		public System.Drawing.Image Portrait
		{
			get { return Template.Portrait; }
		}

		public override string ToString()
		{
			return Name;
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

		public int MaxHitpoints
		{
			get { return Template.Durability; }
		}

		public int MaxNormalShields
		{
			get { return 0; }
		}

		public int MaxPhasedShields
		{
			get { return 0; }
		}

		public void ReplenishShields(int? amount = null)
		{
			// nothing to do
		}

		public int TakeDamage(Hit hit, PRNG dice = null)
		{
			int damage = hit.NominalDamage;
			var realhit = new Hit(hit.Shot, this, damage);
			var df = realhit.Shot.DamageType.ComponentDamage.Evaluate(realhit);
			var dp = realhit.Shot.DamageType.ComponentPiercing.Evaluate(realhit);
			var factoredDmg = df.PercentOfRounded(damage);
			var piercing = dp.PercentOfRounded(damage);
			var realdmg = Math.Min(Hitpoints, factoredDmg);
			var nominalDamageSpent = realdmg / ((df + dp) / 100);
			Hitpoints -= realdmg;
			return damage - nominalDamageSpent;
		}


		public int? Repair(int? amount = null)
		{
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

		/// <summary>
		/// Component hit chances are normally determined by their maximum hitpoints.
		/// This is what makes leaky armor work.
		/// </summary>
		public int HitChance
		{
			// TODO - moddable hit chance
			get { return MaxHitpoints; }
		}

		[DoNotCopy]
		private Reference<IVehicle> container { get; set; }

		[DoNotSerialize]
		public IVehicle Container
		{
			get
			{
				return container == null ? null : container.Value;
			}
			set
			{
				container = value.Reference();
			}
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

		public AbilityTargets AbilityTarget
		{
			get { return AbilityTargets.Component; }
		}


		public int ShieldHitpoints
		{
			get { return NormalShields + PhasedShields; }
		}

		public int ArmorHitpoints
		{
			get 
			{
				return this.HasAbility("Armor") ? Hitpoints : 0;
			}
		}

		public int HullHitpoints
		{
			get
			{
				return this.HasAbility("Armor") ? 0 : Hitpoints;
			}
		}

		public int MaxShieldHitpoints
		{
			get { return MaxNormalShields + MaxPhasedShields; }
		}

		public int MaxArmorHitpoints
		{
			get
			{
				return this.HasAbility("Armor") ? MaxHitpoints : 0;
			}
		}

		public int MaxHullHitpoints
		{
			get
			{
				return this.HasAbility("Armor") ? 0 : MaxHitpoints;
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

		public IEnumerable<IAbilityObject> Children
		{
			get
			{
				// The mounted component template might seem like a descendant, but it can't be,
				// because its abilities shouldn't be passed up when the component is destroyed.
				yield break;
			}
		}

		public IAbilityObject Parent
		{
			get { return Container; }
		}

		public long ID
		{
			get;
			set;
		}

		public bool IsDisposed
		{
			get;
			set;
		}

		public void Dispose()
		{
			Hitpoints = 0;
			IsDisposed = true;
		}

		public Empire Owner
		{
			get { return Container == null ? null : Container.Owner; }
		}
	}
}
