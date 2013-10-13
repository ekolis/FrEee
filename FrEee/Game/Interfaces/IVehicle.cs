using FrEee.Game.Objects.Technology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A space or ground vehicle.
	/// </summary>
	public interface IVehicle : IConstructable, IAbilityObject, IReferrable, IDamageable, IFoggable
	{
		/// <summary>
		/// The design of this vehicle.
		/// </summary>
		IDesign Design { get; }

		/// <summary>
		/// The components on this vehicle.
		/// </summary>
		IList<Component> Components { get; }

		/// <summary>
		/// The hull of this vehicle.
		/// </summary>
		IHull Hull { get; }
	}
}
