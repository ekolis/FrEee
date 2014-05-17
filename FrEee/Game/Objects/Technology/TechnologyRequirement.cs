using FrEee.Game.Interfaces;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using System;
using System.Collections.Generic;

namespace FrEee.Game.Objects.Technology
{
	/// <summary>
	/// Requirement for a technology to be researched to a particular level.
	/// </summary>
	[Serializable]
	public class TechnologyRequirement : IContainable<IResearchable>
	{
		public TechnologyRequirement(IResearchable container, Technology tech, Formula<int> level)
		{
			Container = container;
			Technology = tech;
			Level = level;
		}

		/// <summary>
		/// The technology to be researched.
		/// </summary>
		public Technology Technology { get; set; }

		/// <summary>
		/// The level required.
		/// </summary>
		public Formula<int> Level { get; set; }

		public IResearchable Container
		{
			get;
			private set;
		}
	}
}
