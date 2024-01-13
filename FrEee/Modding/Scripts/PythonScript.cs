using FrEee.Utility; using FrEee.Serialization;
using FrEee.Extensions;
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
	public class PythonScript : IScript
	{
		/// <summary>
		/// Creates a script.
		/// </summary>
		/// <param name="text"></param>
		public PythonScript(string moduleName, string text, params PythonScript[] externalScripts)
		{
			ModuleName = moduleName;
			Text = text;
			ExternalScripts = externalScripts.ToHashSet();
			GlobalVariables = new Dictionary<string, object>();
		}

		/// <summary>
		/// All referenced external scripts (recursive).
		/// </summary>
		public IEnumerable<PythonScript> AllExternalScripts
		{
			get
			{
				var result = new List<PythonScript>();
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
		public ISet<PythonScript> ExternalScripts { get; set; }

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
				// https://stackoverflow.com/questions/54271292/ironpython-runtime-unboundnameexception-name-str-is-not-defined
				if (Text == "")
					return "str = \"\".__class__\n";
				if (ModuleName == "builtins")
					return
@"import sys, imp;
import __builtin__ as builtins
str = """".__class__
" + ModuleName + "_code = \"\"\"" + Text + "\"\"\";\n" +
"exec " + ModuleName + @"_code in " + ModuleName + @".__dict__;
";
				else
					return
@"import sys, imp;
str = """".__class__
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
		public static PythonScript Load(string path)
		{
			var scriptFilename = Path.Combine(path + ".script");
			var pythonFilename = Path.Combine(path + ".py");
			var moduleName = Path.GetFileName(path);
			if (!File.Exists(pythonFilename))
				return null;
			var text = File.ReadAllText(pythonFilename);
			var externalScripts = new List<PythonScript>();
			if (File.Exists(scriptFilename))
			{
				foreach (var externalScriptPath in File.ReadAllLines(scriptFilename))
				{
					var externalScript = PythonScript.Load(externalScriptPath);
					if (externalScript == null)
						throw new ScriptException("Cannot find script file: " + Path.GetFullPath(externalScriptPath + ".py"));
					externalScripts.Add(externalScript);
				}
			}
			return new PythonScript(moduleName, text, externalScripts.ToArray());
		}

		public static bool operator !=(PythonScript s1, PythonScript s2)
		{
			return !(s1 == s2);
		}

		public static bool operator ==(PythonScript s1, PythonScript s2)
		{
			if (s1.IsNull() && s2.IsNull())
				return true;
			if (s1.IsNull() || s2.IsNull())
				return false;
			return s1.ModuleName == s2.ModuleName && s1.Text == s2.Text && s1.ExternalScripts.SafeSequenceEqual(s2.ExternalScripts);
		}

		public override bool Equals(object obj)
		{
			if (obj is PythonScript)
			{
				var s = (PythonScript)obj;
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