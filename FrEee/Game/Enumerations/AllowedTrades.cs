using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Enumerations
{
	public enum AllowedTrades
	{
		/// <summary>
		/// No trades or gifts are allowed.
		/// </summary>
		None = 0,
		/// <summary>
		/// Anything except technology can be traded or gifted.
		/// </summary>
		AllButTechnology = 1,
		/// <summary>
		/// Anything, including technology, can be traded or gifted.
		/// </summary>
		All = 2
	}
}
