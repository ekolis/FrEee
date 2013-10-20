﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Game.Objects.Commands;
using FrEee.Utility.Extensions;

namespace FrEee.Tests.Game.Objects.Technology
{
	[TestClass]
	public class TechnologyTest
	{
		/// <summary>
		/// Tests the ability of an empire to research technologies using percentage allocation.
		/// </summary>
		[TestMethod]
		public void PercentageResearch()
		{
			// setup
			var mod = Mod.Load(null);
			var tech = mod.Technologies.FindByName("Ice Planet Colonization");
			tech.LevelCost = 500000; // in case the mod changes
			var gal = new Galaxy(mod);
			var emp = new Empire();
			gal.Empires.Add(emp);
			emp.BonusResearch = 1500000;
			var cmd = new ResearchCommand();
			cmd.Issuer = emp;
			cmd.Executor = emp;
			cmd.SetSpending(tech, 100);

			// check command client safety
			cmd.CheckForClientSafety();

			// perform research
			emp.ResearchCommand = cmd;
			Galaxy.ProcessTurn(false);

			// verify research was done
			// 500K for first level, 1M for second
			Assert.AreEqual(2, emp.ResearchedTechnologies[tech]);
		}
	}
}
