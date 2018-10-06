using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Extensions;

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
		public IReferrable Target { get { return target.Value; } set { target = value.ReferViaGalaxy(); } }

		private GalaxyReference<IReferrable> target { get; set; }

		public override void Execute()
		{
			Executor.PlayerNotes.Remove(target);
		}
	}
}