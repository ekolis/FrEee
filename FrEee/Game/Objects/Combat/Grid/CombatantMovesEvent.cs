using FrEee.Game.Interfaces;
using FrEee.Utility;

#nullable enable

namespace FrEee.Game.Objects.Combat.Grid
{
	public class CombatantMovesEvent : BattleEvent
	{
		public CombatantMovesEvent(Battle battle, ICombatant combatant, IntVector2 here, IntVector2 there)
			: base(battle, combatant, here, there)
		{
		}
	}
}
