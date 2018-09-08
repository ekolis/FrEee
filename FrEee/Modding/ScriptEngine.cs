using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;

namespace FrEee.Modding
{
	/// <summary>
	/// IronPython scripting engine
	/// </summary>
	public class ScriptEngine : MarshalByRefObject
	{
		/// <summary>
		/// Creates the IronPython scripting engine
		/// </summary>
		static ScriptEngine()
		{
			engine = Python.CreateEngine();
			engine.Runtime.LoadAssembly(typeof(string).Assembly); // load System.dll
			engine.Runtime.LoadAssembly(typeof(Uri).Assembly); // load mscorlib.dll
			engine.Runtime.LoadAssembly(Assembly.GetAssembly(typeof(Enumerable))); // load System.Core.dll
			engine.Runtime.LoadAssembly(Assembly.GetAssembly(typeof(CommonExtensions))); // load FrEee.Core.dll
			scope = engine.CreateScope();
		}

		private static IDictionary<ScriptCode, Script> codeScripts = new Dictionary<ScriptCode, Script>();

		private static IDictionary<Script, CompiledCode> compiledScripts = new Dictionary<Script, CompiledCode>();

		private static Microsoft.Scripting.Hosting.ScriptEngine engine;

		private static Galaxy lastGalaxy = null;

		private static IDictionary<string, IReferrable> lastReferrables = new SafeDictionary<string, IReferrable>();

		private static IDictionary<string, object> lastVariables = new SafeDictionary<string, object>();

		private static AppDomain sandbox;

		private static ScriptScope scope;

		/// <summary>
		/// Calls a script function in a sandboxed environment.
		/// Note that the return value of the script may still contain insecure code, so be careful!
		/// </summary>
		/// <param name="script">The script containing the function.</param>
		/// <param name="function">The name of the function.</param>
		/// <param name="args">Arguments to pass to the function.</param>
		/// <returns>The return value.</returns>
		public static T CallFunction<T>(Script script, string function, params object[] args)
		{
			var deserializers = new List<string>();
			if (args != null)
			{
				deserializers.Add("from FrEee.Utility import Serializer;");
				deserializers.Add("if (newGalaxy and not galaxy is None):");
				deserializers.Add("\tgalaxy = Serializer.DeserializeFromString(_galaxy);");
				deserializers.Add("\tMod.Current = galaxy.Mod;");
				for (int i = 0; i < args.Length; i++)
					deserializers.Add("arg" + i + " = Serializer.DeserializeFromString(_arg" + i + ");");
				// TODO - serializers so the objects can be modified by the script
			}
			var arglist = new List<string>();
			for (var i = 0; i < args.Length; i++)
				arglist.Add("arg" + i);
			var code = string.Join("\n", deserializers.ToArray()) + "\n" + script.ModuleName + "." + function + "(" + string.Join(", ", arglist) + ")";
			var variables = args.ToDictionary(arg => "arg" + args.IndexOf(arg));
			var sc = new ScriptCode("functionCall", code, new Script[] { script }.Concat(script.ExternalScripts).ToArray());
			var functionCall = GetCodeScript(sc);
			var compiledScript = GetCompiledScript(functionCall);
			UpdateScope(variables);
			try
			{
				return compiledScript.Execute<T>(scope);
			}
			catch (Exception ex)
			{
				if (ex.Data.Values.Count > 0)
				{
					dynamic info = ex.Data.Values.Cast<dynamic>().First();
					var debugInfo = info[0].DebugInfo;
					if (debugInfo != null)
					{
						int startLine = debugInfo.StartLine;
						int endLine = debugInfo.StartLine;
						throw new ScriptException(ex, string.Join("\n", functionCall.FullText.Split('\n').Skip(startLine - 1).Take(endLine - startLine + 1).ToArray()));
					}
					else
						throw new ScriptException(ex, "(unknown code)");
				}
				else
					throw new ScriptException(ex, "(unknown code)");
			}
		}

