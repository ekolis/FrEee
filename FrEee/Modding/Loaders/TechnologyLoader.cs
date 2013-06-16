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
			 : base(Filename, DataFile.Load(modPath, Filename))
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

				tech.MaximumLevel = rec.FindField("Maximum Level", ref index, true, 0, true).IntValue(rec);
				tech.LevelCost = rec.FindField("Level Cost", ref index, true, 0, true).IntValue(rec);
				tech.StartLevel = rec.FindField("Start Level", ref index, true, 0, true).IntValue(rec);
				tech.RaiseLevel = rec.FindField("Raise Level", ref index, true, 0, true).IntValue(rec);
				tech.RacialTechID = rec.FindField("Racial Area", ref index, true, 0, true).IntValue(rec);
				tech.UniqueTechID = rec.FindField("Unique Area", ref index, true, 0, true).IntValue(rec);
				tech.CanBeRemoved = rec.FindField("Can Be Removed", ref index, true, 0, true).BoolValue(rec);

			}

			foreach (var tech in mod.Technologies)
			{
				// find this tech's record
				int index = -1;
				var rec = DataFile.Records.Single(r => r.GetString("Name", ref index, false, 0, true) == tech.Name);

				// load its tech reqs
				// couldn't do it before because some early techs can reference later techs
				foreach (var tr in TechnologyRequirementLoader.Load(rec))
					tech.TechnologyRequirements.Add(tr);
			}
		}
	}
}
