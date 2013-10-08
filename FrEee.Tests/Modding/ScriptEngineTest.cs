using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrEee.Modding;
using System.Collections.Generic;

namespace FrEee.Tests.Modding
{
	[TestClass]
	public class ScriptEngineTest
	{
		[TestMethod]
		public void RunScript()
		{
			var script = "return x * y;";
			var variables = new Dictionary<string, object>();
			variables.Add("x", 6);
			variables.Add("y", 7);
			Assert.AreEqual(42, (int)ScriptEngine.RunScript(script, variables));
		}

		[TestMethod]
		public void CallFunction()
		{
			var script =
				@"
					def square(x):
						return x ^ 2;
				";
			Assert.AreEqual(64, (int)ScriptEngine.CallFunction(script, "square", 8));
		}
	}
}
