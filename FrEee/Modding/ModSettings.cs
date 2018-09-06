using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Modding
{
	/// <summary>
	/// General settings for a mod.
	/// </summary>
	public class ModSettings
	{
		public ModSettings()
		{
			DefaultColonyConstructionRate = new ResourceQuantity();
			// TODO - moddable default colony construction rate
			DefaultColonyConstructionRate.Add(Resource.Minerals, 2000);
			DefaultColonyConstructionRate.Add(Resource.Organics, 2000);
			DefaultColonyConstructionRate.Add(Resource.Radioactives, 2000);

			PopulationModifiers = new SortedDictionary<long, PopulationModifier>();

			// TODO - split apart production and construction modifiers as a mod option?
			MoodProductivityModifiers = new Dictionary<Mood, int>();

			// TODO - moddable mood reproduction modifiers
			MoodReproductionModifiers = new Dictionary<Mood, int>();
			MoodReproductionModifiers.Add(Mood.Rioting, -999);
			MoodReproductionModifiers.Add(Mood.Angry, -5);
			MoodReproductionModifiers.Add(Mood.Unhappy, -2);
			MoodReproductionModifiers.Add(Mood.Indifferent, 0);
			MoodReproductionModifiers.Add(Mood.Happy, 2);
			MoodReproductionModifiers.Add(Mood.Jubilant, 5);
            MoodReproductionModifiers.Add(Mood.Emotionless, 2);

            // TODO - moddable mood thresholds
            MoodThresholds = new Dictionary<Mood, int>();
			MoodThresholds.Add(Mood.Rioting, 750);
			MoodThresholds.Add(Mood.Angry, 600);
			MoodThresholds.Add(Mood.Unhappy, 450);
			MoodThresholds.Add(Mood.Indifferent, 300);
			MoodThresholds.Add(Mood.Happy, 150);
			MoodThresholds.Add(Mood.Jubilant, 0);

            // TODO - moddable planetary conditions thresholds
            ConditionsThresholds = new Dictionary<Conditions, int>();
			ConditionsThresholds.Add(Conditions.Deadly, 0);
			ConditionsThresholds.Add(Conditions.Harsh, 20);
			ConditionsThresholds.Add(Conditions.Unpleasant, 35);
			ConditionsThresholds.Add(Conditions.Average, 50);
			ConditionsThresholds.Add(Conditions.Mild, 65);
			ConditionsThresholds.Add(Conditions.Good, 80);
			ConditionsThresholds.Add(Conditions.Optimal, 95);

			// TODO - moddable planetary conditions modifiers
			ConditionsReproductionModifiers = new Dictionary<Conditions, int>();
			ConditionsReproductionModifiers.Add(Conditions.Deadly, -20);
			ConditionsReproductionModifiers.Add(Conditions.Harsh, -5);
			ConditionsReproductionModifiers.Add(Conditions.Unpleasant, -2);
			ConditionsReproductionModifiers.Add(Conditions.Average, 0);
			ConditionsReproductionModifiers.Add(Conditions.Mild, 2);
			ConditionsReproductionModifiers.Add(Conditions.Good, 5);
			ConditionsReproductionModifiers.Add(Conditions.Optimal, 8);

			IntroSongs = new List<string>();
			GameplaySongs = new List<string>();
			CombatSongs = new List<string>();
			VictorySongs = new List<string>();
			DefeatSongs = new List<string>();

			VictoryPictures = new List<string>();
			DefeatPictures = new List<string>();

			ValueChangeFrequency = 10;
		}

		/// <summary>
		/// Racial aptitudes.
		/// </summary>
		public IEnumerable<Aptitude> Aptitudes { get { return Aptitude.All; } }

		/// <summary>
		/// Population of the colony ship crew's race to spawn on colonization.
		/// </summary>
		public long AutomaticColonizationPopulation { get; set; }

		/// <summary>
		/// Can bases join fleets?
		/// </summary>
		public bool BasesCanJoinFleets { get; set; }

		/// <summary>
		/// Can a colonizer component be added via retrofit?
		/// </summary>
		public bool CanAddColonizerViaRetrofit { get; set; }

		/// <summary>
		/// Can a spaceyard component be added via retrofit?
		/// </summary>
		public bool CanAddSpaceyardViaRetrofit { get; set; }

		/// <summary>
		/// Additional reload time for weapons on a captured ship.
		/// </summary>
		public int CapturedShipReloadDelay { get; set; }

		/// <summary>
		/// Music tracks for combat.
		/// </summary>
		public IList<string> CombatSongs { get; private set; }

		/// <summary>
		/// Percentage of strategic speed that's applied to combat speed.
		/// </summary>
		public int CombatSpeedPercentPerStrategicSpeed { get; set; }

		/// <summary>
		/// Cooperative research treaty breakthrough chance.
		/// </summary>
		public double CooperativeResearchBreakthroughChance { get; set; }

		/// <summary>
		/// Maximum sight obscuration level for artificial storms.
		/// </summary>
		public int CreatedStormMaxCloakLevel { get; set; }

		/// <summary>
		/// Maximum damage for artificial storms.
		/// </summary>
		public int CreatedStormMaxDamage { get; set; }

		/// <summary>
		/// Maximum shield disruption for artificial storms.
		/// </summary>
		public int CreatedStormMaxShieldDisruption { get; set; }

		/// <summary>
		/// The construction rate for colonies lacking a spaceyard.
		/// </summary>
		public ResourceQuantity DefaultColonyConstructionRate { get; set; }

		/// <summary>
		/// Pictures for defeat.
		/// </summary>
		public IList<string> DefeatPictures { get; set; }

		/// <summary>
		/// Music tracks for defeat.
		/// </summary>
		public IList<string> DefeatSongs { get; private set; }

		/// <summary>
		/// Automatic supply drain each turn for drones.
		/// </summary>
		public int DroneSupplyDrain { get; set; }

		/// <summary>
		/// Percentage of normal rate for emergency build.
		/// </summary>
		public int EmergencyBuildRate { get; set; }

		/// <summary>
		/// High event frequency, chance per 1000 per player for an event each turns
		/// </summary>
		public double EventFrequencyHigh { get; set; }

		/// <summary>
		/// Low event frequency, chance per 1000 per player for an event each turns
		/// </summary>
		public double EventFrequencyLow { get; set; }

		/// <summary>
		/// Medium event frequency, chance per 1000 per player for an event each turns
		/// </summary>
		public double EventFrequencyMedium { get; set; }

		/// <summary>
		/// Standard maintenance cost for facilities (%/turn).
		/// </summary>
		public int FacilityMaintenanceRate { get; set; }

		/// <summary>
		/// Automatic supply drain each turn for fighters.
		/// </summary>
		public int FighterSupplyDrain { get; set; }

		/// <summary>
		/// Music tracks for gameplay.
		/// </summary>
		public IList<string> GameplaySongs { get; private set; }

		/// <summary>
		/// Loss of conditions of glassed planets.
		/// </summary>
		public int GlassedPlanetConditionsLoss { get; set; }

		/// <summary>
		/// Loss of resource value of glassed planets.
		/// </summary>
		public int GlassedPlanetValueLoss { get; set; }

		/// <summary>
		/// Percent effectiveness of ground combat weapons.
		/// </summary>
		public int GroundCombatDamagePercent { get; set; }

		/// <summary>
		/// Number of turns in ground combat.
		/// </summary>
		public int GroundCombatTurns { get; set; }

		/// <summary>
		/// Home systems can still produce some resources even without a spaceport.
		/// </summary>
		public int HomeSystemValueWithoutSpaceport { get; set; }

		/// <summary>
		/// Percent effectiveness of intelligence defense.
		/// </summary>
		public int IntelligenceDefensePercent { get; set; }

		/// <summary>
		/// Music tracks for the title screen.
		/// </summary>
		public IList<string> IntroSongs { get; private set; }

		/// <summary>
		/// Below this much supply a ship gets a low supply warning.
		/// </summary>
		public int LowSupplyWarningAmount { get; set; }

		/// <summary>
		/// Below this % supply a ship gets a low supply warning.
		/// </summary>
		public int LowSupplyWarningPercent { get; set; }

		/// <summary>
		/// Maintenance deficit required to destroy one ship per turn.
		/// </summary>
		public int MaintenanceDeficitToDestroyOneShip { get; set; }

		// TODO - moddable min/max anger values
		public int MaxAnger => 1000;
		
		// TODO - moddable min/max conditions values
		public int MaxConditions => 100;

		/// <summary>
		/// Maximum number of consecutive turns a queue can be on emergency build.
		/// </summary>
		public int MaxEmergencyBuildTurns { get; set; }

		/// <summary>
		/// Maximum mines per player per sector.
		/// </summary>
		public int MaxPlayerMinesPerSector { get; set; }

		/// <summary>
		/// Maximum satellites per player per sector.
		/// </summary>
		public int MaxPlayerSatellitesPerSector { get; set; }

		/// <summary>
		/// Maximum population for which an "abandon colony" order can be given.
		/// </summary>
		public int MaxPopulationToAbandonColony { get; set; }

		/// <summary>
		/// Maxiumum trade percentage for treaties.
		/// </summary>
		public double MaxTradePercent { get; set; }

		/// <summary>
		/// Score percent (of second place player) to trigger Mega Evil Empire on an AI player.
		/// </summary>
		public int MegaEvilEmpireAIScorePercent { get; set; }

		/// <summary>
		/// Does the AI use Mega Evil Empire to gang up on the leading player?
		/// </summary>
		public bool MegaEvilEmpireEnabled { get; set; }

		/// <summary>
		/// Score percent (of second place player) to trigger Mega Evil Empire on a human player.
		/// </summary>
		public int MegaEvilEmpireHumanScorePercent { get; set; }

		/// <summary>
		/// Score threshold to trigger Mega Evil Empire.
		/// </summary>
		public int MegaEvilEmpireScoreThreshold { get; set; }

		/// <summary>
		/// Damage inflicted when a militia unit attacks.
		/// </summary>
		public int MilitiaFirepower { get; set; }

		/// <summary>
		/// Hitpoints of a militia unit.
		/// </summary>
		public int MilitiaHitpoints { get; set; }

		// TODO - moddable min/max anger values (or place in game setup)
		public int MinAnger => 0;

		// TODO - moddable min/max planetary conditions values (or place in game setup)
		public int MinConditions => 0;

		// TODO - moddable min/max planetary conditions values  (or place in game setup)
		public int MinRandomPlanetConditions => 20;

		// TODO - moddable min/max planetary conditions values (or place in game setup)
		public int MaxRandomPlanetConditions => 100;

		// TODO - moddable homeworld conditions (or place in game setup)
		public int HomeworldConditions => 80;

		/// <summary>
		/// Can drones be affected by mines?
		/// </summary>
		public bool MinesAffectDrones { get; set; }

		/// <summary>
		/// Can fighters be affected by mines?
		/// </summary>
		public bool MinesAffectFighters { get; set; }

		/// <summary>
		/// Minimum income for an empire, even if it doesn't have any normal resource income.
		/// </summary>
		public ResourceQuantity MinimumEmpireIncome { get; set; }

		/// <summary>
		/// Modifiers to production and construction rates from population mood.
		/// </summary>
		public IDictionary<Mood, int> MoodProductivityModifiers { get; private set; }

		/// <summary>
		/// Modifiers to reproduction rates from population mood.
		/// </summary>
		public IDictionary<Mood, int> MoodReproductionModifiers { get; private set; }

		/// <summary>
		/// Minimum anger thresholds for each mood, in tenths of a percent.
		/// </summary>
		public IDictionary<Mood, int> MoodThresholds { get; private set; }

		/// <summary>
		/// Modifiers to reproduction rates from planetary conditions.
		/// </summary>
		public IDictionary<Conditions, int> ConditionsReproductionModifiers { get; private set; }

		/// <summary>
		/// Minimum thresholds for each conditions value, in percent.
		/// </summary>
		public IDictionary<Conditions, int> ConditionsThresholds { get; private set; }

		/// <summary>
		/// Accuracy rating of planets.
		/// </summary>
		public int PlanetAccuracy { get; set; }

		/// <summary>
		/// Evasion rating of planets.
		/// </summary>
		public int PlanetEvasion { get; set; }

		/// <summary>
		/// How many people does 1 population represent in the mod files?
		/// </summary>
		public long PopulationFactor { get; set; }

		/// <summary>
		/// Damage required to kill 1 person.
		/// </summary>
		public double PopulationHitpoints { get; set; }

		/// <summary>
		/// Modifiers to production and construction rates from population amounts.
		/// </summary>
		public SortedDictionary<long, PopulationModifier> PopulationModifiers { get; set; }

		/// <summary>
		/// Population required to spawn one militia unit.
		/// </summary>
		public long PopulationPerMilitia { get; set; }

		/// <summary>
		/// Cargo space used to store 1 person.
		/// </summary>
		public double PopulationSize { get; set; }

		/// <summary>
		/// Tribute percent of income paid by protected empires.
		/// </summary>
		public int ProtectorateTributePercent { get; set; }

		/// <summary>
		/// How much of the ramming ship's HP gets applied as damage to the target in ramming?
		/// </summary>
		public int RammingSourceHitpointsDamagePercent { get; set; }

		/// <summary>
		/// How much of the target's HP gets applied as damage to the ramming ship in ramming?
		/// </summary>
		public int RammingTargetHitpointsDamagePercent { get; set; }

		/// <summary>
		/// Standard reproduction rate for populations (%/year).
		/// </summary>
		public int Reproduction { get; set; }

		// TODO - remove ReproductionDelay once the PBW game is over
		[Obsolete("ModSettings.ReproductionDelay is deprecated. Use ReproductionFrequency instead.")]
		public int ReproductionDelay
		{
			get { return ReproductionFrequency; }
			set { ReproductionFrequency = value; }
		}

		/// <summary>
		/// Number of turns between population reproduction.
		/// </summary>
		// TODO - remove DoNotSerialize from ReproductionFrequency once the PBW game is over
		[DoNotSerialize]
		public int ReproductionFrequency { get; set; }

		/// <summary>
		/// Global reproduction rate multiplier to convert mod values to per-turn values.
		/// Defaults to 0.1 since 20%/year reproduction in SE4 really meant 2% per turn.
		/// </summary>
		public double ReproductionMultiplier { get; set; }

		/// <summary>
		/// Cost to remove a component when retrofitting, as a percentage of the component's cost.
		/// </summary>
		public int RetrofitRemovalCostPercent { get; set; }

		/// <summary>
		/// Cost to replace a component when retrofitting, as a percentage of the new component's cost.
		/// </summary>
		public int RetrofitReplacementCostPerecnt { get; set; }

		/// <summary>
		/// Percentage of facility cost returned when scrapping.
		/// </summary>
		public int ScrapFacilityReturnRate { get; set; }

		/// <summary>
		/// Percentage of ship/base cost returned when scrapping.
		/// </summary>
		public int ScrapShipOrBaseReturnRate { get; set; }

		/// <summary>
		/// Percentage of unit cost returned when scrapping.
		/// </summary>
		public int ScrapUnitReturnRate { get; set; }

		/// <summary>
		/// Evasion rating of seekers.
		/// </summary>
		public int SeekerEvasion { get; set; }

		/// <summary>
		/// Standard maintenance cost for ships and bases (%/turn).
		/// </summary>
		public int ShipBaseMaintenanceRate { get; set; }

		/// <summary>
		/// Percentage of normal rate for slow build.
		/// </summary>
		public int SlowBuildRate { get; set; }

		/// <summary>
		/// Number of turns in space combat.
		/// </summary>
		public int SpaceCombatTurns { get; set; }

		// TODO - moddable starting population anger
		public int StartPopulationAnger => 400;

		/// <summary>
		/// Starting trade percentage for treaties.
		/// </summary>
		public double StartTradePercent { get; set; }

		/// <summary>
		/// Tribute percent of income paid by subjugated empires.
		/// </summary>
		public int SubjugationTributePercent { get; set; }

		/// <summary>
		/// Trade percentage increase per turn for treaties.
		/// </summary>
		public double TradePercentPerTurn { get; set; }

		/// <summary>
		/// Standard maintenance cost for units (%/turn).
		/// </summary>
		public int UnitMaintenanceRate { get; set; }

		/// <summary>
		/// Percentage of ship/base cost to unmothball.
		/// </summary>
		public int UnmothballPercentCost { get; set; }

		/// <summary>
		/// Percentage cost (of the new facility) to upgrade a facility.
		/// </summary>
		public int UpgradeFacilityPercentCost { get; set; }

		/// <summary>
		/// How often (in turns) should planetary value change abilities take effect?
		/// </summary>
		public int ValueChangeFrequency { get; set; }

		/// <summary>
		/// Pictures for victory.
		/// </summary>
		public IList<string> VictoryPictures { get; set; }

		/// <summary>
		/// Music tracks for victory.
		/// </summary>
		public IList<string> VictorySongs { get; private set; }

		/// <summary>
		/// Weapon accuracy loss per square of distance to the target.
		/// </summary>
		public int WeaponAccuracyLossPerSquare { get; set; }

		/// <summary>
		/// Weapon accuracy at 0 squares distance.
		/// </summary>
		public int WeaponAccuracyPointBlank { get; set; }

		public double GetMoodFactor(int anger)
		{
			Mood mood = Mood.Emotionless;
			foreach (var mt in MoodThresholds.OrderBy(mt => mt.Value))
			{
				if (mt.Value < anger)
					mood = mt.Key;
				else
					break;
			}
			if (MoodProductivityModifiers.ContainsKey(mood))
				return MoodProductivityModifiers[mood] / 100d;
			return 1d;
		}

		public double GetPopulationConstructionFactor(long population)
		{
			if (population == 0)
				return 0;
			double result = 1d;
			foreach (var pm in PopulationModifiers.OrderBy(pm => pm.Key))
			{
				result = pm.Value.ConstructionRate / 100d;
				if (pm.Key > population)
					break;
			}
			return result;
		}

		public double GetPopulationProductionFactor(long population)
		{
			double result = 1d;
			foreach (var pm in PopulationModifiers.OrderBy(pm => pm.Key))
			{
				result = pm.Value.ConstructionRate / 100d;
				if (pm.Key > population)
					break;
			}
			return result;
		}
	}
}