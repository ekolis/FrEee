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
		/// Tests the ability of an empire to research technologies.
		/// </summary>
		[TestMethod]
		public void Research()
		{
			// steup
			var mod = new Mod();
			var tech = new FrEee.Game.Objects.Technology.Technology
			{
				Name = "Test Tech",
				LevelCost = 10000
			};
			mod.Technologies.Add(tech);
			var gal = new Galaxy(mod);
			var emp = new Empire();
			gal.Empires.Add(emp);
			emp.Income[Resource.Research] = 30000;
			var cmd = new ResearchCommand(emp);

			// check command client safety
			cmd.CheckForClientSafety();

			// perform research
			emp.Commands.Add(cmd);
			cmd.Execute();
			gal.ProcessTurn();

			// verify research was done
			// 10K for first level, 20K for second
			Assert.AreEqual(2, emp.ResearchedTechnologies[tech]);
		}
	}
}
