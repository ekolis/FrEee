using System;
using System.Linq;
using System.Collections.Generic;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Game.Objects.Space;
using FrEee.Modding;

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
			TechnologyRequirements = new List<TechnologyRequirement>();
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
				return Mod.Current.Traits.Where(t => t.Abilities.Any(a => a.Name == "Tech Area" && a.Value1 == RacialTechID));
			}
		}

		public bool IsRacial
		{
			get
			{
				return RacialTechID == "0" || string.IsNullOrWhiteSpace(RacialTechID);
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
				return UniqueTechID == "0" || string.IsNullOrWhiteSpace(UniqueTechID);
			}
		}

		/// <summary>
		/// Should the game offer game hosts the option of removing this tech from their games?
		/// </summary>
		public bool CanBeRemoved { get; set; }

		/// <summary>
		/// The prerequisites for this technology.
		/// </summary>
		public IList<TechnologyRequirement> TechnologyRequirements { get; private set; }

		public int ID
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
			// TODO - use galaxy tech cost formula
			return LevelCost * level;
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
		public Progress Progress
		{
			get
			{
				if (Empire.Current == null)
					return new Progress(0, LevelCost);
				return Empire.Current.ResearchProgress.SingleOrDefault(p => p.Item == this);
			}
		}

		/// <summary>
		/// Current empire's spending percentage on this technology.
		/// TODO - refactor this into an EmpireTechnology class
		/// </summary>
		public Progress Spending
		{
			get
			{
				if (Empire.Current == null)
					return new Progress(0, 100);
				return new Progress(Empire.Current.ResearchSpending[this], 100);
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
			foreach (var item in emp.Referrables.OfType<IResearchable>())
			{
				bool ok = true;
				foreach (var req in item.TechnologyRequirements)
				{
					if (levels[req.Technology] < req.Level)
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
			get { return null; }
		}

		/// <summary>
		/// TODO - technology portraits?
		/// </summary>
		public System.Drawing.Image Portrait
		{
			get { return null; }
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
			Galaxy.Current.Unregister(this);
			foreach (var emp in Galaxy.Current.Empires)
				Galaxy.Current.Unregister(this, emp);
		}
	}
}
