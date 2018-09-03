using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// Information about a player.
	/// </summary>
	[ClientSafe]
	public class PlayerInfo
	{
		public string Name { get; set; }

		public string Email { get; set; }

		public string Pbw { get; set; }

		public string Irc { get; set; }

		public string Discord { get; set; }

		public string Website { get; set; }

		public string Notes { get; set; }
	}
}
