using System.Linq;
using FrEee.Extensions;
using NUnit.Framework;

namespace FrEee.Utility.Extensions;

/// <summary>
/// Tests extension methods.
/// </summary>
public class EnumerableExtensionsTest
{
	[Test]
	public void ExceptSingle()
	{
		var list = new object[] { 42, "fred", null };
		Assert.AreEqual(2, list.ExceptSingle(null).Count());
	}
}
