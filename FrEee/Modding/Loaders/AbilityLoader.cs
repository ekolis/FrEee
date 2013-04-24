using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

				if (!rec.TryFindFieldValue("Ability " + count + " Type", out temp, ref start, null, start))
				{
					if (!rec.TryFindFieldValue("Ability Type", out temp, ref start, null, start))
						yield break; // couldn't load next ability
					else
						abil.Name = temp;
				}
				else
					abil.Name = temp;
				start++;
				if (!rec.TryFindFieldValue("Ability " + count + " Descr", out temp, ref start, null, start))
				{
					if (!rec.TryFindFieldValue("Ability Descr", out temp, ref start, null, start))
						abil.Description = ""; // no description for this ability
					else
						abil.Description = temp;
				}
				else
					abil.Description = temp;
				start++;
				if (!rec.TryFindFieldValue("Ability " + count + " Val 1", out temp, ref start, null, start))
				{
					if (!rec.TryFindFieldValue("Ability Val", out temp, ref start, null, start))
						continue; // leave default values
					else
						abil.Values.Add(temp);
				}
				else
					abil.Values.Add(temp);
				start++;
				if (!rec.TryFindFieldValue("Ability " + count + " Val 2", out temp, ref start, null, start))
				{
					if (!rec.TryFindFieldValue("Ability Val", out temp, ref start, null, start))
						continue; // leave default values
					else
						abil.Values.Add(temp);
				}
				else
					abil.Values.Add(temp);
				start++;

				yield return abil;
			}
		}
	}
}
