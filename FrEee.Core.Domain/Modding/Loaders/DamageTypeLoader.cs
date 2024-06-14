using System.Collections.Generic;

namespace FrEee.Modding.Loaders;

/// <summary>
/// Loads damage types from DamageTypes.txt.
/// </summary>
public class DamageTypeLoader : DataFileLoader
{
	public DamageTypeLoader(string modPath)
		: base(modPath, Filename, DataFile.Load(modPath, Filename))
	{
	}

	public const string Filename = "DamageTypes.txt";

	public override IEnumerable<IModObject> Load(Mod mod)
	{
		foreach (var rec in DataFile.Records)
		{
			var dt = new DamageType();
			dt.TemplateParameters = rec.Parameters;
			mod.DamageTypes.Add(dt);

			dt.ModID = rec.Get<string>("ID", dt);
			dt.Name = rec.Get<string>("Name", dt);
			dt.Description = rec.Get<string>("Description", dt);
			dt.NormalShieldDamage = rec.Get<int>("Normal Shield Damage", dt) ?? 100;
			dt.NormalShieldPiercing = rec.Get<int>("Normal Shield Piercing", dt) ?? 0;
			dt.PhasedShieldDamage = rec.Get<int>("Phased Shield Damage", dt) ?? 100;
			dt.PhasedShieldPiercing = rec.Get<int>("Phased Shield Piercing", dt) ?? 0;
			dt.ComponentDamage = rec.Get<int>("Component Damage", dt) ?? 100;
			dt.ComponentPiercing = rec.Get<int>("Component Piercing", dt) ?? 0;
			dt.SeekerDamage = rec.Get<int>("Seeker Damage", dt) ?? dt.ComponentDamage;
			dt.PopulationDamage = rec.Get<int>("Population Damage", dt) ?? 100;
			dt.ConditionsDamage = rec.Get<int>("Conditions Damage", dt) ?? 0;
			dt.FacilityDamage = rec.Get<int>("Facility Damage", dt) ?? 100;
			dt.FacilityPiercing = rec.Get<int>("Facility Piercing", dt) ?? 100;
			dt.PlagueLevel = rec.Get<int>("Plague Level", dt) ?? 0;
			dt.TargetPush = rec.Get<int>("Target Push", dt) ?? 0;
			dt.TargetTeleport = rec.Get<int>("Target Teleport", dt) ?? 0;
			dt.IncreaseReload = rec.Get<int>("Increase Reload", dt) ?? 0;
			dt.DisruptReload = rec.Get<int>("Disrupt Reload", dt) ?? 0;
			dt.ShipCapture = rec.Get<int>("Ship Capture", dt) ?? 0;
			dt.EmissiveArmor = rec.Get<int>("Emissive Armor", dt) ?? 100;
			dt.ShieldGenerationFromDamage = rec.Get<int>("Shield Generation From Damage", dt) ?? 100;

			yield return dt;
		}
	}
}