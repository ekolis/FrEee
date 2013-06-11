using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A space object which can contain cargo.
	/// </summary>
	public interface ICargoContainer : IOrderable, ISpaceObject, IReferrable
	{
		/// <summary>
		/// The cargo contained by this object.
		/// </summary>
		Cargo Cargo { get; }

		/// <summary>
		/// The total amount of cargo storage possessed by this object.
		/// </summary>
		int CargoStorage { get; }
	}
}
