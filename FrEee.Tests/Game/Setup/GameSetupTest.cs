using FrEee.Game.Objects.Space;
using FrEee.Game.Setup;
using FrEee.Modding;
using NUnit.Framework;

namespace FrEee.Tests.Game.Setup
{
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
}