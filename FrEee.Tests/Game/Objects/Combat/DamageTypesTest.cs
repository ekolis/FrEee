using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Vehicles;
using FrEee.Modding;
using FrEee.Utility.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FrEee.Tests.Game.Objects.Combat
{
	[TestClass]
	public class DamageTypesTest
	{
		private static Mod mod;

		private Ship attacker;
		private IDesign<Ship> attackerDesign;
		private Ship defender;
		private IDesign<Ship> defenderDesign;

		[ClassInitialize]
		public static void ClassInit(TestContext ctx)
		{
			// load stock mod
			mod = Mod.Load(null);

			// create a galaxy for referencing things
			new Galaxy();
		}

		[TestInitialize]
		public void Init()
		{
			// create dummy designs
			attackerDesign = new Design<Ship>();
			attackerDesign.AddComponent(mod.ComponentTemplates.FindByName("Bridge"));
			attackerDesign.AddComponent(mod.ComponentTemplates.FindByName("Life Support"));
			attackerDesign.AddComponent(mod.ComponentTemplates.FindByName("Crew Quarters"));
			attackerDesign.AddComponent(mod.ComponentTemplates.FindByName("Quantum Reactor"));
			defenderDesign = new Design<Ship>();
			defenderDesign.AddComponent(mod.ComponentTemplates.FindByName("Bridge"));
			defenderDesign.AddComponent(mod.ComponentTemplates.FindByName("Life Support"));
			defenderDesign.AddComponent(mod.ComponentTemplates.FindByName("Crew Quarters"));
			defenderDesign.AddComponent(mod.ComponentTemplates.FindByName("Quantum Reactor"));
		}

		/// <summary>
		/// Makes sure that normal damage doesn't pierce shields or armor, and can destroy a ship.
		/// </summary>
		[TestMethod]
		public void NormalDamageVersusShips()
		{
			attackerDesign.AddComponent(mod.ComponentTemplates.FindByName("Depleted Uranium Cannon I"));
			attacker = attackerDesign.Instantiate();

			// armor should get hit before hull
			defenderDesign.AddComponent(mod.ComponentTemplates.FindByName("Armor I"));
			defender = defenderDesign.Instantiate();
			TestDamage(attacker, defender, "Depleted Uranium Cannon II", 1, expectedArmorDmg: 1);

			// phased shields should get hit before normal shields
			defenderDesign.AddComponent(mod.ComponentTemplates.FindByName("Shield Generator I"));
			defenderDesign.AddComponent(mod.ComponentTemplates.FindByName("Phased - Shield Generator I"));
			defender = defenderDesign.Instantiate();
			TestDamage(attacker, defender, "Depleted Uranium Cannon II", 1, expectedNormalShieldDmg: 1);

			// make sure our ship can be destroyed
			TestDamage(attacker, defender, "Depleted Uranium Cannon II", 99999, defender.HullHitpoints, defender.ArmorHitpoints, defender.PhasedShields, defender.NormalShields);
		}

		private void TestDamage(Ship attacker, IDamageable defender, string weaponName, int dmg, int expectedHullDmg = 0, int expectedArmorDmg = 0, int expectedPhasedShieldDmg = 0, int expectedNormalShieldDmg = 0)
		{
			defender.ReplenishShields();
			defender.Repair();

			var hhp = defender.HullHitpoints;
			var ahp = defender.ArmorHitpoints;
			var pshp = defender.PhasedShields;
			var nshp = defender.NormalShields;

			var hit = new Hit(new Shot(attacker, attacker.Components.FindByName(weaponName), defender, 0), defender, dmg);
			defender.TakeDamage(hit);
			Assert.AreEqual(hhp - expectedHullDmg, defender.HullHitpoints);
			Assert.AreEqual(ahp - expectedArmorDmg, defender.ArmorHitpoints);
			Assert.AreEqual(nshp - expectedNormalShieldDmg, defender.NormalShields);
			Assert.AreEqual(pshp - expectedPhasedShieldDmg, defender.PhasedShields);

			if (defender.HullHitpoints > 0)
				Assert.IsFalse(defender.IsDestroyed);
			else
				Assert.IsTrue(defender.IsDestroyed);

			defender.ReplenishShields();
			defender.Repair();
		}
	}
}