using FrEee.Modding;
using FrEee.Objects.GameState;
using FrEee.Processes.Setup;
using NUnit.Framework;

namespace FrEee.Setup;

public class GameSetupTest
{
	[Test]
	[Ignore("quickstart needs to be rebuilt in new version")]
	public void Quickstart()
	{
		var setup = GameSetup.Load(@"..\..\..\..\FrEee\GameSetups\quickstart.gsu");
		Mod.Current = Mod.Load(null);
		Galaxy.Initialize(setup, null);
	}
}
