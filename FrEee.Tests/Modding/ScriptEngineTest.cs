using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrEee.Modding;
using System.Collections.Generic;

namespace FrEee.Tests.Modding
{
	[TestClass]
	[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
	public class ScriptEngineTest
	{
		[TestMethod]
		public void EvaluateExpression()
		{
			var expr = "x * y";
			var variables = new Dictionary<string, object>();
			variables.Add("x", 6);
			variables.Add("y", 7);
			Assert.AreEqual(42, ScriptEngine.EvaluateExpression<int>(expr, false, variables));
		}

		[TestMethod]
		public void CallFunction()
		{
			var script = new Script("test", "def square(x):\n\treturn x ** 2;");
			Assert.AreEqual(64, ScriptEngine.CallFunction<int>(script, "square", false, 8));
		}
	}
}
