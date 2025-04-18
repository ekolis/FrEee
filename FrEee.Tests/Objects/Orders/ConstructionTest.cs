﻿using FrEee.Extensions;
using FrEee.Modding;
using FrEee.Modding.Loaders;
using FrEee.Modding.Templates;
using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Objects.Space;
using FrEee.Objects.Technology;
using FrEee.Plugins;
using FrEee.Utility;
using NUnit.Framework;

namespace FrEee.Objects.Orders;

/// <summary>
/// Tests construction queue capabilities.
/// </summary>
public class ConstructionTest
{
	private static Colony colony;
	private static Empire empire;
	private static Planet planet;
	private static Race race;
	private static FacilityTemplate sy;

	[OneTimeSetUp]
	public static void ClassInit()
	{
		// initalize DI
		PluginLibrary.Instance.LoadDefaultPlugins();

		// initialize galaxy
		TestUtilities.Initialize();

		// initialize empires
		empire = new Empire();
		empire.Name = "Engi";
		race = new Race();
		race.Name = "Engi";
		race.Aptitudes["Construction"] = 100;

		// initialize components
		sy = Mod.Current.FacilityTemplates.FindByName("Space Yard Facility I");
	}

	[SetUp]
	public void Init()
	{
		// initialize colony
		planet = new Planet();
		colony = new Colony();
		colony.Facilities.Add(new Facility(sy));
		colony.Population.Add(race, (long)1e9); // 1 billion population;
		colony.ConstructionQueue = Services.ConstructionQueues.CreateConstructionQueue(planet);
		planet.Colony = colony;
	}

	/// <summary>
	/// If there's no population on a colony, it shouldn't be able to build anything.
	/// </summary>
	[Test]
	public void NoPopNoBuild()
	{
		colony.Population.Clear();
		Assert.IsTrue(planet.ConstructionQueue.Rate.IsEmpty);

		colony.Population[race] = 0;
		Assert.IsTrue(planet.ConstructionQueue.Rate.IsEmpty);
	}

	// TODO - test construction of facilities/units/ships
}
