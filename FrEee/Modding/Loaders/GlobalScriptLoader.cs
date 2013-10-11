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
			string stockFilename = filename = Path.Combine("Scripts", Path.GetFileNameWithoutExtension(FileName));
			if (ModPath == null)
				filename = stockFilename;
			else
				filename = Path.Combine(ModPath, "Scripts", Path.GetFileNameWithoutExtension(FileName));
			mod.GlobalScript = Script.Load(filename) ?? Script.Load(stockFilename);
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
