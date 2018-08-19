using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Abilities;
using System.Collections.Generic;

namespace FrEee.Game.Interfaces
{
    /// <summary>
    /// Something which can have intrinsic abilities of its own that can be added and removed.
    /// </summary>
    public interface IAbilityContainer : IAbilityObject
    {
        #region Public Properties

        /// <summary>
        /// Intrinsic abilities of this object which can be added or removed.
        /// TODO - rename to IntrinsicAbilities after the current game is over
        /// </summary>
        IList<Ability> Abilities { get; }

        #endregion Public Properties
    }

    /// <summary>
    /// Something which can have abilities.
    /// </summary>
    public interface IAbilityObject
    {
        #region Public Properties

        /// <summary>
        /// The type of ability target that this object represents.
        /// </summary>
        AbilityTargets AbilityTarget { get; }

        /// <summary>
        /// Child objects that can pass up abilities to this object.
        /// </summary>
        IEnumerable<IAbilityObject> Children { get; }

        /// <summary>
        /// Abilities possessed intrinsically by this object.
        /// </summary>
        IEnumerable<Ability> IntrinsicAbilities { get; }

        /// <summary>
        /// Parent objects from which this object can inherit abilities.
        /// </summary>
        IEnumerable<IAbilityObject> Parents { get; }

        #endregion Public Properties
    }
}