		/// <summary>
		/// Calls a script subroutine in a sandboxed environment.
		/// Note that the return value of the script may still contain insecure code, so be careful!
		/// </summary>
		/// <param name="script">The script containing the function.</param>
		/// <param name="function">The name of the function.</param>
		/// <param name="args">Arguments to pass to the function.</param>
		/// <returns>The return value.</returns>
		public static void CallSubroutine(Script script, string function, params object[] args)
		{
			var deserializers = new List<string>();
			if (args != null)
			{
				deserializers.Add("from FrEee.Utility import Serializer;");
				deserializers.Add("if (newGalaxy):");
				deserializers.Add("\tgalaxy = Serializer.DeserializeFromString(_galaxy);");
				deserializers.Add("\tMod.Current = galaxy.Mod;");
				for (int i = 0; i < args.Length; i++)
					deserializers.Add("arg" + i + " = Serializer.DeserializeFromString(_arg" + i + ");");
				// TODO - serializers so the objects can be modified by the script
			}
			var arglist = new List<string>();
			for (var i = 0; i < args.Length; i++)
				arglist.Add("arg" + i);
			var code = string.Join("\n", deserializers.ToArray()) + "\n" + script.ModuleName + "." + function + "(" + string.Join(", ", arglist) + ")";
			var variables = args.ToDictionary(arg => "arg" + args.IndexOf(arg));
			var sc = new ScriptCode("runner", code, new Script[] { script }.Concat(script.ExternalScripts).ToArray());
			var subCall = GetCodeScript(sc);
			var compiledScript = GetCompiledScript(subCall);
			UpdateScope(variables);
			try
			{
				compiledScript.Execute(scope);
			}
			catch (Exception ex)
			{
				if (ex.Data.Values.Count > 0)
				{
					dynamic info = ex.Data.Values.Cast<dynamic>().First();
					var debugInfo = info[0].DebugInfo;
					if (debugInfo != null)
					{
						int startLine = debugInfo.StartLine;
						int endLine = debugInfo.StartLine;
						throw new ScriptException(ex, string.Join("\n", subCall.FullText.Split('\n').Skip(startLine - 1).Take(endLine - startLine + 1).ToArray()));
					}
					else
						throw new ScriptException(ex, "(unknown code)");
				}
				else
					throw new ScriptException(ex, "(unknown code)");
			}
		}

		/// <summary>
		/// Clears any variables in the script scope.
		/// </summary>
		public static void ClearScope()
		{
			lastReferrables.Clear();
			lastVariables.Clear();
			lastGalaxy = null;
			foreach (var name in scope.GetVariableNames())
				scope.RemoveVariable(name);
		}

		/// <summary>
		/// Compiles a script.
		/// </summary>
		/// <returns></returns>
		public static CompiledCode Compile(Script script)
		{
			var compiledScript = engine.CreateScriptSourceFromString(script.FullText);
			return compiledScript.Compile();
		}

		/// <summary>
		/// Evaluates a script expression in a sandboxed environment.
		/// Note that the return value of the script may still contain insecure code, so be careful!
		/// </summary>
		/// <param name="expression">The script code to run.</param>
		/// <param name="readOnlyVariables">Variables to inject into the script.</param>
		/// <returns>Any .</returns>
		public static T EvaluateExpression<T>(string expression, IDictionary<string, object> readOnlyVariables = null)
		{
			var script = new Script("expression", expression);
			return RunScript<T>(script, null, readOnlyVariables);
		}

		public static IDictionary<string, object> RetrieveVariablesFromScope(ScriptScope scope, IEnumerable<string> variableNames)
		{
			var dict = new Dictionary<string, object>();
			foreach (var varName in variableNames)
				dict.Add(varName, Serializer.DeserializeFromString(scope.GetVariable<string>("_" + varName)));
			return dict;
		}

