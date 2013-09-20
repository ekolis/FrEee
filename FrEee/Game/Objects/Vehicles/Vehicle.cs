using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Vehicles
{
	/// <summary>
	/// A ship, base, or unit.
	/// </summary>
	[Serializable]
	public abstract class Vehicle : INamed, IConstructable, IVehicle, ICombatObject
	{
		public Vehicle()
		{
			Components = new List<Component>();
			ConstructionProgress = new ResourceQuantity();
		}

		/// <summary>
		/// The name of this vehicle.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The design of this vehicle.
		/// </summary>
		public IDesign Design { get; set; }

		/// <summary>
		/// The components on this vehicle.
		/// </summary>
		public IList<Component> Components { get; private set; }

		public bool RequiresColonyQueue
		{
			get { return false; }
		}

		public abstract bool RequiresSpaceYardQueue { get; }

		public ResourceQuantity Cost
		{
			get
			{
				return Design.Hull.Cost + Components.Select(c => c.Template.Cost).Aggregate((c1, c2) => c1 + c2);
			}
		}

		public ResourceQuantity ConstructionProgress
		{
			get;
			set;
		}

		[DoNotSerialize]
		public Image Icon
		{
			get { return Design.Hull.GetIcon(Design.Owner.ShipsetPath); }
		}

		[DoNotSerialize]
		public Image Portrait
		{
			get { return Design.Hull.GetPortrait(Design.Owner.ShipsetPath); }
		}

		public abstract void Place(ISpaceObject target);

		/// <summary>
		/// The owner of this vehicle.
		/// </summary>
		public Empire Owner { get; set; }

		public IEnumerable<Ability> Abilities
		{
			get
			{
				return UnstackedAbilities.Stack();
			}
		}

		public IEnumerable<Ability> UnstackedAbilities
		{
			get
			{
				return Design.Hull.Abilities.Concat(Components.Where(c => !c.IsDestroyed).SelectMany(c => c.Abilities)).Concat(Owner.Abilities);
			}
		}

		public int Speed
		{
			get
			{
				// no Engines Per Move rating? then no movement
				if (Design.Hull.Mass == 0)
					return 0;
				var thrust = this.GetAbilityValue("Standard Ship Movement").ToInt();
				// TODO - make sure that Movement Bonus and Extra Movement are not in fact affected by Engines Per Move in SE4
				return thrust / Design.Hull.Mass + this.GetAbilityValue("Movement Bonus").ToInt() + this.GetAbilityValue("Extra Movement Generation").ToInt();
			}
		}

		public override string ToString()
		{
			return Name;
		}

		public long ID
		{
			get;
			set;
		}

		public bool IsHostileTo(Empire emp)
		{
			// TODO - treaties making empires non-hostile
			return emp != null && Owner != null && emp != Owner;
		}

		/// <summary>
		/// The undamaged weapons installed on this vehicle.
		/// </summary>
		public IEnumerable<Component> Weapons
		{
			get
			{
				return Components.Where(c => !c.IsDestroyed && c.Template.ComponentTemplate.WeaponInfo != null);
			}
		}

		public int TakeDamage(DamageType damageType, int damage, Battle battle)
		{
			if (IsDestroyed)
				return damage; // she canna take any more!

			// TODO - worry about damage types, and make sure we have components that are not immune to the damage type so we don't get stuck in an infinite loop
			int shieldDmg = 0;
			if (NormalShields > 0)
			{
				var dmg = Math.Min(damage, NormalShields);
				NormalShields -= dmg;
				damage -= dmg;
				shieldDmg += dmg;
			}
			if (PhasedShields > 0)
			{
				var dmg = Math.Min(damage, PhasedShields);
				NormalShields -= dmg;
				damage -= dmg;
				shieldDmg += dmg;
			}
			if (shieldDmg > 0 && battle != null)
				battle.LogShieldDamage(this.CombatObject, shieldDmg);
			while (damage > 0 && !IsDestroyed)
			{
				var comps = Components.Where(c => c.Hitpoints > 0);
				var armor = comps.Where(c => c.HasAbility("Armor"));
				var internals = comps.Where(c => !c.HasAbility("Armor"));
				var canBeHit = armor.Any() ? armor : internals;
				var comp = comps.ToDictionary(c => c, c => c.HitChance).PickWeighted();
				damage = comp.TakeDamage(damageType, damage, battle);
			}

			if (IsDestroyed)
			{
				battle.LogTargetDeath(this);
				Dispose();
			}

			return damage;
		}

		/// <summary>
		/// Is this vehicle destroyed?
		/// Vehicles are destroyed when all components are destroyed.
		/// </summary>
		public bool IsDestroyed { get { return Components.All(c => c.IsDestroyed ); } }

		/// <summary>
		/// The current amount of shields.
		/// </summary>
		public int NormalShields { get; set; }

		/// <summary>
		/// The current amount of phased shields.
		/// </summary>
		public int PhasedShields { get; set; }

		/// <summary>
		/// Total shields.
		/// </summary>
		public int Shields { get { return NormalShields + PhasedShields; } }

		/// <summary>
		/// The maximum shields.
		/// </summary>
		public int MaxNormalShields
		{
			get
			{
				var comps = Components.Where(comp => !comp.IsDestroyed);
				return comps.GetAbilityValue("Shield Generation").ToInt() + comps.GetAbilityValue("Planet - Shield Generation").ToInt();
			}
		}

		/// <summary>
		/// The maximum phased shields.
		/// </summary>
		public int MaxPhasedShields
		{
			get
			{
				var comps = Components.Where(comp => !comp.IsDestroyed);
				return comps.GetAbilityValue("Phased Shield Generation").ToInt();
			}
		}

		public void ReplenishShields()
		{
			NormalShields = MaxNormalShields;
			PhasedShields = MaxPhasedShields;
		}

		/// <summary>
		/// The combat object associated with this vehicle.
		/// For autonomous vehicles, this would be the vehicle itself.
		/// For grouped vehicles, this would be the group.
		/// </summary>
		public abstract ICombatObject CombatObject {get; }

		public virtual void Dispose()
		{
			Galaxy.Current.UnassignID(this);
		}

		[DoNotSerialize]
		public int Hitpoints
		{
			get
			{
				return Components.Sum(c => c.Hitpoints);
			}
			set
			{
				throw new NotSupportedException("Cannot directly set the hitpoints of a vehicle. Its hitpoints are determined by its components.");
			}
		}

		public int MaxHitpoints
		{
			get { return Components.Sum(c => c.MaxHitpoints); }
		}

		/// <summary>
		/// Repairs a specified number of components.
		/// </summary>
		/// <param name="amount"></param>
		/// <returns></returns>
		public int Repair(int? amount = null)
		{
			if (amount == null)
			{
				foreach (var comp in Components)
					comp.Repair();
				return 0;
			}
			else
			{
				// repair most-damage components first
				// TODO - other repair priorities
				foreach (var comp in Components.OrderBy(c => (double)c.Hitpoints / (double)c.MaxHitpoints))
					amount = comp.Repair(amount);
				return amount.Value;
			}
		}


		public int HitChance
		{
			get { return 1; }
		}

		public abstract Visibility CheckVisibility(Empire emp);

		public bool CanTarget(ICombatObject target)
		{
			// TODO - alliances
			return target.Owner != Owner && Components.Any(c => !c.IsDestroyed && c.Template.ComponentTemplate.WeaponInfo != null && c.Template.ComponentTemplate.WeaponInfo.Targets.HasFlag(target.WeaponTargetType));
		}

		public abstract WeaponTargets WeaponTargetType { get; }

		public int Accuracy
		{
			get
			{
				return this.GetAbilityValue("Combat To Hit Offense Plus").ToInt() - this.GetAbilityValue("Combat To Hit Offense Minus").ToInt() + Owner.Culture.SpaceCombat;
			}
		}

		public int Evasion
		{
			get
			{
				return this.GetAbilityValue("Combat To Hit Defense Plus").ToInt() - this.GetAbilityValue("Combat To Hit Defense Minus").ToInt() + Owner.Culture.SpaceCombat;
			}
		}

	}
}
