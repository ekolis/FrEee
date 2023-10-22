using FrEee.Extensions;
using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Serialization;
using FrEee.Utility;
using System.Collections.Generic;

namespace FrEee.Objects.Commands
{
	/// <summary>
	/// Clears a player note.
	/// </summary>
	public class ClearPlayerNoteCommand : Command<Empire>
	{
		public ClearPlayerNoteCommand(IReferrable target)
			: base(Empire.Current)
		{
			Target = target;
		}

		[GameReference]
		public IReferrable Target { get; set; }

		public override void Execute()
		{
			Executor.PlayerNotes.Remove(Target);
		}
	}
}
