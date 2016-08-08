using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using System.Linq;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using System.Drawing;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility.Extensions;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Interfaces;
using FrEee.Modding;
using FrEee.Modding.Templates;

namespace FrEee.Tests.Game.Objects.Orders
{
	/// <summary>
	/// Tests construction queue capabilities.
	/// </summary>
	[TestClass]
	public class ConstructionTest
	{
		#region variables
		Planet planet;
		Colony colony;
		Empire empire;
		Race race;
		FacilityTemplate sy;
		#endregion

		[TestInitialize]
		public void Setup()
		{
			// initialize galaxy
			new Galaxy();
			Mod.Load(null);

			// initialize empires
			empire = new Empire();
			empire.Name = "Engi";
			race = new Race();
			race.Name = "Engi";
			race.Aptitudes["Construction"] = 100;


			// initialize components
			sy = Mod.Current.FacilityTemplates.FindByName("Space Yard Facility I");

			// initialize colony
			planet = new Planet();
			colony = new Colony();
			colony.Facilities.Add(new Facility(sy));
			colony.Population.Add(race, (long)1e9); // 1 billion population;
			colony.ConstructionQueue = new ConstructionQueue(planet);
			planet.Colony = colony;
		}

		/// <summary>
		/// If there's no population on a colony, it shouldn't be able to build anything.
		/// </summary>
		[TestMethod]
		public void NoPopNoBuild()
		{
			colony.Population.Clear();
			IsTrue(planet.ConstructionQueue.Rate.IsEmpty);

			colony.Population[race] = 0;
			IsTrue(planet.ConstructionQueue.Rate.IsEmpty);
		}

		// TODO - test construction of facilities/units/ships
	}
}
