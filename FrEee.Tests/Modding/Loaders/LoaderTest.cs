using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrEee.Modding;

namespace FrEee.Tests.Modding.Loaders
{
	/// <summary>
	/// Tests mod loaders.
	/// </summary>
	[TestClass]
	public class LoaderTest
	{
		/// <summary>
		/// Makes sure the stock mod loads with no errors.
		/// </summary>
		[TestMethod]
		public void LoadStockModWithoutErrors()
		{
			Mod.Load(null);
			Assert.AreEqual(0, Mod.Errors.Count);
		}
	}
}
