using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using FrEee.Ecs.Interactions;
using FrEee.Ecs.Stats;
using FrEee.Extensions;

namespace FrEee.Ecs.Abilities
{
	/// <summary>
	/// Extensions for managing stats.
	/// </summary>
	public static class StatExtensions
	{
		/// <summary>
		/// Determines if an entity has a particular stat.
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="statType"></param>
		/// <returns></returns>
		public static bool HasStat(this IEntity entity, StatType statType)
		{
			return entity.GetStatNames().Contains(statType.Name);
		}

		/// <summary>
		/// Gets the names of all stats belonging to an entity.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static IEnumerable<string> GetStatNames(this IEntity obj)
		{
			var interaction = new GetStatNamesInteraction(new HashSet<string>());
			obj.Interact(interaction);
			return interaction.StatNames;
		}

		/// <summary>
		/// Gets the value of a stat as a <see cref="decimal"/>,
		/// or null if the entity doesn't have that stat.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="statType"></param>
		/// <returns></returns>
		public static decimal? GetStatValue(this IEntity obj, StatType statType)
		{
			var interaction = new GetStatValueInteraction(new Stat(statType, []));
			obj.Interact(interaction);
			return interaction.Stat.Value;
		}

		/// <summary>
		/// Gets the value of a stat, as a particular numeric type.
		/// If the type is not nullable, defaults to zero.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <param name="statType"></param>
		/// <returns></returns>
		public static T GetStatValue<T>(this IEntity obj, StatType statType)
			where T : struct, INumber<T>
		{
			var value = obj.GetStatValue(statType);
			if (value is not null)
			{
				return value.Value.ConvertTo<T>();
			}
			else
			{
				return default;
			}
		}
	}
}
