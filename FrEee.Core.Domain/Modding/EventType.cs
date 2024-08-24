using FrEee.Modding.Scripts;
using FrEee.Objects.GameState;
using System.Collections.Generic;

namespace FrEee.Modding;

/// <summary>
/// A type of random event or intelligence project.
/// </summary>
public class EventType : IModObject
{
	public EventType()
	{
	}

	/// <summary>
	/// Action to execute when the event occurs.,
	/// </summary>
	public PythonScript Action { get; set; }

	/// <summary>
	/// The import statements required to run the target selector and action scripts.
	/// </summary>
	public PythonScript Imports { get; set; }

	public bool IsDisposed { get; set; }

	/// <summary>
	/// When is this a negative event?
	/// </summary>
	public Formula<bool> IsNegativeWhen { get; set; }

	public string ModID { get; set; }

	/// <summary>
	/// The name of this event type.
	/// </summary>
	public Formula<string> Name { get; set; }

	string INamed.Name => Name;

	/// <summary>
	/// Scripts to set parameters for the event type.
	/// </summary>
	public PythonScript Parameters { get; set; }

	/// <summary>
	/// Script to select a target for the event.
	/// </summary>
	public ObjectFormula<GameReferenceSet<IReferrable>> TargetSelector { get; set; }

	/// <summary>
	/// Parameters from the mod meta templates.
	/// </summary>
	public IDictionary<string, object> TemplateParameters { get; set; }

	public override string ToString()
	{
		return Name;
	}
}