using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System.Linq;
using Tech = FrEee.Game.Objects.Technology.Technology;

#nullable enable

namespace FrEee.Game.Objects.Civilization.Diplomacy.Clauses
{
	/// <summary>
	/// A treaty clause which grants an empire an occasional breakthrough
	/// if the other empire has researched or is researching
	/// any technology that the first empire does not already know.
	/// </summary>
	public class CooperativeResearchClause : Clause
	{
		public CooperativeResearchClause(Empire giver, Empire receiver)
			: base(giver, receiver)
		{
		}

		public override string BriefDescription => "Cooperative Research";

		public override string FullDescription => $"{Receiver.WeOrName()} has a {Mod.Current.Settings.CooperativeResearchBreakthroughChance}% chance of achieving a breakthrough each turn in any technology that {Giver.WeOrName(false)} {(Giver == Empire.Current ? "are" : "is")} more advanced in, provided {Receiver.WeOrName()} meet{(Receiver == Empire.Current ? "" : "s")} the prerequisites.";

		public override void PerformAction()
		{
			foreach (var tech in Receiver.UnlockedItems.OfType<Tech>())
			{
				var hasLevels = Receiver.ResearchedTechnologies[tech];
				var hasProgress = Receiver.ResearchProgress.SingleOrDefault(p => p.Item == tech);
				var hasAmount = hasProgress == null ? 0 : hasProgress.Value;
				var giveLevels = Giver.ResearchedTechnologies[tech];
				var giveProgress = Giver.ResearchProgress.SingleOrDefault(p => p.Item == tech);
				var giveAmount = giveProgress == null ? 0 : giveProgress.Value;
				if (giveLevels > hasLevels || giveLevels == hasLevels && giveAmount > hasAmount)
				{
					if (RandomHelper.Next(100d) < Mod.Current.Settings.CooperativeResearchBreakthroughChance)
					{
						// breakthrough!
						// get 1 level if a full level or more below
						// get up to current progress if working on same level
						if (hasProgress == null)
						{
							// create a progress object so we can track progress on the learned tech
							Receiver.Research(tech, 0);
							hasProgress = Receiver.ResearchProgress.Single(p => p.Item == tech);
						}
						if (giveLevels > hasLevels)
						{
							Receiver.ResearchedTechnologies[tech]++;
							hasProgress.Value = 0;
						}
						else
							hasProgress.Value = giveProgress?.Value ?? 0;
						Giver.Log.Add(Receiver.CreateLogMessage("The " + Receiver + " has achieved a breakthrough in " + tech + " due to our cooperative research treaty.", LogMessages.LogMessageType.Generic));
						Receiver.Log.Add(tech.CreateLogMessage("We have achieved a breakthrough in " + tech + " due to our cooperative research treaty with the " + Giver + ".", LogMessages.LogMessageType.ResearchComplete));
					}
				}
			}
		}
	}
}
