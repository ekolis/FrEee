using FrEee.Objects.Technology;
using FrEee.Utility;
using FrEee.Vehicles;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Modding.Loaders;

/// <summary>
/// Loads hulls from VehicleSize.txt.
/// </summary>
public class HullLoader : DataFileLoader
{
	public HullLoader(string modPath)
		: base(modPath, Filename, DataFile.Load(modPath, Filename))
	{
	}

	public const string Filename = "VehicleSize.txt";

	public override IEnumerable<IModObject> Load(Mod mod)
	{
		foreach (var rec in DataFile.Records)
		{
			IHull<IVehicle> hull;
			var hullname = rec.Get<string>("Name", null);
			var hulltype = rec.Get<string>("Vehicle Type", null);
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
			hull.TemplateParameters = rec.Parameters;
			hull.ModID = rec.Get<string>("ID", hull);
			hull.Name = hullname;
			mod.Hulls.Add(hull);

			hull.ShortName = rec.Get<string>("Short Name", hull);
			hull.Description = rec.Get<string>("Description", hull);
			hull.Code = rec.Get<string>("Code", hull);

			var bitmapfields = rec.Fields.Where(f => f.Name.EndsWith("Bitmap Name"));
			foreach (var f in bitmapfields)
			{
				hull.PictureNames.Add(f.Value);
			}

			hull.Size = rec.Get<int>("Tonnage", hull);

			foreach (var costfield in rec.Fields.Where(cf => cf.Name.StartsWith("Cost ")))
				hull.Cost[Resource.Find(costfield.Name.Substring("Cost ".Length))] = costfield.CreateFormula<int>(hull);

			hull.ThrustPerMove = rec.Get<int>("Engines Per Move", hull);

			foreach (var tr in RequirementLoader.LoadEmpireRequirements(rec, hull, RequirementType.Unlock))
				hull.UnlockRequirements.Add(tr);

			// TODO - build and use requirements

			foreach (var abil in AbilityLoader.Load(Filename, rec, hull))
				hull.Abilities.Add(abil);

			hull.NeedsBridge = rec.Get<bool>("Requirement Must Have Bridge", hull);
			hull.CanUseAuxiliaryControl = rec.Get<bool>("Requirement Can Have Aux Con", hull);
			hull.MinLifeSupport = rec.Get<int>("Requirement Min Life Support", hull);
			hull.MinCrewQuarters = rec.Get<int>("Requirement Min Crew Quarters", hull);
			hull.MaxEngines = rec.Get<int>("Requirement Max Engines", hull);
			hull.MinPercentFighterBays = rec.Get<int>("Requirement Pct Fighter Bays", hull);
			hull.MinPercentColonyModules = rec.Get<int>("Requirement Pct Colony Mods", hull);
			hull.MinPercentCargoBays = rec.Get<int>("Requirement Pct Cargo", hull);

			yield return hull;
		}
	}
}