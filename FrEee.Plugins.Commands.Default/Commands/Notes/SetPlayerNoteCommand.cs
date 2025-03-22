using FrEee.Extensions;
using FrEee.Gameplay.Commands;
using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Serialization;

namespace FrEee.Plugins.Commands.Default.Commands.Notes;

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