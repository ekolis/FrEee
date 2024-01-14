using FrEee.Utility; using FrEee.Serialization;
using NUnit.Framework;

namespace FrEee.Tests.Utility;

/// <summary>
/// Tries to test the random number generator. As much as a RNG is testable...
/// </summary>
public class PrngTest
{
	private PRNG prng;

	[SetUp]
	public void Init()
	{
		prng = new PRNG(42);
	}

	[Test]
	public void NextLong()
	{
		for (var i = 0; i < 1000; i++)
		{
			long max = 1234567890;
			Assert.IsFalse(prng.Next(max) > max);
		}
	}
}