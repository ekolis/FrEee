using FrEee.Objects.Space;
using FrEee.Setup;
using FrEee.Modding;
using NUnit.Framework;
using FrEee.Utility;

namespace FrEee.Tests.Game.Setup
{
	public class GameSetupTest
	{
		[Test]
		[Ignore("quickstart needs to be rebuilt in new version")]
		public void Quickstart()
		{
			var setup = GameSetup.Load(@"..\..\..\..\FrEee\GameSetups\quickstart.gsu");
			The.Mod = Mod.Load(null);
			FrEee.Game.Start(The.Mod, setup);
		}
	}
}
