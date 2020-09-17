#nullable enable

namespace FrEee.Game.Enumerations
{
	/// <summary>
	/// Diplomatic relations from one empire to another.
	/// Note that diplomatic relations are not necessarily mutual!
	/// Also relations may vary by star system if the empires have a neutral zone treaty.
	/// </summary>
	public enum Relations
	{
		/// <summary>
		/// This empire has not encountered the other empire.
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// This empire is hostile toward the other empire and will attack on sight.
		/// </summary>
		Hostile = 1,

		/// <summary>
		/// This empire has no formal relations toward the other empire and will only fight if provoked.
		/// TODO - actually implement this, right now neutral empires are treated as hostile
		/// </summary>
		Neutral = 2,

		/// <summary>
		/// This empire is allied with the other empire and will not attack it.
		/// </summary>
		Allied = 3,

		/// <summary>
		/// This empire *is* the other empire.
		/// </summary>
		Self = 4,
	}
}
