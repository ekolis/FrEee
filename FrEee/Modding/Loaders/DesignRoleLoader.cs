using FrEee.Modding.Interfaces;
using System;
using System.Collections.Generic;

namespace FrEee.Modding.Loaders;

/// <summary>
/// Loads design roles from DefaultDesignTypes.txt
/// </summary>
[Serializable]
public class DesignRoleLoader : ILoader
{
	public DesignRoleLoader(string modPath)
	{
		ModPath = modPath;
		FileName = Filename;
		DataFile = DataFile.Load(modPath, Filename);
	}

	public DataFile DataFile { get; set; }
	public string FileName { get; set; }
	public string ModPath { get; set; }
	public const string Filename = "DefaultDesignTypes.txt";

	public IEnumerable<IModObject> Load(Mod mod)
	{
		foreach (var rec in DataFile.Records)
		{
			mod.DesignRoles.Add(rec.Get<string>("Name"));
		}
		yield break; // no actual mod objects to load
	}
}