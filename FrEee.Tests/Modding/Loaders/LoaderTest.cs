using FrEee.Modding;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrEee.Tests.Modding.Loaders
{
    /// <summary>
    /// Tests mod loaders.
    /// </summary>
    [TestClass]
    public class LoaderTest
    {
        #region Public Methods

        [TestMethod]
        public void LoadIncludeModWithoutErrors()
        {
            TestUtilities.SetEntryAssembly();
            Mod.Load("Include Mod");
            Assert.AreEqual(0, Mod.Errors.Count);
        }

        /// <summary>
        /// Makes sure the stock mod loads with no errors.
        /// </summary>
        [TestMethod]
        public void LoadStockModWithoutErrors()
        {
            Mod.Load(null);
            Assert.AreEqual(0, Mod.Errors.Count);
        }

        #endregion Public Methods
    }
}
