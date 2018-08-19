namespace FrEee.Game.Interfaces
{
    /// <summary>
    /// Something that has a name.
    /// </summary>
    public interface INamed
    {
        #region Public Properties

        /// <summary>
        /// The name of the object.
        /// </summary>
        string Name { get; }

        #endregion Public Properties
    }
}
