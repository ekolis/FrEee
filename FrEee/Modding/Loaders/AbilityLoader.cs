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

				// TODO - abilities with more or less than 2 values?

				yield return abil;
			}
		}

		/// <summary>
		/// Loads ability percentages or modifiers.
		/// </summary>
		/// <param name="rec"></param>
		/// <param name="what"></param>
		/// <returns></returns>
		public static IDictionary<string, IEnumerable<int>> LoadPercentagesOrModifiers(Record rec, string what)
		{
			var dict = new Dictionary<string, IEnumerable<int>>();
			int count = 0;
			int start = 0;
			while (true)
			{
				count++;
				string abilName;
				var vals = new List<int>();

				var nameField = rec.FindField(new string[] { "Ability " + count + " " + what + " Type", "Ability " + what + " Type" }, ref start, false, start, true);
				if (nameField == null)
					break; // no more abilities

				abilName = nameField.Value;

				dict.Add(abilName, vals);

				int vcount = 0;
				while (true)
				{
					vcount++;
					var valField = rec.FindField(new string[]
						{
							"Ability " + count + " " + what + " " + vcount,
							"Ability " + count + " " + what,
							"Ability " + what + " " + vcount,
							"Ability " + what
						}, ref start, false, start, true);

					if (valField == null)
						break; // no more values

					vals.Add(valField.IntValue(rec));
				}

				dict.Add(abilName, vals);
			}
			return dict;
		}
	}
}
