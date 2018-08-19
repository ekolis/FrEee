using System;

namespace FrEee.Utility
{
    /// <summary>
    /// Generates random numbers.
    /// </summary>
    public static class RandomHelper
    {
        #region Public Constructors

        static RandomHelper()
        {
            PRNG = new PRNG((int)DateTime.Now.Ticks);
        }

        #endregion Public Constructors

        #region Public Properties

        public static PRNG PRNG { get; private set; }

        #endregion Public Properties

        #region Public Methods

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

        #endregion Public Methods
    }
}
