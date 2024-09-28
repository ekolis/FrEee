using System.Collections.Generic;
using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Utility;

namespace FrEee.Processes;

/// <summary>
/// Processes turns.
/// </summary>
public interface ITurnProcessor
{
	IEnumerable<Empire> ProcessTurn(Game game, bool safeMode, Status? status = null, double desiredProgress = 1);
}