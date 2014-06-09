using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrEee.Modding.Templates;
using FrEee.Game.Objects.Technology;
using FrEee.Modding.Loaders;
using FrEee.Modding;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Space;

namespace FrEee.Tests.Game.Objects.Technology
{
	/// <summary>
	/// Tests weapon info.
	/// </summary>
	[TestClass]
	public class WeaponInfoTest
	{
        Galaxy gal = new Galaxy();
		/// <summary>
		/// Tests non-formula damage values.
		/// </summary>
		[TestMethod]
		public void NonFormulaDamage()
		{
			var data =
@"*BEGIN*


Name := Generic Weapon
Tonnage Space Taken := 20
Tonnage Structure := 20
Weapon Damage At Rng := 30 20 10
Weapon Type := Direct Fire
Weapon Display Type := Beam
Pic := test
Min Range := 3
Max Range := 5";
			var df = new DataFile(data);
			var loader = new ComponentLoader("Test Mod");
			loader.DataFile = df;
			var mod = new Mod();
			loader.Load(mod);
			var ct = mod.ComponentTemplates.Single();
			var comp = ct.Instantiate();
			Assert.AreEqual<int>(3, ct.WeaponInfo.MinRange);
			Assert.AreEqual<int>(5, ct.WeaponInfo.MaxRange);
			Assert.AreEqual<int>(0, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 2)));
			Assert.AreEqual<int>(20, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 4)));
			Assert.AreEqual<int>(0, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 6)));
		}

		/// <summary>
		/// Tests formula damage values.
		/// </summary>
		[TestMethod]
		public void FormulaDamage()
		{
			var data =
@"*BEGIN*


Name := Generic Weapon
Tonnage Space Taken := 20
Tonnage Structure := 20
Weapon Damage At Rng := ==60 - range * 10
Weapon Type := Direct Fire
Weapon Display Type := Beam
Pic := test
Min Range := 3
Max Range := 5";
			var df = new DataFile(data);
			var loader = new ComponentLoader("Test Mod");
			loader.DataFile = df;
			var mod = new Mod();
			loader.Load(mod);
			var ct = mod.ComponentTemplates.Single();
			var comp = ct.Instantiate();
			Assert.AreEqual<int>(3, ct.WeaponInfo.MinRange);
			Assert.AreEqual<int>(5, ct.WeaponInfo.MaxRange);
			Assert.AreEqual<int>(0, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 2)));
			Assert.AreEqual<int>(20, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 4)));
			Assert.AreEqual<int>(0, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 6)));
		}
	}
}
