﻿using FrEee.Objects.Technology;
using FrEee.Extensions;
using System.Collections.Generic;
using System.Linq;
using FrEee.Processes.Combat;
using FrEee.Vehicles.Types;

namespace FrEee.Modding.Loaders;

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
				m.WeaponTypes = wtstring.Value.Capitalize().ParseEnum<WeaponTypes>();
			var vtstring = rec.Get<string>(new string[] { "Vehicle Type", "Vehicle Type Requirement" }, m);
			if (vtstring == null)
				m.VehicleTypes = VehicleTypes.All;
			else
				m.VehicleTypes = vtstring.Value.Capitalize().ParseEnum<VehicleTypes>();
			m.AbilityPercentages = AbilityLoader.LoadPercentagesOrModifiers(rec, "Percent", m);
			m.AbilityModifiers = AbilityLoader.LoadPercentagesOrModifiers(rec, "Modifier", m);
			m.UnlockRequirements = RequirementLoader.LoadEmpireRequirements(rec, m, RequirementType.Unlock).ToList();
			// TODO - build and use requirements

			yield return m;
		}
	}
}