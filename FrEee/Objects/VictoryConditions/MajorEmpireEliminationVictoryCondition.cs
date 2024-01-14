using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Objects.VictoryConditions;

/// <summary>
/// Eliminate all other major empires. Minor empires may survive.
/// </summary>
public class MajorEmpireEliminationVictoryCondition : IVictoryCondition
{
	public string GetDefeatMessage(Empire emp, IEnumerable<Empire> winners)
	{
		return "The " + emp + " is defeated! " + winners.Single() + " has conquered the galaxy!";
	}

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
}