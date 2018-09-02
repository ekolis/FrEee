using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using System;
using System.Collections.Generic;

namespace FrEee.Game.Objects.Commands
{
	/// <summary>
	/// Sets the player info for an empire.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class SetPlayerInfoCommand: Command<Empire>
	{
		public SetPlayerInfoCommand(Empire target)
			: base(target)
		{
		}

		public override IEnumerable<IReferrable> NewReferrables
		{
			get
			{
				yield break;
			}
		}

		public override void Execute()
		{
			Executor.PlayerInfo = PlayerInfo;
		}

		public PlayerInfo PlayerInfo { get; set; }
	}
}