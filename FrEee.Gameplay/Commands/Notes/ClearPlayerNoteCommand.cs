using FrEee.Objects.Civilization;
using FrEee.Serialization;
using FrEee.Extensions;
using FrEee.Objects.GameState;

namespace FrEee.Gameplay.Commands;

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