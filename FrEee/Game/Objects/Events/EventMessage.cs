using FrEee.Modding.Interfaces;

#nullable enable

namespace FrEee.Game.Objects.Events
{
	/// <summary>
	/// A message sent to a player when an event is triggered.
	/// </summary>
	public class EventMessage
	{
		public IFormula<string>? Title { get; set; }
		public IFormula<string>? Text { get; set; }
	}
}
