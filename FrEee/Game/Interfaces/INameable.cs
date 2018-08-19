namespace FrEee.Game.Interfaces
{
    /// <summary>
    /// Something which can be named by the player who owns it.
    /// Even if you don't own it, you can always set a "private" name which only you can see.
    /// </summary>
    public interface INameable : IOwnable, IReferrable, INamed
    {
        #region Public Properties

        new string Name { get; set; }

        #endregion Public Properties
    }
}
