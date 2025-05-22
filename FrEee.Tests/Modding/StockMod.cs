using FrEee.Modding.Loaders;

namespace FrEee.Modding;

/// <summary>
/// The stock FrEee mod.
/// </summary>
public static class StockMod
{
	public static Mod Instance
	{
		get
		{
			if (instance == null)
				instance = new ModLoader().Load(null, includeGuiPlugins: false);
			return instance;
		}
	}

	private static Mod instance;
}
