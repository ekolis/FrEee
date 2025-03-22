using FrEee.Extensions;
using FrEee.Gameplay.Commands;
using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Serialization;

namespace FrEee.Plugins.Commands.Default.Commands.Notes;

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

	[DoNotSerialize]
	public IReferrable Target { get { return target.Value; } set { target = value.ReferViaGalaxy(); } }

	private GameReference<IReferrable> target { get; set; }

	public override void Execute()
	{
		Executor.PlayerNotes.Remove(target);
	}
}