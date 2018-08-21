using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Tests.Utility
{
	[TestClass]
	public class AssemblyInitializer
	{
		/// <summary>
		/// Performs necessary initialization before any tests run.
		/// </summary>
		[AssemblyInitialize]
		public static void AssemblyInit(TestContext ctx)
		{
			// Unit test projects don't have an entry assembly but we need one.
			TestUtilities.SetEntryAssembly();
		}
	}
}