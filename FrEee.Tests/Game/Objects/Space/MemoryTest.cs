using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using System.Linq;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using System.Drawing;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility.Extensions;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Interfaces;
using FrEee.Modding;
using FrEee.Modding.Templates;

namespace FrEee.Tests.Game.Objects.Space
{
	/// <summary>
	/// Tests memory sight / fog of war.
	/// </summary>
	[TestClass]
	public class MemoryTest
	{
		#region variables
		/// <summary>
		/// The ship that is looking for an enemy ship.
		/// </summary>
		Ship destroyer;

		/// <summary>
		/// They're controlling the destroyer.
		/// </summary>
		Empire seekers;

		/// <summary>
		/// The ship that is hiding.
		/// </summary>
		Ship submarine;

		/// <summary>
		/// They're controlling the submarine.
		/// </summary>
		Empire hiders;

		/// <summary>
		/// Where the ships are.
		/// </summary>
		StarSystem here;

		/// <summary>
		/// Where the submarine is going.
		/// </summary>
		StarSystem there;
		#endregion

		[TestInitialize]
		public void Setup()
		{
			// initialize galaxy
			new Galaxy();
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

		private void HideSubmarine()
		{
			submarine.Sector = there.GetSector(0, 0);
		}

		private void ReturnSubmarine()
		{
			submarine.Sector = here.GetSector(0, 0);
		}

		[TestMethod]
		public void CreatingMemory()
		{
			// make sure a memory is created when the vehicle is seen
			submarine.UpdateEmpireMemories();
			var mem = (Ship)seekers.Memory[submarine.ID];
			IsNotNull(mem, "Memory was not created for visible ship.");
			IsNotNull(mem.StarSystem, "Memory was not placed in a star system for visible ship.");
			HideSubmarine();

			// make sure the memory was not updated when the sub was moved
			AreEqual(here, mem.StarSystem, "Memory of ship was updated even though it is no longer visible.");
		}

		// TODO - create test for fogged ship reappearing
	}
}
