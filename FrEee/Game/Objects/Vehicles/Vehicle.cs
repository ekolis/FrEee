using AutoMapper;
using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
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
	public abstract class Vehicle : INamed, IConstructable, IVehicle, ICombatant, IFoggable
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
		/// Needs to be protected, not private, so reflection can find it from a derived class.
		/// Can change CommonExtensions.CopyEnumerableProperties to scan base classes, though...
		/// </summary>
		public IList<Component> Components { get; protected set; }

		public bool RequiresColonyQueue
		{
			get { return false; }
		}

		public abstract bool RequiresSpaceYardQueue { get; }

		public ResourceQuantity Cost
		{
			get
			{
				if (!Components.Any())
					return new ResourceQuantity();
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
			get { return Design.Icon; }
		}

		[DoNotSerialize]
		public Image Portrait
		{
			get { return Design.Portrait; }
		}

		public abstract void Place(ISpaceObject target);

		/// <summary>
		/// The owner of this vehicle.
		/// </summary>
		public Empire Owner { get; set; }

		public IEnumerable<Ability> IntrinsicAbilities
		{
			get { yield break; }
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

		public int TakeDamage(DamageType damageType, int damage, PRNG dice = null)
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

			// emissive armor negates a certain amount of damage that penetrates the shields
			// TODO - emissive should be ineffective vs. armor piercing damage
			var emissive = this.GetAbilityValue("Emissive Armor").ToInt();
			damage -= emissive;

			while (damage > 0 && !IsDestroyed)
			{
				// save off damage counter for shield generation from damage ability
				var sgfdStart = damage;
				var sgfdAbility = this.GetAbilityValue("Shield Generation From Damage").ToInt();

				var comps = Components.Where(c => c.Hitpoints > 0);
				var armor = comps.Where(c => c.HasAbility("Armor"));
				var internals = comps.Where(c => !c.HasAbility("Armor"));
				var canBeHit = armor.Any() ? armor : internals;
				var comp = canBeHit.ToDictionary(c => c, c => c.HitChance).PickWeighted(dice);
				damage = comp.TakeDamage(damageType, damage, dice);
				
				// shield generation from damage
				var sgfd = Math.Min(sgfdStart - damage, sgfdAbility);
				ReplenishShields(sgfd);
			}


			if (IsDestroyed)
				Dispose();

			// update memory sight
			this.UpdateEmpireMemories();

			return damage;
		}

		/// <summary>
		/// Is this vehicle destroyed?
		/// Vehicles are destroyed when all components are destroyed.
		/// </summary>
		public bool IsDestroyed { get { return Components.All(c => c.IsDestroyed); } }

		/// <summary>
		/// The current amount of shields.
		/// </summary>
		public int NormalShields { get; set; }

		/// <summary>
		/// The current amount of phased shields.
		/// </summary>
		public int PhasedShields { get; set; }

		/// <summary>
		/// Total current shield HP.
		/// </summary>
		public int ShieldHitpoints { get { return NormalShields + PhasedShields; } }

		/// <summary>
		/// Current HP of all armor components.
		/// </summary>
		public int ArmorHitpoints
		{
			get
			{
				return Components.Sum(c => c.ArmorHitpoints);
			}
		}

		public Progress ArmorHitpointsFill
		{
			get { return new Progress(ArmorHitpoints, MaxArmorHitpoints); }
		}

		/// <summary>
		/// Current HP of all non-armor components.
		/// </summary>
		public int HullHitpoints
		{
			get
			{
				return Components.Sum(c => c.HullHitpoints);
			}
		}

		public Progress HullHitpointsFill
		{
			get { return new Progress(HullHitpoints, MaxHullHitpoints); }
		}

		public Progress ShieldHitpointsFill
		{
			get { return new Progress(ShieldHitpoints, MaxShieldHitpoints); }
		}

		/// <summary>
		/// The maximum shields.
		/// </summary>
		public int MaxNormalShields
		{
			get
			{
				var comps = Components.Where(comp => !comp.IsDestroyed);
				return comps.GetAbilityValue("Shield Generation", this).ToInt() + comps.GetAbilityValue("Planet - Shield Generation", this).ToInt();
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
				return comps.GetAbilityValue("Phased Shield Generation", this).ToInt();
			}
		}

		public void ReplenishShields(int? amount = null)
		{
			if (amount == null)
			{
				NormalShields = MaxNormalShields;
				PhasedShields = MaxPhasedShields;
			}
			else
			{
				PhasedShields += amount.Value;
				if (PhasedShields > MaxPhasedShields)
				{
					var overflow = PhasedShields - MaxPhasedShields;
					PhasedShields = MaxPhasedShields;
					NormalShields += overflow;
					if (NormalShields > MaxNormalShields)
						NormalShields = MaxNormalShields;
				}
			}
		}

		public virtual void Dispose()
		{
			if (IsDisposed)
				return;
			Galaxy.Current.UnassignID(this);
			this.UpdateEmpireMemories();
		}

		[DoNotSerialize]
		[IgnoreMap]
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
		public int? Repair(int? amount = null)
		{
			if (amount == null)
			{
				foreach (var comp in Components)
					comp.Repair();
			}
			else
			{
				// repair most-damage components first
				// TODO - other repair priorities
				foreach (var comp in Components.OrderBy(c => (double)c.Hitpoints / (double)c.MaxHitpoints))
					amount = comp.Repair(amount);
			}
			return amount;
		}


		public int HitChance
		{
			get { return 1; }
		}

		public abstract Visibility CheckVisibility(Empire emp);

		public bool CanTarget(ITargetable target)
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

		/// <summary>
		/// Does this vehicle participate in ground combat?
		/// </summary>
		public abstract bool ParticipatesInGroundCombat
		{
			get;
		}

		public abstract void Redact(Empire emp);

		public bool IsMemory
		{
			get;
			set;
		}

		public double Timestamp { get; set; }

		public abstract bool IsObsoleteMemory(Empire emp);

		public abstract AbilityTargets AbilityTarget { get; }

		public abstract Sector Sector { get; set; }

		public abstract StarSystem StarSystem { get; }

		/// <summary>
		/// Resource cost per turn to maintain this vehicle.
		/// </summary>
		[IgnoreMap]
		public ResourceQuantity MaintenanceCost
		{
			get
			{
				double pct;
				if (Design.Hull.VehicleType == VehicleTypes.Ship || Design.Hull.VehicleType == VehicleTypes.Base)
					pct = Mod.Current.Settings.ShipBaseMaintenanceRate;
				else
					pct = Mod.Current.Settings.UnitMaintenanceRate;

				if (pct > 0)
				{
					pct += this.GetAbilityValue("Modified Maintenance Cost").ToInt();
					if (Sector != null)
						pct -= this.Sector.GetAbilityValue(Owner, "Reduced Maintenance Cost - Sector").ToInt();
					if (StarSystem != null)
						pct -= this.StarSystem.GetAbilityValue(Owner, "Reduced Maintenance Cost - System").ToInt();
					if (Owner != null)
					{
						pct -= this.Owner.GetAbilityValue("Reduced Maintenance Cost - Empire").ToInt();
						pct -= Owner.Culture.MaintenanceReduction;
						if (Owner.PrimaryRace.Aptitudes.ContainsKey(Aptitude.Maintenance.Name))
							pct -= Owner.PrimaryRace.Aptitudes[Aptitude.Maintenance.Name] - 100;
					}
					return Cost * pct / 100d;
				}
				else
					return new ResourceQuantity();
			}
		}

		public int MineralsMaintenance
		{
			get { return MaintenanceCost[Resource.Minerals]; }
		}

		public int OrganicsMaintenance
		{
			get { return MaintenanceCost[Resource.Organics]; }
		}

		public int RadioactivesMaintenance
		{
			get { return MaintenanceCost[Resource.Radioactives]; }
		}

		public int MaxShieldHitpoints
		{
			get { return MaxNormalShields + MaxPhasedShields; }
		}

		public int MaxArmorHitpoints
		{
			get { return Components.Sum(c => c.MaxArmorHitpoints); }
		}

		public int MaxHullHitpoints
		{
			get { return Components.Sum(c => c.MaxHullHitpoints); }
		}


		public IEnumerable<IAbilityObject> Children
		{
			get { return Components.Cast<IAbilityObject>().Append(Design.Hull); }
		}

		public virtual IAbilityObject Parent
		{
			get { return Owner ; }
		}

		public IConstructionTemplate Template
		{
			get { return Design; }
		}

		public bool IsDisposed { get; set; }

		public IHull Hull { get { return Design.Hull; } }

		public bool IsOurs { get { return Owner == Empire.Current; } }

		public int? Size
		{
			get { return Design.Hull.Size; }
		}

	}
}
