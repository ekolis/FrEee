using FrEee.Ecs;
using FrEee.Ecs.Abilities;
using FrEee.Extensions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace FrEee.Modding.Loaders;

public static class AbilityLoader
{
	/// <summary>
	/// Loads abilities from a record.
	/// </summary>
	/// <param name="rec"></param>
	public static IEnumerable<Ability> Load(string filename, Record rec, IEntity obj)
	{
		int count = 0;
		int index = -1;
		while (true)
		{
			count++;

			Ability abil = new Ability(obj);

			var nfield = rec.FindField(new string[]
					{
						"Ability " + count + " Type",
						"Ability Type"
					}, ref index, false, index + 1);
			if (nfield == null)
				break; // no more abilities
			var abilname = nfield.CreateFormula<string>(abil).Value;

			lock (Mod.Current.AbilityRules)
			{
				var rules = Mod.Current.AbilityRules.Where(r => r.Matches(abilname)).ToArray();
				if (rules.Count() > 1)
				{
					Mod.Errors.Add(new DataParsingException("Ambiguous ability name match for " + abilname + " alias between the following abilities: " + string.Join(", ", rules.Select(r => r.Name).ToArray()) + ".", filename, rec));
					continue;
				}
				else if (rules.Count() == 0)
				{
					// create an ad hoc ability rule
					abil.Rule = new AbilityRule { Name = abilname };
					Mod.Current.AbilityRules.Add(abil.Rule);
				}
				else
					abil.Rule = rules.Single();

				abil.Description = rec.Get<string>(new string[] { "Ability " + count + " Descr", "Ability Descr" }, obj);

				int valnum = 0;
				int idx = -1;
				while (true)
				{
					valnum++;

					var vfield = rec.FindField(new string[]
						{
						"Ability " + count + " Val " + valnum,
						"Ability " + count + " Val",
						"Ability Val " + valnum,
						"Ability Val"
						}, ref idx, false, idx + 1);
					if (vfield == null)
						break;
					var val = vfield.CreateFormula<string>(abil);
					abil.Values.Add(val);
				}
			}

			if (abil.Rule.AbilityType != typeof(Ability))
			{
				// it's an ECS ability, create that type instead
				abil = AbilityFactory.Build(abil.Rule.AbilityType, obj, abil.Rule, abil.Description, abil.Values.ToArray());
			}

			yield return abil;
		}
	}

	/// <summary>
	/// Loads ability percentages or modifiers.
	/// </summary>
	/// <param name="rec"></param>
	/// <param name="what"></param>
	/// <param name="obj">Formula context.</param>
	/// <returns></returns>
	public static IDictionary<AbilityRule, IDictionary<int, Formula<int>>> LoadPercentagesOrModifiers(Record rec, string what, object obj)
	{
		var dict = new Dictionary<AbilityRule, IDictionary<int, Formula<int>>>();
		int count = 0;
		int start = -1;
		while (true)
		{
			count++;
			AbilityRule abilRule;
			var vals = new Dictionary<int, Formula<int>>();

			var nameField = rec.FindField(new string[] { "Ability " + count + " " + what + " Type", "Ability " + what + " Type" }, ref start, false, start + 1, true);
			if (nameField == null)
				break; // no more abilities

			abilRule = Mod.Current.FindAbilityRule(nameField.Value);

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
					}, ref start, false, start + 1);

				if (valField == null)
					break; // no more values

				vals.Add(vcount, valField.CreateFormula<int>(obj));
			}

			dict.Add(abilRule, vals);
		}
		return dict;
	}
}