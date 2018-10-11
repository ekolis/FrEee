using System;
using FrEee.Game.Objects.Space;
using FrEee.Game.Setup;
using FrEee.Modding;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrEee.Tests.Game.Setup
{
	[TestClass]
	public class GameSetupTest
	{
		[TestMethod]
		public void Quickstart()
		{
			var setup = GameSetup.Load(@"..\..\..\..\FrEee\GameSetups\quickstart.gsu");
			Mod.Current = Mod.Load(null);
			Galaxy.Initialize(setup, null);
		}
	}
}