using System;
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
			var tech = new FrEee.Game.Objects.Technology.Technology
			{
				Name = "Test Tech",
				LevelCost = 10000,
				MaximumLevel = 2,
			};
			mod.Technologies.Add(tech);
			var gal = new Galaxy(mod);
			var emp = new Empire();
			gal.Empires.Add(emp);
			emp.BonusResearch = 30000;
			var cmd = new ResearchCommand(emp);
			cmd.SetSpending(tech, 100);

			// check command client safety
			cmd.CheckForClientSafety();

			// perform research
			emp.ResearchCommand = cmd;
			Galaxy.ProcessTurn();

			// verify research was done
			// 10K for first level, 20K for second
			emp = gal.Empires[0];
			Assert.AreEqual(2, emp.ResearchedTechnologies[tech]);
		}
	}
}
