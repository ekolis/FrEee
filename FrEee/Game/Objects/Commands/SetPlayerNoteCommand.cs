using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Extensions;

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

		[DoNotSerialize]
		public IReferrable Target { get { return target.Value; } set { target = value.ReferViaGalaxy(); } }

		private GalaxyReference<IReferrable> target { get; set; }

		public string Note { get; set; }

		public override void Execute()
		{
			Executor.PlayerNotes[target] = Note;
		}
	}
}
