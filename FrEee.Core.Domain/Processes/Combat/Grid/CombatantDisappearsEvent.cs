using FrEee.Utility;
using System;
using FrEee.Utility;
using FrEee.Processes.Combat;

namespace FrEee.Processes.Combat.Grid;

[Obsolete("This class is deprecated; use CombatantDestroyedEvent if a combatant is destroyed.")]
public class CombatantDisappearsEvent : BattleEvent
{
    public CombatantDisappearsEvent(IBattle battle, ICombatant combatant, Vector2<int> position)
        : base(battle, combatant, position, position)
    {
    }

}