namespace FrEee.Enumerations
{
	/// <summary>
	/// Priority for sharing of abilities.
	/// </summary>
	public enum SharingPriority
	{
		/// <summary>
		/// Empires with no priority won't get the ability shared to them.
		/// </summary>
		None = 0,

		/// <summary>
		/// Empires with low priority get last pick at the ability.
		/// If high and medium priority empires used the ability up, low priority empires won't get any.
		/// </summary>
		Low = 1,

		/// <summary>
		/// Empires with medium priority get a pick at the ability along with the owner of the ability.
		/// If high priority empires used the ability up, medium priority empires won't get any.
		/// </summary>
		Medium = 2,

		/// <summary>
		/// Empires with high priority get a pick at the ability prior to even the owner of the ability.
		/// </summary>
		High = 3
	}
}