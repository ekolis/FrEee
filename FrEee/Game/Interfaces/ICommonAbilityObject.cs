using FrEee.Game.Objects.Civilization;
using System.Collections.Generic;

namespace FrEee.Game.Interfaces
{
    /// <summary>
    /// An object that can contain different abilities for different empires.
    /// </summary>
    public interface ICommonAbilityObject : IAbilityObject
    {
        #region Public Methods

        /// <summary>
        /// Finds any child ability objects owned by an empire.
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        IEnumerable<IAbilityObject> GetContainedAbilityObjects(Empire emp);

        #endregion Public Methods
    }
}
