using FrEee.Extensions;
using FrEee.Gameplay.Commands;
using FrEee.Gameplay.Commands.Projects;
using FrEee.Modding;
using FrEee.Modding.Loaders;
using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Processes;
using FrEee.Plugins;
using FrEee.Utility;
using NUnit.Framework;

namespace FrEee.Objects.Technology;

public class TechnologyTest
{
	private Empire emp;
	private static ITurnProcessor processor;

	/// <summary>
	/// Loads the stock mod. Done once before running ALL the tests (not each individually; that would be pointless).
	// TODO - hardcode some techs so we aren't reliant on stock files
	/// </summary>
	[OneTimeSetUp]
	public static void ClassInit()
	{
		PluginLibrary.Instance.LoadDefaultPlugins();
		processor = Services.TurnProcessor;
		new ModLoader().Load(null);
	}

	/// <summary>
	/// Tests the ability of an empire to research technologies using percentage allocation.
	/// </summary>
	[Test]
	public void PercentageResearch()
	{
		var mod = Mod.Current;
		var gal = Game.Current;

		var tech = mod.Technologies.FindByName("Ice Planet Colonization");
		tech.LevelCost = 500000; // in case the mod changes
		tech.MaximumLevel = 5; // so we can actually test getting past level 1

		// TODO - allocate to multiple techs

		emp.ResearchedTechnologies[tech] = 0;
		emp.BonusResearch = tech.GetBaseLevelCost(1) + tech.GetBaseLevelCost(2);

		var cmd = Services.ProjectCommands.Research();
		cmd.Issuer = emp;
		cmd.Executor = emp;
		cmd.Spending[tech] = 100;

		// check command client safety
		cmd.CheckForClientSafety();

		// perform research
		emp.ResearchCommand = cmd;
		processor.ProcessTurn(gal, false);

		// verify research was done
		// 500K for first level, 1M for second
		Assert.AreEqual(2, emp.ResearchedTechnologies[tech]);
	}

	[Test]
	public void QueuedResearch()
	{
		// for convenience
		var mod = Mod.Current;
		var gal = Game.Current;

		// set up techs
		var t1 = mod.Technologies.FindByName("Planetary Weapons");
		t1.LevelCost = 5000; // in case the mod changes
		var t2 = mod.Technologies.FindByName("Smaller Weapons");
		t2.LevelCost = 5000; // in case the mod changes
		emp.AccumulatedResearch[t1] = 1000; // make sure to test spillover properly

		// reset tech levels
		emp.ResearchedTechnologies[t1] = 0;
		emp.ResearchedTechnologies[t2] = 0;

		// give them some RP
		emp.BonusResearch = t1.GetBaseLevelCost(1) + 1;

		// create research command
		var cmd = Services.ProjectCommands.Research();
		cmd.Issuer = emp;
		cmd.Executor = emp;
		cmd.Queue.Add(t1);
		cmd.Queue.Add(t2);

		// perform research
		emp.ResearchCommand = cmd;
		processor.ProcessTurn(gal, false);

		// should have level 1 in t1 and level 0 in t2
		Assert.AreEqual(1, emp.ResearchedTechnologies[t1]);
		Assert.AreEqual(0, emp.ResearchedTechnologies[t2]);

		// should have no spillover in t1 and 1001 points in t2
		Assert.AreEqual(0, emp.GetResearchProgress(t1, 2).Value);
		Assert.AreEqual(1001, emp.GetResearchProgress(t2, 1).Value);

		// another turn!
		emp.BonusResearch = 100000;
		emp.ResearchCommand = cmd;
		processor.ProcessTurn(gal, false);

		// should have level 2+ in t1 and level 1+ in t2
		// (levels could be higher if extra RP gets randomly assigned to those techs)
		Assert.IsTrue(2 <= emp.ResearchedTechnologies[t1], $"{t1} level should be at least 2");
		Assert.IsTrue(1 <= emp.ResearchedTechnologies[t2], $"{t2} level should be at least 1");
	}

	/// <summary>
	/// Sets up a game. Done once before EACH test.
	/// </summary>
	[SetUp]
	public void TestInit()
	{
		var game = TestUtilities.Initialize();
		game.Setup = new()
		{
			TechnologyCost = TechnologyCost.Low
		};
		emp = new Empire();
		game.Empires.Add(emp);
	}
}
