using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.VictoryConditions
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

		public double GetProgress(Empire emp)
		{
			if (emp.IsDefeated)
				return 0;
			return (double)emp.ResearchedTechnologies.Where(t => t.Key.RacialTechID == 0 && t.Key.UniqueTechID == 0).Sum(t => t.Value) / (double)Mod.Current.Technologies.Where(t => t.RacialTechID == 0 && t.UniqueTechID == 0).Sum(t => t.MaximumLevel);
		}

		public string GetVictoryMessage(Empire emp)
		{
			return "The " + emp + " has become a technological powerhouse! All hail " + emp.LeaderName + ", leader of the most advanced empire in the galaxy!";
		}

		public string GetDefeatMessage(Empire emp, IEnumerable<Empire> winners)
		{
			return "The " + emp + " has failed to keep up with the latest technology! The following empires have achieved a technology victory: " + string.Join(", ", winners.Select(e => e.ToString()).ToArray()) + ".";
		}
	}
}
