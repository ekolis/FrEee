using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Objects.Technology;
using FrEee.Utility;
using FrEee.Modding.Enumerations;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads facilities from Facility.txt.
	/// </summary>
	 [Serializable] public class FacilityLoader : DataFileLoader
	{
		 public const string Filename = "Facility.txt";

		 public FacilityLoader(string modPath)
			 : base(modPath, Filename, DataFile.Load(modPath, Filename))
		{
		}

		public override void Load(Mod mod)
		{
			foreach (var rec in DataFile.Records)
			{
				var f = new FacilityTemplate();
				mod.FacilityTemplates.Add(f);

				int index = -1;

				f.Name = rec.Get<string>("Name", f);
				f.Description = rec.Get<string>("Description", f);
				f.Group = rec.Get<string>("Facility Group", f);
				f.Family = rec.Get<string>("Facility Family", f);
				f.RomanNumeral = rec.Get<int>("Roman Numeral", f);
				var picfield = rec.FindField("Pic", ref index, false, 0, true);
				if (picfield != null)
					f.PictureName = picfield.Value;
				else
					f.PictureName = "Facil_" + rec.Get<int>("Pic Num", f).Value.ToString("000"); // for compatibility with SE4

				foreach (var costfield in rec.Fields.Where(cf => cf.Name.StartsWith("Cost ")))
					f.Cost[Resource.Find(costfield.Name.Substring("Cost ".Length))] = costfield.IntValue(rec);

				foreach (var tr in RequirementLoader.LoadEmpireRequirements(rec, f, RequirementType.Unlock))
					f.UnlockRequirements.Add(tr);

				// TODO - build and use requirements

				foreach (var abil in AbilityLoader.Load(rec, f))
					f.Abilities.Add(abil);
			}
		}
	}
}
