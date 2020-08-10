using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Extensions;

#nullable enable

namespace FrEee.Game.Objects.Commands
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
		public IReferrable? Target { get => target?.Value; set => target = value.ReferViaGalaxy(); }

		private GalaxyReference<IReferrable?>? target { get; set; }

		public override void Execute()
		{
			if (Executor is null || target is null)
				return;
			Executor.PlayerNotes[target] = Note;
		}
	}
}
