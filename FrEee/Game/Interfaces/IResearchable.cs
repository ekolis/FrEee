using System.Collections.Generic;
using FrEee.Game.Objects.Technology;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Something that can be unlocked with research.
	/// </summary>
	public interface IResearchable : IReferrable, IPictorial, INamed
	{
		/// <summary>
		/// The technology requirements of this item.
		/// </summary>
		IList<TechnologyRequirement> TechnologyRequirements { get; }

		/// <summary>
		/// A group to display on the research screen, such as "Components" or "Facilities".
		/// </summary>
		string ResearchGroup { get; }
	}
}
