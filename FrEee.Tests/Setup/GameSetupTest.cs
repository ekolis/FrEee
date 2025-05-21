using FrEee.Modding;
using FrEee.Modding.Loaders;
using FrEee.Objects.GameState;
using FrEee.Processes.Setup;
using FrEee.Utility;
using NUnit.Framework;

namespace FrEee.Setup;

public class GameSetupTest
{
	[Test]
	[Ignore("quickstart needs to be rebuilt in new version")]
	public void Quickstart()
	{
		var setup = Services.Persistence.GameSetup.LoadFromFile(@"..\..\..\..\FrEee\GameSetups\quickstart.gsu");
		Mod.Current = new ModLoader().Load(null, includeGuiPlugins: false);
		Game.Initialize(setup, null);
	}
}
