using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
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
	/// </summary>
	[Serializable]
	public class Component : IAbilityObject, INamed, IPictorial, IDamageable, IFoggable, IContainable<IVehicle>
	{
		public Component(MountedComponentTemplate template)
		{
			this.Template = template;
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
		public bool CanTarget(ICombatObject target)
		{
			if (IsDestroyed)
				return false; // damaged weapons can't fire!
			if (Template.ComponentTemplate.WeaponInfo == null)
				return false; // not a weapon!
			return Template.ComponentTemplate.WeaponInfo.Targets.HasFlag(target.WeaponTargetType);
		}

		/// <summary>
		/// If this is a weapon, attempts to attack the target.
		/// If not a weapon, does nothing.
		/// </summary>
		/// <param name="target"></param>
		public void Attack(ICombatObject attacker, ICombatObject defender, Battle battle)
		{
			if (!CanTarget(defender))
				return;

			// TODO - check range too
			var tohit = Mod.Current.Settings.WeaponAccuracyPointBlank + Template.WeaponAccuracy + attacker.Accuracy - defender.Evasion;
			// TODO - moddable min/max hit chances with per-weapon overrides
			if (tohit > 99)
				tohit = 99;
			if (tohit < 1)
				tohit = 1;
			var hit = RandomHelper.Range(0, 99) < tohit;
			battle.LogShot(this, hit);
			if (hit)
			{
				defender.TakeDamage(Template.ComponentTemplate.WeaponInfo.DamageType, Template.WeaponDamage[1], battle);
				if (defender.MaxNormalShields < defender.NormalShields)
					defender.NormalShields = defender.MaxNormalShields;
				if (defender.MaxPhasedShields < defender.PhasedShields)
					defender.PhasedShields = defender.MaxPhasedShields;
				if (defender.IsDestroyed)
					battle.LogTargetDeath(defender);
			}
		}

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
		[DoNotSerialize]
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
		[DoNotSerialize]
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

		public void ReplenishShields()
		{
			// nothing to do
		}

		public int TakeDamage(DamageType dmgType, int damage, Battle battle)
		{
			// TODO - take into account damage types
			int realDamage;
			realDamage = Math.Min(Hitpoints, damage);
			Hitpoints -= realDamage;
			if (battle != null)
				battle.LogComponentDamage(this, realDamage);
			return damage - realDamage;
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

		public Visibility CheckVisibility(Empire emp)
		{
			return Container.CheckVisibility(emp);
		}

		public void Redact(Empire emp)
		{
			var vis = CheckVisibility(emp);
			// TODO - show components without damage info on vehicles whose design is known
			if (vis < Visibility.Scanned)
				Dispose();
		}

		public bool IsMemory
		{
			get;
			set;
		}

		public bool IsKnownToBeDestroyed
		{
			get;
			set;
		}

		public long ID
		{
			get;
			set;
		}

		public void Dispose()
		{
			IsKnownToBeDestroyed = true;
			Container.Components.Remove(this);
			Galaxy.Current.UnassignID(this);
		}

		public Civilization.Empire Owner
		{
			get { throw new NotImplementedException(); }
		}

		public IVehicle Container
		{
			get { throw new NotImplementedException(); }
		}

		public bool IsVisibleTo(Empire emp)
		{
			return CheckVisibility(emp) >= Visibility.Visible;
		}
	}
}
