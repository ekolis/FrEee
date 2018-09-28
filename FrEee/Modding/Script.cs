using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FrEee.Modding
{
	/// <summary>
	/// Encapsulates a game script and any references to external scripts and global variables.
	/// </summary>
	[Serializable]
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
		/// All referenced external scripts (recursive).
		/// </summary>
		public IEnumerable<Script> AllExternalScripts
		{
			get
			{
				var result = new List<Script>();
				foreach (var script in ExternalScripts.ExceptSingle(null)) // HACK - why do we have null scripts?
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
		/// Any external scripts directly referenced by this script.
		/// </summary>
		public IList<Script> ExternalScripts { get; set; }

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
		/// Any global variables that should be injected into the script.
		/// </summary>
		public IDictionary<string, object> GlobalVariables { get; private set; }

		/// <summary>
		/// The name of this script module. This should be a valid Python module name.
		/// </summary>
		public string ModuleName { get; set; }

		/// <summary>
		/// The script text.
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// Creates Python code to import this script as a module.
		/// </summary>
		private string ModuleImportCode
		{
			get
			{
				// http://stackoverflow.com/questions/5362771/load-module-from-string-in-python
				// http://stackoverflow.com/questions/3799545/dynamically-importing-python-module/3799609#3799609
				if (ModuleName == "builtins")
					return
@"import sys, imp;
import __builtin__ as builtins;
" + ModuleName + "_code = \"\"\"" + Text + "\"\"\";\n" +
"exec " + ModuleName + @"_code in " + ModuleName + @".__dict__;
";
				else
					return
@"import sys, imp;
" + ModuleName + "_code = \"\"\"" + Text + "\"\"\";\n" +
ModuleName + @" = imp.new_module('" + ModuleName + @"');
exec " + ModuleName + @"_code in " + ModuleName + @".__dict__;
sys.modules['" + ModuleName + "'] = " + ModuleName + @";
import " + ModuleName + @";
";
			}
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
				{
					var externalScript = Script.Load(externalScriptPath);
					if (externalScript == null)
						throw new ScriptException("Cannot find script file: " + Path.GetFullPath(externalScriptPath + ".py"));
					externalScripts.Add(externalScript);
				}
			}
			return new Script(moduleName, text, externalScripts.ToArray());
		}

		public static bool operator !=(Script s1, Script s2)
		{
			return !(s1 == s2);
		}

		public static bool operator ==(Script s1, Script s2)
		{
			if (s1.IsNull() && s2.IsNull())
				return true;
			if (s1.IsNull() || s2.IsNull())
				return false;
			return s1.ModuleName == s2.ModuleName && s1.Text == s2.Text && s1.ExternalScripts.SafeSequenceEqual(s2.ExternalScripts);
		}

		public override bool Equals(object obj)
		{
			if (obj is Script)
			{
				var s = (Script)obj;
				return s == this;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCodeMasher.Mash(ModuleName, Text, HashCodeMasher.MashList(ExternalScripts));
		}

		public override string ToString()
		{
			return ModuleName;
		}
	}
}