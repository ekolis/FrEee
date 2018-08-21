using FrEee.Modding;
using FrEee.Utility.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrEee.Tests.Modding.Loaders
{
	/// <summary>
	/// Summary description for ComponentLoaderTest
	/// </summary>
	[TestClass]
	public class ComponentLoaderTest
	{
		[ClassInitialize]
		public static void ClassInit(TestContext ctx)
		{
			Mod.Load("ComponentLoaderTest");
		}

		/// <summary>
		/// Makes sure standard weapons have sane values for their minimum/maximum range.
		/// </summary>
		[TestMethod]
		public void WeaponRange()
		{
			var w = Mod.Current.ComponentTemplates.FindByName("Range 2 Weapon");
			Assert.AreEqual(0, w.WeaponInfo.MinRange.Value);
			Assert.AreEqual(2, w.WeaponInfo.MaxRange.Value);
		}

		/// <summary>
		/// Makes sure weapons with blind spots have sane values for their minimum/maximum range.
		/// </summary>
		[TestMethod]
		public void WeaponRangeBlindSpot()
		{
			var w = Mod.Current.ComponentTemplates.FindByName("Blind Spot Weapon");
			Assert.AreEqual(2, w.WeaponInfo.MinRange.Value);
			Assert.AreEqual(3, w.WeaponInfo.MaxRange.Value);
		}
	}
}