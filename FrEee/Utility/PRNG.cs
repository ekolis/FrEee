using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Utility
{
    /// <summary>
    /// PredictibleRandomNumberGenerator.
    /// pretty much the same as RandomHelper, but not static, and requires a seed.
    /// </summary>
    public class PRNG
    {
        int seed;
        Random prng;
        public PRNG(int seed)
        {
            this.seed = seed;
            prng = new Random(seed);
        }

        public int Next(int upper)
        {
            return prng.Next(upper);
        }

        /// <summary>
        /// Generates a random number >= 0 but less than the upper bound.
        /// </summary>
        /// <param name="upper">The upper bound.</param>
        /// <returns></returns>
        public long Next(long upper)
        {
            return Next((int)(upper / (long)int.MaxValue / 4L)) * (long)int.MaxValue * 4L + Next((int)(upper % ((long)int.MaxValue))) * 2 + Next(2);
        }

        /// <summary>
        /// Generates a random number within a range (inclusive).
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        public int Range(int min, int max)
        {
            return prng.Next(min, max + 1);
        }

        /// <summary>
        /// Generates a random number within a range (inclusive).
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        public long Range(long min, long max)
        {
            return Next(max - min + 1) + min;
        }

        /// <summary>
        /// Generates a random number >= 0 but less than the upper bound.
        /// </summary>
        /// <param name="upper">The upper bound.</param>
        /// <returns></returns>
        public double Next(double upper)
        {
            return prng.NextDouble() * upper;
        }
    }
}
