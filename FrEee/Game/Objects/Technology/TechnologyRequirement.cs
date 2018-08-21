using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Modding;
using System;

namespace FrEee.Game.Objects.Technology
{
	/// <summary>
	/// Requirement for a technology to be researched to a particular level.
	/// </summary>
	[Serializable]
	public class TechnologyRequirement : Requirement<Empire>, IContainable<IResearchable>
	{
		public TechnologyRequirement(IResearchable container, Technology tech, Formula<int> level)
			: base("Requires level " + level + " " + tech + ".")
		{
			Container = container;
			Technology = tech;
			Level = level;
		}

		/// <summary>
		/// What researchable object "owns" this technology requirement.
		/// </summary>
		public IResearchable Container
		{
			get;
			private set;
		}

		/// <summary>
		/// Technology requirements are always strict.
		/// </summary>
		public override bool IsStrict
		{
			get { return true; }
		}

		/// <summary>
		/// The level required.
		/// </summary>
		public Formula<int> Level { get; set; }

		/// <summary>
		/// The technology to be researched.
		/// </summary>
		public Technology Technology { get; set; }

		/// <summary>
		/// Is this technology requirement met by a particular empire?
		/// </summary>
		/// <param name="emp">The empire being tested.</param>
		/// <returns>true if the requirement is met, otherwise false.</returns>
		public override bool IsMetBy(Empire emp)
		{
			return emp.ResearchedTechnologies[Technology] >= Level;
		}
	}
}