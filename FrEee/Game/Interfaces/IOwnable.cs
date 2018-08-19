using FrEee.Game.Objects.Civilization;
using FrEee.Utility;

namespace FrEee.Game.Interfaces
{
    /// <summary>
    /// Something which can be owned by an empire.
    /// </summary>
    public interface IOwnable
    {
        #region Public Properties

        [DoNotCopy]
        Empire Owner { get; }

        #endregion Public Properties
    }

    /// <summary>
    /// Something whose ownership can be changed.
    /// </summary>
    public interface ITransferrable : IOwnable
    {
        #region Public Properties

        new Empire Owner { get; set; }

        #endregion Public Properties
    }
}
