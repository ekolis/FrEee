using FrEee.Interfaces;
using FrEee.Utility; using FrEee.Serialization;

namespace FrEee.Objects.Combat.Grid;

public class CombatantMovesEvent : BattleEvent
{
	public CombatantMovesEvent(Battle battle, ICombatant combatant, IntVector2 here, IntVector2 there)
		: base(battle, combatant, here, there)
	{
	}
}