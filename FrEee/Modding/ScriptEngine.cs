using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace FrEee.Modding
{
	/// <summary>
	/// IronPython scripting engine
	/// </summary>
	public class ScriptEngine : MarshalByRefObject
	{
		private static void CreatePython()
		{
			// http://msdn.microsoft.com/en-us/library/bb763046.aspx

			//Setting the AppDomainSetup. It is very important to set the ApplicationBase to a folder 
			//other than the one in which the sandboxer resides.
			AppDomainSetup adSetup = new AppDomainSetup();
			adSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
			adSetup.ApplicationName = "FrEee";
			adSetup.DynamicBase = "ScriptEngine";

			//Setting the permissions for the AppDomain. We give the permission to execute and to 
			//read/discover the location where the untrusted code is loaded.
			PermissionSet permSet = new PermissionSet(PermissionState.None);
			permSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));

			//Now we have everything we need to create the AppDomain, so let's create it.
			AppDomain newDomain = AppDomain.CreateDomain("ScriptEngine", null, adSetup, permSet);

			engine = Python.CreateEngine(newDomain);
		}

		private static ScriptSource CompileScript(string script, IDictionary<string, object> variables = null)
		{
			// Create the IronPython interpreter
			if (engine == null)
				CreatePython();

			// Load the script
			var compiledScript = engine.CreateScriptSourceFromString(script);
			return compiledScript;
		}

		private static ScriptScope InjectVariables(IDictionary<string, object> variables = null)
		{
			// Create the IronPython interpreter
			if (engine == null)
				CreatePython();

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
		/// </summary>
		/// <param name="script">The script code to run.</param>
		/// <param name="variables">Variables to inject into the script.</param>
		/// <returns>Any .</returns>
		public static T EvaluateExpression<T>(string script, IDictionary<string, object> variables = null)
		{
			if (script.IndexOf("\n") >= 0)
				throw new ScriptException("Cannot evaluate a script containing newlines. Consider using CallFunction instead.");
			var compiledScript = CompileScript(script);
			var scope = InjectVariables(variables);
			return compiledScript.Execute<T>(scope);
		}

		/// <summary>
		/// Runs a script in a sandboxed environment.
		/// </summary>
		/// <param name="script">The script code to run.</param>
		/// <param name="variables">Variables to inject into the script.</param>
		public static void RunScript(string script, IDictionary<string, object> variables = null)
		{
			var compiledScript = CompileScript(script);
			var scope = InjectVariables(variables);
			compiledScript.Execute();
		}

		/// <summary>
		/// Calls a script function in a sandboxed environment.
		/// Note that the return value of the script may still contain insecure code, so be careful!
		/// </summary>
		/// <param name="script">The script containing the function.</param>
		/// <param name="function">The name of the function.</param>
		/// <param name="args">Arguments to pass to the function.</param>
		/// <returns>The return value.</returns>
		public static T CallFunction<T>(string script, string function, params object[] args)
		{
			var fullScript = script + "\n\nresult = " + function + "(" + string.Join(", ", args) + ")";
			var compiledScript = CompileScript(fullScript);
			var scope = engine.CreateScope();
			compiledScript.Execute(scope);
			return scope.GetVariable<T>("result");
		}

		/// <summary>
		/// Calls a script subroutine in a sandboxed environment.
		/// Note that the return value of the script may still contain insecure code, so be careful!
		/// </summary>
		/// <param name="script">The script containing the function.</param>
		/// <param name="function">The name of the function.</param>
		/// <param name="args">Arguments to pass to the function.</param>
		/// <returns>The return value.</returns>
		public static void CallSubroutine(string script, string function, params object[] args)
		{
			var fullScript = script + "\n\n" + function + "(" + string.Join(", ", args) + ");";
			var compiledScript = CompileScript(script);
			var handle = compiledScript.ExecuteAndWrap();
			var eo = engine.GetService<ExceptionOperations>();
			var obj = handle.Unwrap();
			if (obj is Exception)
				throw (Exception)obj;
		}

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
