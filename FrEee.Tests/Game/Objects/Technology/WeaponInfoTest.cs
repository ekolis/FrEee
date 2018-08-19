using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FrEee.Tests.Game.Objects.Technology
{
    /// <summary>
    /// Tests weapon info.
    /// </summary>
    [TestClass]
    public class WeaponInfoTest
    {
        #region Private Fields

        private Galaxy gal = new Galaxy();

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// Tests formula damage values.
        /// </summary>
        [TestMethod]
        public void FormulaDamage()
        {
            var mod = Mod.Load("WeaponInfoTest");
            var ct = mod.ComponentTemplates.Single(x => x.Name == "Formula Weapon");
            var comp = ct.Instantiate();
            Assert.AreEqual<int>(3, ct.WeaponInfo.MinRange);
            Assert.AreEqual<int>(5, ct.WeaponInfo.MaxRange);
            Assert.AreEqual<int>(0, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 2)));
            Assert.AreEqual<int>(20, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 4)));
            Assert.AreEqual<int>(0, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 6)));
        }

        [TestInitialize]
        public void Init()
        {
            TestUtilities.SetEntryAssembly();
        }

        /// <summary>
        /// Tests non-formula damage values.
        /// </summary>
        [TestMethod]
        public void NonFormulaDamage()
        {
            var mod = Mod.Load("WeaponInfoTest");
            var ct = mod.ComponentTemplates.Single(x => x.Name == "Non-Formula Weapon");
            var comp = ct.Instantiate();
            Assert.AreEqual<int>(3, ct.WeaponInfo.MinRange);
            Assert.AreEqual<int>(5, ct.WeaponInfo.MaxRange);
            Assert.AreEqual<int>(0, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 2)));
            Assert.AreEqual<int>(20, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 4)));
            Assert.AreEqual<int>(0, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 6)));
        }

        #endregion Public Methods
    }
}
