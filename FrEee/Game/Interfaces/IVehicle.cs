using FrEee.Game.Objects.Technology;
using FrEee.Utility;
using System.Collections.Generic;

namespace FrEee.Game.Interfaces
{
    /// <summary>
    /// A space or ground vehicle.
    /// </summary>
    public interface IVehicle : IConstructable, IOwnableAbilityObject, IReferrable, IDamageable, ICombatant, IRecyclable, IIncomeProducer, IUpgradeable<IVehicle>, INameable
    {
        #region Public Properties

        IList<Component> Components { get; }

        /// <summary>
        /// The design of this vehicle.
        /// </summary>
        [DoNotCopy]
        IDesign Design { get; set; }

        /// <summary>
        /// Cost to maintain this vehicle per turn.
        /// </summary>
        ResourceQuantity MaintenanceCost { get; }

        #endregion Public Properties
    }
}
