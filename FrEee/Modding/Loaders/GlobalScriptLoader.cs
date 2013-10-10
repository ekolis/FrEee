using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads the mod's global script.
	/// </summary>
	public class GlobalScriptLoader : ILoader
	{
		public GlobalScriptLoader(string modPath)
		{
			ModPath = modPath;
			FileName = "GlobalScript.py";
		}

		public void Load(Mod mod)
		{
			string filename;
			if (ModPath == null)
				filename = Path.Combine("Data", FileName);
			else
				filename = Path.Combine(ModPath, "Data", FileName);
			var scriptText = "";
			if (File.Exists(filename))
				scriptText = File.ReadAllText(filename);
			mod.GlobalScript = new Script("globalScript", scriptText);
		}

		public string ModPath
		{
			get;
			set;
		}

		public string FileName
		{
			get;
			set;
		}
	}
}
