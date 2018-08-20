using FrEee.Modding;
using FrEee.Utility.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrEee.Tests.Modding.Loaders
{
	/// <summary>
	/// Summary description for ComponentLoaderTest
	/// </summary>
	[TestClass]
	public class ComponentLoaderTest
	{
		[TestInitialize]
		public void Init()
		{
			TestUtilities.SetEntryAssembly();
		}

		/// <summary>
		/// Makes sure standard weapons have sane values for their minimum/maximum range.
		/// </summary>
		[TestMethod]
		public void WeaponRange()
		{
			Mod.Load("ComponentLoaderTest");
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
			Mod.Load("ComponentLoaderTest");
			var w = Mod.Current.ComponentTemplates.FindByName("Blind Spot Weapon");
			Assert.AreEqual(2, w.WeaponInfo.MinRange.Value);
			Assert.AreEqual(3, w.WeaponInfo.MaxRange.Value);
		}
	}
}