using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using FrEee.Utility;

namespace FrEee.Tests.Utility
{
	/// <summary>
	/// Tests math.
	/// </summary>
	[TestClass]
	public class MathXTest
	{
		[TestMethod]
		public void Ceiling()
		{
			Assert.AreEqual(0, MathX.Ceiling(0, 0));
			Assert.AreEqual(1, MathX.Ceiling(0.1));
			Assert.AreEqual(0.1, MathX.Ceiling(0.01, 1));
		}
	}
}