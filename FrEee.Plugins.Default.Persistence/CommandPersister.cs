﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FrEee.Gameplay.Commands;
using FrEee.Gameplay.Commands.Designs;
using FrEee.Objects.GameState;
using FrEee.Persistence;
using static Community.CsharpSqlite.Sqlite3;

namespace FrEee.Plugins.Default.Persistence;

/// <summary>
/// Persists player commands.
/// </summary>
/// 
[Export(typeof(IPlugin))]
public class CommandPersister
	: Plugin<ICommandPersister>, ICommandPersister
{
	public override string Name { get; } = "CommandPersister";

	public override ICommandPersister Implementation => this;

	public IList<ICommand> Load(GameID id)
	{
		return LoadFromFile(id.GameStateFilename);
	}

	public IList<ICommand> LoadFromFile(string filename)
	{
		using FileStream fs = new(
			Path.Combine(
				Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
				FrEeeConstants.SaveGameDirectory,
				filename),
			FileMode.Open);
		return LoadFromStream(fs);
	}

	public IList<ICommand> LoadFromStream(Stream stream)
	{
		return Serializer.Deserialize<IList<ICommand>>(stream);
	}

	// TODO: remove unused methods below if they are really unused

	public Game LoadFromString(string data)
	{
		return Serializer.DeserializeFromString<Game>(data);
	}

	public void Save(IList<ICommand> commands, GameID id)
	{
		using FileStream fs = new(
			Path.Combine(
				Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
				FrEeeConstants.SaveGameDirectory,
				id.CommandFilename),
			FileMode.Open);
		Serializer.Serialize(commands, fs);
	}

	public void SaveToStream(IList<ICommand> commands, Stream stream)
	{
		Serializer.Serialize(commands, stream);
	}

	public string SaveToString(IList<ICommand> commands)
	{
		return Serializer.SerializeToString(commands);
	}
}
