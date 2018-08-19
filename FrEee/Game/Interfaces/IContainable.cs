using FrEee.Utility;

namespace FrEee.Game.Interfaces
{
    /// <summary>
    /// Something which can be contained by another object.
    /// </summary>
    public interface IContainable<out TContainer>
    {
        #region Public Properties

        /// <summary>
        /// The container of this object.
        /// </summary>
        [DoNotCopy]
        TContainer Container { get; }

        #endregion Public Properties
    }
}
