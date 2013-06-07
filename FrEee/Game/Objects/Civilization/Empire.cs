using System;
using System.Drawing;
using System.Linq;
using FrEee.Game.Interfaces;
using FrEee.Utility;
using System.Collections.Generic;
using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;
using System.IO;
using FrEee.Modding;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Commands;
using Tech = FrEee.Game.Objects.Technology.Technology;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// An empire attempting to rule the galaxy.
	/// </summary>
	[Serializable]
	public class Empire : INamed, ICommandable<Empire>
	{
		/// <summary>
		/// The current empire being controlled by the player.
		/// </summary>
		public static Empire Current
		{
			get
			{
				if (Galaxy.Current == null)
					return null;
				return Galaxy.Current.CurrentEmpire;
			}
		}

		public Empire()
		{
			StoredResources = new Resources();

			// TODO - make starting resources moddable
			StoredResources.Add("Minerals", 50000);
			StoredResources.Add("Organics", 50000);
			StoredResources.Add("Radioactives", 50000);

			Commands = new List<ICommand>();
			KnownDesigns = new List<IDesign>();
			Log = new List<LogMessage>();
			ResearchedTechnologies = new SafeDictionary<Technology.Technology, int>();
			AccumulatedResearch = new SafeDictionary<Tech, int>();
			ResearchSpending = new SafeDictionary<Technology.Technology, int>();
			ResearchQueue = new List<Technology.Technology>();
		}

		/// <summary>
		/// The name of the empire.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The title of the emperor.
		/// </summary>
		public string EmperorTitle { get; set; }

		/// <summary>
		/// The name of the emperor.
		/// </summary>
		public string EmperorName { get; set; }

		/// <summary>
		/// The folder (under Pictures/Races) where the empire's shipset is located.
		/// </summary>
		public string ShipsetPath { get; set; }

		/// <summary>
		/// The empire's flag.
		/// </summary>
		public Image Flag
		{
			get
			{
				if (Mod.Current.RootPath != null)
				{
					return
						Pictures.GetCachedImage(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", ShipsetPath, "Flag")) ??
						Pictures.GetCachedImage(Path.Combine("Pictures", "Races", ShipsetPath, "Flag")) ??
						Pictures.GetGenericImage(typeof(Empire));
				}
				else
				{
					return
						Pictures.GetCachedImage(Path.Combine("Pictures", "Races", ShipsetPath, "Flag")) ??
						Pictures.GetGenericImage(typeof(Empire));
				}
			}
		}

		/// <summary>
		/// The color used to represent this empire's star systems on the galaxy map.
		/// </summary>
		public Color Color { get; set; }

		public override string ToString()
		{
			return Name;
		}

		/// <summary>
		/// The resources stored by the empire.
		/// </summary>
		public Resources StoredResources { get; set; }

		/// <summary>
		/// Commands issued by the player this turn.
		/// </summary>
		public IList<ICommand> Commands { get; private set; }

		/// <summary>
		/// The empire's resource income.
		/// </summary>
		/// <param name="galaxy"></param>
		/// <returns></returns>
		public Resources Income
		{
			get
			{
				// TODO - take into account maintenance costs
				return ColonizedPlanets.Select(p => p.Income).Aggregate((r1, r2) => r1 + r2);
			}
		}

		/// <summary>
		/// Finds star systems explored by the empire.
		/// </summary>
		public IEnumerable<StarSystem> ExploredStarSystems
		{
			get { return Galaxy.Current.StarSystemLocations.Select(ssl => ssl.Item).Where(sys => sys.ExploredByEmpires.Contains(this)); }
		}

		/// <summary>
		/// Planets colonized by the empire.
		/// </summary>
		public IEnumerable<Planet> ColonizedPlanets
		{
			get
			{
				return Galaxy.Current.StarSystemLocations.Select(ssl => ssl.Item).SelectMany(ss => ss.FindSpaceObjects<Planet>(p => p.Owner == this).Flatten());
			}
		}

		/// <summary>
		/// Designs known by this empire.
		/// </summary>
		public ICollection<IDesign> KnownDesigns { get; private set; }

		/// <summary>
		/// Not the empire index (1 for player 1, etc.), just the index in the list of orderable objects.
		/// </summary>
		public int ID
		{
			get;
			set;
		}

		/// <summary>
		/// The empire owns itself, of course.
		/// </summary>
		public Empire Owner
		{
			get { return this; }
		}

		/// <summary>
		/// Empire history log.
		/// </summary>
		public IList<LogMessage> Log { get; set; }

		/// <summary>
		/// Technologies that have been researched by this empire and the levels they have been researched to.
		/// </summary>
		public IDictionary<Technology.Technology, int> ResearchedTechnologies
		{
			get;
			private set;
		}

		/// <summary>
		/// Progress towards completing next levels of techs.
		/// </summary>
		public IEnumerable<Progress<Tech>> ResearchProgress
		{
			get
			{
				if (researchProgress == null)
				{
					ComputeResearchProgress();
				}
				return researchProgress;
			}
		}

		/// <summary>
		/// Recomputes the empire's research progress stats.
		/// Call this when you modify the research spending priorities or the research queue.
		/// </summary>
		public void ComputeResearchProgress()
		{
			researchProgress = AvailableTechnologies.Select(t => GetResearchProgress(t, ResearchedTechnologies[t] + 1)).ToArray();
		}

		private Progress<Tech>[] researchProgress;

		public Progress<Tech> GetResearchProgress(Tech tech, int level)
		{
			var totalRP = Income["Research"];
			var pctSpending = AvailableTechnologies.Sum(t => ResearchSpending[t]);
			var queueSpending = 100 - pctSpending;
			return new Progress<Tech>(tech, AccumulatedResearch[tech], tech.GetLevelCost(level),
					ResearchSpending[tech] * totalRP / 100, GetResearchQueueDelay(tech, level), queueSpending * totalRP / 100);
		}

		/// <summary>
		/// How long until this empire can research a tech in its queue?
		/// </summary>
		/// <param name="tech"></param>
		/// <returns></returns>
		private double? GetResearchQueueDelay(Tech tech, int level)
		{
			if (!ResearchQueue.Contains(tech))
				return null;
			var totalRP = Income["Research"];
			var pctSpending = AvailableTechnologies.Sum(t => ResearchSpending[t]);
			var queueSpending = 100 - pctSpending;
			var foundLevels = new Dictionary<Tech, int>(ResearchedTechnologies);
			int costBefore = 0;
			foreach (var queuedTech in ResearchQueue)
			{
				foundLevels[queuedTech]++;
				if (queuedTech == tech && foundLevels[queuedTech] == level)
					break; // found the tech and level we ant
				costBefore += tech.GetLevelCost(foundLevels[queuedTech]);
			}
			if (queueSpending == 0)
				return double.PositiveInfinity;
			return costBefore / (queueSpending * totalRP / 100);
		}

		/// <summary>
		/// Accumulated research points.
		/// </summary>
		public IDictionary<Technology.Technology, int> AccumulatedResearch
		{
			get;
			private set;
		}

		/// <summary>
		/// Research spending as a percentage of budget.
		/// </summary>
		public IDictionary<Technology.Technology, int> ResearchSpending
		{
			get;
			private set;
		}

		/// <summary>
		/// Queue for unallocated research spending.
		/// </summary>
		public IList<Technology.Technology> ResearchQueue
		{
			get;
			private set;
		}

		/// <summary>
		/// The empire's research priorities for this turn.
		/// </summary>
		public ResearchCommand ResearchCommand
		{
			get
			{
				return Commands.OfType<ResearchCommand>().SingleOrDefault();
			}
			set
			{
				Commands.Remove(ResearchCommand);
				Commands.Add(value);
			}
		}

		/// <summary>
		/// Determines if something has been unlocked in the tech tree.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool HasUnlocked(IResearchable item)
		{
			return item.TechnologyRequirements.All(r => ResearchedTechnologies[r.Technology] >= r.Level);
		}

		/// <summary>
		/// Spends research points on a technology.
		/// </summary>
		/// <param name="tech"></param>
		/// <param name="points"></param>
		public void Research(Technology.Technology tech, int points)
		{
			var oldlvl = ResearchedTechnologies[tech];
			AccumulatedResearch[tech] += points;
			var newStuff = new List<IResearchable>();
			while (AccumulatedResearch[tech] >= tech.GetNextLevelCost(this))
			{
				// advanced a level!
				AccumulatedResearch[tech] -= tech.GetNextLevelCost(this);
				newStuff.AddRange(tech.GetExpectedResults(this));
				ResearchedTechnologies[tech]++;
			}
			if (ResearchedTechnologies[tech] > oldlvl)
				Log.Add(tech.CreateLogMessage("We have advanced from level " + oldlvl + " to level " + ResearchedTechnologies[tech] + " in " + tech + "!"));
			foreach (var item in newStuff)
				Log.Add(item.CreateLogMessage("We have unlocked a new " + item.ResearchGroup.ToLower() + ", the " + item + "!"));
			// if it was in the queue, remove the first instance
			ResearchQueue.Remove(tech);
		}

		/// <summary>
		/// Technologies which are available for research.
		/// </summary>
		public IEnumerable<Technology.Technology> AvailableTechnologies
		{
			get
			{
				return Galaxy.Current.Referrables.OfType<Technology.Technology>().Where(
					t => HasUnlocked(t) && ResearchedTechnologies[t] < t.MaximumLevel);
			}
		}

		/// <summary>
		/// Unlocked research items such as component and facility templates.
		/// </summary>
		public IEnumerable<IResearchable> UnlockedItems
		{
			get
			{
				return Galaxy.Current.Referrables.OfType<IResearchable>().Where(r => HasUnlocked(r));
			}
		}
	}
}
