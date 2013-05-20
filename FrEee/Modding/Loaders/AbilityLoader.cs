using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Objects.Abilities;

namespace FrEee.Modding.Loaders
{
	public static class AbilityLoader
	{
		/// <summary>
		/// Loads abilities from a record.
		/// </summary>
		/// <param name="rec"></param>
		public static IEnumerable<Ability> Load(Record rec)
		{
			int count = 0;
			int start = 0;
			while (true)
			{
				count++;
				var abil = new Ability();
				string temp;

				if (!rec.TryFindFieldValue(new string[] { "Ability " + count + " Type", "Ability Type" }, out temp, ref start, null, start, count == 1))
					yield break; // couldn't load next ability
				else
					abil.Name = temp;
				start++;

				if (!rec.TryFindFieldValue(new string[] { "Ability " + count + " Descr", "Ability Descr" }, out temp, ref start, null, start))
					abil.Description = ""; // no description for this ability
				else
					abil.Description = temp;
				start++;

				if (!rec.TryFindFieldValue(new string[] { "Ability " + count + " Val 1", "Ability " + count + " Val", "Ability Val 1", "Ability Val" }, out temp, ref start, null, start))
					continue; // leave default values
				else
					abil.Values.Add(temp);
				start++;

				if (!rec.TryFindFieldValue(new string[] { "Ability " + count + " Val 2", "Ability " + count + " Val", "Ability Val 2", "Ability Val" }, out temp, ref start, null, start))
					continue; // leave default value
				else
					abil.Values.Add(temp);
				start++;

				yield return abil;
			}
		}
	}
}
