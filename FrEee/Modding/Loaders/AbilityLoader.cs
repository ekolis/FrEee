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
			int index = -1;
			while (true)
			{
				count++;

				var abilname = rec.GetNullString(new string[] { "Ability " + count + " Type", "Ability Type" }, ref index, index + 1, true);
				if (abilname == null)
					break; // no more abilities

				var abil = new Ability();
				abil.Name = abilname;
				abil.Description = rec.GetNullString(new string[] { "Ability " + count + " Descr", "Ability Descr" }, ref index, index + 1, false);

				int valnum = 0;
				while (true)
				{
					valnum++;

					var val = rec.GetNullString(new string[]
						{
							"Ability " + count + " Val " + valnum,
							"Ability " + count + " Val",
							"Ability Val " + valnum,
							"Ability Val"
						}, ref index, index + 1, false);
					if (val == null)
						break;
					abil.Values.Add(val);
				}

				for (int i = 1; i < valnum; i++)
				{
					// replace [%Amount1%] and such
					abil.Description.Replace("[%Amount" + i + "%]", abil.Values[i]);
				}

				yield return abil;
			}
		}

		/// <summary>
		/// Loads ability percentages or modifiers.
		/// </summary>
		/// <param name="rec"></param>
		/// <param name="what"></param>
		/// <returns></returns>
		public static IDictionary<string, IDictionary<int, int>> LoadPercentagesOrModifiers(Record rec, string what)
		{
			var dict = new Dictionary<string, IDictionary<int,int>>();
			int count = 0;
			int start = 0;
			while (true)
			{
				count++;
				string abilName;
				var vals = new Dictionary<int, int>();

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

					vals.Add(vcount, valField.IntValue(rec));
				}

				dict.Add(abilName, vals);
			}
			return dict;
		}
	}
}
