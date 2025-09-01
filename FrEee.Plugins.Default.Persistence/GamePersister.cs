using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FrEee.Extensions;
using FrEee.Objects.GameState;
using FrEee.Persistence;
using static Community.CsharpSqlite.Sqlite3;

namespace FrEee.Plugins.Default.Persistence;

/// <summary>
/// Persists game state, either for the host or for a player, depending on the <see cref="GameID"/>.
/// </summary>
[Export(typeof(IPlugin))]
public class GamePersister
	: Plugin<IGamePersister>, IGamePersister
{
	public override string Name { get; } = "GamePerister";

	public override IGamePersister Implementation => this;

	public Game Load(GameID id)
	{
		return LoadFromFile(id.GameStateFilename);
	}

	public Game LoadFromFile(string filename)
	{
		using FileStream fs = new(
			Path.Combine(
				Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
				FrEeeConstants.SaveGameDirectory,
				filename),
			FileMode.Open);
		return Serializer.Deserialize<Game>(fs);
	}

	public Game LoadFromString(string data)
	{
		return Serializer.DeserializeFromString<Game>(data);
	}

	public GameID Save(Game obj)
	{
		foreach (var referrable in obj.Referrables.Where(q => !q.HasValidID()))
		{
			obj.AssignID(referrable);
		}
		var gameID = new GameID(obj.Name, obj.TurnNumber, obj.PlayerNumber);
		using FileStream fs = new(
			Path.Combine(
				Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
				FrEeeConstants.SaveGameDirectory,
				gameID.GameStateFilename),
			FileMode.Open);
		Serializer.Serialize(obj, fs);
		return gameID;
	}

	public void SaveToStream(Game game, Stream stream)
	{
		Serializer.Serialize(game, stream);
	}

	public string SaveToString(Game game)
	{
		return Serializer.SerializeToString(game);
	}

	// HACK: for preventing mines from detonating when deserializing a game
	public bool IsDeserializing => Serializer.IsDeserializing;
}
