using System.Drawing;
using FrEee.Extensions;
using FrEee.Modding.Abilities;
using FrEee.Modding.Templates;
using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Objects.Space;
using FrEee.Vehicles;
using FrEee.Vehicles.Types;
using NUnit.Framework;

namespace FrEee.Objects.Abilities;

/// <summary>
/// Tests abilities and ability rules.
/// </summary>
public class AbilityTest
{
	StarSystem sys;
	Empire emp;
	IDesign design;
	IHull hull;
	IVehicle ship;

	[SetUp]
	public void SetUp()
	{
		// create galaxy
		TestUtilities.CreateGalaxyWithMod();

		// create star system
		sys = new(0);
		Galaxy.Current.StarSystemLocations.Add(new(sys, new Point()));

		// create stuff
		emp = TestUtilities.CreateEmpire();
		design = TestUtilities.CreateDesign(emp, VehicleTypes.Ship);
		hull = TestUtilities.CreateHull(VehicleTypes.Ship, design.Name);
		ship = TestUtilities.CreateVehicle(design, emp);
	}

	[Test]
	public void AbilitiesFromMultipleSources()
	{
		// create a storm
		Storm storm = new();

		// place 'em
		ship.Sector = sys.GetSector(0, 0);
		storm.Sector = ship.Sector;

		// assign some abilities
		design.Hull.AddAbility("Combat To Hit Offense Plus", 1);
		storm.AddAbility("Combat Modifier - Sector", 2);
		design.Hull.AddAbility("Combat Modifier - System", 4);
		design.Hull.AddAbility("Combat Modifier - Empire", 8);

		// make the testing a bit faster
		Game.Current.EnableAbilityCache();

		// make sure the ship picked them all up
		// TODO - we need to define ability scopes since some abilities should apply only to a single empire and others to everyone (see issue #1015)
		Assert.AreEqual(1 + 2 + 4 + 8, ship.Accuracy);

		// make sure the ship has all the abiliites inherited in
		Assert.AreEqual(1, ship.GetAbilityValue("Combat To Hit Offense Plus").ToInt());
		Assert.AreEqual(2, ship.GetAbilityValue("Combat Modifier - Sector").ToInt());
		Assert.AreEqual(4, ship.GetAbilityValue("Combat Modifier - System").ToInt());
		Assert.AreEqual(8, ship.GetAbilityValue("Combat Modifier - Empire").ToInt());
	}

	[Test]
	public void AbilitiesFromIdenticalComponents()
	{
		// create engine component template
		ComponentTemplate engineTemplate = new()
		{
			Name = "Ion Engine"
		};
		engineTemplate.Abilities.Add(new Ability(
			engineTemplate,
			AbilityRule.Find("Standard Ship Movement"),
			null,
			1));

		// add some engines
		int numEngines = 6;
		for (var i = 0; i < numEngines; i++)
		{
			design.AddComponent(engineTemplate);
		}

		// test thrust
		Assert.AreEqual(numEngines, design.StrategicSpeed);
	}
}
