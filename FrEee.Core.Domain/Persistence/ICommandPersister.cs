using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Gameplay.Commands;
using FrEee.Objects.GameState;

namespace FrEee.Persistence;

/// <summary>
/// Saves and loads player commands.
/// </summary>
public interface ICommandPersister
{
	/// <summary>
	/// Loads commands.
	/// </summary>
	/// <param name="id">The ID of the game whose commands we are loading.</param>
	/// <returns>The commands.</returns>
	IList<ICommand> Load(GameID id);

	/// <summary>
	/// Loads commands from a stream.
	/// </summary>
	/// <param name="stream">The stream to load from.</param>
	/// <returns>The commands loaded.</returns>
	IList<ICommand> LoadFromStream(Stream stream);

	/// <summary>
	/// Saves commands.
	/// </summary>
	/// <param name="commands">The commands to save.</param>
	/// <param name="id">The ID of the game whose commands we are saving.</param>
	void Save(IList<ICommand> commands, GameID id);

	/// <summary>
	/// Saves commands to a stream.
	/// </summary>
	/// <param name="commands"></param>
	/// <param name="stream"></param>
	void SaveToStream(IList<ICommand> commands, Stream stream);
}
