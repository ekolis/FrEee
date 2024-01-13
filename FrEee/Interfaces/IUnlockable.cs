using FrEee.Objects.Civilization;
using FrEee.Modding;
using System.Collections.Generic;

namespace FrEee.Interfaces
{
	/// <summary>
	/// Something which can be unlocked, either via research or at empire creation.
	/// </summary>
	public interface IUnlockable : INamed, IPictorial
	{
		/// <summary>
		/// A group to display on the research screen, such as "Components" or "Facilities".
		/// Or for racial/empire traits, just "Trait".
		/// </summary>
		string ResearchGroup { get; }

		/// <summary>
		/// The requirements of this item to be unlocked.
		/// </summary>
		IList<Requirement<Empire>> UnlockRequirements { get; }
	}
}