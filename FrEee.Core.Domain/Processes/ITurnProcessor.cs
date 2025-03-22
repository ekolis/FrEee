using System.Collections.Generic;
using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Plugins;
using FrEee.Utility;

namespace FrEee.Processes;

/// <summary>
/// Processes turns.
/// </summary>
public interface ITurnProcessor
	: IPlugin<ITurnProcessor>
{
	/// <summary>
	/// Processes the turn.
	/// </summary>
	/// <param name="safeMode">Stop processing if PLR files are missing?</param>
	/// <returns>Player empires which did not submit commands and are not defeated.</returns>
	/// <exception cref="InvalidOperationException">if the current empire is not null, or this game is not the current game.</exception>
	IEnumerable<Empire> ProcessTurn(Game game, bool safeMode, Status? status = null, double desiredProgress = 1);
}