using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FrEee.Modding.Loaders;

/// <summary>
/// Loads modinfo.txt.
/// </summary>
public class ModInfoLoader : ILoader
{
	public ModInfoLoader(string modPath)
	{
		ModPath = modPath;
		FileName = "MODINFO.TXT";
	}

	/// <summary>
	/// The file name to read from.
	/// Note that ModInfoLoader will load from the mod root folder, not from the data folder.
	/// </summary>
	public string FileName { get; set; }

	/// <summary>
	/// The mod path, or null to use stock.
	/// </summary>
	public string ModPath { get; set; }

	public IEnumerable<IModObject> Load(Mod mod)
	{
		if (mod.Info == null)
			mod.Info = new ModInfo();
		mod.Info.Folder = ModPath;

		string filepath;
		if (ModPath == null)
			filepath = FileName;
		else
			filepath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", ModPath, FileName);
		if (!File.Exists(filepath))
		{
			mod.Info.Name = ModPath ?? "Stock";
			mod.Info.Version = "(Unknown)";
			mod.Info.Author = "(Unknown)";
			mod.Info.Email = "(Unknown)";
			mod.Info.Website = "(Unknown)";
			mod.Info.Description = "(This mod does not have a MODINFO.TXT)";
		}
		else
		{
			IEnumerable<string> lines = File.ReadAllLines(filepath);
			lines = lines.Skip(1); // skip Modinfo2 line
			string curField = null;
			foreach (var line in lines)
			{
				if (curField == null)
				{
					if (line == "Obsolete:-")
						curField = "Obsolete";
					else if (line == "ModName:-")
						curField = "ModName";
					else if (line == "Version:-")
						curField = "Version";
					else if (line == "Author:-")
						curField = "Author";
					else if (line == "EMail:-")
						curField = "EMail";
					else if (line == "Website:-")
						curField = "Website";
					else if (line == "Description:-")
						curField = "Description";
				}
				else if (curField == "Obsolete")
				{
					bool isObsolete;
					bool.TryParse(line, out isObsolete);
					mod.Info.IsObsolete = isObsolete;
					curField = null;
				}
				else if (curField == "ModName")
				{
					mod.Info.Name = line;
					curField = null;
				}
				else if (curField == "Version")
				{
					mod.Info.Version = line;
					curField = null;
				}
				else if (curField == "Author")
				{
					mod.Info.Author = line;
					curField = null;
				}
				else if (curField == "EMail")
				{
					mod.Info.Email = line;
					curField = null;
				}
				else if (curField == "Website")
				{
					mod.Info.Website = line;
					curField = null;
				}
				else if (curField == "Description")
				{
					if (!string.IsNullOrEmpty(mod.Info.Description))
						mod.Info.Description += '\n';
					mod.Info.Description += line;
				}
			}
		}

		return Enumerable.Empty<IModObject>();
	}
}