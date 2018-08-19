using FrEee.Modding;

namespace FrEee.Tests.Modding
{
    /// <summary>
    /// The stock FrEee mod.
    /// </summary>
    public static class StockMod
    {
        #region Private Fields

        private static Mod instance;

        #endregion Private Fields

        #region Public Properties

        public static Mod Instance
        {
            get
            {
                if (instance == null)
                    instance = Mod.Load(null);
                return instance;
            }
        }

        #endregion Public Properties
    }
}
