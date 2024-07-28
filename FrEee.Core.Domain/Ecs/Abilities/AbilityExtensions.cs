using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Ecs.Abilities.Utility;
using FrEee.Ecs.Stats;
using FrEee.Extensions;
using FrEee.Modding;

namespace FrEee.Ecs.Abilities
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
            var a = new Ability(obj, rule, vals);
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

		/// <summary>
		/// Checks if an entity has any abilities of a particular type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity"></param>
		/// <returns></returns>
		public static bool HasAbility<T>(this IEntity entity)
			where T : IAbility
			=> entity.Abilities.OfType<T>().Any();

		/// <summary>
		/// Gets the single ability of a particular type belonging to an entity.
		/// Will throw if there is not exactly one ability of that type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity"></param>
		/// <returns></returns>
		public static T GetAbility<T>(this IEntity entity)
			where T : IAbility
			=> entity.GetAbilities<T>().Single();

		/// <summary>
		/// Gets the single ability of a particular type belonging to an entity.
		/// Will return null if there is no ability of that type
		/// and throw if there is more than one.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity"></param>
		/// <returns></returns>
		public static T? GetAbilityOrNull<T>(this IEntity entity)
			where T : IAbility
			=> entity.GetAbilities<T>().SingleOrDefault();

		/// <summary>
		/// Gets the abilities of a particular type belonging to an entity.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity"></param>
		/// <returns></returns>
		public static IEnumerable<T> GetAbilities<T>(this IEntity entity)
			where T : IAbility
			=> entity.Abilities.OfType<T>();
	}
}
