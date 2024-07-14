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
public class ColonyResourceExtractionAbilityTest
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
	public void ColonyResourceExtraction()
	{
		// arrange
		FacilityTemplate facilityTemplate = new()
		{
			Name = "Soylent Green Facility"
		};
		facilityTemplate.Abilities = facilityTemplate.Abilities.Append(new ColonyResourceExtractionAbility(
			facilityTemplate,
			AbilityRule.Find("Resource Generation - Organics"),
			Resource.Organics.Name.ToLiteralFormula(),
			500.ToLiteralFormula()
		));
		Colony colony = new();
		for (var i = 0; i < 10; i++)
		{
			Facility facility = new(facilityTemplate);
			colony.FacilityAbilities.Add(facility.GetAbility<FacilityAbility>());
		}
		ExtractResourcesFromColoniesInteraction interaction = new();

		// act
		colony.Interact(interaction);

		// assert
		// TODO: handle population/happiness/etc modifiers
		Assert.AreEqual(5000 * Resource.Organics, interaction.ColonyResources.Sum( kvp => kvp.Value));
	}
}
