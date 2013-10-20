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
	/// Reach a specified score.
	/// </summary>
	public class ScoreVictoryCondition : IVictoryCondition
	{
		public ScoreVictoryCondition(long score)
		{
			Score = score;
		}

		/// <summary>
		/// The score needed to achieve.
		/// </summary>
		public long Score { get; set; }

		public double GetProgress(Empire emp)
		{
			if (emp.IsDefeated)
				return 0;
			return (double)(emp.Score) / (double)Score;
		}

		public string GetVictoryMessage(Empire emp)
		{
			return "The " + emp + " has grown into a mighty power! All hail " + emp.LeaderName + ", leader of a great empire!";
		}

		public string GetDefeatMessage(Empire emp, IEnumerable<Empire> winners)
		{
			return "The " + emp + " has stumbled where others flourished! The following empires have achieved a score victory: " + string.Join(", ", winners.Select(e => e.ToString()).ToArray()) + ".";
		}
	}
}
