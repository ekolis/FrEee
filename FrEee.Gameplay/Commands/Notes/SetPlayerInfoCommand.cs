using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using System;
using System.Collections.Generic;

namespace FrEee.Gameplay.Commands.Notes;

/// <summary>
/// Sets the player info for an empire.
/// </summary>
/// <typeparam name="T"></typeparam>
[Serializable]
public class SetPlayerInfoCommand
	: Command<Empire>, ISetPlayerInfoCommand
{
	public SetPlayerInfoCommand(Empire target, PlayerInfo playerInfo)
		: base(target)
	{
		PlayerInfo = playerInfo;
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