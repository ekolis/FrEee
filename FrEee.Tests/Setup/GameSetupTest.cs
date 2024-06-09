using FrEee.Modding;
using NUnit.Framework;
using FrEee.Objects.GameState;
using FrEee.Processes.Setup;

namespace FrEee.Tests.Setup;

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