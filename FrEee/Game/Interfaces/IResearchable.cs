using System.Collections.Generic;
using FrEee.Game.Objects.Technology;
using FrEee.Modding.Interfaces;
using FrEee.Modding;
using FrEee.Game.Objects.Civilization;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Something that can be unlocked in-game, as opposed to only in empire setup.
	/// </summary>
	public interface IResearchable : IUnlockable, IFoggable
	{
		
	}
}
