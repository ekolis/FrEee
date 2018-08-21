using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Game.Objects.VictoryConditions
{
	/// <summary>
	/// Eliminate all other empires, major or minor.
	/// </summary>
	public class TotalEliminationVictoryCondition : IVictoryCondition
	{
		public string GetDefeatMessage(Empire emp, IEnumerable<Empire> winners)
		{
			return "The " + emp + " is defeated! " + winners.Single() + " has conquered the galaxy!";
		}

		public double GetProgress(Empire emp)
		{
			if (emp.IsDefeated)
				return 0;
			if (Galaxy.Current.Empires.Count == 1)
				return 1;
			return (double)Galaxy.Current.Empires.Where(e => e.IsDefeated).Count() / ((double)Galaxy.Current.Empires.Count - 1);
		}

		public string GetVictoryMessage(Empire emp)
		{
			return "The " + emp + " has eliminated all opposition and conquered the galaxy! All hail " + emp.LeaderName + ", ruler of the cosmos!";
		}
	}
}