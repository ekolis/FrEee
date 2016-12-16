﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Game.Objects.Commands;
using FrEee.Utility.Extensions;
using FrEee.Game.Enumerations;

namespace FrEee.Tests.Game.Objects.Technology
{
	[TestClass]
	public class TechnologyTest
	{
		/// <summary>
		/// Loads the stock mod. Done once before running ALL the tests (not each individually; that would be pointless).
		// TODO - hardcode some techs so we aren't reliant on stock files
		/// </summary>
		[ClassInitialize]
		public static void ClassInit(TestContext ctx)
		{
			Mod.Load(null);
		}

		/// <summary>
		/// Sets up a game. Done once before EACH test.
		/// </summary>
		[TestInitialize]
		public void TestInit()
		{
			new Galaxy(Mod.Current);
			Galaxy.Current.TechnologyCost = TechnologyCost.Low;
			emp = new Empire();
			Galaxy.Current.Empires.Add(emp);
		}

		private Empire emp;

		/// <summary>
		/// Tests the ability of an empire to research technologies using percentage allocation.
		/// </summary>
		[TestMethod]
		public void PercentageResearch()
		{
			var mod = Mod.Current;
			var gal = Galaxy.Current;

			var tech = mod.Technologies.FindByName("Ice Planet Colonization");
			tech.LevelCost = 500000; // in case the mod changes
			tech.MaximumLevel = 5; // so we can actually test getting past level 1

			// TODO - allocate to multiple techs

			emp.ResearchedTechnologies[tech] = 0;
			emp.BonusResearch = tech.GetLevelCost(1) + tech.GetLevelCost(2);

			var cmd = new ResearchCommand();
			cmd.Issuer = emp;
			cmd.Executor = emp;
			cmd.Spending[tech] = 100;

			// check command client safety
			cmd.CheckForClientSafety();

			// perform research
			emp.ResearchCommand = cmd;
			Galaxy.ProcessTurn(false);

			// verify research was done
			// 500K for first level, 1M for second
			Assert.AreEqual(2, emp.ResearchedTechnologies[tech]);
		}

		[TestMethod]
		public void QueuedResearch()
		{
			// for convenience
			var mod = Mod.Current;
			var gal = Galaxy.Current;

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
			emp.BonusResearch = t1.GetLevelCost(1) + 1;

			// create research command
			var cmd = new ResearchCommand
			{
				Issuer = emp,
				Executor = emp,
			};
			cmd.Queue.Add(t1);
			cmd.Queue.Add(t2);

			// perform research
			emp.ResearchCommand = cmd;
			Galaxy.ProcessTurn(false);

			// should have level 1 in t1 and level 0 in t2
			Assert.AreEqual(1, emp.ResearchedTechnologies[t1]);
			Assert.AreEqual(0, emp.ResearchedTechnologies[t2]);

			// should have no spillover in t1 and 1001 points in t2
			Assert.AreEqual(0, emp.GetResearchProgress(t1, 2).Value);
			Assert.AreEqual(1001, emp.GetResearchProgress(t2, 1).Value);

			// another turn!
			emp.BonusResearch = 100000;
			emp.ResearchCommand = cmd;
			Galaxy.ProcessTurn(false);

			// should have level 2+ in t1 and level 1+ in t2
			// (levels could be higher if extra RP gets randomly assigned to those techs)
			Assert.IsTrue(2 <= emp.ResearchedTechnologies[t1], $"{t1} level should be at least 2");
			Assert.IsTrue(1 <= emp.ResearchedTechnologies[t2], $"{t2} level should be at least 1");
		}
	}
}
