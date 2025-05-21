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
        template = mod.FacilityTemplates.FindByName("Mineral Miner Facility I");
        order1 = new ConstructionOrder<Facility, FacilityTemplate> { Template = template, Item = new Facility(template) };
		order2 = new ConstructionOrder<Facility, FacilityTemplate> { Template = template, Item = new Facility(template) };
		empire = TestUtilities.CreateEmpire();
		planet = new();
        planet.Owner = empire;
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
		Assert.AreEqual(0, order1.Item.ConstructionProgress[Resource.Minerals]);
	}
}
