using FrEee.Game.Interfaces;

namespace FrEee.Modding.Interfaces
{
    /// <summary>
    /// An object which can be stored in mod files.
    /// </summary>
    public interface IModObject : INamed
    {
        #region Public Properties

        bool IsDisposed { get; }

        /// <summary>
        /// An ID used to represent this mod object when patching a mod.
        /// </summary>
        string ModID { get; set; }

        #endregion Public Properties
    }
}
