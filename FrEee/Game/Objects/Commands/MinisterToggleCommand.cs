using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Extensions;

#nullable enable

namespace FrEee.Game.Objects.Commands
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

		[DoNotSerialize]
		public IReferrable? Target { get => target?.Value; set => target = value.ReferViaGalaxy(); }

		private GalaxyReference<IReferrable?>? target { get; set; }

		public override void Execute()
		{
			if (target is null)
				return;
			Executor?.PlayerNotes.Remove(target);
		}
	}
}
