using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Modding;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Space
{
	public class GalaxySettings
	{
		public GalaxySettings()
		{
			TurnNumber = 1;
			VictoryConditions = new List<IVictoryCondition>();
			Mod = Mod.Current;
		}

		/// <summary>
		/// Should players have an omniscient view of all explored systems?
		/// Does not prevent cloaking from working; this is just basic sight.
		/// Also does not give battle reports for other empires' battles.
		/// </summary>
		public bool OmniscientView { get; set; }

		/// <summary>
		/// Model to use for standard planetary mining.
		/// </summary>
		public MiningModel StandardMiningModel { get; set; }

		/// <summary>
		/// Model to use for remote mining.
		/// </summary>
		public MiningModel RemoteMiningModel { get; set; }

		public int MinPlanetValue { get; set; }

		public int MinSpawnedPlanetValue { get; set; }

		public int MaxSpawnedPlanetValue { get; set; }

		public int MaxPlanetValue { get; set; }

		public int MinAsteroidValue { get; set; }

		public int MinSpawnedAsteroidValue { get; set; }

		public int MaxSpawnedAsteroidValue { get; set; }

		/// <summary>
		/// Who can view empire scores?
		/// </summary>
		public ScoreDisplay ScoreDisplay { get; set; }

		/// <summary>
		/// Is this a single player game? If so, autoprocess the turn after the player takes his turn.
		/// </summary>
		public bool IsSinglePlayer { get; set; }

		/// <summary>
		/// The mod being played.
		/// </summary>
		public Mod Mod { get; set; }

		/// <summary>
		/// The game name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Technology research cost formula.
		/// Low = Level * BaseCost
		/// Medium = BaseCost for level 1, Level ^ 2 * BaseCost / 2 otherwise
		/// Hight = Level ^ 2 * BaseCost
		/// </summary>
		public TechnologyCost TechnologyCost { get; set; }

		/// <summary>
		/// The current turn number.
		/// </summary>
		public int TurnNumber { get; set; }

		public int Width
		{
			get;
			set;
		}

		public int Height
		{
			get;
			set;
		}

		/// <summary>
		/// Game victory conditions.
		/// </summary>
		public IList<IVictoryCondition> VictoryConditions { get; set; }

		/// <summary>
		/// Delay in turns before victory conditions take effect.
		/// </summary>
		public int VictoryDelay { get; set; }

		/// <summary>
		/// Is this a "humans vs. AI" game?
		/// </summary>
		public bool IsHumansVsAI { get; set; }

		/// <summary>
		/// Allowed trades in this game.
		/// </summary>
		public AllowedTrades AllowedTrades { get; set; }

		public bool IsSurrenderAllowed { get; set; }

		public bool IsIntelligenceAllowed { get; set; }

		public bool IsAnalysisAllowed { get; set; }

		public bool CanColonizeOnlyBreathable { get; set; }

		public bool CanColonizeOnlyHomeworldSurface { get; set; }
	}
}
