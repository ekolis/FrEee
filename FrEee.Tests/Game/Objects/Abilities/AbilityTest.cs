using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using FrEee.Game.Objects.Civilization;
using System.Drawing;
using FrEee.Game.Objects.Vehicles;
using FrEee.Game.Objects.Abilities;
using FrEee.Utility.Extensions;

namespace FrEee.Tests.Game.Objects.Abilities
{
	/// <summary>
	/// Tests abilities and ability rules.
	/// </summary>
	[TestClass]
	public class AbilityTest
	{
		public AbilityTest()
		{

		}

		[TestMethod]
		public void AbilitiesFromMultipleSources()
		{
			// initialize galaxy
			new Galaxy();
			Mod.Current = Mod.Load(null);
			var sys = new StarSystem(0);
			Galaxy.Current.StarSystemLocations.Add(new ObjectLocation<StarSystem>(sys, new Point()));

			// initialize empire
			var emp = new Empire();
			emp.Name = "Galactic Empire";

			// create a ship
			var design = new Design<Ship>();
			design.BaseName = "Shippy";
			design.CreateHull();
			design.Owner = emp;
			var ship = design.Instantiate();
			ship.Owner = emp;

			// create a storm
			var storm = new Storm();

			// place 'em
			ship.Sector = sys.GetSector(0, 0);
			storm.Sector = ship.Sector;

			// assign some abilities
			design.Hull.AddAbility("Combat To Hit Offense Plus", 1);
			storm.AddAbility("Combat Modifier - Sector", 2);
			design.Hull.AddAbility("Combat Modifier - System", 4);
			design.Hull.AddAbility("Combat Modifier - Empire", 8);

			// make the testing a bit faster
			Galaxy.Current.EnableAbilityCache();

			// make sure the ship picked them all up
			Assert.AreEqual(1 + 2 + 4 + 8, ship.Accuracy);

			// make sure the ship has all the abiliites inherited in
			Assert.AreEqual(1, ship.GetAbilityValue("Combat To Hit Offense Plus").ToInt());
			Assert.AreEqual(2, ship.GetAbilityValue("Combat Modifier - Sector").ToInt());
			Assert.AreEqual(4, ship.GetAbilityValue("Combat Modifier - System").ToInt());
			Assert.AreEqual(8, ship.GetAbilityValue("Combat Modifier - Empire").ToInt());
		}
	}
}
