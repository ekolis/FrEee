using System.Collections.Generic;

namespace FrEee.Modding.Loaders;

/// <summary>
/// Loads data from a data file.
/// </summary>
public abstract class DataFileLoader : ILoader
{
	public DataFileLoader(string modPath, string filename, DataFile df)
	{
		ModPath = modPath;
		FileName = filename;
		DataFile = df;
	}

	public DataFile DataFile { get; set; }

	public string FileName { get; set; }

	public string ModPath { get; set; }

	public abstract IEnumerable<IModObject> Load(Mod mod);
}