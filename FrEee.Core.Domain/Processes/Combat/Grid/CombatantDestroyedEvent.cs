using FrEee.Utility;
namespace FrEee.Processes.Combat.Grid;

public class CombatantDestroyedEvent : BattleEvent
{
    public CombatantDestroyedEvent(IBattle battle, ICombatant combatant, Vector2<int> position)
        : base(battle, combatant, position, position)
    {
    }

}