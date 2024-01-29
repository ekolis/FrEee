using FrEee.Interfaces;
using FrEee.Utility;
namespace FrEee.Objects.Combat.Grid;

public class CombatantDestroyedEvent : BattleEvent
{
	public CombatantDestroyedEvent(IBattle battle, ICombatant combatant, Vector2<int> position)
		: base(battle, combatant, position, position)
	{
	}

}