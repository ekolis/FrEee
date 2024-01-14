using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using System.Collections.Generic;

namespace FrEee.Objects.VictoryConditions;

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

	public string GetDefeatMessage(Empire emp, IEnumerable<Empire> winners)
	{
		return "The " + emp + " has failed to stand the test of time!";
	}

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
}