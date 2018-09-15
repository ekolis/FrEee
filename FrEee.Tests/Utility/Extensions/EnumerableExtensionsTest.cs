using FrEee.Utility.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Tests.Utility.Extensions
{
	/// <summary>
	/// Tests extension methods.
	/// </summary>
	[TestClass]
	public class EnumerableExtensionsTest
	{
		[TestMethod]
		public void ExceptSingle()
		{
			var list = new object[] { 42, "fred", null };
			Assert.AreEqual(2, list.ExceptSingle(null).Count());
		}
	}
}