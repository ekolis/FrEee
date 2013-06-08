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
	public abstract class Vehicle : INamed, IConstructable, IVehicle
	{
		public Vehicle()
		{
			Components = new List<Component>();
			ConstructionProgress = new Resources();
			if (Galaxy.Current != null)
				Galaxy.Current.Register(this);
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

		public Resources Cost
		{
			get
			{
				return Design.Hull.Cost + Components.Select(c => c.Template.Cost).Aggregate((c1, c2) => c1 + c2);
			}
		}

		public Resources ConstructionProgress
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
				return Design.Hull.Abilities.Concat(Components.Where(c => !c.IsDestroyed).SelectMany(c => c.Abilities).Stack());
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

		public int ID
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

		public void TakeDamage(DamageType damageType, int damage, Battle battle)
		{
			if (IsDestroyed)
				return; // she canna take any more!

			// TODO - worry about damage types
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
			if (shieldDmg > 0)
				battle.LogShieldDamage(this.CombatObject, shieldDmg);
			var comps = Components.Where(c => c.Hitpoints > 0);
			var comp = Components.PickRandom();
			if (comp.Hitpoints < damage)
			{
				var actualDamage = comp.Hitpoints;
				var leftover = damage - comp.Hitpoints;
				comp.Hitpoints = 0;
				battle.LogComponentDamage(comp, actualDamage);
				TakeDamage(damageType, leftover, battle);
			}
			else
			{
				comp.Hitpoints -= damage;
				battle.LogComponentDamage(comp, damage);
			}
		}

		/// <summary>
		/// Is this vehicle destroyed?
		/// Vehicles are destroyed when all components are destroyed.
		/// </summary>
		public bool IsDestroyed { get { return Components.All(c => c.Hitpoints <= 0); } }

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
	}
}