		/// <summary>
		/// Runs a script in a sandboxed environment.
		/// </summary>
		/// <param name="script">The script code to run.</param>
		/// <param name="variables">Read/write variables to inject into the script.</param>
		/// <param name="readOnlyVariables">Read-only variables to inject into the script.</param>
		public static T RunScript<T>(Script script, IDictionary<string, object> variables = null, IDictionary<string, object> readOnlyVariables = null)
		{
			var preCommands = new List<string>();
			var postCommands = new List<string>();
			preCommands.Add("import clr");
			preCommands.Add("clr.AddReference('System.Core')");
			preCommands.Add("import System");
			preCommands.Add("clr.ImportExtensions(System.Linq)");
			preCommands.Add("import FrEee");
			preCommands.Add("import FrEee.Utility");
			preCommands.Add("clr.ImportExtensions(FrEee.Utility.Extensions)");
			preCommands.Add("from FrEee.Modding import Mod");
			preCommands.Add("from FrEee.Game.Objects.Space import Galaxy");
			preCommands.Add("from FrEee.Game.Objects.Civilization import Empire");
			preCommands.Add("from FrEee.Utility import Serializer");
			preCommands.Add("if newGalaxy:");
			preCommands.Add("\tgalaxy = Serializer.DeserializeFromString(_galaxy);");
			preCommands.Add("\tGalaxy.Current = galaxy;");
			preCommands.Add("\tMod.Current = galaxy.Mod;");
			if (variables != null)
			{
				foreach (var variable in variables.Keys)
				{
					if (variables[variable] is IReferrable)
						preCommands.Add(variable + " = galaxy.GetReferrable(_" + variable + ");");
					else
						preCommands.Add(variable + " = Serializer.DeserializeFromString(_" + variable + ");");
					postCommands.Add("_" + variable + " = Serializer.SerializeToString(" + variable + ");");
				}
			}
			if (readOnlyVariables != null)
			{
				foreach (var variable in readOnlyVariables.Keys)
				{
					if (readOnlyVariables[variable] == Galaxy.Current && Galaxy.Current != null)
						preCommands.Add(variable + " = galaxy;");
					else if (readOnlyVariables[variable] is IReferrable)
						preCommands.Add(variable + " = galaxy.GetReferrable(_" + variable + ");");
					else
						preCommands.Add(variable + " = Serializer.DeserializeFromString(_" + variable + ");");
				}
			}
			var code =
				string.Join("\n", preCommands.ToArray()) + "\n" +
				script.Text + "\n" +
				string.Join("\n", postCommands.ToArray());
			var external = new List<Script>(script.ExternalScripts);
			if (Mod.Current != null)
				external.Add(Mod.Current.GlobalScript);
			var sc = new ScriptCode("runner", code, external.ToArray());
			var runner = GetCodeScript(sc);
			var compiledScript = GetCompiledScript(runner);
			var allVariables = new Dictionary<string, object>();
			if (variables != null)
			{
				foreach (var v in variables)
					allVariables.Add(v.Key, v.Value);
			}
			if (readOnlyVariables != null)
			{
				foreach (var v in readOnlyVariables)
					allVariables.Add(v.Key, v.Value);
			}
			UpdateScope(allVariables);
			T result;
			try
			{
				result = (T)compiledScript.Execute(scope);
				if (variables != null)
				{
					var newvals = RetrieveVariablesFromScope(scope, variables.Keys);
					foreach (var kvp in variables)
						newvals[kvp.Key].CopyTo(kvp.Value);
				}
			}
			catch (Exception ex)
			{
				if (ex.Data.Values.Count > 0)
				{
					Array info = ex.Data.Values.Cast<dynamic>().First();
					var debugInfo = info.Cast<dynamic>().FirstOrDefault(o => o.DebugInfo != null)?.DebugInfo;
					if (debugInfo != null)
					{
						int startLine = debugInfo.StartLine;
						int endLine = debugInfo.StartLine;
						throw new ScriptException(ex, string.Join("\n", runner.FullText.Split('\n').Skip(startLine - 1).Take(endLine - startLine + 1).ToArray()));
					}
					else
						throw new ScriptException(ex, "(unknown code)");
				}
				else
					throw new ScriptException(ex, "(unknown code)");
			}
			return result;
		}

