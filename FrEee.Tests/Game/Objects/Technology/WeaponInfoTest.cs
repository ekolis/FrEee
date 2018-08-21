using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FrEee.Tests.Game.Objects.Technology
{
	/// <summary>
	/// Tests weapon info.
	/// </summary>
	[TestClass]
	public class WeaponInfoTest
	{
		private static Galaxy gal = new Galaxy();

		private static Mod mod;

		[ClassInitialize]
		public static void ClassInit(TestContext ctx)
		{
			mod = Mod.Load("WeaponInfoTest");
		}

		/// <summary>
		/// Tests formula damage values.
		/// </summary>
		[TestMethod]
		public void FormulaDamage()
		{
			var ct = mod.ComponentTemplates.Single(x => x.Name == "Formula Weapon");
			var comp = ct.Instantiate();
			Assert.AreEqual<int>(3, ct.WeaponInfo.MinRange);
			Assert.AreEqual<int>(5, ct.WeaponInfo.MaxRange);
			Assert.AreEqual<int>(0, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 2)));
			Assert.AreEqual<int>(20, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 4)));
			Assert.AreEqual<int>(0, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 6)));
		}

		/// <summary>
		/// Tests non-formula damage values.
		/// </summary>
		[TestMethod]
		public void NonFormulaDamage()
		{
			var ct = mod.ComponentTemplates.Single(x => x.Name == "Non-Formula Weapon");
			var comp = ct.Instantiate();
			Assert.AreEqual<int>(3, ct.WeaponInfo.MinRange);
			Assert.AreEqual<int>(5, ct.WeaponInfo.MaxRange);
			Assert.AreEqual<int>(0, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 2)));
			Assert.AreEqual<int>(20, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 4)));
			Assert.AreEqual<int>(0, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 6)));
		}
	}
}