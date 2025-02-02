using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Extensions;

namespace FrEee.Persistence;

/// <summary>
/// Uniquely identifies a game state.
/// </summary>
/// <param name="Name">The name of the game.</param>
/// <param name="TurnNumber">The turn number (e.g. 0 for the first turn, 1 for the second, etc.)</param>
/// <param name="PlayerNumber">The player number (e.g. 0 for the host, 1 for the first player, etc.)</param>
public record GameID(string Name, int TurnNumber, int PlayerNumber)
	: IParsable<GameID>
{
	/// <summary>
	/// The filename used to store the game state.
	/// </summary>
	public string GameStateFilename
	{
		get
		{
			if (PlayerNumber == 0)
			{
				// host gam file: ABarefootJaywalk_42.gam
				return Name + "_" + TurnNumber + FrEeeConstants.SaveGameExtension;
			}
			else
			{
				// player gam file: ABarefootJaywalk_42_0007.gam
				return Name + "_" + TurnNumber + "_" + PlayerNumber.ToString("d4") + FrEeeConstants.SaveGameExtension;
			}
		}
	}

	/// <summary>
	/// The filename used to store the player commands.
	/// </summary>
	public string CommandFilename
	{
		get
		{
			if (PlayerNumber == 0)
			{
				// host plr file: ABarefootJaywalk_42.plr (not currently used by game)
				return Name + "_" + TurnNumber + FrEeeConstants.PlayerCommandsSaveGameExtension;
			}
			else
			{
				// player plr file: ABarefootJaywalk_42_0007.plr
				return Name + "_" + TurnNumber + "_" + PlayerNumber.ToString("d4") + FrEeeConstants.PlayerCommandsSaveGameExtension;
			}
		}
	}

	public static GameID Parse(string s, IFormatProvider? provider)
	{
		var split = s.Split("_");
		if (split.Length == 2)
		{
			return new GameID(split[0], int.Parse(split[1]), 0);
		}
		else if (split.Length == 3)
		{
			return new GameID(split[0], int.Parse(split[1]), int.Parse(split[2]));
		}
		else
		{
			throw new FormatException("Invalid format for GameID. Should be GameName_TurnNumber.gam or GameName_TurnNumber_PlayerNumber.gam.");
		}
	}

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out GameID result)
	{
		// TODO: implement this better
		try
		{
			result = Parse(s, provider);
			return true;
		}
		catch
		{
			result = default;
		}
		return false;
	}
}