		/// <summary>
		/// Runs a script in a sandboxed environment.
		/// </summary>
		/// <param name="script">The script code to run.</param>
		/// <param name="variables">Read/write variables to inject into the script.</param>
		/// <param name="readOnlyVariables">Read-only variables to inject into the script.</param>
		public static void RunScript(Script script, IDictionary<string, object> variables = null, IDictionary<string, object> readOnlyVariables = null)
		{
			var preCommands = new List<string>();
			var postCommands = new List<string>();
			preCommands.Add("import clr");
			preCommands.Add("clr.AddReference('System.Core')");
			preCommands.Add("import System");
			preCommands.Add("clr.ImportExtensions(System.Linq)");
			preCommands.Add("import FrEee");
			preCommands.Add("import FrEee.Utility");
			preCommands.Add("clr.ImportExtensions(FrEee.Utility.Extensions)");
			preCommands.Add("from FrEee.Modding import Mod");
			preCommands.Add("from FrEee.Game.Objects.Space import Galaxy");
			preCommands.Add("from FrEee.Game.Objects.Civilization import Empire");
			preCommands.Add("from FrEee.Utility import Serializer");
			preCommands.Add("if newGalaxy:");
			preCommands.Add("\tgalaxy = Serializer.DeserializeFromString(_galaxy);");
			preCommands.Add("\tGalaxy.Current = galaxy;");
			preCommands.Add("\tMod.Current = galaxy.Mod;");
			if (variables != null)
			{
				foreach (var variable in variables.Keys)
				{
					if (variables[variable] is IReferrable)
						preCommands.Add(variable + " = galaxy.GetReferrable(_" + variable + ");");
					else
						preCommands.Add(variable + " = Serializer.DeserializeFromString(_" + variable + ");");
					postCommands.Add("_" + variable + " = Serializer.SerializeToString(" + variable + ");");
				}
			}
			if (readOnlyVariables != null)
			{
				foreach (var variable in readOnlyVariables.Keys)
				{
					if (readOnlyVariables[variable] == Galaxy.Current && Galaxy.Current != null)
						preCommands.Add(variable + " = galaxy;");
					else if (readOnlyVariables[variable] is IReferrable)
						preCommands.Add(variable + " = galaxy.GetReferrable(_" + variable + ");");
					else
						preCommands.Add(variable + " = Serializer.DeserializeFromString(_" + variable + ");");
				}
			}
			var code =
				string.Join("\n", preCommands.ToArray()) + "\n" +
				script.Text + "\n" +
				string.Join("\n", postCommands.ToArray());
			var external = new List<Script>(script.ExternalScripts);
			if (Mod.Current != null)
				external.Add(Mod.Current.GlobalScript);
			var sc = new ScriptCode("runner", code, external.ToArray());
			var runner = GetCodeScript(sc);
			var compiledScript = GetCompiledScript(runner);
			var allVariables = new Dictionary<string, object>();
			if (variables != null)
			{
				foreach (var v in variables)
					allVariables.Add(v.Key, v.Value);
			}
			if (readOnlyVariables != null)
			{
				foreach (var v in readOnlyVariables)
					allVariables.Add(v.Key, v.Value);
			}
			UpdateScope(allVariables);
			try
			{
				compiledScript.Execute(scope);
				if (variables != null)
				{
					var newvals = RetrieveVariablesFromScope(scope, variables.Keys);
					foreach (var kvp in variables)
						newvals[kvp.Key].CopyTo(kvp.Value);
				}
			}
			catch (Exception ex)
			{
				if (ex.Data.Values.Count > 0)
				{
					Array info = ex.Data.Values.Cast<dynamic>().First();
					var debugInfo = info.Cast<dynamic>().FirstOrDefault(o => o.DebugInfo != null)?.DebugInfo;
					if (debugInfo != null)
					{
						int startLine = debugInfo.StartLine;
						int endLine = debugInfo.StartLine;
						throw new ScriptException(ex, string.Join("\n", runner.FullText.Split('\n').Skip(startLine - 1).Take(endLine - startLine + 1).ToArray()));
					}
					else
						throw new ScriptException(ex, "(unknown code)");
				}
				else
					throw new ScriptException(ex, "(unknown code)");
			}
		}

		public static IDictionary<string, string> SerializeScriptVariables(IDictionary<string, object> variables)
		{
			return variables.Select(kvp => new KeyValuePair<string, string>(
				kvp.Key,
				Serializer.SerializeToString(kvp.Value)))
				.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
		}

