using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Objects.Technology;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads technologies from TechArea.txt.
	/// </summary>
	 [Serializable] public class TechnologyLoader : DataFileLoader
	{
		 public const string Filename = "TechArea.txt";

		 public TechnologyLoader(string modPath)
			 : base(modPath, Filename, DataFile.Load(modPath, Filename))
		{
		}

		public override void Load(Mod mod)
		{
			foreach (var rec in DataFile.Records)
			{
				var tech = new Technology();
				string temp;
				int index = -1;

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
				tech.RaiseLevel = rec.Get<int>("Raise Level Level", tech);
				tech.RacialTechID = rec.Get<string>("Racial Area", tech);
				tech.UniqueTechID = rec.Get<string>("Unique Area", tech);
				tech.CanBeRemoved = rec.Get<bool>("Can Be Removed", tech);

			}

			foreach (var tech in mod.Technologies)
			{
				// find this tech's record
				var rec = DataFile.Records.Single(r => r.Get<string>("Name", null) == tech.Name);

				// load its tech reqs
				// couldn't do it before because some early techs can reference later techs
				foreach (var tr in TechnologyRequirementLoader.Load(rec, tech))
					tech.TechnologyRequirements.Add(tr);
			}
		}
	}
}
