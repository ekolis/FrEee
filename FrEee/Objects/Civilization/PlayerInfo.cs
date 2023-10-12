using FrEee.Interfaces;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Objects.Civilization
{
	/// <summary>
	/// Information about a player.
	/// </summary>
	public class PlayerInfo : IPromotable
	{
		public string Name { get; set; }

		public string Email { get; set; }

		public string Pbw { get; set; }

		public string Irc { get; set; }

		public string Discord { get; set; }

		public string Website { get; set; }

		public string Notes { get; set; }

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			// nothing to do here
		}
	}
}
