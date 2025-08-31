using FrEee.Extensions;
using FrEee.Gameplay.Commands;
using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Serialization;

namespace FrEee.Plugins.Default.Commands.Notes;

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
	public IReferrable Target { get; set; }

	private GameReference<IReferrable> target
	{
		get => Target?.ReferViaGalaxy();
		set => Target = value?.Value;
	}

	public override void Execute()
	{
		Executor.PlayerNotes.Remove(target);
	}
}