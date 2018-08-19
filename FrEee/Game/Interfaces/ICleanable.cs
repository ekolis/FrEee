namespace FrEee.Game.Interfaces
{
    /// <summary>
    /// For classes that need extra processing after being copied or whatnot.
    /// </summary>
    public interface ICleanable
    {
        #region Public Methods

        void Clean();

        #endregion Public Methods
    }
}
