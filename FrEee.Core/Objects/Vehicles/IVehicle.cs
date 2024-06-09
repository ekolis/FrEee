using FrEee.Objects.Abilities;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Construction;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.Combat;
using FrEee.Objects.Technology;
using FrEee.Serialization;
using FrEee.Utility;
using System.Collections.Generic;
using FrEee.Utility;
using FrEee.Serialization;

namespace FrEee.Objects.Vehicles;

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
