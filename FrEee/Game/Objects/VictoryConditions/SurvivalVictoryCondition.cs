using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.VictoryConditions
{
	/// <summary>
	/// Survive for a specified number of turns.
	/// </summary>
	public class SurvivalVictoryCondition : IVictoryCondition
	{
		public SurvivalVictoryCondition(int turns)
		{
			Turns = turns;
		}

		/// <summary>
		/// The number of turns needed to survive.
		/// </summary>
		public int Turns { get; set; }

		public double GetProgress(Empire emp)
		{
			if (emp.IsDefeated)
				return 0;
			return (double)(Galaxy.Current.TurnNumber - 1) / (double)Turns;
		}

		public string GetVictoryMessage(Empire emp)
		{
			return "The " + emp + " has stood the test of time! All hail " + emp.LeaderName + ", leader of a great empire!";
		}

		public string GetDefeatMessage(Empire emp, IEnumerable<Empire> winners)
		{
			return "The " + emp + " has failed to stand the test of time!";
		}
	}
}
