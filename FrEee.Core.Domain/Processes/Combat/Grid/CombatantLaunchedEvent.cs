﻿using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Extensions;
using FrEee.Objects.GameState;

namespace FrEee.Processes.Combat.Grid;

public class CombatantLaunchedEvent : BattleEvent
{
    public CombatantLaunchedEvent(Battle battle, ICombatant launcher, ICombatant combatant, Vector2<int> position)
        : base(battle, combatant, position, position)
    {
        Launcher = launcher;
    }

    private GameReference<ICombatant> launcher { get; set; }

    [DoNotSerialize]
    public ICombatant Launcher
    {
        get => launcher?.Value ?? Battle?.StartCombatants?[launcher.ID];
        set => launcher = value.ReferViaGalaxy();
    }

}