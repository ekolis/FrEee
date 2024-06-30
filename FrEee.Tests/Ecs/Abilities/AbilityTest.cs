using System.Drawing;
using System.Linq;
using FrEee.Ecs.Abilities.Utility;
using FrEee.Ecs.Interactions;
using FrEee.Ecs.Stats;
using FrEee.Extensions;
using FrEee.Modding;
using FrEee.Modding.Templates;
using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Objects.Space;
using FrEee.Objects.Technology;
using FrEee.Objects.Vehicles;
using FrEee.Utility;
using NUnit.Framework;

namespace FrEee.Ecs.Abilities;

/// <summary>
/// Tests abilities and ability rules.
/// </summary>
public class AbilityTest
{
    StarSystem sys;
    Empire emp;
    IDesign<Ship> design;
    IHull<Ship> hull;
    Ship ship;

    [SetUp]
    public void SetUp()
    {
        // set up reflection stuff
        SafeType.RegisterAssembly(typeof(Ability).Assembly);

        // create galaxy
        TestUtilities.CreateGalaxyWithMod();

        // create star system
        sys = new(0);
        Galaxy.Current.StarSystemLocations.Add(new(sys, new Point()));

        // create stuff
        emp = TestUtilities.CreateEmpire();
        design = TestUtilities.CreateDesign<Ship>(emp);
        hull = design.CreateHull();
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
        Galaxy.Current.EnableAbilityCache();

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
        engineTemplate.Abilities = [new Ability(
            engineTemplate,
            AbilityRule.Find("Standard Ship Movement"),
            null,
            1)];

        // add some engines
        int numEngines = 6;
        for (var i = 0; i < numEngines; i++)
        {
            design.AddComponent(engineTemplate);
        }

        // test thrust
        Assert.AreEqual(numEngines, design.StrategicSpeed);
    }

    [Test]
    public void ColonyInheritsAbilitiesFromFacilities()
    {
        FacilityTemplate facilityTemplate = new()
        {
            Name = "Soylent Green Facility"
        };
        facilityTemplate.Abilities = facilityTemplate.Abilities.Append(new ColonyResourceExtractionAbility(
            facilityTemplate,
            AbilityRule.Find("Resource Generation - Organics"),
            Resource.Organics.Name.ToLiteralFormula(),
            666.ToLiteralFormula()
		));
        Colony colony = new();
        for (var i = 0; i < 10; i++)
        {
            Facility facility = new(facilityTemplate);
            colony.FacilityAbilities.Add(facility.GetAbility<FacilityAbility>());
        }
        Assert.AreEqual(6660, colony.GetStatValue<int>(StatType.ColonyResourceExtractionOrganics));
    }
}
