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
		[TestInitialize]
		public void Init()
		{
			TestUtilities.SetEntryAssembly();
		}

        Galaxy gal = new Galaxy();
		/// <summary>
		/// Tests non-formula damage values.
		/// </summary>
		[TestMethod]
		public void NonFormulaDamage()
		{
			var mod = Mod.Load("WeaponInfoTest");
			var ct = mod.ComponentTemplates.Single(x => x.Name == "Non-Formula Weapon");
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
			var mod = Mod.Load("WeaponInfoTest");
			var ct = mod.ComponentTemplates.Single(x => x.Name == "Formula Weapon");
			var comp = ct.Instantiate();
			Assert.AreEqual<int>(3, ct.WeaponInfo.MinRange);
			Assert.AreEqual<int>(5, ct.WeaponInfo.MaxRange);
			Assert.AreEqual<int>(0, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 2)));
			Assert.AreEqual<int>(20, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 4)));
			Assert.AreEqual<int>(0, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 6)));
		}
	}
}
