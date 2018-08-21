using FrEee.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrEee.Tests.Utility
{
	/// <summary>
	/// Tries to test the random number generator. As much as a RNG is testable...
	/// </summary>
	[TestClass]
	public class PrngTest
	{
		private PRNG prng;

		[TestInitialize]
		public void Init()
		{
			prng = new PRNG(42);
		}

		[TestMethod]
		public void NextLong()
		{
			for (var i = 0; i < 1000; i++)
			{
				long max = 1234567890;
				Assert.IsFalse(prng.Next(max) > max);
			}
		}
	}
}