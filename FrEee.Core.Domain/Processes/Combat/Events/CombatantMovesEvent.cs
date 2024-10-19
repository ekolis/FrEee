using FrEee.Utility;
namespace FrEee.Processes.Combat.Events;

public class CombatantMovesEvent : BattleEvent
{
	public CombatantMovesEvent(IBattle battle, ICombatant combatant, Vector2<int> here, Vector2<int> there)
		: base(battle, combatant, here, there)
	{
	}
}