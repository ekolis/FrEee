using System.Collections.Generic;
using FrEee.Game.Objects.Research;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Something that can be unlocked with research.
	/// </summary>
	public interface IResearchable
	{
		IList<TechnologyRequirement> TechnologyRequirements { get; }
	}
}
