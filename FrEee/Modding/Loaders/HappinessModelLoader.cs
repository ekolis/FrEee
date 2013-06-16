using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads happiness models from Happiness.txt.
	/// </summary>
	public class HappinessModelLoader : ILoader
	{
		public void Load(DataFile df, Mod mod)
		{
			foreach (var rec in df.Records)
			{
				var h = new HappinessModel();
				mod.HappinessModels.Add(h);

				int index = -1;

				h.Name = rec.GetString("Name", ref index, true);
				h.Description = rec.GetString("Description", ref index);
				h.MaxPositiveTurnAngerChange = rec.GetInt("Max Positive Anger Change", ref index);
				h.MaxNegativeTurnAngerChange = rec.GetInt("Max Negative Anger Change", ref index);
				h.OurHomeworldLost = rec.GetInt("Homeworld Lost", ref index);
				h.OurPlanetLost = rec.GetInt("Any Planet Lost", ref index);
				h.PlanetColonized = rec.GetInt("Any Planet Colonizer", ref index);
				h.OurPlanetCaptured = rec.GetInt("Any Our Planet Captured", ref index);
				h.EnemyPlanetCaptured = rec.GetInt("Any Enemy Planet Captured", ref index);
				h.OurShipLost = rec.GetInt("Any Ship Lost", ref index);
				h.AnyShipConstructed = rec.GetInt("Any Ship Constructed", ref index);
				h.TreatyWar = rec.GetInt("New Treaty War", ref index);
				h.TreatyNonIntercourse = rec.GetInt("New Treaty Non Intercourse", ref index);
				h.TreatyNone = rec.GetInt("New Treaty None", ref index);
				h.TreatyNonAggression = rec.GetInt("New Treaty Non Aggression", ref index);
				h.TreatySubjugationSubordinate = rec.GetInt("New Treaty Subjugated (Sub)", ref index);
				h.TreatyProtectorateSubordinate = rec.GetInt("New Treaty Protectorate (Sub)", ref index);
				h.TreatySubjugationDominant = rec.GetInt("New Treaty Subjugated (Dom)", ref index);
				h.TreatyProtectorateDominant = rec.GetInt("New Treaty Protectorate (Dom)", ref index);
				h.TreatyTrade = rec.GetInt("New Treaty Trade", ref index);
				h.TreatyTradeAndResearch = rec.GetInt("New Treaty Trade and Research", ref index);
				h.TreatyMilitaryAlliance = rec.GetInt("New Treaty Military Alliance", ref index);
				h.TreatyPartnership = rec.GetInt("New Treaty Partnership", ref index);
				h.BattleInSystemWin = rec.GetInt("Battle in System - Win", ref index);
				h.BattleInSystemLoss = rec.GetInt("Battle in System - Loss", ref index);
				h.BattleInSystemStalemate = rec.GetInt("Battle in System - Stalemate", ref index);
				h.BattleInSectorWin = rec.GetInt("Battle in Sector - Win", ref index);
				h.BattleInSectorLoss = rec.GetInt("Battle in Sector - Loss", ref index);
				h.BattleInSectorStalemate = rec.GetInt("Battle in Sector - Stalemate", ref index);
				h.EnemyShipInSystem = rec.GetInt("Enemy Ship in System", ref index);
				h.EnemyShipInSector = rec.GetInt("Enemy Ship in Sector", ref index);
				h.OurShipInSector = rec.GetInt("Our Ship in Sector", ref index);
				h.OurShipInSystem = rec.GetInt("Our Ship in System", ref index);
				h.EnemyTroopsOnPlanet = rec.GetInt("Enemy Troops on Planet", ref index);
				h.OurTroopOnPlanet = rec.GetInt("Our Troops on Planet", ref index);
				h.OneMillionPopulationKilled = rec.GetInt("1M Population Killed", ref index);
				h.OurShipLostInSystem = rec.GetInt("Ship Lost In System", ref index);
				h.ShipConstructed = rec.GetInt("Ship Constructed", ref index);
				h.FacilityConstructed = rec.GetInt("Facility Constructed", ref index);
				h.OurPlanetPlagued = rec.GetInt("Planet Plagued", ref index);
				h.NaturalTurnAngerChangeOurRace = rec.GetInt("Natural Decrease", ref index);
				h.NaturalTurnAngerChangeOtherRaces = rec.GetInt("Natural Decrease for Other Races", ref index);
			}
		}
	}
}
