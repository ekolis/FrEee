using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Combat;
using FrEee.Modding.Templates;
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
	public class Component : IAbilityObject, INamed, IPictorial
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
		public void Attack(ICombatObject target, Battle battle)
		{
			if (!CanTarget(target))
				return;

			// TODO - check range too
			// TODO - take into account weapon mounts
			battle.LogShot(this);
			target.TakeDamage(Template.ComponentTemplate.WeaponInfo.DamageType, Template.ComponentTemplate.WeaponInfo.Damage[1], battle);
			if (target.MaxNormalShields < target.NormalShields)
				target.NormalShields = target.MaxNormalShields;
			if (target.MaxPhasedShields < target.PhasedShields)
				target.PhasedShields = target.MaxPhasedShields;
			if (target.IsDestroyed)
				battle.LogTargetDeath(target);
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
	}
}
