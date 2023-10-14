using FrEee.Enumerations;
using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FrEee.Objects.Technology
{
	/// <summary>
	/// A technology that can be researched in the game.
	/// </summary>
	[Serializable]
	public class Technology : IModObject, IResearchable
	{
		public Technology()
		{
			UnlockRequirements = new List<Requirement<Empire>>();
		}

		/// <summary>
		/// Should the game offer game hosts the option of removing this tech from their games?
		/// </summary>
		public bool CanBeRemoved { get; set; }

		/// <summary>
		/// Current empire's level in this technology.
		/// TODO - refactor this into an EmpireTechnology class
		/// </summary>
		public int CurrentLevel
		{
			get
			{
				if (Empire.Current == null)
					return 0;
				return Empire.Current.ResearchedTechnologies[this];
			}
		}

		/// <summary>
		/// A description of the technology.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Current empire's expected results for researching the next level of this tech.
		/// TODO - refactor this into an EmpireTechnology class
		/// </summary>
		public IEnumerable<IUnlockable> ExpectedResults
		{
			get
			{
				if (Empire.Current == null)
					return Enumerable.Empty<IResearchable>();
				return GetExpectedResults(Empire.Current);
			}
		}

		/// <summary>
		/// The group that the technology belongs to. For grouping on the research screen.
		/// </summary>
		public string Group { get; set; }

		/// <summary>
		/// TODO - technology icons?
		/// </summary>
		public Image Icon
		{
			get { return Resource.Research.Icon; }
		}

		public Image Icon32 => Icon.Resize(32);

		public IEnumerable<string> IconPaths
		{
			get
			{
				return Resource.Research.IconPaths;
			}
		}

		public long ID
		{
			get;
			set;
		}

		public bool IsDisposed { get; set; }

		public bool IsMemory
		{
			get;
			set;
		}

		public bool IsRacial
		{
			get
			{
				return RacialTechID != "0" && !string.IsNullOrWhiteSpace(RacialTechID);
			}
		}

		public bool IsUnique
		{
			get
			{
				return UniqueTechID != "0" && !string.IsNullOrWhiteSpace(UniqueTechID);
			}
		}

		/// <summary>
		/// The cost of the first level.
		/// </summary>
		public int LevelCost { get; set; }

		/// <summary>
		/// The maximum level that can be researched.
		/// </summary>
		public int MaximumLevel { get; set; }

		public string ModID { get; set; }

		/// <summary>
		/// The name of the technology.
		/// </summary>
		public string Name { get; set; }

		string INamed.Name
		{
			get { return Name; }
		}

		/// <summary>
		/// Current empire's cost to research the next level.
		/// TODO - refactor this into an EmpireTechnology class
		/// </summary>
		public int NextLevelCost
		{
			get
			{
				return GetNextLevelCost(Empire.Current);
			}
		}

		public Empire Owner
		{
			get { return null; }
		}

		/// <summary>
		/// TODO - technology portraits?
		/// </summary>
		public Image Portrait
		{
			get { return Resource.Research.Icon; }
		}

		public IEnumerable<string> PortraitPaths
		{
			get
			{
				return Resource.Research.PortraitPaths;
			}
		}

		/// <summary>
		/// Current empire's research progress in this technology.
		/// TODO - refactor this into an EmpireTechnology class
		/// </summary>
		public ModProgress<Technology> Progress
		{
			get
			{
				if (Empire.Current == null)
					return new ModProgress<Technology>(this, 0, LevelCost);
				var progress = Empire.Current.ResearchProgress.SingleOrDefault(p => p.Item == this);
				if (progress == null)
					return new ModProgress<Technology>(this, 0, LevelCost);
				return progress;
			}
		}

		/// <summary>
		/// If not null or zero, this tech is a "racial tech" and will not be researchable
		/// except by empires possessing the trait referencing this ID.
		/// </summary>
		public string RacialTechID { get; set; }

		/// <summary>
		/// The starting level for empires at medium tech.
		/// </summary>
		public int RaiseLevel { get; set; }

		public string ResearchGroup
		{
			get { return "Technology"; }
		}

		/// <summary>
		/// Current empire's spending percentage on this technology.
		/// TODO - refactor this into an EmpireTechnology class
		/// </summary>
		public ModProgress<Technology> Spending
		{
			get
			{
				if (Empire.Current == null)
					return new ModProgress<Technology>(this, 0, 100);
				return new ModProgress<Technology>(this, Empire.Current.ResearchSpending[this], 100);
			}
		}

		/// <summary>
		/// The starting level for empires at low tech.
		/// </summary>
		public int StartLevel { get; set; }

		/// <summary>
		/// Parameters from the mod meta templates.
		/// </summary>
		public IDictionary<string, object> TemplateParameters { get; set; }

		public double Timestamp
		{
			get;
			set;
		}

		/// <summary>
		/// Traits which unlock this technology.
		/// </summary>
		public IEnumerable<Trait> Traits
		{
			get
			{
				if (!IsRacial)
					return Enumerable.Empty<Trait>();
				return Mod.Current.Traits.Where(t => t.Abilities.Any(a => a.Rule.Matches("Tech Area") && a.Value1 == RacialTechID));
			}
		}

		/// <summary>
		/// If not null or zero, this tech is a "unique tech" and will not be researchable.
		/// Instead it will appear on ruins worlds referencing its ID.
		/// </summary>
		public string UniqueTechID { get; set; }

		/// <summary>
		/// The prerequisites for this technology.
		/// </summary>
		public IList<Requirement<Empire>> UnlockRequirements { get; private set; }

		/// <summary>
		/// Determines what would be unlocked by granting additional technology to an empire.
		/// </summary>
		/// <param name="emp">The empire.</param>
		/// <param name="levels">The technology levels to grant.</param>
		/// <returns>Newly unlocked items.</returns>
		public static IEnumerable<IUnlockable> GetUnlockedItems(Empire emp, IDictionary<Technology, int> levels)
		{
			// find out what the empire already knows
			var oldItems = emp.UnlockedItems.ToArray();

			// save off the old levels so we can restore them
			var oldLevels = new Dictionary<Technology, int>(emp.ResearchedTechnologies);

			// set the new levels
			foreach (var kvp in levels.ToArray())
				emp.ResearchedTechnologies.Add(kvp);

			// find out what the empire would know
			emp.RefreshUnlockedItems();
			var newItems = emp.UnlockedItems.ToArray();

			// reset known levels
			emp.ResearchedTechnologies.Clear();
			foreach (var kvp in oldLevels)
				emp.ResearchedTechnologies.Add(kvp);
			emp.RefreshUnlockedItems();

			// return newly learned items
			return newItems.Except(oldItems);
		}

		/// <summary>
		/// Mod objects are fully known to everyone.
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public Visibility CheckVisibility(Empire emp)
		{
			return Visibility.Scanned;
		}

		public void Dispose()
		{
			if (IsDisposed)
				return;
			if (Mod.Current != null)
				Mod.Current.Technologies.Remove(this);
		}

		public int GetBaseLevelCost(int level)
		{
			if (The.Setup.TechnologyCost == TechnologyCost.Low)
				return LevelCost * level;
			else if (The.Setup.TechnologyCost == TechnologyCost.Medium)
			{
				if (Math.Abs(level) == 1)
					return LevelCost * level;
				else
					return LevelCost * level * level / 2;
			}
			else if (The.Setup.TechnologyCost == TechnologyCost.High)
				return LevelCost * level * level;
			throw new Exception("Invalid technology cost for galaxy: " + The.Setup.TechnologyCost);
		}

		/// <summary>
		/// Determines what an empire would unlock by researching the next level of this technology.
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public IEnumerable<IUnlockable> GetExpectedResults(Empire emp)
		{
			var techs = new SafeDictionary<Technology, int>();
			foreach (var kvp in emp.ResearchedTechnologies)
				techs.Add(kvp.Key, kvp.Value);
			techs[this]++;
			return GetUnlockedItems(emp, techs);
		}

		public int GetLevelCost(int level, Empire emp)
		{
			var baseCost = GetBaseLevelCost(level);
			if (The.Setup.TechnologyUniqueness == 0)
				return baseCost;
			var playerRatio = emp.OtherPlayersTechLevels[this]?.Count(x => x >= level) ?? 0;
			var uniquenessFactor = Math.Pow(2, The.Setup.TechnologyUniqueness * playerRatio);
			return (int)(GetBaseLevelCost(level) * uniquenessFactor);
		}

		public int GetNextLevelCost(Empire emp)
		{
			if (emp == null)
				return LevelCost;
			return GetLevelCost(emp.ResearchedTechnologies[this] + 1, emp);
		}

		public bool IsObsoleteMemory(Empire emp)
		{
			return false;
		}

		public void Redact(Empire emp)
		{
			// TODO - tech items that aren't visible until some requirements are met
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
