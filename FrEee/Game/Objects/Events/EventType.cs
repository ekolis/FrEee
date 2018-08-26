using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using System.Collections.Generic;
using System.IO;

namespace FrEee.Game.Objects.Technology
{
	/// <summary>
	/// A type of random event or intelligence project.
	/// </summary>
	public class EventType : IModObject
	{
		public EventType()
		{
		}

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
		/// Script to select a target for the event.
		/// </summary>
		public ObjectFormula<IEnumerable<object>> TargetSelector { get; set; }

		/// <summary>
		/// Scripts to set parameters for the event type.
		/// </summary>
		public ICollection<Script> Parameters { get; set; }

		/// <summary>
		/// Actiosn to execute when the event occurs.,
		/// </summary>
		public ICollection<Script> Actions { get; set; }
	}
}