using FrEee.Modding;
using NUnit.Framework;
using System.Collections.Generic;

namespace FrEee.Tests.Modding;

//[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
public class ScriptEngineTest
{
	[Test]
	public void CallFunction()
	{
		var script = new PythonScript("test", "def square(x):\n\treturn x ** 2;");
		Assert.AreEqual(64, PythonScriptEngine.CallFunction<int>(script, "square", 8));
	}

	[Test]
	public void EvaluateExpression()
	{
		var expr = "x * y";
		var variables = new Dictionary<string, object>();
		variables.Add("x", 6);
		variables.Add("y", 7);
		Assert.AreEqual(42, PythonScriptEngine.EvaluateExpression<int>(expr, variables));
	}
}