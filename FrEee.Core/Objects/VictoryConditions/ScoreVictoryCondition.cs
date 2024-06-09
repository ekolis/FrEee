using FrEee.Objects.Civilization;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Objects.VictoryConditions;

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

	public string GetDefeatMessage(Empire emp, IEnumerable<Empire> winners)
	{
		return "The " + emp + " has stumbled where others flourished! The following empires have achieved a score victory: " + string.Join(", ", winners.Select(e => e.ToString()).ToArray()) + ".";
	}

	public double GetProgress(Empire emp)
	{
		if (emp.IsDefeated)
			return 0;
		return (double)(int)(emp.Score) / (double)(int)Score;
	}

	public string GetVictoryMessage(Empire emp)
	{
		return "The " + emp + " has grown into a mighty power! All hail " + emp.LeaderName + ", leader of a great empire!";
	}
}