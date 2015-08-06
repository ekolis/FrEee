﻿using FrEee.Game.Interfaces;
using FrEee.Modding.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Modding;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// Model for population happiness.
	/// Keeps track of anger modifiers for various events.
	/// Values are in tenths of a percent.
	/// </summary>
	public class HappinessModel : IModObject
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public int MaxPositiveTurnAngerChange { get; set; }

		public int MaxNegativeTurnAngerChange { get; set; }

		public int NaturalTurnAngerChangeOurRace { get; set; }

		public int NaturalTurnAngerChangeOtherRaces { get; set; }

		public int OurHomeworldLost { get; set; }

		public int OurPlanetLost { get; set; }

		public int OurPlanetCaptured { get; set; }

		public int EnemyPlanetCaptured { get; set; }

		public int PlanetColonized { get; set; }

		public int OurShipLost { get; set; }

		public int OurShipLostInSystem { get; set; }

		public int AnyShipConstructed { get; set; }

		public int ShipConstructed { get; set; }

		public int TreatyWar { get; set; }

		public int TreatyNonIntercourse { get; set; }

		public int TreatyNone { get; set; }

		public int TreatyNonAggression { get; set; }

		public int TreatySubjugationSubordinate { get; set; }

		public int TreatySubjugationDominant { get; set; }

		public int TreatyProtectorateSubordinate { get; set; }

		public int TreatyProtectorateDominant { get; set; }

		public int TreatyTrade { get; set; }

		public int TreatyTradeAndResearch { get; set; }

		public int TreatyMilitaryAlliance { get; set; }

		public int TreatyPartnership { get; set; }

		public int BattleInSystemWin { get; set; }

		public int BattleInSystemLoss { get; set; }

		public int BattleInSystemStalemate { get; set; }

		public int BattleInSectorWin { get; set; }

		public int BattleInSectorLoss { get; set; }

		public int BattleInSectorStalemate { get; set; }

		public int EnemyShipInSystem { get; set; }

		public int EnemyShipInSector { get; set; }

		public int OurShipInSystem { get; set; }

		public int OurShipInSector { get; set; }

		public int EnemyTroopsOnPlanet { get; set; }

		public int OurTroopOnPlanet { get; set; }

		public int OneMillionPopulationKilled { get; set; }

		public int FacilityConstructed { get; set; }

		public int OurPlanetPlagued { get; set; }

		public override string ToString()
		{
			return Name;
		}

		public string ModID { get; set; }

		public void Dispose()
		{
			Mod.Current.HappinessModels.Remove(this);
			IsDisposed = true;
		}

		public bool IsDisposed
		{
			get; private set;
		}
	}
}
