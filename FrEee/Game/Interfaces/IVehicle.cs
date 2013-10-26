using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A space or ground vehicle.
	/// </summary>
	public interface IVehicle : IConstructable, IAbilityObject, IReferrable, IDamageable
	{
		/// <summary>
		/// The design of this vehicle.
		/// </summary>
		IDesign Design { get; }

		int Accuracy { get; }

		int Evasion { get; }
	}
}
