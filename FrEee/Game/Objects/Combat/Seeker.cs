using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Technology;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Combat
{
	/// <summary>
	/// A seeking missile or torpedo.
	/// </summary>
	public class Seeker : ICombatObject
	{
		public Seeker(Empire owner, Component launcher, ICombatObject target)
		{
			Owner = owner;
			if (launcher.Template.ComponentTemplate.WeaponInfo is SeekingWeaponInfo)
				Launcher = launcher;
			else
				throw new Exception(launcher + " cannot launch seekers.");
			Name = Owner.Name + " " + launcher.Name;
			if (WeaponInfo.Targets.HasFlag(target.WeaponTargetType))
				Target = target;
			else
				throw new Exception(launcher + " cannot target a " + target.WeaponTargetType + ".");
			Hitpoints = WeaponInfo.SeekerDurability; // TODO - can mounts affect seeker durability?
		}

		/// <summary>
		/// The name of the seeker.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// The owner of the seeker.
		/// </summary>
		public Empire Owner { get; private set; }

		/// <summary>
		/// The target of the seeker.
		/// </summary>
		public ICombatObject Target { get; private set; }

		/// <summary>
		/// The component which launched this seeker.
		/// </summary>
		public Component Launcher { get; private set; }

		public SeekingWeaponInfo WeaponInfo
		{
			get { return (SeekingWeaponInfo)Launcher.Template.ComponentTemplate.WeaponInfo; }
		}

		public int[] Damage
		{
			get
			{
				// TODO - let mounts affect damage
				return WeaponInfo.Damage;
			}
		}

		/// <summary>
		/// The remaining durability of this seeker.
		/// </summary>
		public int Hitpoints { get; set; }

		public bool CanTarget(ICombatObject target)
		{
			return Launcher.Template.ComponentTemplate.WeaponInfo.Targets.HasFlag(target.WeaponTargetType);
		}

		public WeaponTargets WeaponTargetType
		{
			get { return WeaponTargets.Seeker; }
		}

		/// <summary>
		/// Seekers do not carry other weapons.
		/// </summary>
		public IEnumerable<Component> Weapons
		{
			get { return Enumerable.Empty<Component>(); }
		}

		public void TakeDamage(DamageType damageType, int damage, Battle battle)
		{
			// TODO - take into account damage types
			Hitpoints -= damage;
			if (Hitpoints <= 0)
				battle.LogTargetDeath(this);
		}

		public bool IsDestroyed
		{
			get { return Hitpoints <= 0; }
		}

		/// <summary>
		/// Seekers do not have shields.
		/// TODO - maybe let seekers have shields?
		/// </summary>
		public int NormalShields
		{
			get
			{
				return 0;
			}
			set
			{
				// do nothing
			}
		}

		/// <summary>
		/// Seekers do not have shields.
		/// TODO - maybe let seekers have shields?
		/// </summary>
		public int PhasedShields
		{
			get
			{
				return 0;
			}
			set
			{
				// do nothing
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

		public void ReplenishShields()
		{
			// do nothing, seekers don't have shields
		}

		public System.Drawing.Image Icon
		{
			get { return Pictures.GetIcon(this); }
		}

		/// <summary>
		/// Just use the icon.
		/// </summary>
		public System.Drawing.Image Portrait
		{
			get { return Pictures.GetIcon(this); }
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
