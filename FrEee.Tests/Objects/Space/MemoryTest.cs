using System.Drawing;
using FrEee.Extensions;
using FrEee.Modding;
using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Plugins;
using FrEee.Vehicles;
using FrEee.Vehicles.Types;
using NUnit.Framework;

namespace FrEee.Objects.Space;

/// <summary>
/// Tests memory sight / fog of war.
/// </summary>
public class MemoryTest
{
	/// <summary>
	/// The ship that is looking for an enemy ship.
	/// </summary>
	private IMajorSpaceVehicle destroyer;

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
	private IMajorSpaceVehicle submarine;

	/// <summary>
	/// Where the submarine is going.
	/// </summary>
	private StarSystem there;

	[Test]
	public void CreatingMemory()
	{
		// make sure a memory is created when the vehicle is seen
		submarine.UpdateEmpireMemories();
		var mem = (IMajorSpaceVehicle)seekers.Memory[submarine.ID];
		Assert.AreEqual(Visibility.Visible, submarine.CheckVisibility(seekers), "Ship is not visible to empire in same star system.");
		Assert.IsNotNull(mem, "Memory was not created for visible ship.");
		Assert.IsNotNull(mem.StarSystem, "Memory was not placed in a star system for visible ship.");
		Assert.IsTrue(mem.IsMemory, "Memory is not flagged as a memory.");

		// make sure the original vehicle is invisible when it moves
		HideSubmarine();
		Assert.AreEqual(Visibility.Fogged, submarine.CheckVisibility(seekers), "Ship is not fogged after it's left the star system.");

		// make sure the memory was not updated when the sub was moved
		Assert.AreEqual(here, mem.StarSystem, "Memory of ship was updated even though it is no longer visible.");

		// make sure the memory is visible to the correct empire
		Assert.AreEqual(Visibility.Fogged, mem.CheckVisibility(seekers), "Memory is not fogged.");
		Assert.AreEqual(Visibility.Unknown, mem.CheckVisibility(hiders), "Other empire's memory is not hidden from empire owning vehicle.");
	}

	[Test]
	public void NotDisappearingMemory()
	{
		// create memory of vehicle
		submarine.UpdateEmpireMemories();

		// move it away
		HideSubmarine();

		// redact things for the empire
		Game.Current.CurrentEmpire = seekers;
		Game.Current.Redact();

		// make sure it's still visible as a memory
		Assert.AreEqual(Visibility.Fogged, submarine.CheckVisibility(seekers), "Ship is not fogged after it's left the star system.");
		Assert.AreEqual(here.GetSector(0, 0), submarine.Sector, $"Ship should be appearing in its last known location {here.GetSector(0, 0)} but it's actually appearing at {submarine.Sector}.");
	}

	[SetUp]
	public void Setup()
	{
		// initialize DI
		PluginConfiguration.LoadDefaultPlugins();

		// initialize galaxy
		TestUtilities.Initialize();

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
		Game.Current.Empires.Add(seekers);
		Game.Current.Empires.Add(hiders);

		//. initialize exploration
		here.ExploredByEmpires.Add(seekers);

		// initialize ships
		Assert.IsNotNull(Mod.Current);
		var dsHull = TestUtilities.CreateHull(VehicleTypes.Ship, "Destroyer");
		var dsDesign = TestUtilities.CreateDesign(seekers, dsHull, "Destroyer");
		dsDesign.BaseName = "Destroyer";
		dsDesign.Owner = seekers;
		destroyer = (IMajorSpaceVehicle)dsDesign.Instantiate();
		destroyer.Owner = seekers;
		var subHull = TestUtilities.CreateHull(VehicleTypes.Ship, "Submarine");
		var subDesign = TestUtilities.CreateDesign(seekers, dsHull, "Submarine");
		subDesign.BaseName = "Submarine";
		subDesign.Owner = hiders;
		submarine = (IMajorSpaceVehicle)subDesign.Instantiate();
		submarine.Owner = hiders;

		// place ships
		destroyer.Sector = here.GetSector(0, 0);
		submarine.Sector = here.GetSector(0, 0);

		// register objects
		Game.Current.CleanGameState();
	}

	private void HideSubmarine()
	{
		submarine.Sector = there.GetSector(0, 0);
	}

	private void ReturnSubmarine()
	{
		submarine.Sector = here.GetSector(0, 0);
	}

	// TODO - create test for fogged ship reappearing
}
