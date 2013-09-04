using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Something that can be obscured by fog of war.
	/// </summary>
	public interface IFoggable
	{
		/// <summary>
		/// The visibility of this object to an empire.
		/// </summary>
		Visibility CheckVisibility(Empire emp);
	}
}
