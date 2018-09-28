using FrEee.Game.Objects.Civilization;
using FrEee.Modding.Interfaces;
using System.Collections.Generic;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads happiness models from Happiness.txt.
	/// </summary>
	public class HappinessModelLoader : DataFileLoader
	{
		public HappinessModelLoader(string modPath)
			: base(modPath, Filename, DataFile.Load(modPath, Filename))
		{
		}

		public const string Filename = "Happiness.txt";

		public override IEnumerable<IModObject> Load(Mod mod)
		{
			foreach (var rec in DataFile.Records)
			{
				var h = new HappinessModel();
				h.TemplateParameters = rec.Parameters;
				mod.HappinessModels.Add(h);

				h.ModID = rec.Get<string>("ID", h);
				h.Name = rec.Get<string>("Name", h);
				h.Description = rec.Get<string>("Description", h);
				h.MaxPositiveTurnAngerChange = rec.Get<int>("Max Positive Anger Change", h);
				h.MaxNegativeTurnAngerChange = rec.Get<int>("Max Negative Anger Change", h);
				h.OurHomeworldLost = rec.Get<int>("Homeworld Lost", h);
				h.OurPlanetLost = rec.Get<int>("Any Planet Lost", h);
				h.PlanetColonized = rec.Get<int>("Any Planet Colonized", h);
				h.OurPlanetCaptured = rec.Get<int>("Any Our Planet Captured", h);
				h.EnemyPlanetCaptured = rec.Get<int>("Any Enemy Planet Captured", h);
				h.OurShipLost = rec.Get<int>("Any Ship Lost", h);
				h.AnyShipConstructed = rec.Get<int>("Any Ship Constructed", h);
				h.TreatyWar = rec.Get<int>("New Treaty War", h);
				h.TreatyNonIntercourse = rec.Get<int>("New Treaty Non Intercourse", h);
				h.TreatyNone = rec.Get<int>("New Treaty None", h);
				h.TreatyNonAggression = rec.Get<int>("New Treaty Non Aggression", h);
				h.TreatySubjugationSubordinate = rec.Get<int>("New Treaty Subjugated (Sub)", h);
				h.TreatyProtectorateSubordinate = rec.Get<int>("New Treaty Protectorate (Sub)", h);
				h.TreatySubjugationDominant = rec.Get<int>("New Treaty Subjugated (Dom)", h);
				h.TreatyProtectorateDominant = rec.Get<int>("New Treaty Protectorate (Dom)", h);
				h.TreatyTrade = rec.Get<int>("New Treaty Trade", h);
				h.TreatyTradeAndResearch = rec.Get<int>("New Treaty Trade and Research", h);
				h.TreatyMilitaryAlliance = rec.Get<int>("New Treaty Military Alliance", h);
				h.TreatyPartnership = rec.Get<int>("New Treaty Partnership", h);
				h.BattleInSystemWin = rec.Get<int>("Battle in System - Win", h);
				h.BattleInSystemLoss = rec.Get<int>("Battle in System - Loss", h);
				h.BattleInSystemStalemate = rec.Get<int>("Battle in System - Stalemate", h);
				h.BattleInSectorWin = rec.Get<int>("Battle in Sector - Win", h);
				h.BattleInSectorLoss = rec.Get<int>("Battle in Sector - Loss", h);
				h.BattleInSectorStalemate = rec.Get<int>("Battle in Sector - Stalemate", h);
				h.EnemyShipInSystem = rec.Get<int>("Enemy Ship in System", h);
				h.EnemyShipInSector = rec.Get<int>("Enemy Ship in Sector", h);
				h.OurShipInSector = rec.Get<int>("Our Ship in Sector", h);
				h.OurShipInSystem = rec.Get<int>("Our Ship in System", h);
				h.EnemyTroopsOnPlanet = rec.Get<int>("Enemy Troops on Planet", h);
				h.OurTroopOnPlanet = rec.Get<int>("Our Troops on Planet", h);
				h.OneMillionPopulationKilled = rec.Get<int>("1M Population Killed", h);
				h.OurShipLostInSystem = rec.Get<int>("Ship Lost in System", h);
				h.ShipConstructed = rec.Get<int>("Ship Constructed", h);
				h.FacilityConstructed = rec.Get<int>("Facility Constructed", h);
				h.OurPlanetPlagued = rec.Get<int>("Planet Plagued", h);
				h.NaturalTurnAngerChangeOurRace = rec.Get<int>("Natural Decrease", h);
				h.NaturalTurnAngerChangeOtherRaces = rec.Get<int>("Natural Decrease for Other Races", h);

				yield return h;
			}
		}
	}
}