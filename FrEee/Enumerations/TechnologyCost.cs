namespace FrEee.Enumerations
{
	/// <summary>
	/// Cost to research technology.
	/// </summary>
	public enum TechnologyCost
	{
		/// <summary>
		/// Cost = Level * BaseCost
		/// 1x, 2x, 3x, 4x...
		/// </summary>
		Low = 0,

		/// <summary>
		/// Cost = Level for level 1, Level ^ 2 * BaseCost / 2 for subsequent levels
		/// 1x, 2x, 4.5x, 8x...
		/// </summary>
		Medium = 1,

		/// <summary>
		/// Cost = Level ^ 2 * BaseCost
		/// 1x, 4x, 9x, 16x...
		/// </summary>
		High = 2,
	}
}