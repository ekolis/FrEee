using System.Collections.Generic;
using FrEee.Game.Objects.Technology;
using FrEee.Modding.Interfaces;
using FrEee.Modding;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Something that can be unlocked with research.
	/// </summary>
	public interface IResearchable : IReferrable, IPictorial, INamed
	{
		/// <summary>
		/// The requirements of this item to be unlocked.
		/// </summary>
		IList<Requirement> UnlockRequirements { get; }

		/// <summary>
		/// A group to display on the research screen, such as "Components" or "Facilities".
		/// </summary>
		string ResearchGroup { get; }
	}
}
