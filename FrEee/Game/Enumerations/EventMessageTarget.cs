#nullable enable

namespace FrEee.Game.Enumerations
{
	/// <summary>
	/// Determines who will receive a message when an event occurs.
	/// </summary>
	public enum EventMessageTarget
	{
		/// <summary>
		/// No one will receive a message.
		/// </summary>
		None = 0,

		/// <summary>
		/// The owner of the affected object will receive a message.
		/// </summary>
		Owner = 1,

		/// <summary>
		/// Whoever is in the same sector as the event will receive a message.
		/// </summary>
		Sector = 2,

		/// <summary>
		/// Whoever is in the same sector as the event will receive a message.
		/// </summary>
		System = 3,

		/// <summary>
		/// Everyone will receive a message.
		/// </summary>
		All = 4
	}
}
