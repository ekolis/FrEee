using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Technology;
using FrEee.Modding.Enumerations;
using FrEee.Modding.Interfaces;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads mounts from CompEnhancement.txt.
	/// </summary>
	public class MountLoader : DataFileLoader
	{
		public MountLoader(string modPath)
			: base(modPath, Filename, DataFile.Load(modPath, Filename))
		{
		}

		public const string Filename = "CompEnhancement.txt";

		public override IEnumerable<IModObject> Load(Mod mod)
		{
			foreach (var rec in DataFile.Records)
			{
				var m = new Mount();
				m.TemplateParameters = rec.Parameters;
				mod.Mounts.Add(m);

				m.ModID = rec.Get<string>("ID", m);
				m.Name = rec.Get<string>("Long Name", m);
				m.ShortName = rec.Get<string>("Short Name", m) ?? m.Name; // default to long name
				m.Description = rec.Get<string>("Description", m);
				m.Code = rec.Get<string>("Code", m);
				m.PictureName = rec.Get<string>("Pic", m);
				m.CostPercent = rec.Get<int>("Cost Percent", m) ?? 100;
				m.SizePercent = rec.Get<int>("Tonnage Percent", m) ?? 100;
				m.DurabilityPercent = rec.Get<int>("Tonnage Structure Percent", m) ?? 100;
				m.WeaponDamagePercent = rec.Get<int>("Damage Percent", m) ?? 100;
				m.SupplyUsagePercent = rec.Get<int>("Supply Percent", m) ?? 100;
				m.WeaponRangeModifier = rec.Get<int>("Range Modifier", m) ?? 0;
				m.WeaponAccuracyModifier = rec.Get<int>("Weapon To Hit Modifier", m) ?? 0;
				m.MinimumVehicleSize = rec.Get<int>("Vehicle Size Minimum", m);
				m.MaximumVehicleSize = rec.Get<int>("Vehicle Size Maximum", m);
				m.RequiredComponentFamily = rec.Get<string>(new string[] { "Comp Family Requirement", "Component Family Requirement", "Comp Family", "Component Family" }, m);
				var wtstring = rec.Get<string>(new string[] { "Weapon Type Requirement", "Weapon Type" }, m);
				if (wtstring == null)
					m.WeaponTypes = WeaponTypes.AnyComponent;
				else
				{
					m.WeaponTypes = WeaponTypes.None;
					foreach (var s in wtstring.Value.Split(',').Select(s => s.Trim()))
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
				var vtstring = rec.Get<string>(new string[] { "Vehicle Type", "Vehicle Type Requirement" }, m);
				if (vtstring == null)
					m.VehicleTypes = VehicleTypes.All;
				else
				{
					m.VehicleTypes = VehicleTypes.None;
					foreach (var s in vtstring.Value.Split(',').Select(s => s.Trim()))
					{
						// special cases
						if (s == "Weapon Platform")
							m.VehicleTypes |= VehicleTypes.WeaponPlatform;
						else
						{
							bool found = false;
							foreach (var val in Enum.GetNames(typeof(VehicleTypes)))
							{
								if (val == s)
								{
									m.VehicleTypes |= val.ParseEnum<VehicleTypes>();
									found = true;
									break;
								}
							}
							if (!found)
								Mod.Errors.Add(new DataParsingException("Unknown vehicle type \"" + s + "\".", Mod.CurrentFileName, rec));
						}
					}
				}
				m.AbilityPercentages = AbilityLoader.LoadPercentagesOrModifiers(rec, "Percent", m);
				m.AbilityModifiers = AbilityLoader.LoadPercentagesOrModifiers(rec, "Modifier", m);
				m.UnlockRequirements = RequirementLoader.LoadEmpireRequirements(rec, m, RequirementType.Unlock).ToList();
				// TODO - build and use requirements

				yield return m;
			}
		}
	}
}