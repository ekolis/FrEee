using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Construction;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.Technology;
using FrEee.Serialization;
using FrEee.Utility;
using System.Collections.Generic;
using FrEee.Processes.Combat;
using FrEee.Modding.Abilities;
using FrEee.Objects.GameState;
using FrEee.Vehicles.Types;

namespace FrEee.Vehicles;

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

	IHull Hull => Design.Hull;

	VehicleTypes VehicleType => Hull.VehicleType;

	/// <summary>
	/// Cost to maintain this vehicle per turn.
	/// </summary>
	ResourceQuantity MaintenanceCost { get; }

	/// <summary>
	/// Does this vehicle detonate to inflict damage on enemies that enter its sector?
	/// (i.e. in stock, is it a mine?)
	/// </summary>
	// TODO: make this an ability on hulls and/or components
	bool DetonatesWhenEnemiesEnterSector { get; }
}
