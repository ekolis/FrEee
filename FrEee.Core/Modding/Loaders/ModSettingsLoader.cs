using FrEee.Objects.Civilization;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Modding.Loaders;

/// <summary>
/// Loads mod settings from Settings.txt.
/// </summary>
public class ModSettingsLoader : DataFileLoader
{
	public ModSettingsLoader(string modPath)
		: base(modPath, Filename, DataFile.Load(modPath, Filename))
	{
	}

	public const string Filename = "Settings.txt";

	public override IEnumerable<IModObject> Load(Mod mod)
	{
		var rec = DataFile.Records.FirstOrDefault();
		if (rec == null)
		{
			Mod.Errors.Add(new DataParsingException("Could not load Settings.txt - no record found.", Filename));
			yield break;
		}

		var settings = new ModSettings();
		mod.Settings = settings;

		// TODO - load more settings

		settings.ScrapFacilityReturnRate = rec.Get<int>("Scrap Facility Percent Returned", null) ?? 30;
		settings.ScrapUnitReturnRate = rec.Get<int>("Scrap Unit Percent Returned", null) ?? 30;
		settings.ScrapShipOrBaseReturnRate = rec.Get<int>("Scrap Ship Percent Returned", null) ?? 30;

		// TODO - load more settings

		settings.StartTradePercent = rec.Get<double>("Starting Trade Percentage", null) ?? 0;
		settings.TradePercentPerTurn = rec.Get<double>("Trade Percentage Increase Per Turn", null) ?? 1;
		settings.MaxTradePercent = rec.Get<double>("Maximum Trade Percentage", null) ?? 20;
		settings.CooperativeResearchBreakthroughChance = rec.Get<double>("Cooperative Research Breakthrough Chance", null) ?? 5;

		// TODO - load more settings

		settings.UpgradeFacilityPercentCost = rec.Get<int>("Upgrade Facility Percent Cost", null) ?? 50;

		// TODO - load more settings

		// event frequency is really per mille per player per turn but silly aaron said it was percent :P
		settings.EventFrequencyLow = rec.Get<double>("Event Percent Chance Low") ?? 5;
		settings.EventFrequencyMedium = rec.Get<double>("Event Percent Chance Medium") ?? 109;
		settings.EventFrequencyHigh = rec.Get<double>("Event Percent Chance High") ?? 25;
		settings.MaintenanceDeficitToDestroyOneShip = rec.Get<int>("Maintenance Cost Amt Per Dead", null) ?? 20000;
		settings.ShipBaseMaintenanceRate = rec.Get<int>("Empire Ship And Base Percent Maint Cost", null) ?? rec.Get<int>("Empire Starting Percent Maint Cost", null);
		settings.UnitMaintenanceRate = rec.Get<int>("Empire Unit Percent Maint Cost", null) ?? 0;
		settings.FacilityMaintenanceRate = rec.Get<int>("Empire Facility Percent Maint Cost", null) ?? 0;

		// TODO - load more settings

		settings.PopulationPerMilitia = rec.Get<int>("Defending Units Per Population", null) ?? 20; // yes, Aaron did say units per pop but it's really pops per unit!
		settings.MilitiaFirepower = rec.Get<int>("Population Defender Attack Strength") ?? 10;
		settings.MilitiaHitpoints = rec.Get<int>("Population Defender Hit Points") ?? 30;

		// TODO - load more settings


		settings.Reproduction = rec.Get<int>("Empire Starting Percent Reproduction", null) ?? 10;
		settings.ReproductionMultiplier = rec.Get<double>("Reproduction Multiplier", null) ?? 0.1;

		// TODO - load more settings

		settings.PopulationFactor = rec.Get<int>("Population Factor", null) ?? (int)1e6; // specifies unit of population for other settings
		settings.PopulationHitpoints = (rec.Get<double>("Damage Points To Kill One Population", null) ?? 10).Value / settings.PopulationFactor;

		// TODO - load more settings

		settings.PopulationModifiers = new SortedDictionary<long, PopulationModifier>(PopulationModifierLoader.Load(rec).ToDictionary(pm => pm.PopulationAmount));

		// TODO - load more settings

		// load aptitudes
		foreach (var a in Aptitude.All)
		{
			a.MinPercent = rec.Get<int>("Characteristic " + a.Name + " Min Pct", null);
			a.MaxPercent = rec.Get<int>("Characteristic " + a.Name + " Max Pct", null);
			a.Cost = rec.Get<int>("Characteristic " + a.Name + " Pct Cost", null);
			a.Threshold = rec.Get<int>("Characteristic " + a.Name + " Threshold", null);
			a.LowCost = rec.Get<int>("Characteristic " + a.Name + " Threshhold Pct Cost Neg", null);
			a.HighCost = rec.Get<int>("Characteristic " + a.Name + " Threshhold Pct Cost Pos", null);
		}

		// load mood modifiers
		foreach (var mood in new Mood[] { Mood.Riot, Mood.Angry, Mood.Unhappy, Mood.Indifferent, Mood.Happy, Mood.Jubilant })
			settings.MoodProductivityModifiers.Add(mood, rec.Get<int>($"Mood {mood} Modifier"));
            settings.MoodProductivityModifiers.Add(Mood.Emotionless, rec.Get<int>("Mood Emotionless Modifier") ?? settings.MoodProductivityModifiers[Mood.Happy]);

		// TODO - load more settings

		settings.WeaponAccuracyPointBlank = rec.Get<int>("Combat Base To Hit Value", null) ?? 100;
		settings.WeaponAccuracyLossPerSquare = rec.Get<int>("Combat To Hit Modifier Per Square Distance", null) ?? 10;

		// TODO - load more settings

		settings.SpaceCombatTurns = rec.Get<int>("Number Of Space Combat Turns", null) ?? 30;
		settings.GroundCombatTurns = rec.Get<int>("Number Of Ground Combat Turns", null) ?? 10;
		settings.CombatSpeedPercentPerStrategicSpeed = rec.Get<int>("Combat Speed Percent Per Strategic Speed", null) ?? 50;

		// TODO - load more settings

		settings.PopulationSize = (double)(rec.Get<int>("Population Mass", null) ?? 5) / settings.PopulationFactor;
		settings.ReproductionFrequency = rec.Get<int>("Reproduction Check Frequency", null) ?? 1; // TODO - change property name to ReproductionFrequency
		settings.ValueChangeFrequency = rec.Get<int>("Value Change Frequency", null) ?? 10;

		// TODO - load more settings

		settings.SeekerEvasion = rec.Get<int>("Seeker Combat Defense Modifier");
		settings.PlanetAccuracy = rec.Get<int>("Planet Combat Offense Modifier");
		settings.PlanetEvasion = rec.Get<int>("Planet Combat Defense Modifier");

		yield break;
	}
}