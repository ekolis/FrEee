using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Technology;
using FrEee.Modding.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Combat
{
	/// <summary>
	/// A weapon's fire.
	/// </summary>
	public class Shot : IFormulaHost
	{
		public Shot(Component weapon, ICombatObject target, int range)
		{
			Weapon = weapon;
			Target = target;
			Range = range;
		}

		public Component Weapon { get; set; }

		public ICombatObject Target { get; set; }

		public int Range { get; set; }

		public int Damage
		{
			get
			{
				if (Range < Weapon.Template.WeaponMinRange || Range > Weapon.Template.WeaponMaxRange)
					return 0;
				return Weapon.Template.WeaponDamage.Evaluate(this);
			}
		}

		public IDictionary<IDamageable, int> DamageInflicted { get; private set; }

		public IDictionary<string, object> Variables
		{
			get
			{
				return new Dictionary<string, object>
				{
					{ "weapon", Weapon}, 
					{ "target", Target}, 
					{ "range", Range}, 
				};
			}
		}
	}
}