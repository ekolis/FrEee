using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads the mod's general purpose scripts (not AI scripts).
	/// </summary>
	public class ScriptLoader : ILoader
	{
		public ScriptLoader(string modPath)
		{
			ModPath = modPath;
			FileName = "*.py";
		}

		public void Load(Mod mod)
		{
			{
				var name = "Global";
				string filename;
				string stockFilename = filename = Path.Combine("Scripts", name);
				if (ModPath == null)
					filename = stockFilename;
				else
					filename = Path.Combine(ModPath, "Scripts", name);
				mod.GlobalScript = Script.Load(filename) ?? Script.Load(stockFilename) ?? new Script(name, "");
			}
			{
				var name = "GameInit";
				string filename;
				string stockFilename = filename = Path.Combine("Scripts", name);
				if (ModPath == null)
					filename = stockFilename;
				else
					filename = Path.Combine(ModPath, "Scripts", name);
				mod.GameInitScript = Script.Load(filename) ?? Script.Load(stockFilename) ?? new Script(name, "");
			}
			{
				var name = "EndTurn";
				string filename;
				string stockFilename = filename = Path.Combine("Scripts", name);
				if (ModPath == null)
					filename = stockFilename;
				else
					filename = Path.Combine(ModPath, "Scripts", name);
				mod.EndTurnScript = Script.Load(filename) ?? Script.Load(stockFilename) ?? new Script(name, "");
			}
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
