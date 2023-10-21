using FrEee.Objects.Technology;
using FrEee.Objects.Vehicles;
using FrEee.Utility;
using FrEee.Utility.Serialization;
using System.Collections.Generic;

namespace FrEee.Interfaces
{
	/// <summary>
	/// A space or ground vehicle.
	/// </summary>
	public interface IVehicle : IConstructable, IOwnableAbilityObject, IReferrable, IDamageable, ICombatant, IRecyclable, IIncomeProducer, IUpgradeable<IVehicle>, INameable
	{
		[DoNotSerialize(false)]
		new IList<Component> Components { get; }

		/// <summary>
		/// Damage that has been applied to this vehicle's components.
		/// </summary>
		SafeDictionary<MountedComponentTemplate, IList<int>> Damage { get; set; }

		/// <summary>
		/// The design of this vehicle.
		/// </summary>
		[DoNotCopy]
		IDesign Design { get; set; }

		/// <summary>
		/// Cost to maintain this vehicle per turn.
		/// </summary>
		ResourceQuantity MaintenanceCost { get; }
	}
}
