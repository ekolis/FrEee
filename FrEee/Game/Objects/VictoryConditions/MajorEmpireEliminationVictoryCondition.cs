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
	/// Eliminate all other major empires. Minor empires may survive.
	/// </summary>
	public class MajorEmpireEliminationVictoryCondition : IVictoryCondition
	{
		public double GetProgress(Empire emp)
		{
			if (emp.IsDefeated)
				return 0;
			return (double)Galaxy.Current.Empires.Where(e => e.IsDefeated && !e.IsMinorEmpire).Count() / (double)Galaxy.Current.Empires.Where(e => !e.IsMinorEmpire).Count();
		}

		public string GetVictoryMessage(Empire emp)
		{
			return "The " + emp + " has eliminated all other major empires and conquered the galaxy! All hail " + emp.LeaderName + ", ruler of the cosmos!";
		}

		public string GetDefeatMessage(Empire emp, IEnumerable<Empire> winners)
		{
			return "The " + emp + " is defeated! " + winners.Single() + " has conquered the galaxy!";
		}
	}
}
