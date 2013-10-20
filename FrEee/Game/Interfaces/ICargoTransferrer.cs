using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A space object which can contain cargo and receive cargo transfer orders.
	/// </summary>
	public interface ICargoTransferrer : ICargoContainer, ISpaceObject, IOrderable
	{
		
	}
}
