using FrEee.Objects.Technology;
using FrEee.Modding.Enumerations;
using FrEee.Modding.Interfaces;
using FrEee.Utility; using FrEee.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads facilities from Facility.txt.
	/// </summary>
	[Serializable]
	public class FacilityLoader : DataFileLoader
	{
		public FacilityLoader(string modPath)
			 : base(modPath, Filename, DataFile.Load(modPath, Filename))
		{
		}

		public const string Filename = "Facility.txt";

		public override IEnumerable<IModObject> Load(Mod mod)
		{
			foreach (var rec in DataFile.Records)
			{
				var f = new FacilityTemplate();
				f.TemplateParameters = rec.Parameters;
				mod.FacilityTemplates.Add(f);

				int index = -1;

				f.ModID = rec.Get<string>("ID", f);
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
					f.Cost[Resource.Find(costfield.Name.Substring("Cost ".Length))] = costfield.CreateFormula<int>(f);

				foreach (var tr in RequirementLoader.LoadEmpireRequirements(rec, f, RequirementType.Unlock))
					f.UnlockRequirements.Add(tr);

				// TODO - build and use requirements

				foreach (var abil in AbilityLoader.Load(Filename, rec, f).ToArray())
					f.Abilities.Add(abil);

				yield return f;
			}
		}
	}
}
