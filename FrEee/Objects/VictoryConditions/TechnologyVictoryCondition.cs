using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Modding;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Objects.VictoryConditions
{
	/// <summary>
	/// Research a specified percentage of all non-racial non-unique tech levels.
	/// </summary>
	public class TechnologyVictoryCondition : IVictoryCondition
	{
		public TechnologyVictoryCondition(int percentage)
		{
			Percentage = percentage;
		}

		/// <summary>
		/// The percentage needed to achieve.
		/// </summary>
		public int Percentage { get; set; }

		public string GetDefeatMessage(Empire emp, IEnumerable<Empire> winners)
		{
			return "The " + emp + " has failed to keep up with the latest technology! The following empires have achieved a technology victory: " + string.Join(", ", winners.Select(e => e.ToString()).ToArray()) + ".";
		}

		public double GetProgress(Empire emp)
		{
			if (emp.IsDefeated)
				return 0;
			return (double)emp.ResearchedTechnologies.Where(t => !t.Key.IsRacial && !t.Key.IsUnique).Sum(t => t.Value) / (double)Mod.Current.Technologies.Where(t => !t.IsRacial && !t.IsUnique).Sum(t => t.MaximumLevel);
		}

		public string GetVictoryMessage(Empire emp)
		{
			return "The " + emp + " has become a technological powerhouse! All hail " + emp.LeaderName + ", leader of the most advanced empire in the galaxy!";
		}
	}
}