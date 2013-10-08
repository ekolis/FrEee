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
			adSetup.ApplicationBase = null;

			//Setting the permissions for the AppDomain. We give the permission to execute and to 
			//read/discover the location where the untrusted code is loaded.
			PermissionSet permSet = new PermissionSet(PermissionState.None);
			permSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));

			//We want the script engine assembly's strong name, so that we can add it to the full trust list.
			StrongName fullTrustAssembly = typeof(ScriptEngine).Assembly.Evidence.GetHostEvidence<StrongName>();

			//Now we have everything we need to create the AppDomain, so let's create it.
			AppDomain newDomain = AppDomain.CreateDomain("ScriptEngine", null, adSetup, permSet, fullTrustAssembly);

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
		/// Runs a script in a sandboxed environment.
		/// Note that the return value of the script may still contain insecure code, so be careful!
		/// </summary>
		/// <param name="script">The script code to run.</param>
		/// <param name="variables">Variables to inject into the script.</param>
		/// <returns></returns>
		public static dynamic RunScript(string script, IDictionary<string, object> variables = null)
		{
			var compiledScript = CompileScript(script);
			var scope = InjectVariables(variables);
			
			// run it and return the result
			return compiledScript.Execute(scope);
		}

		/// <summary>
		/// Calls a script function in a sandboxed environment.
		/// Note that the return value of the script may still contain insecure code, so be careful!
		/// </summary>
		/// <param name="script">The script containing the function.</param>
		/// <param name="function">The name of the function.</param>
		/// <param name="args">Arguments to pass to the function.</param>
		/// <returns></returns>
		public static dynamic CallFunction(string script, string function, params object[] args)
		{
			var compiledScript = CompileScript(script);
			var scope = InjectVariables(null);
			
			// run the specified function
			return engine.Operations.Invoke(scope.GetVariable(function), args);
		}

		private static Microsoft.Scripting.Hosting.ScriptEngine engine;
	}
}
