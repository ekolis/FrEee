using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Persistence;
using FrEee.Processes.Setup;

namespace FrEee.Plugins.Default.Persistence;

/// <summary>
/// Saves and loads game setups.
/// </summary>
[Export(typeof(IPlugin))]
public class GameSetupPersister
	: Plugin<IGameSetupPersister>, IGameSetupPersister
{
	public override string Name { get; } = "GameSetupPersister";

	public override IGameSetupPersister Implementation => this;

	public GameSetup LoadFromFile(string filename)
	{
		using FileStream fs = new(filename, FileMode.Open);
		return Serializer.Deserialize<GameSetup>(fs);
	}

	public void SaveToFile(GameSetup gameSetup, string filename)
	{
		using FileStream fs = new(filename, FileMode.Create);
		Serializer.Serialize(this, fs);
	}
}
