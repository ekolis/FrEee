using FrEee.Modding;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrEee.Tests.Modding.Loaders
{
	/// <summary>
	/// Tests mod loaders.
	/// </summary>
	[TestClass]
	public class LoaderTest
	{
		[TestMethod]
		public void LoadIncludeModWithoutErrors()
		{
			var mod = Mod.Load("Include Mod");
			Assert.AreEqual(0, Mod.Errors.Count);
			var mod2 = Mod.Load("ComponentLoaderTest", false);
			Assert.AreEqual(mod2.ComponentTemplates.Count, mod.ComponentTemplates.Count);
		}

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