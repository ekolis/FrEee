﻿using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Game.Setup;
using FrEee.Modding;
using FrEee.Utility.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace FrEee.Tests.Game.Objects.Space
{
    /// <summary>
    /// Tests memory sight / fog of war.
    /// </summary>
    [TestClass]
    public class MemoryTest
    {
        #region Private Fields

        /// <summary>
        /// The ship that is looking for an enemy ship.
        /// </summary>
        private Ship destroyer;

        /// <summary>
        /// Where the ships are.
        /// </summary>
        private StarSystem here;

        /// <summary>
        /// They're controlling the submarine.
        /// </summary>
        private Empire hiders;

        /// <summary>
        /// They're controlling the destroyer.
        /// </summary>
        private Empire seekers;

        /// <summary>
        /// The ship that is hiding.
        /// </summary>
        private Ship submarine;

        /// <summary>
        /// Where the submarine is going.
        /// </summary>
        private StarSystem there;

        #endregion Private Fields

        #region Public Methods

        [TestMethod]
        public void CreatingMemory()
        {
            // make sure a memory is created when the vehicle is seen
            submarine.UpdateEmpireMemories();
            var mem = (Ship)seekers.Memory[submarine.ID];
            AreEqual(Visibility.Visible, submarine.CheckVisibility(seekers), "Ship is not visible to empire in same star system.");
            IsNotNull(mem, "Memory was not created for visible ship.");
            IsNotNull(mem.StarSystem, "Memory was not placed in a star system for visible ship.");
            IsTrue(mem.IsMemory, "Memory is not flagged as a memory.");

            // make sure the original vehicle is invisible when it moves
            HideSubmarine();
            AreEqual(Visibility.Fogged, submarine.CheckVisibility(seekers), "Ship is not fogged after it's left the star system.");

            // make sure the memory was not updated when the sub was moved
            AreEqual(here, mem.StarSystem, "Memory of ship was updated even though it is no longer visible.");

            // make sure the memory is visible to the correct empire
            AreEqual(Visibility.Fogged, mem.CheckVisibility(seekers), "Memory is not fogged.");
            AreEqual(Visibility.Unknown, mem.CheckVisibility(hiders), "Other empire's memory is not hidden from empire owning vehicle.");
        }

        [TestMethod]
        public void NotDisappearingMemory()
        {
            // create memory of vehicle
            submarine.UpdateEmpireMemories();

            // move it away
            HideSubmarine();

            // redact things for the empire
            Empire.Current = seekers;
            Galaxy.Current.Redact();

            // make sure it's still visible as a memory
            AreEqual(Visibility.Fogged, submarine.CheckVisibility(seekers), "Ship is not fogged after it's left the star system.");
            AreEqual(here.GetSector(0, 0), submarine.Sector, $"Ship should be appearing in its last known location {here.GetSector(0, 0)} but it's actually appearing at {submarine.Sector}.");
        }

        [TestInitialize]
        public void Setup()
        {
            // initialize galaxy
            new Galaxy();
			Galaxy.Current.GameSetup = new GameSetup();
            Mod.Current = new Mod();

            // initialize star systems
            here = new StarSystem(0) { Name = "Here" };
            there = new StarSystem(0) { Name = "There" };
            Galaxy.Current.StarSystemLocations.Add(new ObjectLocation<StarSystem>(here, new Point()));
            Galaxy.Current.StarSystemLocations.Add(new ObjectLocation<StarSystem>(there, new Point(1, 1)));

            // initialize empires
            seekers = new Empire();
            seekers.Name = "Seekers";
            hiders = new Empire();
            hiders.Name = "Hiders";
            Galaxy.Current.Empires.Add(seekers);
            Galaxy.Current.Empires.Add(hiders);

            //. initialize exploration
            here.ExploredByEmpires.Add(seekers);

            // initialize ships
            Assert.IsNotNull(Mod.Current);
            var dsDesign = new Design<Ship>();
            dsDesign.BaseName = "Destroyer";
            dsDesign.CreateHull();
            dsDesign.Owner = seekers;
            destroyer = dsDesign.Instantiate();
            destroyer.Owner = seekers;
            var subDesign = new Design<Ship>();
            subDesign.BaseName = "Submarine";
            subDesign.CreateHull();
            subDesign.Owner = hiders;
            submarine = subDesign.Instantiate();
            submarine.Owner = hiders;

            // place ships
            destroyer.Sector = here.GetSector(0, 0);
            submarine.Sector = here.GetSector(0, 0);

            // register objects
            Galaxy.Current.AssignIDs();
        }

        #endregion Public Methods

        #region Private Methods

        private void HideSubmarine()
        {
            submarine.Sector = there.GetSector(0, 0);
        }

        private void ReturnSubmarine()
        {
            submarine.Sector = here.GetSector(0, 0);
        }

        #endregion Private Methods

        // TODO - create test for fogged ship reappearing
    }
}
