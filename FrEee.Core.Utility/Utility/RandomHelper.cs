using System;
using FrEee.Utility;
using FrEee.Utility;

namespace FrEee.Utility;

/// <summary>
/// Generates random numbers.
/// </summary>
public static class RandomHelper
{
	static RandomHelper()
	{
		PRNG = new PRNG((int)DateTime.Now.Ticks);
	}

	public static PRNG PRNG { get; private set; }

	/// <summary>
	/// Generates a random number >= 0 but less than the upper bound.
	/// </summary>
	/// <param name="upper">The upper bound.</param>
	/// <returns></returns>
	public static int Next(int upper, PRNG prng = null)
	{
		return (prng ?? PRNG).Next(upper);
	}

	/// <summary>
	/// Generates a random number >= 0 but less than the upper bound.
	/// </summary>
	/// <param name="upper">The upper bound.</param>
	/// <returns></returns>
	public static long Next(long upper, PRNG prng = null)
	{
		return (prng ?? PRNG).Next(upper);
	}

	/// <summary>
	/// Generates a random number >= 0 but less than the upper bound.
	/// </summary>
	/// <param name="upper">The upper bound.</param>
	/// <returns></returns>
	public static double Next(double upper, PRNG prng = null)
	{
		return (prng ?? PRNG).Next(upper);
	}

	/// <summary>
	/// Generates a random number within a range (inclusive).
	/// </summary>
	/// <param name="min">The minimum.</param>
	/// <param name="max">The maximum.</param>
	/// <returns></returns>
	public static int Range(int min, int max, PRNG prng = null)
	{
		return (prng ?? PRNG).Range(min, max);
	}

	/// <summary>
	/// Generates a random number within a range (inclusive).
	/// </summary>
	/// <param name="min">The minimum.</param>
	/// <param name="max">The maximum.</param>
	/// <returns></returns>
	public static long Range(long min, long max, PRNG prng = null)
	{
		return (prng ?? PRNG).Range(min, max);
	}

	/// <summary>
	/// Generates a random number within a range (inclusive).
	/// </summary>
	/// <param name="min">The minimum.</param>
	/// <param name="max">The maximum.</param>
	/// <returns></returns>
	public static double Range(double min, double max, PRNG prng = null)
	{
		return (prng ?? PRNG).Range(min, max);
	}

	/// <summary>
	/// Determines if something happens based on a percentage chance.
	/// </summary>
	/// <param name="chance">The chance of something happening, between 0 and 100.</param>
	/// <returns>Should something happen?</returns>
	public static bool PercentageChance(double chance, PRNG prng = null)
	{
		return Range(0d, 100d, prng) < chance;
	}

	/// <summary>
	/// Determines if something happens based on a per mille chance.
	/// </summary>
	/// <param name="chance">The chance of something happening, between 0 and 1000.</param>
	/// <returns>Should something happen?</returns>
	public static bool PerMilleChance(double chance, PRNG prng = null)
	{
		return Range(0d, 1000d, prng) < chance;
	}
}
