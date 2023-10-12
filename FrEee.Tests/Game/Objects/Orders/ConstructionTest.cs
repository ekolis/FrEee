using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Objects.Technology;
using FrEee.Modding;
using FrEee.Utility.Extensions;
using NUnit.Framework;

namespace FrEee.Tests.Game.Objects.Orders
{
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

		[SetUp]
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
}