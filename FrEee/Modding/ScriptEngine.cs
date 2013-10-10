using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using FrEee.Utility.Extensions;
using IronPython.Hosting;
using IronPython.Compiler;
using Microsoft.Scripting.Hosting;
using System.Reflection;
using System.Data;

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
			// http://msdn.microsoft.com/en-us/library/bb763046.aspx
			// http://grokbase.com/t/python/ironpython-users/123kjgw8k8/passing-python-exceptions-in-a-sandboxed-domain

			//Setting the AppDomainSetup. It is very important to set the ApplicationBase to a folder 
			//other than the one in which the sandboxer resides.
			AppDomainSetup adSetup = new AppDomainSetup();
			adSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
			adSetup.ApplicationName = "FrEee";
			adSetup.DynamicBase = "ScriptEngine";

			//Setting the permissions for the AppDomain. We give the permission to execute and to 
			//read/discover the location where the untrusted code is loaded.
			var evidence = new Evidence();
			evidence.AddHostEvidence(new Zone(SecurityZone.MyComputer));
			var permissions = SecurityManager.GetStandardSandbox(evidence);
			var reflection = new ReflectionPermission(PermissionState.Unrestricted);
			permissions.AddPermission(reflection);

			//Now we have everything we need to create the AppDomain, so let's create it.
			sandbox = AppDomain.CreateDomain("ScriptEngine", null, adSetup, permissions, AppDomain.CurrentDomain.GetAssemblies().Select(a => a.Evidence.GetHostEvidence<StrongName>()).Where(sn => sn != null).ToArray());
			engine = Python.CreateEngine(sandbox);
		}

		/// <summary>
		/// Compiles a script.
		/// </summary>
		/// <returns></returns>
		public static ScriptSource Compile(Script script)
		{
			var compiledScript = engine.CreateScriptSourceFromString(script.FullText);
			return compiledScript;
		}

		/// <summary>
		/// Creates a script scope containing user defined variables.
		/// </summary>
		/// <param name="variables">The variables to set, or null to set no variables.</param>
		/// <returns></returns>
		public static ScriptScope CreateScope(IDictionary<string, object> variables = null)
		{
			// Set injected variables
			var scope = engine.CreateScope();
			if (variables != null)
			{
				foreach (var kvp in variables)
					scope.SetVariable(kvp.Key, kvp.Value);
			}
			return scope;
		}

		/// <summary>
		/// Evaluates a script expression in a sandboxed environment.
		/// Note that the return value of the script may still contain insecure code, so be careful!
		/// Expressions will have the following modules/classes imported:
		/// * System.Linq;
		/// * FrEee.Utility.Extensions
		/// * Math (from System)
		/// * modGlobalScript
		/// </summary>
		/// <param name="expression">The script code to run.</param>
		/// <param name="variables">Variables to inject into the script.</param>
		/// <returns>Any .</returns>
		public static T EvaluateExpression<T>(string expression, IDictionary<string, object> variables = null)
		{
			if (expression.IndexOf("\n") >= 0)
				throw new ScriptException("Cannot evaluate a script containing newlines. Consider using CallFunction instead.");

			var imports = new List<string>();
			imports.Add("import clr;");
			imports.Add("import System;");
			imports.Add("clr.AddReference('System.Core');");
			imports.Add("from System import Linq;");
			imports.Add("clr.AddReferenceToFileAndPath('FrEee.Core.dll');");
			imports.Add("from FrEee.Utility import Extensions;");
			imports.Add("clr.ImportExtensions(Extensions);");
			imports.Add("from System import Math;");

			Script script;
			if (Mod.Current == null || Mod.Current.GlobalScript == null)
				script = new Script("expression", string.Join("\n", imports.ToArray()) + "\n" + expression);
			else
			{
				imports.Add("import " + Mod.Current.GlobalScript.ModuleName + ";");
				script = new Script("expression", string.Join("\n", imports.ToArray()) + "\n" + expression, Mod.Current.GlobalScript);
			}
			var compiledScript = Compile(script);
			var scope = CreateScope(variables);
			object result;
			try
			{
				var handle = compiledScript.ExecuteAndWrap(scope);
				result = handle.Unwrap();
				if (result is T)
					return (T)result;
				else
					throw new ScriptException("Expected " + typeof(T) + " return value from " + expression + ", got " + result.GetType() + ".");
			}
			catch (Microsoft.Scripting.SyntaxErrorException ex)
			{
				throw new ScriptException(
@"Syntax error in script on line " + ex.Line + @" column " + ex.Column + @":
" + ex.Message + @"
Source code in question:
" + ex.SourceCode);
			}
		}

		/// <summary>
		/// Runs a script in a sandboxed environment.
		/// </summary>
		/// <param name="script">The script code to run.</param>
		/// <param name="variables">Variables to inject into the script.</param>
		public static void RunScript(Script script, IDictionary<string, object> variables = null)
		{
			var compiledScript = Compile(script);
			var scope = CreateScope(variables);
			var handle = compiledScript.ExecuteAndWrap(scope);
			var error = engine.GetService<ExceptionOperations>().FormatException(handle);
			if (error != null)
				throw new ScriptException(error);
		}

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
			var functionCall = new Script("functionCall", script.ModuleName + "." + function + "(" + string.Join(", ", args) + ")", script);
			var compiledScript = Compile(functionCall);
			object result;
			try
			{
				var handle = compiledScript.ExecuteAndWrap();
				result = handle.Unwrap();
				if (result is T)
					return (T)result;
				else
					throw new ScriptException("Expected " + typeof(T) + " return value from calling " + function + " in module " + script.ModuleName + ", got " + result.GetType() + ".");
			}
			catch (Microsoft.Scripting.SyntaxErrorException ex)
			{
				throw new ScriptException(
@"Syntax error in script on line " + ex.Line + @" column " + ex.Column + @":
" + ex.Message + @"
Source code in question:
" + ex.SourceCode);
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
			var subCall = new Script("subCall", function + "(" + string.Join(", ", args) + ");", script);
			var compiledScript = Compile(subCall);
			string error = null;
			sandbox.DoCallBack(() =>
			{
				try
				{
					compiledScript.ExecuteAndWrap();
				}
				catch (Exception ex)
				{
					error = ex.Message;
				}
			});
			if (error != null)
				throw new ScriptException(error);
		}

		private static AppDomain sandbox;
		private static Microsoft.Scripting.Hosting.ScriptEngine engine;
	}

	public class ScriptException : Exception
	{
		public ScriptException(string message)
			: base(message)
		{
		}
	}
}
