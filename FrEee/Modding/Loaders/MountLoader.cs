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
	public class MountLoader : ILoader
	{
		public void Load(DataFile df, Mod mod)
		{
			foreach (var rec in df.Records)
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
				m.WeaponTypes = rec.GetNullEnum<WeaponTypes>("Weapon Type Requirement", ref index) ?? WeaponTypes.All;
				m.VehicleTypes = rec.GetNullEnum<VehicleTypes>(new string[]{"Vehicle Type", "Vehicle Types", "Vehicle Type Requirement"}, ref index) ?? VehicleTypes.All;
				m.AbilityPercentages = AbilityLoader.LoadPercentagesOrModifiers(rec, "Percentage");
				m.AbilityModifiers = AbilityLoader.LoadPercentagesOrModifiers(rec, "Modifier");
				m.TechnologyRequirements = TechnologyRequirementLoader.Load(rec).ToList();
			}
		}
	}
}
