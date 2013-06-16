using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Technology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads mounts from CompEnhancement.txt.
	/// </summary>
	public class MountLoader : DataFileLoader
	{
		public const string Filename = "CompEnhancement.txt";

		public MountLoader(string modPath)
			: base(Filename, DataFile.Load(modPath, Filename))
		{
		}

		public override void Load(Mod mod)
		{
			foreach (var rec in DataFile.Records)
			{
				var m = new Mount();
				mod.Mounts.Add(m);

				int index = -1;

				m.Name = rec.GetString("Long Name", ref index, true, 0, true);
				m.ShortName = rec.GetString("Short Name", ref index, false, 0, true) ?? m.Name; // default to long name
				m.Description = rec.GetString("Description", ref index, false, 0, true);
				m.Code = rec.GetString("Code", ref index, true, 0, true);
				m.PictureName = rec.GetString("Pic", ref index, false, 0, true);
				m.CostPercent = rec.GetNullInt("Cost Percent", ref index) ?? 100;
				m.SizePercent = rec.GetNullInt("Tonnage Percent", ref index) ?? 100;
				m.DurabilityPercent = rec.GetNullInt("Tonnage Structure Percent", ref index) ?? 100;
				m.SupplyUsagePercent = rec.GetNullInt("Supply Percent", ref index) ?? 100;
				m.WeaponRangeModifier = rec.GetNullInt("Range Modifier", ref index) ?? 0;
				m.WeaponAccuracyModifier = rec.GetNullInt("Weapon To Hit Modifier", ref index) ?? 0;
				m.MinimumVehicleSize = rec.GetNullInt("Vehicle Size Minimum", ref index, 0, true);
				m.MaximumVehicleSize = rec.GetNullInt("Vehicle Size Maximum", ref index, 0, true);
				m.RequiredComponentFamily = rec.GetNullString("Component Family Requirement", ref index, 0, true);
				var wtstring = rec.GetNullString(new string[]{"Weapon Type Requirement", "Weapon Type"}, ref index);
				if (wtstring == null)
					m.WeaponTypes = WeaponTypes.AnyComponent;
				else
				{
					m.WeaponTypes = WeaponTypes.None;
					foreach (var s in wtstring.Split(',').Select(s => s.Trim()))
					{
						if (s == "None") // none here really means not a weapon
							m.WeaponTypes |= WeaponTypes.NotAWeapon;
						else if (s == "Direct Fire")
							m.WeaponTypes |= WeaponTypes.DirectFire;
						else if (s == "Seeking")
							m.WeaponTypes |= WeaponTypes.Seeking;
						else if (s == "Warhead")
							m.WeaponTypes |= WeaponTypes.Warhead;
						else if (s == "Point-Defense" || s == "Direct Fire Point-Defense")
							m.WeaponTypes |= WeaponTypes.DirectFirePointDefense;
						else if (s == "Seeking Point-Defense")
							m.WeaponTypes |= WeaponTypes.SeekingPointDefense;
						else if (s == "Warhead Point-Defense")
							m.WeaponTypes |= WeaponTypes.WarheadPointDefense;
						else if (s == "All")
							m.WeaponTypes |= WeaponTypes.All;
						else if (s == "Any Component")
							m.WeaponTypes |= WeaponTypes.AnyComponent;
						else
							Mod.Errors.Add(new DataParsingException("Unknown weapon type \"" + s + "\".", Mod.CurrentFileName, rec));
					}
				}
				var vtstring = rec.GetNullString(new string[]{"Vehicle Type", "Vehicle Type Requirement"}, ref index);
				if (vtstring == null)
					m.VehicleTypes = VehicleTypes.All;
				else
				{
					m.VehicleTypes = VehicleTypes.None;
					foreach (var s in vtstring.Split(',').Select(s => s.Trim()))
					{
						var vals = Enum.GetValues(typeof(VehicleTypes)).Cast<VehicleTypes>();
						// special cases
						if (s == "Weapon Platform")
							m.VehicleTypes |= VehicleTypes.WeaponPlatform;
						else
						{
							bool found = false;
							foreach (var val in vals)
							{
								if (val.ToString() == s)
								{
									m.VehicleTypes |= val;
									found = true;
									break;
								}
							}
							if (!found)
								Mod.Errors.Add(new DataParsingException("Unknown vehicle type \"" + s + "\".", Mod.CurrentFileName, rec));
						}
					}
				}
				m.AbilityPercentages = AbilityLoader.LoadPercentagesOrModifiers(rec, "Percentage");
				m.AbilityModifiers = AbilityLoader.LoadPercentagesOrModifiers(rec, "Modifier");
				m.TechnologyRequirements = TechnologyRequirementLoader.Load(rec).ToList();
			}
		}
	}
}
