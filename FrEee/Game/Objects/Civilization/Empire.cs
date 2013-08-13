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
using AI = FrEee.Game.Objects.AI.EmpireAI;
using FrEee.Game.Objects.AI;
using FrEee.Game.Objects.Abilities;
using FrEee.Modding.Templates;
using FrEee.Game.Objects.Vehicles;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// An empire attempting to rule the galaxy.
	/// </summary>
	[Serializable]
	public class Empire : INamed, ICommandable, IAbilityObject, IPictorial, IComparable<Empire>, IComparable
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
			StoredResources = new ResourceQuantity();
			IntrinsicResourceStorage = new ResourceQuantity();
			Commands = new List<ICommand>();
			KnownDesigns = new List<IDesign>();
			Log = new List<LogMessage>();
			ResearchedTechnologies = new SafeDictionary<Technology.Technology, int>();
			AccumulatedResearch = new SafeDictionary<Tech, int>();
			ResearchSpending = new SafeDictionary<Technology.Technology, int>();
			ResearchQueue = new List<Technology.Technology>();
			UniqueTechsFound = new List<string>();
		}

		/// <summary>
		/// The name of the empire.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The name and/or title of the leader of the empire.
		/// </summary>
		public string LeaderName { get; set; }

		/// <summary>
		/// The native race of this empire.
		/// </summary>
		public Race PrimaryRace { get; set; }

		/// <summary>
		/// Traits of this empire.
		/// </summary>
		public IList<Trait> Traits { get; private set; }

		/// <summary>
		/// The name of the insignia picture file, relative to Pictures/Insignia.
		/// </summary>
		public string InsigniaName { get; set; }

		/// <summary>
		/// The name of the shipset, relative to Pictures/Shipsets.
		/// </summary>
		public string ShipsetPath { get; set; }

		/// <summary>
		/// The name of the leader's image file, relative to Pictures/Leaders.
		/// </summary>
		public string LeaderPortraitName { get; set; }

		/// <summary>
		/// The AI which controls the behavior of empires of this race.
		/// </summary>
		public EmpireAI AI { get; set; }

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
		public ResourceQuantity StoredResources { get; set; }

		/// <summary>
		/// Commands issued by the player this turn.
		/// </summary>
		public IList<ICommand> Commands { get; private set; }

		/// <summary>
		/// The empire's resource income, not including maintenance costs.
		/// </summary>
		/// <param name="galaxy"></param>
		/// <returns></returns>
		public ResourceQuantity GrossIncome
		{
			get
			{
				if (!ColonizedPlanets.Any())
					return new ResourceQuantity();
				return ColonizedPlanets.Sum(p => p.Income);
				// TODO - remote mining and raw resource/points generation
			}
		}

		/// <summary>
		/// Resources the empire spends on maintenance.
		/// </summary>
		public ResourceQuantity Maintenance
		{
			get
			{
				// TODO - facility/unit maintenance?
				return OwnedSpaceObjects.OfType<AutonomousSpaceVehicle>().Sum(v => v.MaintenanceCost);
			}
		}

		/// <summary>
		/// Gross income less maintenance.
		/// </summary>
		public ResourceQuantity NetIncome
		{
			get { return GrossIncome - Maintenance; }
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
		/// Empires don't really have owners...
		/// They could be said to own themselves but it makes more sense for referencing purposes to have no owner.
		/// </summary>
		public Empire Owner
		{
			get { return null; }
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
			var totalRP = NetIncome[Resource.Research] + BonusResearch;
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
			var totalRP = NetIncome[Resource.Research];
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
		/// Unique techs that this empire has found from ruins.
		/// </summary>
		public ICollection<string> UniqueTechsFound
		{
			get;
			private set;
		}

		/// <summary>
		/// Determines if something has been unlocked in the tech tree.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool HasUnlocked(IResearchable item)
		{
			if (item == null)
				return true;
			if (item is Tech && ((Tech)item).IsRacial && !this.Abilities.Any(a => a.Name == "Tech Area" && a.Value1 == ((Tech)item).RacialTechID))
				return false; // racial tech that this empire doesn't have the trait for
			if (item is Tech && ((Tech)item).IsUnique && !this.UniqueTechsFound.Any(t => t == ((Tech)item).UniqueTechID))
				return false; // unique tech that this empire hasn't discovered
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
			int advanced = 0;
			while (AccumulatedResearch[tech] >= tech.GetNextLevelCost(this) && ResearchedTechnologies[tech] < tech.MaximumLevel)
			{
				// advanced a level!
				advanced++;
				AccumulatedResearch[tech] -= tech.GetNextLevelCost(this);
				newStuff.AddRange(tech.GetExpectedResults(this));
				ResearchedTechnologies[tech]++;
			}
			if (ResearchedTechnologies[tech] > oldlvl)
				Log.Add(tech.CreateLogMessage("We have advanced from level " + oldlvl + " to level " + ResearchedTechnologies[tech] + " in " + tech + "!"));
			foreach (var item in newStuff)
				Log.Add(item.CreateLogMessage("We have unlocked a new " + item.ResearchGroup.ToLower() + ", the " + item + "!"));

			// if it was in the queue and we advanced a level, remove the first instance (for each level advanced)
			for (int i = 0; i < advanced; i++)
				ResearchQueue.Remove(tech);

			// if tech is maxed out, remove all instances from queue and clear spending on it
			if (ResearchedTechnologies[tech] == tech.MaximumLevel)
			{
				while (ResearchQueue.Contains(tech))
					ResearchQueue.Remove(tech);
				ResearchSpending[tech] = 0;
			}
		}

		/// <summary>
		/// Objects which can be referenced from the client side by this empire.
		/// </summary>
		public IEnumerable<IReferrable> Referrables
		{
			get
			{
				var empnum = Galaxy.Current.Empires.IndexOf(this) + 1;
				var list = new List<IReferrable>();
				list.AddRange(Galaxy.Current.Referrables[0]);
				list.AddRange(Galaxy.Current.Referrables[empnum]);
				return list.Distinct();
			}
		}

		/// <summary>
		/// Technologies which are available for research.
		/// </summary>
		public IEnumerable<Technology.Technology> AvailableTechnologies
		{
			get
			{
				return Referrables.OfType<Technology.Technology>().Where(
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
				return Referrables.OfType<IResearchable>().Where(r => HasUnlocked(r));
			}
		}

		public IEnumerable<Ability> Abilities
		{
			get { return Traits == null ? Enumerable.Empty<Ability>() : Traits.SelectMany(t => t.Abilities); }
		}

		public IEnumerable<Ability> UnstackedAbilities
		{
			get { return Abilities; }
		}

		/// <summary>
		/// The insignia icon for this empire.
		/// </summary>
		public Image Icon
		{
			get { return Pictures.GetIcon(this); }
		}

		/// <summary>
		/// The leader portrait for this empire.
		/// </summary>
		public Image Portrait
		{
			get { return Pictures.GetPortrait(this); }
		}

		/// <summary>
		/// Issues an order to an object.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TOrder"></typeparam>
		/// <param name="order"></param>
		public void IssueOrder<T>(T target, IOrder<T> order)
			where T : IOrderable
		{
			target.AddOrder(order);
			var cmd = new AddOrderCommand<T>(this, target, order);
			Commands.Add(cmd);
		}

		/// <summary>
		/// Belays (cancels) an order.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TOrder"></typeparam>
		/// <param name="order"></param>
		public void BelayOrder<T>(T target, IOrder<T> order)
			where T : IOrderable
		{
			target.RemoveOrder(order);
			var cmd = new RemoveOrderCommand<T>(this, target, order);
			Commands.Add(cmd);
		}

		public void Dispose()
		{
			Galaxy.Current.Unregister(this);
			foreach (var emp in Galaxy.Current.Empires)
				Galaxy.Current.Unregister(this, emp);
		}

		/// <summary>
		/// Is this empire defeated?
		/// An empire is defeated when it no longer controls any space objects.
		/// </summary>
		public bool IsDefeated
		{
			get
			{
				return !Galaxy.Current.FindSpaceObjects<ISpaceObject>(sobj => sobj.Owner == this).Any();
			}
		}

		/// <summary>
		/// Can this empire colonize a planet?
		/// </summary>
		/// <returns></returns>
		public bool CanColonize(Planet planet)
		{
			return UnlockedItems.OfType<ComponentTemplate>().Any(c => c.HasAbility(planet.ColonizationAbilityName));
		}

		/// <summary>
		/// Is this empire hostile to another empire?
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public bool IsHostileTo(Empire emp)
		{
			if (emp == null)
				return false;
			if (emp == this)
				return false;
			return true; // TODO - alliances
		}

		/// <summary>
		/// Intrinsic resource storage capacity of this empire (without components, facilities, etc. that provide the abilities).
		/// </summary>
		public ResourceQuantity IntrinsicResourceStorage { get; private set; }

		/// <summary>
		/// Resource storage capacity of this empire.
		/// </summary>
		public ResourceQuantity ResourceStorage
		{
			get
			{
				var r = new ResourceQuantity();
				r += IntrinsicResourceStorage;
				foreach (var sobj in OwnedSpaceObjects)
				{
					// yes, Aaron did spell it "Mineral", not "Minerals"... we can support both though!
					var min = sobj.GetAbilityValue("Resource Storage - Mineral").ToInt() + sobj.GetAbilityValue("Resource Storage - Minerals").ToInt();
					var org = sobj.GetAbilityValue("Resource Storage - Organics").ToInt();
					var rad = sobj.GetAbilityValue("Resource Storage - Radioactives").ToInt();
					r.Add(Resource.Minerals, min);
					r.Add(Resource.Organics, org);
					r.Add(Resource.Radioactives, rad);
				}
				return r;
			}
		}

		public IEnumerable<ISpaceObject> OwnedSpaceObjects
		{
			get
			{
				return Galaxy.Current.FindSpaceObjects<ISpaceObject>(sobj => sobj.Owner == this).Flatten().Flatten();
			}
		}

		/// <summary>
		/// Bonus research available to spend this turn only.
		/// </summary>
		public int BonusResearch { get; set; }

		/// <summary>
		/// Is this a minor empire? Minor empires cannot use warp points.
		/// </summary>
		public bool IsMinorEmpire { get; set; }

		/// <summary>
		/// Is this empire controlled by a human player?
		/// </summary>
		public bool IsPlayerEmpire { get; set; }

		/// <summary>
		/// The empire's happiness model.
		/// </summary>
		public HappinessModel HappinessModel { get; set; }

		/// <summary>
		/// The empire's culture.
		/// </summary>
		public Culture Culture { get; set; }

		/// <summary>
		/// TODO - implement empire score
		/// </summary>
		public long Score { get { return 0; } }

		public int CompareTo(Empire other)
		{
			throw new NotImplementedException();
		}

		int IComparable<Empire>.CompareTo(Empire other)
		{
			return Name.CompareTo(other.Name);
		}

		public int CompareTo(object obj)
		{
			return Name.CompareTo(obj.ToString());
		}
	}
}
