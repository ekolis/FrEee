using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility.Extensions;
using System.IO;

namespace FrEee.Modding
{
	/// <summary>
	/// Encapsulates a game script and any references to external scripts and global variables.
	/// </summary>
	public class Script
	{
		/// <summary>
		/// Creates a script.
		/// </summary>
		/// <param name="text"></param>
		public Script(string moduleName, string text, params Script[] externalScripts)
		{
			ModuleName = moduleName;
			Text = text;
			ExternalScripts = externalScripts.ToList();
			GlobalVariables = new Dictionary<string, object>();
		}

		/// <summary>
		/// Loads a script from disk.
		/// Returns null if the script does not exist.
		/// </summary>
		/// <param name="path">The path to the script, including the filename but not the extension.</param>
		/// <returns></returns>
		public static Script Load(string path)
		{
			var scriptFilename = Path.Combine(path + ".script");
			var pythonFilename = Path.Combine(path + ".py");
			var moduleName = Path.GetFileName(path);
			if (!File.Exists(pythonFilename))
				return null;
			var text = File.ReadAllText(pythonFilename);
			var externalScripts = new List<Script>();
			if (File.Exists(scriptFilename))
			{
				foreach (var externalScriptPath in File.ReadAllLines(scriptFilename))
					externalScripts.Add(Script.Load(externalScriptPath));
			}
			return new Script(moduleName, text, externalScripts.ToArray());
		}

		/// <summary>
		/// The name of this script module. This should be a valid Python module name.
		/// </summary>
		public string ModuleName {get; set;}

		/// <summary>
		/// The script text.
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// Any external scripts directly referenced by this script.
		/// </summary>
		public IList<Script> ExternalScripts { get; private set; }

		/// <summary>
		/// All referenced external scripts (recursive).
		/// </summary>
		public IEnumerable<Script> AllExternalScripts
		{
			get
			{
				var result = new List<Script>();
				foreach (var script in ExternalScripts)
				{
					// don't include the same script twice!
					if (!result.Contains(script))
					{
						result.Add(script);
						foreach (var subscript in script.AllExternalScripts)
						{
							if (!result.Contains(subscript))
								result.Add(script);
						}
					}
				}
				return result;
			}
		}

		/// <summary>
		/// Any global variables that should be injected into the script.
		/// </summary>
		public IDictionary<string, object> GlobalVariables { get; private set; }

		/// <summary>
		/// Full text of this script, including the mod global script and all referenced external scripts.
		/// </summary>
		public string FullText
		{
			get
			{
				string fullText = "";
				if (Mod.Current != null && Mod.Current.GlobalScript != null)
					fullText += Mod.Current.GlobalScript.ModuleImportCode;
				foreach (var externalScript in AllExternalScripts)	
					fullText += externalScript.ModuleImportCode;
				fullText += Text;
				return fullText;
			}
		}

		/// <summary>
		/// Creates Python code to import this script as a module.
		/// </summary>
		private string ModuleImportCode
		{
			get
			{
				// http://stackoverflow.com/questions/5362771/load-module-from-string-in-python
				return
@"import sys, imp;
" + ModuleName + "_code = '" + Text.EscapeBackslashes().EscapeQuotes().EscapeNewlines() + @"';
" + ModuleName + @" = imp.new_module('" + ModuleName + @"');
exec " + ModuleName + @"_code in " + ModuleName + @".__dict__;
";
			}
		}

		public override string ToString()
		{
			return ModuleName;
		}
	}
}
