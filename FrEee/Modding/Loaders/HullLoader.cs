using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads hulls from VehicleSize.txt.
	/// </summary>
	public class HullLoader : ILoader
	{
		public void Load(DataFile df, Mod mod)
		{
			foreach (var rec in df.Records)
			{
				IHull<IVehicle> hull;
				int index = -1;
				var hullname = rec.GetString("Name", ref index, true, 0, true);
				var hulltype = rec.GetString("Vehicle Type", ref index, true, 0, true);
				switch (hulltype)
				{
					case "Ship":
						hull = new Hull<Ship>();
						break;
					case "Base":
						hull = new Hull<Base>();
						break;
					case "Fighter":
						hull = new Hull<Fighter>();
						break;
					case "Satellite":
						hull = new Hull<Satellite>();
						break;
					case "Troop":
						hull = new Hull<Troop>();
						break;
					case "Drone":
						hull = new Hull<Drone>();
						break;
					case "Mine":
						hull = new Hull<Mine>();
						break;
					case "Weapon Platform":
						hull = new Hull<WeaponPlatform>();
						break;
					default:
						Mod.Errors.Add(new DataParsingException("Invalid vehicle type \"" + hulltype + "\" specified for " + hullname + " hull.", Mod.CurrentFileName, rec));
						continue;
				}
				hull.Name = hullname;
				mod.Hulls.Add(hull);

				hull.ShortName = rec.GetString("Short Name", ref index, true, 0, true);
				hull.Description = rec.GetString("Description", ref index, true, 0, true);
				hull.Code = rec.GetString("Code", ref index, true, 0, true);

				var bitmapfields = rec.Fields.Where(f => f.Name.EndsWith("Bitmap Name"));
				foreach (var f in bitmapfields)
				{
					hull.PictureNames.Add(f.Value);
				}

				hull.Size = rec.GetInt("Tonnage", ref index, true, 0, true);

				foreach (var costfield in rec.Fields.Where(cf => cf.Name.StartsWith("Cost ")))
					hull.Cost[Resource.Find(costfield.Name.Substring("Cost ".Length))] = costfield.IntValue(rec);

				hull.Mass = rec.GetInt("Engines Per Move", ref index, true, 0, true);

				foreach (var tr in TechnologyRequirementLoader.Load(rec))
					hull.TechnologyRequirements.Add(tr);

				foreach (var abil in AbilityLoader.Load(rec))
					hull.Abilities.Add(abil);

				hull.NeedsBridge = rec.GetBool("Requirement Must Have Bridge", ref index, true, 0, true);
				hull.CanUseAuxiliaryControl = rec.GetBool("Requirement Can Have Aux Con", ref index, true, 0, true);
				hull.MinLifeSupport = rec.GetInt("Requirement Min Life Support", ref index, true, 0, true);
				hull.MinCrewQuarters = rec.GetInt("Requirement Min Crew Quarters", ref index, true, 0, true);
				hull.MaxEngines = rec.GetInt("Requirement Max Engines", ref index, true, 0, true);
				hull.MinPercentFighterBays = rec.GetInt("Requirement Pct Fighter Bays", ref index, true, 0, true);
				hull.MinPercentColonyModules = rec.GetInt("Requirement Pct Colony Mods", ref index, true, 0, true);
				hull.MinPercentCargoBays = rec.GetInt("Requirement Pct Cargo", ref index, true, 0, true);
			}
		}
	}
}
