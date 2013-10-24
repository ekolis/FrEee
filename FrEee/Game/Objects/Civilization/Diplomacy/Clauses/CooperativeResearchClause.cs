using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility.Extensions;
using FrEee.Modding;
using Tech = FrEee.Game.Objects.Technology.Technology;
using FrEee.Utility;

namespace FrEee.Game.Objects.Civilization.Diplomacy.Clauses
{
	/// <summary>
	/// A treaty clause which grants an empire an occasional breakthrough
	/// if the other empire has researched or is researching
	/// any technology that the first empire does not already know.
	/// </summary>
	public class CooperativeResearchClause : Clause
	{
		protected CooperativeResearchClause(Empire giver, Empire receiver)
			: base(giver, receiver)
		{
		}

		public override void PerformAction()
		{
			foreach (var tech in Receiver.UnlockedItems.OfType<Tech>())
			{
				var hasLevels = Receiver.ResearchedTechnologies[tech];
				var hasProgress = Receiver.ResearchProgress.Single(p => p.Item == tech);
				var giveLevels = Giver.ResearchedTechnologies[tech];
				var giveProgress = Giver.ResearchProgress.Single(p => p.Item == tech);
				if (giveLevels > hasLevels || giveLevels == hasLevels && giveProgress.Value > hasProgress.Value)
				{
					if (RandomHelper.Next(100d) < Mod.Current.Settings.CooperativeResearchBreakthroughChance)
					{
						// breakthrough!
						// get 1 level if a full level or more below
						// get up to current progress if working on same level
						if (giveLevels > hasLevels)
						{
							Receiver.ResearchedTechnologies[tech]++;
							hasProgress.Value = 0;
						}
						else
							hasProgress.Value = giveProgress.Value;
						Giver.Log.Add(Receiver.CreateLogMessage("The " + Receiver + " has achieved a breakthrough in " + tech + " due to our cooperative research treaty."));
						Receiver.Log.Add(tech.CreateLogMessage("We have achieved a breakthrough in " + tech + " due to our cooperative research treaty with the " + Giver + "."));
					}
				}
			}
		}

		public override string Description
		{
			get
			{
				return Receiver.WeOrName() + " has a " + Mod.Current.Settings.CooperativeResearchBreakthroughChance + "% chance of achieving a breakthrough each turn in any technology that " + Giver.WeOrName(false) + " " + (Giver == Empire.Current ? "are" : "is") + " more advanced in, provided " + Receiver.WeOrName() + " meet" + (Receiver == Empire.Current ? "" : "s") + " the prerequisites.";
			}
		}
	}
}
