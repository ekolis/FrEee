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
		public void EvaluateExpression()
		{
			var script = "x * y";
			var variables = new Dictionary<string, object>();
			variables.Add("x", 6);
			variables.Add("y", 7);
			Assert.AreEqual(42, ScriptEngine.EvaluateExpression<int>(script, variables));
		}

		[TestMethod]
		public void CallFunction()
		{
			var script = "def square(x):\n\treturn x ** 2;";
			Assert.AreEqual(64, ScriptEngine.CallFunction<int>(script, "square", 8));
		}
	}
}
