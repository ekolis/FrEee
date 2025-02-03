using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Processes.Setup;

namespace FrEee.Persistence;

/// <summary>
/// Saves and loads game setups.
/// </summary>
public interface IGameSetupPersister
{
	/// <summary>
	/// Loads a game setup from a file.
	/// </summary>
	/// <param name="filename"></param>
	/// <returns></returns>
	GameSetup LoadFromFile(string filename);

	/// <summary>
	/// Saves a game setup to a file.
	/// </summary>
	/// <param name="gameSetup"></param>
	/// <param name="filename"></param>
	void SaveToFile(GameSetup gameSetup, string filename);
}
