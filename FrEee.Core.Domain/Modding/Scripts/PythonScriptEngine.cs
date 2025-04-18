﻿using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Extensions;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using FrEee.Objects.GameState;
using FrEee.Objects.Space;
using System.Threading;

namespace FrEee.Modding.Scripts;

/// <summary>
/// IronPython scripting engine
/// </summary>
public class PythonScriptEngine : MarshalByRefObject
{
	private static readonly Lock codeScriptLock = new();
	private static readonly Lock compiledScriptLock = new();

	/// <summary>
	/// Creates the IronPython scripting engine
	/// </summary>
	static PythonScriptEngine()
	{
		engine = Python.CreateEngine();
		engine.Runtime.LoadAssembly(typeof(string).Assembly); // load System.dll
		engine.Runtime.LoadAssembly(typeof(Uri).Assembly); // load System.Private.CoreLib.dll
		engine.Runtime.LoadAssembly(Assembly.GetAssembly(typeof(Enumerable))); // load System.Core.dll
		engine.Runtime.LoadAssembly(Assembly.GetAssembly(typeof(CommonExtensions))); // load FrEee.Core.dll
		scope = engine.CreateScope();
	}

	/// <summary>
	/// The results
	/// </summary>
	private static SafeDictionary<CompiledCodeWithVariables, object> Results { get; set; } = new SafeDictionary<CompiledCodeWithVariables, object>();

	private static IDictionary<ScriptCode, PythonScript> codeScripts = new Dictionary<ScriptCode, PythonScript>();

	private static IDictionary<PythonScript, CompiledCode> compiledScripts = new Dictionary<PythonScript, CompiledCode>();

	private static ScriptEngine engine;

	private static IDictionary<string, IReferrable> lastReferrables = new SafeDictionary<string, IReferrable>();

	private static IDictionary<string, object> lastVariables = new SafeDictionary<string, object>();

	private static ScriptScope scope;

