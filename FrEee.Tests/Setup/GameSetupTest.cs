using FrEee.Modding;
using FrEee.Objects.GameState;
using FrEee.Processes.Setup;
using FrEee.Utility;
using NUnit.Framework;

namespace FrEee.Setup;

public class GameSetupTest
{
	[Test]
	public void Quickstart()
	{
		SafeType.RegisterAssembly(typeof(GameSetup).Assembly);
		var setup = GameSetup.Load(@"..\..\..\..\FrEee.Assets\GameSetups\quickstart.gsu");
		Mod.Current = Mod.Load(null);
		Galaxy.Initialize(setup, null);
	}
}
