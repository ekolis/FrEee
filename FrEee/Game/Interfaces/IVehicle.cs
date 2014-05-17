using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A space or ground vehicle.
	/// </summary>
	public interface IVehicle : IConstructable, IOwnableAbilityObject, IReferrable, IDamageable, ICombatant
	{
		/// <summary>
		/// The design of this vehicle.
		/// </summary>
		IDesign Design { get; }

		/// <summary>
		/// Cost to maintain this vehicle per turn.
		/// </summary>
		ResourceQuantity MaintenanceCost { get; }
	}
}
