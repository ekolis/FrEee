using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrEee.Modding.Templates;
using FrEee.Game.Objects.Technology;
using FrEee.Modding.Loaders;
using FrEee.Modding;
using FrEee.Game.Objects.Combat;

namespace FrEee.Tests.Game.Objects.Technology
{
	/// <summary>
	/// Tests weapon info.
	/// </summary>
	[TestClass]
	public class WeaponInfoTest
	{
		/// <summary>
		/// Tests non-formula damage values.
		/// </summary>
		[TestMethod]
		public void NonFormulaDamage()
		{
			var data =
@"*BEGIN*


Name := Generic Weapon
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
			var comp = mod.ComponentTemplates.Single();
			Assert.AreEqual<int>(3, comp.WeaponInfo.MinRange);
			Assert.AreEqual<int>(5, comp.WeaponInfo.MaxRange);
			Assert.AreEqual<int>(0, comp.WeaponInfo.GetDamage(new Shot(null, null, 2)));
			Assert.AreEqual<int>(20, comp.WeaponInfo.GetDamage(new Shot(null, null, 4)));
			Assert.AreEqual<int>(0, comp.WeaponInfo.GetDamage(new Shot(null, null, 6)));
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
			var comp = mod.ComponentTemplates.Single();
			Assert.AreEqual<int>(3, comp.WeaponInfo.MinRange);
			Assert.AreEqual<int>(5, comp.WeaponInfo.MaxRange);
			Assert.AreEqual<int>(0, comp.WeaponInfo.GetDamage(new Shot(null, null, 2)));
			Assert.AreEqual<int>(20, comp.WeaponInfo.GetDamage(new Shot(null, null, 4)));
			Assert.AreEqual<int>(0, comp.WeaponInfo.GetDamage(new Shot(null, null, 6)));
		}
	}
}
