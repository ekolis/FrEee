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
		/// Is this object currently visible to an empire?
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		bool IsVisibleTo(Empire emp);
	}
}
