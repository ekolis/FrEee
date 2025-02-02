using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.GameState;

namespace FrEee.Persistence;

/// <summary>
/// Saves and loads game state.
/// </summary>
public interface IGamePersister
{
	/// <summary>
	/// Loads a game.
	/// </summary>
	/// <param name="id">The ID of the game to load.</param>
	/// <returns>The loaded game.</returns>
	Game Load(GameID id);

	/// <summary>
	/// Loads a game from a file.
	/// </summary>
	/// <param name="filename">The file to load from.</param>
	/// <returns>The game loaded.</returns>
	Game LoadFromFile(string filename);

	/// <summary>
	/// Loads a game from a string.
	/// </summary>
	/// <param name="data">The string to load from.</param>
	/// <returns>The game loaded.</returns>
	Game LoadFromString(string data);

	/// <summary>
	/// Saves a game.
	/// </summary>
	/// <param name="game">The game to save.</param>
	/// <returns>The ID of the saved game.</returns>
	GameID Save(Game game);

	/// <summary>
	/// Saves a game to a stream.
	/// </summary>
	/// <param name="game"></param>
	/// <param name="stream"></param>
	void SaveToStream(Game game, Stream stream);

	/// <summary>
	/// Saves a game to a string.
	/// </summary>
	/// <param name="game"></param>
	/// <returns></returns>
	string SaveToString(Game game);
}
