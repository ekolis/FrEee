using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A vehicle which can be contained in cargo.
	/// </summary>
	public interface IUnit : IVehicle, IContainable<ICargoContainer>
	{
	}
}
