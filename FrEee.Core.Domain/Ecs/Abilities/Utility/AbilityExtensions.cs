using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Ecs.Stats;
using FrEee.Extensions;
using FrEee.Modding;

namespace FrEee.Ecs.Abilities.Utility
{
    /// <summary>
    /// Extensions for managing abilities.
    /// </summary>
    public static class AbilityExtensions
    {
        /// <summary>
        /// Adds an abiity to an entity, by specifying the ability rule name.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="abilityName"></param>
        /// <param name="vals"></param>
        /// <returns></returns>
        public static Ability AddAbility(this IEntity obj, string abilityName, params object[] vals)
        {
            return obj.AddAbility(Mod.Current.AbilityRules.Single(r => r.Name == abilityName || r.Aliases.Contains(abilityName)), vals);
        }

        /// <summary>
        /// Adds an ability to an entity, by specifying the ability rule.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="rule"></param>
        /// <param name="vals"></param>
        /// <returns></returns>
        public static Ability AddAbility(this IEntity obj, AbilityRule rule, params object[] vals)
        {
            var a = new Ability(obj, rule, null, vals);
            obj.AddAbility(a);
            return a;
        }

        /// <summary>
        /// Adds an ability to an entity.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ability"></param>
        public static void AddAbility(this IEntity obj, Ability ability)
        {
            obj.Abilities = obj.Abilities.Append(ability).ToImmutableList();
        }
    }
}
