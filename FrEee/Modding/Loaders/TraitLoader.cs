using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads race/empire traits from RacialTraits.txt.
	/// </summary>
	public class TraitLoader : ILoader
	{
		public void Load(DataFile df, Mod mod)
		{
			Trait t;
			int index = -1;
			foreach (var rec in df.Records)
			{
				t = new Trait();
				mod.Traits.Add(t);

				t.Name = rec.GetString("Name", ref index, true);
				t.Description = rec.GetString("Description", ref index);
				t.Cost = rec.GetInt("Cost", ref index, true);

				for (int count = 1; ; count++)
				{
					var f = rec.FindField(new string[]{"Trait Type", "Trait Type " + count}, ref index, false, index + 1);
					if (f == null)
						break;
					var abil = new Ability();
					abil.Name = f.Value;
					for (int vcount = 1; ; vcount++)
					{
						var vf = rec.FindField(new string[] { "Value", "Value " + count }, ref index, false, index + 1);
						if (vf == null)
							break;
						abil.Values.Add(vf.Value);
					}
					t.Abilities.Add(abil);
				}

				if (t.Abilities.Count == 0)
					Mod.Errors.Add(new DataParsingException("Trait \"" + t.Name + "\" does not have any abilities.", Mod.CurrentFileName, rec));

				t.IsRacial = rec.GetBool("Is Racial", ref index);
			}

			// second pass for required/restricted traits
			foreach (var rec in df.Records)
			{
				index = -1;

				t = mod.Traits.Single(t2 => t2.Name == rec.GetString("Name", ref index));

				for (int count = 1; ; count++)
				{
					var f = rec.FindField(new string[] { "Required Trait", "Required Trait " + count }, ref index, false, index + 1);
					if (f == null || f.Value == "None")
						break;
					var rt = mod.Traits.SingleOrDefault(t2 => t2.Name == f.Value);
					if (rt == null)
						Mod.Errors.Add(new DataParsingException("Required trait \"" + f.Value + "\" for trait \"" + t.Name + "\" does not exist in RacialTraits.txt.", Mod.CurrentFileName, rec));
					if (t.IsRacial && !rt.IsRacial)
						Mod.Errors.Add(new DataParsingException("Racial trait \"" + t.Name + "\" cannot require empire trait \"" + rt.Name + ". Racial traits can only require other racial traits.", Mod.CurrentFileName, rec));
					else if (!t.IsRacial && rt.IsRacial)
						Mod.Errors.Add(new DataParsingException("Empire trait \"" + t.Name + "\" cannot require racial trait \"" + rt.Name + ". Empire traits can only require other empire traits.", Mod.CurrentFileName, rec));
					t.RequiredTraits.Add(rt);
				}

				for (int count = 1; ; count++)
				{
					var f = rec.FindField(new string[] { "Restricted Trait", "Restricted Trait " + count }, ref index, false, index + 1);
					if (f == null || f.Value == "None")
						break;
					var rt = mod.Traits.SingleOrDefault(t2 => t2.Name == f.Value);
					if (rt == null)
						Mod.Errors.Add(new DataParsingException("Restricted trait \"" + f.Value + "\" for trait \"" + t.Name + "\" does not exist in RacialTraits.txt.", Mod.CurrentFileName, rec));
					if (t.IsRacial && !rt.IsRacial)
						Mod.Errors.Add(new DataParsingException("Racial trait \"" + t.Name + "\" cannot restrict empire trait \"" + rt.Name + ". Racial traits can only restrict other racial traits.", Mod.CurrentFileName, rec));
					else if (!t.IsRacial && rt.IsRacial)
						Mod.Errors.Add(new DataParsingException("Empire trait \"" + t.Name + "\" cannot restrict racial trait \"" + rt.Name + ". Empire traits can only restrict other empire traits.", Mod.CurrentFileName, rec));
					t.RestrictedTraits.Add(rt);
				}
			}
		}
	}
}
