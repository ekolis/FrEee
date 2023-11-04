using FrEee.Extensions;
using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Serialization; using FrEee.Serialization.Attributes;
using FrEee.Utility;

namespace FrEee.Objects.Commands
{
	/// <summary>
	/// Sets a player note.
	/// </summary>
	public class SetPlayerNoteCommand : Command<Empire>
	{
		public SetPlayerNoteCommand(IReferrable target, string note)
			: base(Empire.Current)
		{
			Target = target;
			Note = note;
		}

		public string Note { get; set; }

		[GameReference]
		public IReferrable Target { get; set; }

		public override void Execute()
		{
			Executor.PlayerNotes[Target] = Note;
		}
	}
}
