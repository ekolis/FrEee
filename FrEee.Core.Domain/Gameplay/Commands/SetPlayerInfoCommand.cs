using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using System;
using System.Collections.Generic;

namespace FrEee.Gameplay.Commands;

/// <summary>
/// Sets the player info for an empire.
/// </summary>
/// <typeparam name="T"></typeparam>
[Serializable]
public class SetPlayerInfoCommand : Command<Empire>
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