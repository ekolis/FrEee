using FrEee.Setup.StarSystemPlacementStrategies;
using FrEee.Modding.Interfaces;
using FrEee.Modding.Templates;
using System;
using System.Collections.Generic;
using FrEee.Extensions;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads galaxy templates from QuadrantTypes.txt.
	/// </summary>
	[Serializable]
	public class GalaxyLoader : DataFileLoader
	{
		public GalaxyLoader(string modPath)
			: base(modPath, Filename, DataFile.Load(modPath, Filename))
		{
		}

		public const string Filename = "QuadrantTypes.txt";

		public override IEnumerable<IModObject> Load(Mod mod)
		{
			foreach (var rec in DataFile.Records)
			{
				var galtemp = new GalaxyTemplate();
				galtemp.TemplateParameters = rec.Parameters;
				string temp;
				int index = -1;

				galtemp.ModID = rec.Get<string>("ID", galtemp);
				rec.TryFindFieldValue("Name", out temp, ref index, Mod.Errors, 0, true);
				galtemp.Name = temp;
				mod.GalaxyTemplates.Add(galtemp);

				rec.TryFindFieldValue("Description", out temp, ref index, Mod.Errors, 0, true);
				galtemp.Description = temp;

				rec.TryFindFieldValue("Min Dist Between Systems", out temp, ref index, Mod.Errors, 0, true);
				int mindist;
				if (!int.TryParse(temp, out mindist))
					Mod.Errors.Add(new DataParsingException("Cannot find field \"Min Dist Between Systems\"", The.ModFileName, rec));
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
					Mod.Errors.Add(new DataParsingException("Invalid value \"" + temp + "\" for field \"System Placement\". Must be Random, Clusters, Spiral, Diffuse, or Grid.", The.ModFileName, rec));
					galtemp.StarSystemPlacementStrategy = new RandomStarSystemPlacementStrategy(); // default
				}

				rec.TryFindFieldValue("Max Warp Points per Sys", out temp, ref index, Mod.Errors, 0, true);
				int maxwarp;
				if (!int.TryParse(temp, out maxwarp))
					Mod.Errors.Add(new DataParsingException("Cannot find field \"Max Warp Points per Sys\"", The.ModFileName, rec));
				galtemp.MaxWarpPointsPerSystem = maxwarp;

				rec.TryFindFieldValue("Min Angle Between WP", out temp, ref index, Mod.Errors, 0, true);
				int minangle;
				if (!int.TryParse(temp, out minangle))
					Mod.Errors.Add(new DataParsingException("Cannot find field \"Min Angle Between WP\"", The.ModFileName, rec));
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
						sst = mod.StarSystemTemplates.FindByName(temp);
						if (sst == null)
						{
							Mod.Errors.Add(new DataParsingException("Could not find star system template \"" + temp + "\".", The.ModFileName, rec, null));
							continue; // skip this chance
						}
					}
					start++;

					if (!rec.TryFindFieldValue(new string[] { "Type " + count + " Chance", "Type Chance" }, out temp, ref start, null, start))
						break; // couldn't load next chance
					else
					{
						if (!int.TryParse(temp, out chance))
							Mod.Errors.Add(new DataParsingException("Type Chance field value must be an integer.", The.ModFileName, rec, null));
					}
					start++;

					// silly Adamant Mod refers to the same star system type twice...
					if (galtemp.StarSystemTemplateChances.ContainsKey(sst))
						galtemp.StarSystemTemplateChances[sst] += chance;
					else
						galtemp.StarSystemTemplateChances.Add(sst, chance);

					count++;
				}

				yield return galtemp;
			}
		}
	}
}