		/// <summary>
		/// Creates a script scope containing user defined variables.
		/// </summary>
		/// <param name="variables">The variables to set, or null to set no variables.</param>
		/// <returns></returns>
		public static void UpdateScope(IDictionary<string, object> variables)
		{
			if (variables == null)
				variables = new Dictionary<string, object>();

			// Separate referrable objects (they just get ID's, not serialized data)
			var referrables = new Dictionary<string, IReferrable>();
			var genericObjects = new Dictionary<string, object>();
			foreach (var kvp in variables)
			{
				if (kvp.Value is IReferrable)
					referrables.Add(kvp.Key, (IReferrable)kvp.Value);
				else if (kvp.Value == null)
					genericObjects.Add(kvp.Key, null);
				else if (kvp.Value != Galaxy.Current)
					genericObjects.Add(kvp.Key, kvp.Value);
				// Galaxy.Current is dealt with separately
			}

			// Set injected variables
			if (referrables != null)
			{
				foreach (var kvp in referrables)
				{
					scope.SetVariable("_" + kvp.Key, kvp.Value.ID);
					lastReferrables.Add(kvp.Key, kvp.Value);
				}
			}
			if (genericObjects != null)
			{
				foreach (var kvp in SerializeScriptVariables(genericObjects))
				{
					scope.SetVariable("_" + kvp.Key, kvp.Value);
					lastVariables.Add(kvp.Key, kvp.Value);
				}
			}
			foreach (var variable in lastReferrables.Keys.ToArray())
			{
				if (!referrables.ContainsKey(variable) && !genericObjects.ContainsKey(variable))
				{
					scope.RemoveVariable("_" + variable);
					scope.RemoveVariable(variable);
					lastReferrables.Remove(variable);
				}
			}
			foreach (var variable in lastVariables.Keys.ToArray())
			{
				if (!referrables.ContainsKey(variable) && !genericObjects.ContainsKey(variable))
				{
					scope.RemoveVariable("_" + variable);
					scope.RemoveVariable(variable);
					lastVariables.Remove(variable);
				}
			}
			if (Galaxy.Current != null && (lastGalaxy == null || lastGalaxy.TurnNumber < Galaxy.Current.TurnNumber))
			{
				lastGalaxy = Galaxy.Current;
				var sval = lastGalaxy.StringValue;
				if (sval != null)
					scope.SetVariable("_galaxy", sval);
				else
					scope.SetVariable("_galaxy", Serializer.SerializeToString(lastGalaxy));
				scope.SetVariable("newGalaxy", true);
			}
			else if (lastGalaxy == null)
			{
				scope.SetVariable("_galaxy", typeof(Galaxy).AssemblyQualifiedName + ":n;");
				scope.SetVariable("newGalaxy", false);
			}
			else
				scope.SetVariable("newGalaxy", false);
		}

		/// <summary>
		/// Creates a script for some code, or returns the cached scripts if it exists.
		/// </summary>
		/// <param name="sc">Information about the code.</param>
		/// <returns>The script.</returns>
		private static Script GetCodeScript(ScriptCode source)
		{
			if (!codeScripts.ContainsKey(source))
				codeScripts.Add(source, new Script(source.ModuleName, source.Code, source.ExternalScripts));
			return codeScripts[source];
		}

		/// <summary>
		/// Compiles and caches a script, or returns the cached script if it exists.
		/// </summary>
		/// <param name="source">The script to search for.</param>
		/// <returns>The compiled code.</returns>
		private static CompiledCode GetCompiledScript(Script source)
		{
			if (!compiledScripts.ContainsKey(source))
				compiledScripts.Add(source, Compile(source));
			return compiledScripts[source];
		}

		private class ScriptCode
		{
			public ScriptCode(string moduleName, string code, params Script[] externalScripts)
			{
				ModuleName = moduleName;
				Code = code;
				ExternalScripts = externalScripts;
			}

			public string Code { get; set; }
			public Script[] ExternalScripts { get; set; }
			public string ModuleName { get; set; }

			public static bool operator !=(ScriptCode sc1, ScriptCode sc2)
			{
				return !(sc1 == sc2);
			}

			public static bool operator ==(ScriptCode sc1, ScriptCode sc2)
			{
				if (sc1.IsNull() && sc2.IsNull())
					return true;
				if (sc1.IsNull() || sc2.IsNull())
					return false;
				return sc1.ModuleName == sc2.ModuleName && sc1.Code == sc2.Code && sc1.ExternalScripts.SequenceEqual(sc2.ExternalScripts);
			}

			public override bool Equals(object obj)
			{
				// TODO - upgrade equals to use "as" operator
				if (obj is ScriptCode)
				{
					var sc = (ScriptCode)obj;
					return sc == this;
				}
				return false;
			}

			public override int GetHashCode()
			{
				return HashCodeMasher.Mash(ModuleName, Code, HashCodeMasher.Mash(ExternalScripts));
			}
		}
	}

	public class ScriptException : Exception
	{
		public ScriptException(string message)
		{
			this.message = message;
		}

		public ScriptException(Exception ex, string code)
			: base(null, ex)
		{
			if (ex is TargetInvocationException)
				message = "In this code:\n" + code + "\n" + ex.InnerException.Message;
			else
				message = "In this code:\n" + code + "\n" + ex.Message;
		}

		public override string Message
		{
			get
			{
				return message;
			}
		}

		private string message;
	}
}