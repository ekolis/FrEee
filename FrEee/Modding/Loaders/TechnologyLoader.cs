using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Objects.Research;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads technologies from TechArea.txt.
	/// </summary>
	 [Serializable] public class TechnologyLoader : ILoader
	{
		public void Load(DataFile df, Mod mod)
		{
			foreach (var rec in df.Records)
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

				foreach (var tr in TechnologyRequirementLoader.Load(rec))
					tech.TechnologyRequirements.Add(tr);

			}
		}
	}
}
