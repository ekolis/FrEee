using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.GameState;

namespace FrEee.Persistence;

/// <summary>
/// Saves and loads game state.
/// </summary>
public interface IGamePersister
	: IPersister<Game, GameID>
{
	/// <summary>
	/// Loads a game from a file.
	/// </summary>
	/// <param name="filename">The file to load from.</param>
	/// <returns>The game loaded.</returns>
	public Game LoadFromFile(string filename);

	/// <summary>
	/// Loads a game from a string.
	/// </summary>
	/// <param name="data">The string to load from.</param>
	/// <returns>The game loaded.</returns>
	public Game LoadFromString(string data);

	/// <summary>
	/// Saves a game to a stream.
	/// </summary>
	/// <param name="game"></param>
	/// <param name="stream"></param>
	public void SaveToStream(Game game, Stream stream);

	/// <summary>
	/// Saves a game to a string.
	/// </summary>
	/// <param name="game"></param>
	/// <returns></returns>
	public string SaveToString(Game game);
}
