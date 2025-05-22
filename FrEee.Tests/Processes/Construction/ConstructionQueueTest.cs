using System;
using System.Linq;
using FrEee.Extensions;
using FrEee.Modding;
using FrEee.Modding.Abilities;
using FrEee.Modding.Loaders;
using FrEee.Modding.Templates;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.GameState;
using FrEee.Objects.Space;
using FrEee.Objects.Technology;
using FrEee.Plugins.Default.Processes.Construction;
using FrEee.Utility;
using FrEee.Vehicles;
using FrEee.Vehicles.Types;
using NUnit.Framework;

namespace FrEee.Processes.Construction;

public class ConstructionQueueTest
{
	private static Mod mod;
	private FacilityTemplate template;
	private Empire empire;
	private Race race;
	private Planet planet;
	private ConstructionQueue queue;
	private ConstructionOrder<Facility, FacilityTemplate> order1;
	private ConstructionOrder<Facility, FacilityTemplate> order2;

	[OneTimeSetUp]
	public static void ClassInit()
	{
		TestUtilities.Initialize();
		mod = Mod.Current;
	}

	[SetUp]
	public void Init()
	{
		// TODO: mock construction queue rate calculations so we don't need a race, population, etc...
		template = mod.FacilityTemplates.FindByName("Mineral Miner Facility I");
		order1 = new ConstructionOrder<Facility, FacilityTemplate> { Template = template };
		order2 = new ConstructionOrder<Facility, FacilityTemplate> { Template = template };
		empire = TestUtilities.CreateEmpire();
		empire.StoredResources += 10000 * Resource.Minerals;
		empire.ResearchedTechnologies.Add(mod.Technologies.FindByName("Minerals Extraction"), 1);
		race = new();
		race.Aptitudes.Add(Aptitude.Construction.Name, 100);
		planet = new()
		{
			Owner = empire,
			Size = new() { MaxPopulation = 1_000_000, MaxPopulationDomed = 1_000_000 }
		};
		planet.AddPopulation(race, 1_000_000);
		planet.Abilities.Add(new(planet, AbilityRule.Find("Space Yard"), null, "1", 1000));
		queue = new(planet);
		queue.Orders.Add(order1);
		queue.Orders.Add(order2);
	}

	/// <summary>
	/// Verifies that only one item is built at a time in a construction queue.
	/// </summary>
	[Test]
	public void OneItemAtATime()
	{
		queue.ExecuteOrders();
		Assert.AreEqual(1000, order1.Item.ConstructionProgress[Resource.Minerals]);
		Assert.AreEqual(0, order2.Item.ConstructionProgress[Resource.Minerals]);
	}
}
