using FrEee.Objects.Events;
using System.Collections.Generic;
using FrEee.Objects.GameState;

namespace FrEee.Modding;

/// <summary>
/// A template for random events.
/// </summary>
public class EventTemplate : IModObject, ITemplate<Event>
{
	public EventTemplate()
	{
	}

	/// <summary>
	/// The amount of effect that this event will have.
	/// What this means depends on the event type.
	/// </summary>
	public Formula<int> EffectAmount { get; set; }

	/// <summary>
	/// Is this EventTemplate disposed?
	/// </summary>
	public bool IsDisposed { get; set; }

	/// <summary>
	/// Who will receive a message when this event occurs?
	/// </summary>
	public EventMessageTarget MessageTarget { get; set; }

	/// <summary>
	/// An ID used to represent this mod object.
	/// </summary>
	public string ModID { get; set; }

	/// <summary>
	/// Not used.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// The message to send to affected players when the event occurs.
	/// </summary>
	public ICollection<EventMessage> OccurrenceMessages { get; set; }

	/// <summary>
	/// The name of the picture to display when this event occurs.
	/// Relative to the Pictures/Events folder.
	/// </summary>
	public Formula<string> Picture { get; set; }

	/// <summary>
	/// The severity of the event.
	/// </summary>
	public EventSeverity Severity { get; set; }

	/// <summary>
	/// Parameters from the mod meta templates.
	/// </summary>
	public IDictionary<string, object> TemplateParameters { get; set; }

	/// <summary>
	/// How long after being triggered until this event actually occurs?
	/// </summary>
	public Formula<int> TimeTillCompletion { get; set; }

	/// <summary>
	/// The type of event.
	/// </summary>
	public EventType Type { get; set; }

	/// <summary>
	/// The message to send to affected players when the event is triggered.
	/// </summary>
	public ICollection<EventMessage> WarningMessages { get; set; }

	/// <summary>
	/// Instantiates an event.
	/// </summary>
	/// <returns>
	/// The new event.
	/// </returns>
	public Event Instantiate()
	{
		return new Event(this);
	}

	public override string ToString()
	{
		return Type.Name;
	}
}