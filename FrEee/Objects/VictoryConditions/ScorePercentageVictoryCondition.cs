using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Objects.VictoryConditions
{
	/// <summary>
	/// Reach a specified percentage of the second place player's score.
	/// </summary>
	public class ScorePercentageVictoryCondition : IVictoryCondition
	{
		public ScorePercentageVictoryCondition(int percentage)
		{
			Percentage = percentage;
		}

		/// <summary>
		/// The percentage needed to achieve.
		/// </summary>
		public int Percentage { get; set; }

		public string GetDefeatMessage(Empire emp, IEnumerable<Empire> winners)
		{
			return "The " + emp + " has stumbled where others flourished! The " + winners.Single() + " has achieved a score percentage victory.";
		}

		public double GetProgress(Empire emp)
		{
			if (emp.IsDefeated)
				return 0;
			var secondPlace = The.Game.Empires.OrderByDescending(e => e.Scores).ElementAtOrDefault(1);
			if (secondPlace == null)
				return double.PositiveInfinity;
			return (double)(int)emp.Score / ((double)(int)secondPlace.Score * (double)Percentage / 100d);
		}

		public string GetVictoryMessage(Empire emp)
		{
			return "The " + emp + " has vastly outstripped her peers! All hail " + emp.LeaderName + ", leader of a great empire!";
		}
	}
}