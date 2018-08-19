using FrEee.Game.Objects.Civilization;
using FrEee.Utility;

namespace FrEee.Game.Interfaces
{
    /// <summary>
    /// Template for constructable items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IConstructionTemplate : IReferrable, IPictorial, IResearchable
    {
        #region Public Properties

        /// <summary>
        /// The cost to build it.
        /// </summary>
        ResourceQuantity Cost { get; }

        /// <summary>
        /// Does this template require a colony to build it?
        /// </summary>
        bool RequiresColonyQueue { get; }

        /// <summary>
        /// Does this template require a space yard to build it?
        /// </summary>
        bool RequiresSpaceYardQueue { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Has the empire unlocked this construction template?
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        bool HasBeenUnlockedBy(Empire emp);

        #endregion Public Methods
    }
}
