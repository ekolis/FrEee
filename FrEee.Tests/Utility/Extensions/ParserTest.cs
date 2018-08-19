using FrEee.Utility;
using FrEee.Utility.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FrEee.Tests.Utility.Extensions
{
    /// <summary>
    /// Tests parsing capabilities.
    /// </summary>
    [TestClass]
    public class ParserTest
    {
        #region Private Enums

        /// <summary>
        /// A test enum.
        /// </summary>
        [Flags]
        private enum Rush
        {
            Rush,

            [Name("Rush Coil")]
            RushCoil,

            [CanonicalName("Rush Jet")]
            RushJet,

            [Name("Rush Jet")]
            FakeRushJet,

            [CanonicalName("Rush Marine")]
            [Name("Marine Adapter")]
            RushMarine,

            [CanonicalName("Rush Marine")]
            [Name("Marine Adapter")]
            ReallyFakeRushMarine,
        }

        #endregion Private Enums

        #region Public Methods

        /// <summary>
        /// Makes sure nonexistent enum values throw an exception.
        /// </summary>
        [TestMethod]
        public void BadEnumValues()
        {
            try
            {
                var bad = "Bass";
                var result = bad.ParseEnum<Rush>();
                Assert.Fail("Expected exception when parsing nonexistent enum value {0}, got {1}.".F(bad, result));
            }
            catch
            {
                // good, there was an exception
            }
        }

        /// <summary>
        /// Makes sure decimals are parsed correctly.
        /// </summary>
        [TestMethod]
        public void Decimals()
        {
            Assert.AreEqual(9000, "0.009M".ParseUnits());
        }

        /// <summary>
        /// Makes sure duplicate enum values throw an exception.
        /// </summary>
        [TestMethod]
        public void DuplicateEnumValues()
        {
            // duplicate canonical names
            try
            {
                var bad = "Rush Marine";
                var result = bad.ParseEnum<Rush>();
                Assert.Fail("Expected exception when parsing duplicate enum value {0}, got {1}.".F(bad, result));
            }
            catch
            {
                // good, there was an exception
            }

            // duplicate regular names, no canonical name
            try
            {
                var bad = "Marine Adapter";
                var result = bad.ParseEnum<Rush>();
                Assert.Fail("Expected exception when parsing duplicate enum value {0}, got {1}.".F(bad, result));
            }
            catch
            {
                // good, there was an exception
            }
        }

        /// <summary>
        /// Makes sure we can find enums by custom names.
        /// </summary>
        [TestMethod]
        public void FindEnumByName()
        {
            // test by alias name
            Assert.AreEqual(Rush.RushCoil, "Rush Coil".ParseEnum<Rush>());

            // test by canonical name
            Assert.AreEqual(Rush.RushJet, "Rush Jet".ParseEnum<Rush>());

            // Rush Marine is broken, it has two canonical names
        }

        /// <summary>
        /// Makes sure we can parse flags enums.
        /// </summary>
        [TestMethod]
        public void FlagsEnum()
        {
            Assert.AreEqual(Rush.RushCoil | Rush.RushJet, "Rush Coil, Rush Jet".ParseEnum<Rush>());
        }

        /// <summary>
        /// Makes sure millis and megas don't get mixed up.
        /// </summary>
        [TestMethod]
        public void MilliMegas()
        {
            Assert.AreEqual(1e6, "1M".ParseUnits(true));
            Assert.AreEqual(1e6, "1M".ParseUnits(false));
            Assert.AreEqual(1e-3, "1m".ParseUnits(true));
            Assert.AreEqual(1e6, "1m".ParseUnits(false));
        }

        /// <summary>
        /// Makes sure plain old numbers still get parsed.
        /// </summary>
        [TestMethod]
        public void RawNumbers()
        {
            Assert.AreEqual(9000, "9000".ParseUnits());
        }

        /// <summary>
        /// Makes sure standard enums are parsed correctly.
        /// </summary>
        [TestMethod]
        public void StandardEnums()
        {
            Assert.AreEqual(Rush.Rush, "Rush".ParseEnum<Rush>());
            Assert.AreEqual(Rush.RushCoil, "RushCoil".ParseEnum<Rush>());
            Assert.AreEqual(Rush.RushJet, "RushJet".ParseEnum<Rush>());
            Assert.AreEqual(Rush.RushMarine, "RushMarine".ParseEnum<Rush>());
        }

        /// <summary>
        /// Makes sure units are parsed correctly.
        /// </summary>
        [TestMethod]
        public void StandardUnits()
        {
            Assert.AreEqual(9000, "9k".ParseUnits());
            Assert.AreEqual(9000, "9K".ParseUnits());

            Assert.AreEqual(42e6, "42M".ParseUnits());
            Assert.AreEqual(42e6, "42000K".ParseUnits());

            Assert.AreEqual(7e9, "7B".ParseUnits());
            Assert.AreEqual(7e9, "7G".ParseUnits());
            Assert.AreEqual(7e9, "7b".ParseUnits());
            Assert.AreEqual(7e9, "7g".ParseUnits());

            Assert.AreEqual(12e12, "12T".ParseUnits());
            Assert.AreEqual(12e12, "12t".ParseUnits());
        }

        #endregion Public Methods
    }
}
