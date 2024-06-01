using FrEee.Objects.Abilities;
using FrEee.Modding.Templates;
using System;
using System.Collections.Generic;

namespace FrEee.Modding.Loaders;

/// <summary>
/// Loads stellar abilities from StellarAbilityTypes.txt.
/// </summary>
[Serializable]
public class StellarAbilityLoader : DataFileLoader
{
	public StellarAbilityLoader(string modPath)
		 : base(modPath, Filename, DataFile.Load(modPath, Filename))
	{
	}

	public const string Filename = "StellarAbilityTypes.txt";

	public override IEnumerable<IModObject> Load(Mod mod)
	{
		foreach (var rec in DataFile.Records)
		{
			var sabil = new RandomAbilityTemplate();
			sabil.TemplateParameters = rec.Parameters;
			string temp;
			int index = -1;

			sabil.ModID = rec.Get<string>("ID", sabil);
			rec.TryFindFieldValue("Name", out temp, ref index, Mod.Errors, 0, true);
			sabil.Name = temp;
			mod.StellarAbilityTemplates.Add(sabil);

			foreach (var abil in LoadRecord(rec, sabil))
				sabil.AbilityChances.Add(abil);

			yield return sabil;
		}
	}

	private static IEnumerable<AbilityChance> LoadRecord(Record rec, RandomAbilityTemplate rat)
	{
		int count = 0;
		int start = 0;
		while (true)
		{
			count++;
			var ac = new AbilityChance();
			ac.Ability = new Ability(null);
			string temp;

			if (!rec.TryFindFieldValue(new string[] { "Ability " + count + " Chance", "Ability Chance" }, out temp, ref start, null, start, true))
				yield break; // couldn't load next ability
			else
			{
				int chance;
				if (!int.TryParse(temp, out chance))
					Mod.Errors.Add(new DataParsingException("Ability Chance field value must be an integer.", Mod.CurrentFileName, rec, null));
				ac.Chance = chance;
			}
			start++;

			if (!rec.TryFindFieldValue(new string[] { "Ability " + count + " Type", "Ability Type" }, out temp, ref start, null, start))
				yield break; // couldn't load next ability
			else
				ac.Ability.Rule = Mod.Current.FindAbilityRule(temp);
			start++;

			if (!rec.TryFindFieldValue(new string[] { "Ability " + count + " Descr", "Ability Descr" }, out temp, ref start, null, start))
				ac.Ability.Description = ""; // no description for this ability
			else
				ac.Ability.Description = temp;
			start++;

			if (!rec.TryFindFieldValue(new string[] { "Ability " + count + " Val 1", "Ability " + count + " Val", "Ability Val 1", "Ability Val" }, out temp, ref start, null, start))
				continue; // leave default values
			else
				ac.Ability.Values.Add(temp);
			start++;

			if (!rec.TryFindFieldValue(new string[] { "Ability " + count + " Val 2", "Ability " + count + " Val", "Ability Val 2", "Ability Val" }, out temp, ref start, null, start))
				continue; // leave default value
			else
				ac.Ability.Values.Add(temp);
			start++;

			yield return ac;
		}
	}
}