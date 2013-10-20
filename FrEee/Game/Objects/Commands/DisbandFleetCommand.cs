using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Commands
{
	/// <summary>
	/// A command to disband a fleet.
	/// </summary>
	public class DisbandFleetCommand : Command<Fleet>
	{
		public DisbandFleetCommand(Fleet fleet)
			: base(fleet)
		{
		}

		public override void Execute()
		{
			Target.Dispose();
		}
	}
}
