using System;
using System.Collections.Generic;
using FrEee.Game.Interfaces;

namespace FrEee.Game.Objects.Research
{
	/// <summary>
	/// A technology that can be researched in the game.
	/// </summary>
	 [Serializable] public class Technology : INamed, IResearchable
	{
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
		/// If greater than zero, this tech is a "racial tech" and will not be researchable
		/// except by empires possessing the racial trait referencing this ID.
		/// </summary>
		public int RacialTechID { get; set; }

		/// <summary>
		/// If greater than zero, this tech is a "unique tech" and will not be researchable.
		/// Instead it will appear on ruins worlds referencing its ID.
		/// </summary>
		public int UniqueTechID { get; set; }

		/// <summary>
		/// Should the game offer game hosts the option of removing this tech from their games?
		/// </summary>
		public bool CanBeRemoved { get; set; }

		/// <summary>
		/// The prerequisites for this technology.
		/// </summary>
		public IList<TechnologyRequirement> TechnologyRequirements { get; private set; }
	}
}
