using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Construction;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.Technology;
using FrEee.Serialization;
using FrEee.Utility;
using System.Collections.Generic;
using FrEee.Utility;
using FrEee.Processes.Combat;
using FrEee.Objects.GameState;
using FrEee.Ecs.Abilities.Utility;
using FrEee.Ecs;

namespace FrEee.Objects.Vehicles;

/// <summary>
/// A space or ground vehicle.
/// </summary>
public interface IVehicle : IEntity, IConstructable, IOwnable, IReferrable, IDamageable, ICombatant, IRecyclable, IIncomeProducer, IUpgradeable<IVehicle>, INameable
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
