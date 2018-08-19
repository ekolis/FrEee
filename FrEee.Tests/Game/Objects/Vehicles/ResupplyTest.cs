﻿using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Vehicles;
using FrEee.Modding;
using FrEee.Modding.Templates;
using FrEee.Utility.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace FrEee.Tests.Game.Objects.Vehicles
{
    /// <summary>
    /// Tests resupply of vehicles.
    /// </summary>
    [TestClass]
    public class ResupplyTest
    {
        #region Private Fields

        private Empire empire;
        private Fleet fleet;
        private Ship ship1, ship2;
        private ComponentTemplate storageComp;
        private int supplyPerComp;
        private StarSystem sys;

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// Can ships in fleets resupply each other?
        /// </summary>
        [TestMethod]
        public void FleetResupply()
        {
            ship1.SupplyRemaining = supplyPerComp;
            ship2.SupplyRemaining = supplyPerComp;
            fleet.ShareSupplies();
            AreEqual(supplyPerComp * 2 / 4, ship1.SupplyRemaining);
            AreEqual(supplyPerComp * 6 / 4, ship2.SupplyRemaining);
        }

        [TestInitialize]
        public void Setup()
        {
            // initialize galaxy
            new Galaxy();
            Mod.Load(null);
            sys = new StarSystem(0);
            Galaxy.Current.StarSystemLocations.Add(new ObjectLocation<StarSystem>(sys, new Point()));

            // initialize empires
            empire = new Empire();
            empire.Name = "Engi";

            // initialize components
            storageComp = Mod.Current.ComponentTemplates.FindByName("Supply Storage I");
            supplyPerComp = storageComp.GetAbilityValue("Supply Storage").ToInt();

            // initialize ships
            Assert.IsNotNull(Mod.Current);
            var dsn1 = new Design<Ship>();
            dsn1.BaseName = "Shippy McShipface";
            dsn1.CreateHull();
            dsn1.Components.Add(new MountedComponentTemplate(dsn1, storageComp));
            dsn1.Owner = empire;
            ship1 = dsn1.Instantiate();
            ship1.Owner = empire;
            var dsn2 = new Design<Ship>();
            dsn2.BaseName = "Shippy McShipface Mk2";
            dsn2.CreateHull();
            dsn2.Components.Add(new MountedComponentTemplate(dsn1, storageComp));
            dsn2.Components.Add(new MountedComponentTemplate(dsn1, storageComp));
            dsn2.Components.Add(new MountedComponentTemplate(dsn1, storageComp));
            dsn2.Owner = empire;
            ship2 = dsn2.Instantiate();
            ship2.Owner = empire;

            // initialize fleet
            fleet = new Fleet();
            fleet.Owner = empire;
            fleet.Vehicles.Add(ship1);
            fleet.Vehicles.Add(ship2);

            // place ships
            sys.Place(fleet, new Point());
        }

        #endregion Public Methods

        // TODO - test quantum reactors

        // TODO - test resupply depots

        // TODO - test system wide resupply
    }
}
