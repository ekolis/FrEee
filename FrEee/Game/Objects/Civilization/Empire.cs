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
using FrEee.Game.Objects.AI;
using FrEee.Game.Objects.Abilities;
using FrEee.Modding.Templates;
using FrEee.Game.Objects.Vehicles;
using FrEee.Game.Enumerations;

using System.Dynamic;
using FrEee.Game.Objects.Orders;
using FrEee.Modding.Interfaces;
using FrEee.Game.Objects.Civilization.Diplomacy;
using FrEee.Game.Objects.Civilization.Diplomacy.Clauses;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// An empire attempting to rule the galaxy.
	/// </summary>
	[Serializable]
	public class Empire : INamed, IFoggable, IAbilityObject, IPictorial, IComparable<Empire>, IComparable, IFormulaHost
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
			KnownDesigns = new HashSet<IDesign>();
			Log = new List<LogMessage>();
			ResearchedTechnologies = new NamedDictionary<Technology.Technology, int>();
			AccumulatedResearch = new NamedDictionary<Tech, int>();
			ResearchSpending = new NamedDictionary<Technology.Technology, int>();
			ResearchQueue = new List<Technology.Technology>();
			UniqueTechsFound = new List<string>();
			Memory = new SafeDictionary<long, IFoggable>();
			AINotes = new DynamicDictionary();
			EncounteredEmpires = new HashSet<Empire>();
			EncounteredEmpires.Add(this);
			IncomingMessages = new HashSet<IMessage>();
			SentMessages = new HashSet<IMessage>();
			Waypoints = new List<Waypoint>();
			NumberedWaypoints = new Waypoint[10];
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
		public AI<Empire, Galaxy> AI { get; set; }

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
		/// TODO - should we include tributes here?
		/// </summary>
		/// <param name="galaxy"></param>
		/// <returns></returns>
		public ResourceQuantity GrossIncome
		{
			get
			{
				// shouldn't change except at turn processing...
				if (grossIncome == null || Empire.Current == null)
				{
					if (!ColonizedPlanets.Any())
						grossIncome = new ResourceQuantity();
					else
						grossIncome = ColonizedPlanets.Sum(p => p.GrossIncome);
					// TODO - remote mining and raw resource/points generation
				}
				return grossIncome;
			}
		}

		private ResourceQuantity grossIncome;

		/// <summary>
		/// Resources the empire spends on maintenance.
		/// </summary>
		public ResourceQuantity Maintenance
		{
			get
			{
				// shouldn't change except at turn processing...
				// TODO - facility/unit maintenance?
				if (maintenance == null || Empire.Current == null)
					maintenance = OwnedSpaceObjects.OfType<SpaceVehicle>().Sum(v => v.MaintenanceCost);
				return maintenance;
			}
		}

		private ResourceQuantity maintenance;

		/// <summary>
		/// Gross income less maintenance.
		/// TODO - should we include tributes here?
		/// </summary>
		public ResourceQuantity NetIncome
		{
			get { return GrossIncome - Maintenance; }
		}

		/// <summary>
		/// All construction queues owned by this empire.
		/// </summary>
		public IEnumerable<ConstructionQueue> ConstructionQueues
		{
			get
			{
				return Galaxy.Current.Referrables.OfType<ConstructionQueue>().Where(q => q.Owner == this && q.Container.Sector != null && q.Rate.Any(kvp => kvp.Value > 0));
			}
		}

		/// <summary>
		/// Spending on construction this turn.
		/// </summary>
		public ResourceQuantity ConstructionSpending
		{
			get
			{
				return ConstructionQueues.Sum(q => q.UpcomingSpending);
			}
		}

		/// <summary>
		/// Net income less construction spending.
		/// </summary>
		public ResourceQuantity NetIncomeLessConstruction
		{
			get
			{
				return NetIncome - ConstructionSpending;
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
				return Galaxy.Current.StarSystemLocations.Select(ssl => ssl.Item).SelectMany(ss => ss.FindSpaceObjects<Planet>(p => p.Owner == this));
			}
		}

		/// <summary>
		/// Designs known by this empire.
		/// </summary>
		public ICollection<IDesign> KnownDesigns { get; private set; }

		public long ID
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
		public NamedDictionary<Technology.Technology, int> ResearchedTechnologies
		{
			get;
			internal set;
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
			var firstQueueSpending = 0;
			var cost = tech.GetLevelCost(level);
			if (ResearchQueue.FirstOrDefault() == tech)
				firstQueueSpending = Math.Min(queueSpending * totalRP / 100, cost - AccumulatedResearch[tech]);
			var laterQueueSpending = 0;
			if (ResearchQueue.FirstOrDefault() != tech && ResearchQueue.Contains(tech))
				laterQueueSpending = Math.Min(queueSpending * totalRP / 100, cost - AccumulatedResearch[tech]);
			return new Progress<Tech>(tech, AccumulatedResearch[tech], cost,
					ResearchSpending[tech] * totalRP / 100 + firstQueueSpending, GetResearchQueueDelay(tech, level), laterQueueSpending);
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
					break; // found the tech and level we want
				costBefore += queuedTech.GetLevelCost(foundLevels[queuedTech]);
				if (queuedTech.CurrentLevel == foundLevels[queuedTech] - 1)
					costBefore -= AccumulatedResearch[queuedTech];
			}
			if (queueSpending == 0)
				return double.PositiveInfinity;
			return (double)costBefore / (queueSpending * totalRP / 100d);
		}

		/// <summary>
		/// Accumulated research points.
		/// </summary>
		public NamedDictionary<Technology.Technology, int> AccumulatedResearch
		{
			get;
			private set;
		}

		/// <summary>
		/// Research spending as a percentage of budget.
		/// </summary>
		public NamedDictionary<Technology.Technology, int> ResearchSpending
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
			return item == null || UnlockedItems.Contains(item);
		}

		public bool CheckUnlockStatus(IResearchable item)
		{
			if (item == null)
				return true;
			// TODO - racial/unique tech should just be requirements
			if (item is Tech && ((Tech)item).IsRacial && !this.Abilities().Any(a => a.Rule != null && a.Rule.Matches("Tech Area") && a.Value1 == ((Tech)item).RacialTechID))
				return false; // racial tech that this empire doesn't have the trait for
			if (item is Tech && ((Tech)item).IsUnique && !this.UniqueTechsFound.Any(t => t == ((Tech)item).UniqueTechID))
				return false; // unique tech that this empire hasn't discovered
			return item.UnlockRequirements.All(r => r.IsMetBy(this));
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

			// if we advanced, recheck unlocks
			if (advanced > 0)
				RefreshUnlockedItems();
		}

		/// <summary>
		/// Technologies which are available for research.
		/// </summary>
		public IEnumerable<Technology.Technology> AvailableTechnologies
		{
			get
			{
				return Mod.Current.Technologies.Where(
					t => HasUnlocked(t) && ResearchedTechnologies[t] < t.MaximumLevel);
			}
		}

		/// <summary>
		/// Unlocked research items such as component and facility templates.
		/// </summary>
		[DoNotSerialize]
		public IEnumerable<IResearchable> UnlockedItems
		{
			get
			{
				if (unlockedItems == null)
					RefreshUnlockedItems();
				return unlockedItems;
			}
		}

		private IEnumerable<IResearchable> unlockedItems;

		public void RefreshUnlockedItems()
		{
			unlockedItems = Galaxy.Current.Referrables.OfType<IResearchable>().Where(r => CheckUnlockStatus(r)).ToArray();
		}

		/// <summary>
		/// Empires don't have intrinsic abilities.
		/// </summary>
		public IEnumerable<Ability> IntrinsicAbilities
		{
			get { yield break; }
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
			var cmd = new AddOrderCommand<T>(target, order);
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
			var cmd = new RemoveOrderCommand<T>(target, order);
			Commands.Add(cmd);
		}

		public void Dispose()
		{
			if (IsDisposed)
				return;
			Galaxy.Current.UnassignID(this);
			if (Galaxy.Current.Empires.Contains(this))
				Galaxy.Current.Empires[Galaxy.Current.Empires.IndexOf(this)] = null;
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
		/// Is this empire hostile to another empire in a particular system?
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public bool IsHostileTo(Empire emp, StarSystem sys)
		{
			if (emp == null)
				return false;
			if (emp == this)
				return false;
			var alliance = GivenTreatyClauses[emp].OfType<AllianceClause>().MaxOrDefault(c => c.AllianceLevel);
			if (alliance >= AllianceLevel.NonAggression)
				return false;
			if (alliance >= AllianceLevel.NeutralZone)
			{
				if (sys == null)
					return true; // assume hostility if unknown system
				return sys.FindSpaceObjects<Planet>().Any(p => p.Owner == this);
			}
			return true;
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
					foreach (var res in Resource.All.Where(res => res.IsGlobal))
						r.Add(res, sobj.GetAbilityValue("Resource Storage - " + res.Name).ToInt());
				}
				return r;
			}
		}

		public IEnumerable<ISpaceObject> OwnedSpaceObjects
		{
			get
			{
				return Galaxy.Current.FindSpaceObjects<ISpaceObject>(sobj => sobj.Owner == this);
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
		/// The empire's culture.
		/// </summary>
		public Culture Culture { get; set; }

		/// <summary>
		/// TODO - implement empire score
		/// </summary>
		public long Score { get { return 0; } }

		public int CompareTo(Empire other)
		{
			return Name.CompareTo(other.Name);
		}

		public int CompareTo(object obj)
		{
			return Name.CompareTo(obj.ToString());
		}

		public Visibility CheckVisibility(Empire emp)
		{
			if (emp == this)
				return Visibility.Owned;
			else if (emp.EncounteredEmpires.Contains(this))
				return Visibility.Scanned;
			else
				return Visibility.Unknown;
		}

		/// <summary>
		/// Information about any foggable objects that this empire has previously seen but cannot currently see.
		/// </summary>
		public SafeDictionary<long, IFoggable> Memory { get; private set; }

		/// <summary>
		/// Updates the memory sight cache for an object.
		/// This should only be called when this empire SEES the object's state change,
		/// or the player decides to delete a sensor ghost.
		/// </summary>
		/// <param name="obj"></param>
		public void UpdateMemory(IFoggable obj)
		{
			if (obj.IsMemory)
				throw new InvalidOperationException("Call UpdateMemory for the physical object, not the memory.");

			// TODO - what happens if a ship/planet is captured by this empire? Then it needs to be updated...
			if (obj.Owner == this)
				return; // don't need to update empire's memory of its own objects!

			// encounter empire if not yet encountered
			if (obj.Owner != null && !EncounteredEmpires.Contains(obj.Owner))
			{
				// not two way encounter if ship is cloaked!
				// in that case you will need to gift your own comms channels to that empire
				// if you want them to be able to message you apart from replying to your messages
				EncounteredEmpires.Add(obj.Owner);
				Log.Add(obj.Owner.CreateLogMessage("We have encountered a new empire, the " + obj.Owner + "."));
			}

			if (obj.ID > 0)
			{
				// object exists, update cache with the data
				if (Memory[obj.ID] != null)
				{
					obj.CopyToExceptID(Memory[obj.ID], IDCopyBehavior.Regenerate);
					Memory[obj.ID].IsMemory = true;
				}
				else
				{
					var memory = obj.CopyAndAssignNewID();
					memory.IsMemory = true;
					memory.Timestamp = Galaxy.Current.TurnNumber + Galaxy.Current.CurrentTick;
					Memory[obj.ID] = memory;
				}

				// update pursue/evade orders' alternate targets if object is fleet
				if (obj is Fleet)
				{
					foreach (var order in this.OwnedSpaceObjects.OfType<IMobileSpaceObject>().SelectMany(sobj => sobj.Orders))
					{
						if (order is PursueOrder<IMobileSpaceObject>)
							((PursueOrder<IMobileSpaceObject>)order).UpdateAlternateTarget();
						if (order is EvadeOrder<IMobileSpaceObject>)
							((EvadeOrder<IMobileSpaceObject>)order).UpdateAlternateTarget();
					}
				}

				// no need to duplicate vehicle designs
				if (obj is IVehicle)
				{
					var copy = (IVehicle)Memory[obj.ID];
					copy.Design.Dispose();
					copy.Design = ((IVehicle)obj).Design;
				}
			}
			else
			{
				// object was destroyed, remove from cache
				var oldid = obj.ID > 0 ? obj.ID : Memory.SingleOrDefault(kvp => kvp.Value == obj).Key;
				if (oldid > 0)
					Memory.Remove(oldid);
			}
		}

		/// <summary>
		/// Arbitrary data stored by the AI to maintain state between turns.
		/// </summary>
		public dynamic AINotes { get; set; }

		public IDictionary<string, object> Variables
		{
			get
			{
				// let scripters refer to empire as empire, not just as host, because host would be confusing
				return new Dictionary<string, object>
				{
					{"empire", this}
				};
			}
		}

		/// <summary>
		/// Any empires that this empire has encountered.
		/// </summary>
		[DoNotCopy]
		public ISet<Empire> EncounteredEmpires { get; private set; }

		/// <summary>
		/// Incoming messages that are awaiting a response.
		/// </summary>
		public ICollection<IMessage> IncomingMessages { get; private set; }

		/// <summary>
		/// Messages sent by this empire.
		/// </summary>
		public ICollection<IMessage> SentMessages { get; private set; }


		public void Redact(Empire emp)
		{
			// clear data about other empires
			var vis = CheckVisibility(emp);
			if (vis < Visibility.Owned)
			{
				// TODO - espionage
				StoredResources.Clear();
				KnownDesigns.Clear();
				Log.Clear();
				ResearchedTechnologies.Clear();
				AccumulatedResearch.Clear();
				ResearchQueue.Clear();
				ResearchSpending.Clear();
				Memory.Clear();
				AINotes = null;

				// TODO - show count of encountered vehicles
				foreach (var d in KnownDesigns.Where(d => d.Owner == this))
					d.VehiclesBuilt = 0;
			}
			if (vis < Visibility.Fogged)
				Dispose();
		}

		public bool IsMemory
		{
			get;
			set;
		}

		public double Timestamp
		{
			get;
			set;
		}

		public bool IsObsoleteMemory(Empire emp)
		{
			return false;
		}

		public AbilityTargets AbilityTarget
		{
			get { return AbilityTargets.Empire; }
		}

		/// <summary>
		/// Any treaty clauses this empire is offering to other empires.
		/// </summary>
		public ILookup<Empire, Clause> GivenTreatyClauses
		{
			get
			{
				if (Galaxy.Current.GivenTreatyClauseCache == null)
					Galaxy.Current.GivenTreatyClauseCache = new SafeDictionary<Empire,ILookup<Empire,Clause>>();
				if (!Galaxy.Current.GivenTreatyClauseCache.ContainsKey(this))
					Galaxy.Current.GivenTreatyClauseCache.Add(this, Galaxy.Current.Referrables.OfType<Clause>().Where(c => c.Giver == this && c.IsInEffect).ToLookup(c => c.Receiver));
				return Galaxy.Current.GivenTreatyClauseCache[this];
			}
		}

		/// <summary>
		/// Any treaty clauses this empire is receiving from other empires.
		/// </summary>
		public ILookup<Empire, Clause> ReceivedTreatyClauses
		{
			get
			{
				if (Galaxy.Current.ReceivedTreatyClauseCache == null)
					Galaxy.Current.ReceivedTreatyClauseCache = new SafeDictionary<Empire,ILookup<Empire,Clause>>();
				if (!Galaxy.Current.ReceivedTreatyClauseCache.ContainsKey(this))
					Galaxy.Current.ReceivedTreatyClauseCache.Add(this, Galaxy.Current.Referrables.OfType<Clause>().Where(c => c.Receiver == this && c.IsInEffect).ToLookup(c => c.Giver));
				return Galaxy.Current.ReceivedTreatyClauseCache[this];
			}
		}

		/// <summary>
		/// Gets all the clauses in a treaty with another empire.
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public IEnumerable<Clause> GetTreaty(Empire emp)
		{
			return GivenTreatyClauses[emp].Union(ReceivedTreatyClauses[emp]);
		}

		/// <summary>
		/// Returns true if empires are not the same and neither empire is hostile to the other in the star system.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool IsAllyOf(Empire other, StarSystem sys)
		{
			return this != other && !this.IsHostileTo(other, sys) && !other.IsHostileTo(this, sys);
		}

		/// <summary>
		/// Returns true if empires are at war.
		/// TODO - implement war status; right now this always returns false
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool IsEnemyOf(Empire other, StarSystem sys)
		{
			return false;
		}

		/// <summary>
		/// Returns true if empires are not the same, and are neither allies nor at war.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool IsNeutralTo(Empire other, StarSystem sys)
		{
			return this != other && !this.IsAllyOf(other, sys) && !this.IsEnemyOf(other, sys);
		}

		public IEnumerable<IAbilityObject> Children
		{
			get { return OwnedSpaceObjects.Cast<IAbilityObject>().Append(PrimaryRace); }
		}

		public IAbilityObject Parent
		{
			get { return null; }
		}

		public bool IsDisposed { get; set; }

		/// <summary>
		/// Gets the memory of an object.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <returns></returns>
		public T Recall<T>(T obj) where T : IFoggable
		{
			return (T)Memory[obj.ID];
		}

		/// <summary>
		/// Waypoints set by this empire.
		/// </summary>
		public IList<Waypoint> Waypoints { get; private set; }

		/// <summary>
		/// Numbered waypoints that are on hotkeys.
		/// </summary>
		public Waypoint[] NumberedWaypoints { get; private set; }

		/// <summary>
		/// Should we have a sensor sweep of the entire galaxy from the very start?
		/// </summary>
		public bool AllSystemsExploredFromStart
		{
			get
			{
				return Galaxy.Current.AllSystemsExploredFromStart || this.HasAbility("Galaxy Seen");
			}
		}
	}
}
