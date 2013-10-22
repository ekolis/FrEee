using System;
using System.Linq;
using System.Collections.Generic;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using FrEee.Game.Enumerations;
using FrEee.Modding.Interfaces;

namespace FrEee.Game.Objects.Technology
{
	/// <summary>
	/// A technology that can be researched in the game.
	/// </summary>
	[Serializable]
	public class Technology : INamed, IResearchable, IReferrable
	{
		public Technology()
		{
			UnlockRequirements = new List<Requirement<Empire>>();
			expectedResults = new Lazy<IEnumerable<IResearchable>>(() => GetExpectedResults(Empire.Current));
		}

		/// <summary>
		/// The name of the technology.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The group that the technology belongs to. For grouping on the research screen.
		/// </summary>
		public string Group { get; set; }

		/// <summary>
		/// A description of the technology.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// The maximum level that can be researched.
		/// </summary>
		public int MaximumLevel { get; set; }

		/// <summary>
		/// The cost of the first level.
		/// </summary>
		public int LevelCost { get; set; }

		/// <summary>
		/// The starting level for empires at low tech.
		/// </summary>
		public int StartLevel { get; set; }

		/// <summary>
		/// The starting level for empires at medium tech.
		/// </summary>
		public int RaiseLevel { get; set; }

		/// <summary>
		/// If not null or zero, this tech is a "racial tech" and will not be researchable
		/// except by empires possessing the trait referencing this ID.
		/// </summary>
		public string RacialTechID { get; set; }

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

		public bool IsRacial
		{
			get
			{
				return RacialTechID != "0" && !string.IsNullOrWhiteSpace(RacialTechID);
			}
		}

		/// <summary>
		/// If not null or zero, this tech is a "unique tech" and will not be researchable.
		/// Instead it will appear on ruins worlds referencing its ID.
		/// </summary>
		public string UniqueTechID { get; set; }

		public bool IsUnique
		{
			get
			{
				return UniqueTechID != "0" && !string.IsNullOrWhiteSpace(UniqueTechID);
			}
		}

		/// <summary>
		/// Should the game offer game hosts the option of removing this tech from their games?
		/// </summary>
		public bool CanBeRemoved { get; set; }

		/// <summary>
		/// The prerequisites for this technology.
		/// </summary>
		public IList<Requirement<Empire>> UnlockRequirements { get; private set; }

		public long ID
		{
			get;
			set;
		}

		public Empire Owner
		{
			get { return null; }
		}

		public int GetLevelCost(int level)
		{
			if (Galaxy.Current.TechnologyCost == TechnologyCost.Low)
				return LevelCost * level;
			else if (Galaxy.Current.TechnologyCost == TechnologyCost.Medium)
			{
				if (Math.Abs(level) == 1)
					return LevelCost * level;
				else
					return LevelCost * level * level / 2;
			}
			else if (Galaxy.Current.TechnologyCost == TechnologyCost.High)
				return LevelCost * level * level;
			throw new Exception("Invalid technology cost for galaxy: " + Galaxy.Current.TechnologyCost);
		}

		public int GetNextLevelCost(Empire emp)
		{
			if (emp == null)
				return LevelCost;
			return GetLevelCost(emp.ResearchedTechnologies[this] + 1);
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
		/// Current empire's research progress in this technology.
		/// TODO - refactor this into an EmpireTechnology class
		/// </summary>
		public Progress<Technology> Progress
		{
			get
			{
				if (Empire.Current == null)
					return new Progress<Technology>(this, 0, LevelCost);
				var progress = Empire.Current.ResearchProgress.SingleOrDefault(p => p.Item == this);
				if (progress == null)
					return new Progress<Technology>(this, 0, LevelCost);
				return progress;
			}
		}

		/// <summary>
		/// Current empire's spending percentage on this technology.
		/// TODO - refactor this into an EmpireTechnology class
		/// </summary>
		public Progress<Technology> Spending
		{
			get
			{
				if (Empire.Current == null)
					return new Progress<Technology>(this, 0, 100);
				return new Progress<Technology>(this, Empire.Current.ResearchSpending[this], 100);
			}
		}

		public IEnumerable<IResearchable> GetExpectedResults(Empire emp)
		{
			var techs = emp.ResearchedTechnologies;
			var techs2 = new SafeDictionary<Technology, int>();
			foreach (var kvp in techs)
				techs2.Add(kvp);
			techs2[this]++;
			var have = GetUnlockedItems(emp, techs);
			var willHave = GetUnlockedItems(emp, techs2);
			return willHave.Except(have);
		}

		private Lazy<IEnumerable<IResearchable>> expectedResults;

		/// <summary>
		/// Current empire's expected results for researching the next level of this tech.
		/// TODO - refactor this into an EmpireTechnology class
		/// </summary>
		public IEnumerable<IResearchable> ExpectedResults
		{
			get
			{
				if (Empire.Current == null)
					return Enumerable.Empty<IResearchable>();
				return expectedResults.Value;
			}
		}

		public static IEnumerable<IResearchable> GetUnlockedItems(Empire emp, IDictionary<Technology, int> levels)
		{
			foreach (var item in Galaxy.Current.Referrables.OfType<IResearchable>())
			{
				bool ok = true;
				foreach (var req in item.UnlockRequirements)
				{
					if (!req.IsMetBy(emp))
					{
						// didn't meet the requirement
						ok = false;
						break;
					}
				}
				if (ok)
					yield return item;
			}
		}

		/// <summary>
		/// TODO - technology icons?
		/// </summary>
		public System.Drawing.Image Icon
		{
			get { return Resource.Research.Icon; }
		}

		/// <summary>
		/// TODO - technology portraits?
		/// </summary>
		public System.Drawing.Image Portrait
		{
			get { return Resource.Research.Icon; }
		}


		public string ResearchGroup
		{
			get { return "Technology"; }
		}

		public override string ToString()
		{
			return Name;
		}

		public void Dispose()
		{
			Galaxy.Current.UnassignID(this);
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

		string INamed.Name
		{
			get { return Name; }
		}
	}
}
