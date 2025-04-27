using FrEee.Objects.GameState;
using System.Collections.Generic;

namespace FrEee.Objects.Civilization;

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

	public IPromotable ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
	{
		// nothing to do here
		return this;
	}
}
