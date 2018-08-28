using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Events;
using FrEee.Modding.Interfaces;
using System.Collections.Generic;

namespace FrEee.Modding
{
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
		public Script Action { get; set; }

		public bool IsDisposed { get; set; }

		/// <summary>
		/// The import statements required to run the target selector and action scripts.
		/// </summary>
		public Script Imports { get; set; }

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
		public IDictionary<string, ObjectFormula<object>> Parameters { get; set; }

		/// <summary>
		/// Script to select a target for the event.
		/// </summary>
		public ObjectFormula<IEnumerable<object>> TargetSelector { get; set; }
	}
}