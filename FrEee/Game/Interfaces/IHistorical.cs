using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Flag interface for objects which should have history saved.
	/// </summary>
	public interface IHistorical
	{
		/// <summary>
		/// The visibility of this object to an empire.
		/// </summary>
		Visibility CheckVisibility(Empire emp);
	}
}
