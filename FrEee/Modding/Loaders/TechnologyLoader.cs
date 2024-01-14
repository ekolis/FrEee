using FrEee.Objects.Technology;
using FrEee.Modding.Enumerations;
using FrEee.Modding.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Modding.Loaders;

/// <summary>
/// Loads technologies from TechArea.txt.
/// </summary>
[Serializable]
public class TechnologyLoader : DataFileLoader
{
	public TechnologyLoader(string modPath)
		 : base(modPath, Filename, DataFile.Load(modPath, Filename))
	{
	}

	public const string Filename = "TechArea.txt";

	public override IEnumerable<IModObject> Load(Mod mod)
	{
		foreach (var rec in DataFile.Records)
		{
			var tech = new Technology();
			tech.TemplateParameters = rec.Parameters;
			string temp;
			int index = -1;

			tech.ModID = rec.Get<string>("ID", tech);
			rec.TryFindFieldValue("Name", out temp, ref index, Mod.Errors, 0, true);
			tech.Name = temp;
			mod.Technologies.Add(tech);

			rec.TryFindFieldValue("Group", out temp, ref index, Mod.Errors, 0, true);
			tech.Group = temp;

			rec.TryFindFieldValue("Description", out temp, ref index, Mod.Errors, 0, true);
			tech.Description = temp;

			tech.MaximumLevel = rec.Get<int>("Maximum Level", tech);
			tech.LevelCost = rec.Get<int>("Level Cost", tech);
			tech.StartLevel = rec.Get<int>("Start Level", tech);
			tech.RaiseLevel = rec.Get<int>("Raise Level", tech);
			tech.RacialTechID = rec.Get<string>("Racial Area", tech);
			tech.UniqueTechID = rec.Get<string>("Unique Area", tech);
			tech.CanBeRemoved = rec.Get<bool>("Can Be Removed", tech);

			yield return tech;
		}

		foreach (var tech in mod.Technologies)
		{
			// find this tech's record
			var rec = DataFile.Records.First(r => r.Get<string>("Name", null) == tech.Name);

			// load its tech reqs
			// couldn't do it before because some early techs can reference later techs
			foreach (var tr in RequirementLoader.LoadEmpireRequirements(rec, tech, RequirementType.Unlock))
				tech.UnlockRequirements.Add(tr);
		}
	}
}