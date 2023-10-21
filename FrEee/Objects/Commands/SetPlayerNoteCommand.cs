using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Extensions;

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

		[DoNotSerialize]
		public IReferrable Target { get { return target.Value; } set { target = value.ReferViaGalaxy(); } }

		private GameReference<IReferrable> target { get; set; }

		public override void Execute()
		{
			Executor.PlayerNotes[target] = Note;
		}
	}
}