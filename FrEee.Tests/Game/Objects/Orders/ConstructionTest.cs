using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Modding;
using FrEee.Utility.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace FrEee.Tests.Game.Objects.Orders
{
	/// <summary>
	/// Tests construction queue capabilities.
	/// </summary>
	[TestClass]
	public class ConstructionTest
	{
		private static Colony colony;
		private static Empire empire;
		private static Planet planet;
		private static Race race;
		private static FacilityTemplate sy;

		[ClassInitialize]
		public static void ClassInit(TestContext ctx)
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
		}

		[TestInitialize]
		public void Init()
		{
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