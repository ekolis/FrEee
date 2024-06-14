using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Technology;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Combat2
{
    /// <summary>
    /// A weapon's fire.
    /// </summary>
    public class CombatShot : IFormulaHost
    {
        public CombatShot(Component weapon, ITargetable target, int range)
        {
            Weapon = weapon;
            Target = target;
            Range = range;
        }

        public GalaxyReference<Component> weapon { get; set; }

        [DoNotSerialize]
        public Component Weapon { get { return weapon; } set { weapon = value; } }

        public GalaxyReference<ITargetable> target { get; set; }

        [DoNotSerialize]
        public ITargetable Target { get { return target == null ? null : target.Value; } set { target = value == null ? null : value.ReferViaGalaxy(); } }

        public int Range { get; set; }

        public int Damage
        {
            get
            {
                if (Range < Weapon.Template.WeaponMinRange || Range > Weapon.Template.WeaponMaxRange)
                    return 0;
				return Weapon.Template.GetWeaponDamage(this);
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