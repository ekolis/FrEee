using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// An order issued by a player to his empire.
	/// </summary>
	public interface IEmpireOrder : IOrder<Empire, IEmpireOrder>
	{
	}
}