	/// <summary>
	/// Calls a script function.
	/// </summary>
	/// <param name="script">The script containing the function.</param>
	/// <param name="function">The name of the function.</param>
	/// <param name="args">Arguments to pass to the function.</param>
	/// <returns>The return value.</returns>
	public static T CallFunction<T>(PythonScript script, string function, params object[] args)
	{
		var preCode = new List<string>();
		preCode.Add("from FrEee.Modding import Mod;");
		preCode.Add("if not game is None:");
		preCode.Add("\tMod.Current = game.Mod;");
		var arglist = new List<string>();
		for (var i = 0; i < args.Length; i++)
			arglist.Add("arg" + i);
		var code = string.Join("\n", preCode.ToArray()) + "\n" + script.ModuleName + "." + function + "(" + string.Join(", ", arglist) + ")";
		var variables = args.ToDictionary(arg => "arg" + args.IndexOf(arg));
		var sc = new ScriptCode("functionCall", code, new PythonScript[] { script }.Concat(script.ExternalScripts).ToArray());
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
	/// Calls a script subroutine.
	/// </summary>
	/// <param name="script">The script containing the function.</param>
	/// <param name="function">The name of the function.</param>
	/// <param name="args">Arguments to pass to the function.</param>
	public static void CallSubroutine(PythonScript script, string function, params object[] args)
	{
		var preCode = new List<string>();
		var arglist = new List<string>();
		for (var i = 0; i < args.Length; i++)
			arglist.Add("arg" + i);
		var code = string.Join("\n", preCode.ToArray()) + "\n" + script.ModuleName + "." + function + "(" + string.Join(", ", arglist) + ")";
		var variables = args.ToDictionary(arg => "arg" + args.IndexOf(arg));
		var sc = new ScriptCode("runner", code, new PythonScript[] { script }.Concat(script.ExternalScripts).ToArray());
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
		foreach (var name in scope.GetVariableNames())
			scope.RemoveVariable(name);
	}

	/// <summary>
	/// Compiles a script.
	/// </summary>
	/// <returns></returns>
	public static CompiledCode Compile(PythonScript script)
	{
		var compiledScript = engine.CreateScriptSourceFromString(script.FullText);
		return compiledScript.Compile();
	}

	/// <summary>
	/// Evaluates a script expression.
	/// </summary>
	/// <param name="expression">The script code to run.</param>
	/// <param name="readOnlyVariables">Variables to inject into the script.</param>
	/// <returns>Any .</returns>
	public static T EvaluateExpression<T>(string expression, IDictionary<string, object> readOnlyVariables = null)
	{
		var script = new PythonScript("expression", expression);
		return RunScript<T>(script, null, readOnlyVariables);
	}

	public static IDictionary<string, object> RetrieveVariablesFromScope(ScriptScope scope, IEnumerable<string> variableNames)
	{
		var dict = new Dictionary<string, object>();
		foreach (var varName in variableNames)
			dict.Add(varName, engine.GetBuiltinModule().GetVariable(varName));
		//dict.Add(varName, scope.GetVariable(varName));
		return dict;
	}

	/// <summary>
	/// Runs a script.
	/// </summary>
	/// <param name="script">The script code to run.</param>
	/// <param name="variables">Read/write variables to inject into the script.</param>
	/// <param name="readOnlyVariables">Read-only variables to inject into the script.</param>
	public static T RunScript<T>(PythonScript script, IDictionary<string, object> variables = null, IDictionary<string, object> readOnlyVariables = null)
	{
		var preCommands = new List<string>();
		var postCommands = new List<string>();
		preCommands.Add("import clr");
		preCommands.Add("clr.AddReference('System.Core')");
		preCommands.Add("import System");
		preCommands.Add("clr.ImportExtensions(System.Linq)");
		preCommands.Add("clr.AddReference('FrEee.Core')");
		preCommands.Add("clr.AddReference('FrEee.Core.Utility')");
		preCommands.Add("import FrEee");
		preCommands.Add("import FrEee.Utility");
		preCommands.Add("clr.ImportExtensions(FrEee.Extensions)");
		preCommands.Add("from FrEee.Modding import Mod");
		preCommands.Add("from FrEee.Objects.GameState import Game");
		preCommands.Add("from FrEee.Objects.Space import Galaxy");
		preCommands.Add("from FrEee.Objects.Civilization import Empire");
		/*if (variables != null)
			UpdateScope(variables);
		if (readOnlyVariables != null)
			UpdateScope(readOnlyVariables);*/
		var code =
			string.Join("\n", preCommands.ToArray()) + "\n" +
			script.Text + "\n" +
			string.Join("\n", postCommands.ToArray());
		var external = new List<PythonScript>(script.ExternalScripts);
		//if (Mod.Current != null)
		//external.Add(Mod.Current.GlobalScript);
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
			var cc = new CompiledCodeWithVariables(compiledScript, allVariables);
			if (Results.ContainsKey(cc))
				return (T)Results[cc];
			Results[cc] = result = (T)compiledScript.Execute(scope);
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
	/// Runs a script.
	/// </summary>
	/// <param name="script">The script code to run.</param>
	/// <param name="variables">Read/write variables to inject into the script.</param>
	/// <param name="readOnlyVariables">Read-only variables to inject into the script.</param>
	public static void RunScript(PythonScript script, IDictionary<string, object> variables = null, IDictionary<string, object> readOnlyVariables = null)
	{
		var preCommands = new List<string>();
		var postCommands = new List<string>();
		preCommands.Add("import clr");
		preCommands.Add("clr.AddReference('System.Core')");
		preCommands.Add("import System");
		preCommands.Add("clr.ImportExtensions(System.Linq)");
		preCommands.Add("clr.AddReference('FrEee.Core')");
		preCommands.Add("clr.AddReference('FrEee.Core.Utility')");
		preCommands.Add("import FrEee");
		preCommands.Add("import FrEee.Utility");
		preCommands.Add("clr.ImportExtensions(FrEee.Extensions)");
		preCommands.Add("from FrEee.Modding import Mod");
		preCommands.Add("from FrEee.Objects.GameState import Game");
		preCommands.Add("from FrEee.Objects.Space import Galaxy");
		preCommands.Add("from FrEee.Objects.Civilization import Empire");
		var code =
			string.Join("\n", preCommands.ToArray()) + "\n" +
			script.Text + "\n" +
			string.Join("\n", postCommands.ToArray());
		var external = new List<PythonScript>(script.ExternalScripts);
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

	/// <summary>
	/// Creates a script scope containing user defined variables.
	/// </summary>
	/// <param name="variables">The variables to set, or null to set no variables.</param>
	/// <returns></returns>
	public static void UpdateScope(IDictionary<string, object> variables)
	{
		scope.SetVariable("game", Game.Current);
		scope.SetVariable("galaxy", Galaxy.Current);

		if (variables != null)
		{
			foreach (var kvp in variables)
			{
				scope.SetVariable(kvp.Key, kvp.Value);
				engine.GetBuiltinModule().SetVariable(kvp.Key, kvp.Value);
			}
		}
	}

	/// <summary>
	/// Creates a script for some code, or returns the cached scripts if it exists.
	/// </summary>
	/// <param name="sc">Information about the code.</param>
	/// <returns>The script.</returns>
	private static PythonScript GetCodeScript(ScriptCode source)
	{
		lock (codeScriptLock)
		{
			if (!codeScripts.ContainsKey(source))
				codeScripts.Add(source, new PythonScript(source.ModuleName, source.Code, source.ExternalScripts));
			return codeScripts[source];
		}
	}

	/// <summary>
	/// Compiles and caches a script, or returns the cached script if it exists.
	/// </summary>
	/// <param name="source">The script to search for.</param>
	/// <returns>The compiled code.</returns>
	private static CompiledCode GetCompiledScript(PythonScript source)
	{
		lock (compiledScriptLock)
		{
			if (!compiledScripts.ContainsKey(source))
				compiledScripts.Add(source, Compile(source));
			return compiledScripts[source];
		}
	}

	private class CompiledCodeWithVariables : IEquatable<CompiledCodeWithVariables>
	{
		public CompiledCodeWithVariables(CompiledCode code, IDictionary<string, object> variables)
		{
			Code = code;
			Variables = new SafeDictionary<string, object>(variables);
		}

		public CompiledCode Code { get; set; }
		public SafeDictionary<string, object> Variables { get; set; }

		public static bool operator !=(CompiledCodeWithVariables c1, CompiledCodeWithVariables c2)
		{
			return !(c1 == c2);
		}

		public static bool operator ==(CompiledCodeWithVariables c1, CompiledCodeWithVariables c2)
		{
			return c1.Equals(c2);
		}

		public bool Equals(CompiledCodeWithVariables? other)
		{
			return Code == other.Code && Variables.Keys.Count == other.Variables.Keys.Count && Variables.All(kvp => kvp.Value.SafeEquals(other.Variables[kvp.Key]));
		}

		public override bool Equals(object? obj)
		{
			if (obj is CompiledCodeWithVariables c)
				return Equals(c);
			return false;
		}

		public override int GetHashCode()
		{
			return HashCodeMasher.Mash(Code) ^ HashCodeMasher.Mash(Variables.Keys.ToArray()) ^ HashCodeMasher.Mash(Variables.Values.ToArray());
		}
	}

	private class ScriptCode
	{
		public ScriptCode(string moduleName, string code, params PythonScript[] externalScripts)
		{
			ModuleName = moduleName;
			Code = code;
			ExternalScripts = externalScripts;
		}

		public string Code { get; set; }
		public PythonScript[] ExternalScripts { get; set; }
		public string ModuleName { get; set; }

		public static bool operator !=(ScriptCode sc1, ScriptCode sc2)
		{
			return !(sc1 == sc2);
		}

		public static bool operator ==(ScriptCode sc1, ScriptCode sc2)
		{
			if (sc1 is null && sc2 is null)
				return true;
			if (sc1 is null || sc2 is null)
				return false;
			return sc1.ModuleName == sc2.ModuleName && sc1.Code == sc2.Code && sc1.ExternalScripts.SequenceEqual(sc2.ExternalScripts);
		}

		public override bool Equals(object? obj)
		{
			return obj as ScriptCode == this;
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