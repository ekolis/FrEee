using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Modding;
using FrEee.Utility.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace FrEee.Tests.Game.Objects.Orders
{
    /// <summary>
    /// Tests construction queue capabilities.
    /// </summary>
    [TestClass]
    public class ConstructionTest
    {
        #region Private Fields

        private Colony colony;
        private Empire empire;
        private Planet planet;
        private Race race;
        private FacilityTemplate sy;

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// If there's no population on a colony, it shouldn't be able to build anything.
        /// </summary>
        [TestMethod]
        public void NoPopNoBuild()
        {
            colony.Population.Clear();
            IsTrue(planet.ConstructionQueue.Rate.IsEmpty);

            colony.Population[race] = 0;
            IsTrue(planet.ConstructionQueue.Rate.IsEmpty);
        }

        [TestInitialize]
        public void Setup()
        {
            // initialize galaxy
            new Galaxy();
            Mod.Load(null);

            // initialize empires
            empire = new Empire();
            empire.Name = "Engi";
            race = new Race();
            race.Name = "Engi";
            race.Aptitudes["Construction"] = 100;

            // initialize components
            sy = Mod.Current.FacilityTemplates.FindByName("Space Yard Facility I");

            // initialize colony
            planet = new Planet();
            colony = new Colony();
            colony.Facilities.Add(new Facility(sy));
            colony.Population.Add(race, (long)1e9); // 1 billion population;
            colony.ConstructionQueue = new ConstructionQueue(planet);
            planet.Colony = colony;
        }

        #endregion Public Methods

        // TODO - test construction of facilities/units/ships
    }
}
