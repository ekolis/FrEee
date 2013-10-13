using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.History
{
	/// <summary>
	/// History of a galaxy over a turn.
	/// This is the player's GAM file.
	/// It will be used to reconstitute a full Galaxy.
	/// </summary>
	public class GalaxyHistory
	{
		public GalaxyHistory(Galaxy galaxy, Empire emp)
		{
			Settings = galaxy.Settings;
			Empire = emp;
		}

		public GalaxySettings Settings { get; set; }

		public Empire Empire { get; set; }
	}
}
