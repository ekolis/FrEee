using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game
{
	/// <summary>
	/// Something that can be unlocked with research.
	/// </summary>
	public interface IResearchable
	{
		IList<TechnologyRequirement> TechnologyRequirements { get; }
	}
}
