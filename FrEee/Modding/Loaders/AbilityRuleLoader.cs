using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Enumerations;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads ability rules from AbilityRules.txt.
	/// </summary>
	 [Serializable] public class AbilityRuleLoader : DataFileLoader
	{
		 public const string Filename = "AbilityRules.txt";

		 public AbilityRuleLoader(string modPath)
			 : base(modPath, Filename, DataFile.Load(modPath, Filename))
		 {
		 }

		public override void Load(Mod mod)
		{
			foreach (var rec in DataFile.Records)
			{
				var r = new AbilityRule();
				mod.AbilityRules.Add(r);

				int index = -1;

				r.Name = rec.Get<string>("Name", null, false);
				r.Aliases = rec.GetMany<string>("Alias", null).Select(f => f.Value).ToList();
				r.Targets = rec.Get<AbilityTargets>("Targets", null) ?? AbilityTargets.All;
				r.Description = rec.Get<string>("Description", null);
				for (int i = 1; i <= 2; i++)
				{
					var f = rec.FindField("Value " + i + " Rule", ref index, false, 0, true);
					if (f == null)
						r.ValueRules.Add(AbilityValueRule.None);
					else
						r.ValueRules.Add(f.CreateFormula<AbilityValueRule>(r));
					f = rec.FindField("Value " + i + " Group Rule", ref index, false, 0, true);
					if (f == null)
						r.GroupRules.Add(AbilityValueRule.None);
					else
						r.GroupRules.Add(f.CreateFormula<AbilityValueRule>(r));
				}
				int j = 3;
				while (true)
				{
					var f = rec.FindField("Value " + j + " Rule", ref index, false, 0, true);
					if (f == null)
						break;
					else
						r.ValueRules.Add(f.CreateFormula<AbilityValueRule>(r));
					f = rec.FindField("Value " + j + " Group Rule", ref index, false, 0, true);
					if (f == null)
						r.GroupRules.Add(AbilityValueRule.None);
					else
						r.GroupRules.Add(f.CreateFormula<AbilityValueRule>(r));
				}
			}
		}
	}
}
