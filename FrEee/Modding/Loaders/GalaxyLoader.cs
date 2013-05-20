using FrEee.Game;
using FrEee.Modding.StarSystemPlacementStrategies;
using FrEee.Modding.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads galaxy templates from QuadrantTypes.txt.
	/// </summary>
	public class GalaxyLoader : ILoader
	{
		public void Load(DataFile df, Mod mod)
		{
			foreach (var rec in df.Records)
			{
				var galtemp = new GalaxyTemplate();
				string temp;
				int index = -1;

				rec.TryFindFieldValue("Name", out temp, ref index, Mod.Errors, 0, true);
				galtemp.Name = temp;
				mod.GalaxyTemplates.Add(galtemp);

				rec.TryFindFieldValue("Description", out temp, ref index, Mod.Errors, 0, true);
				galtemp.Description = temp;

				rec.TryFindFieldValue("Min Dist Between Systems", out temp, ref index, Mod.Errors, 0, true);
				int mindist;
				if (!int.TryParse(temp, out mindist))
					Mod.Errors.Add(new DataParsingException("Cannot find field \"Min Dist Between Systems\"", Mod.CurrentFileName, rec));
				galtemp.MinimumStarSystemDistance = mindist;

				rec.TryFindFieldValue("System Placement", out temp, ref index, Mod.Errors, 0, true);
				if (temp == "Random")
					galtemp.StarSystemPlacementStrategy = new RandomStarSystemPlacementStrategy();
				else if (temp == "Clusters")
					galtemp.StarSystemPlacementStrategy = new ClusteredStarSystemPlacementStrategy();
				else if (temp == "Spiral")
					galtemp.StarSystemPlacementStrategy = new SpiralStarSystemPlacementStrategy();
				else if (temp == "Diffuse")
					galtemp.StarSystemPlacementStrategy = new DiffuseStarSystemPlacementStrategy();
				else if (temp == "Grid")
					galtemp.StarSystemPlacementStrategy = new GridStarSystemPlacementStrategy();
				else
				{
					Mod.Errors.Add(new DataParsingException("Invalid value \"" + temp + "\" for field \"System Placement\". Must be Random, Clusters, Spiral, Diffuse, or Grid.", Mod.CurrentFileName, rec));
					galtemp.StarSystemPlacementStrategy = new RandomStarSystemPlacementStrategy(); // default
				}

				rec.TryFindFieldValue("Max Warp Points per Sys", out temp, ref index, Mod.Errors, 0, true);
				int maxwarp;
				if (!int.TryParse(temp, out maxwarp))
					Mod.Errors.Add(new DataParsingException("Cannot find field \"Max Warp Points per Sys\"", Mod.CurrentFileName, rec));
				galtemp.MaxWarpPointsPerSystem = maxwarp;

				rec.TryFindFieldValue("Min Angle Between WP", out temp, ref index, Mod.Errors, 0, true);
				int minangle;
				if (!int.TryParse(temp, out minangle))
					Mod.Errors.Add(new DataParsingException("Cannot find field \"Min Angle Between WP\"", Mod.CurrentFileName, rec));
				galtemp.MinWarpPointAngle = minangle;

				int count = 1;
				int start = 0;
				while (true)
				{

					StarSystemTemplate sst;
					int chance;

					if (!rec.TryFindFieldValue(new string[] { "Type " + count + " Name", "Type Name" }, out temp, ref start, null, start, true))
						break; // couldn't load next chance
					else
					{
						sst = mod.StarSystemTemplates.Find(temp);
						if (sst == null)
						{
							Mod.Errors.Add(new DataParsingException("Could not find star system template \"" + temp + "\".", Mod.CurrentFileName, rec, null));
							continue; // skip this chance
						}
					}
					start++;

					if (!rec.TryFindFieldValue(new string[] { "Type " + count + " Chance", "Type Chance" }, out temp, ref start, null, start))
						break; // couldn't load next chance
					else
					{
						if (!int.TryParse(temp, out chance))
							Mod.Errors.Add(new DataParsingException("Type Chance field value must be an integer.", Mod.CurrentFileName, rec, null));
					}
					start++;

					galtemp.StarSystemTemplateChances.Add(sst, chance);

					count++;
				}
			}
		}
	}
}
