using NUnit.Framework;

namespace FrEee.Modding.Loaders;

/// <summary>
/// Tests mod loaders.
/// </summary>
public class LoaderTest
{
	[Test]
	public void LoadIncludeModWithoutErrors()
	{
		var mod = new ModLoader().Load("Include Mod");
		Assert.AreEqual(0, Mod.Errors.Count);
		var mod2 = new ModLoader().Load("ComponentLoaderTest", false);
		Assert.AreEqual(mod2.ComponentTemplates.Count, mod.ComponentTemplates.Count);
	}

	/// <summary>
	/// Makes sure the stock mod loads with no errors.
	/// </summary>
	[Test]
	public void LoadStockModWithoutErrors()
	{
		new ModLoader().Load(null);
		Assert.AreEqual(0, Mod.Errors.Count);
	}
}
