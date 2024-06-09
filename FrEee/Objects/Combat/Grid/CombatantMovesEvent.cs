using FrEee.Utility;
using FrEee.Utility;
namespace FrEee.Objects.Combat.Grid;

public class CombatantMovesEvent : BattleEvent
{
	public CombatantMovesEvent(Battle battle, ICombatant combatant, Vector2<int> here, Vector2<int> there)
		: base(battle, combatant, here, there)
	{
	}
}