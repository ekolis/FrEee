using FrEee.Interfaces;
using FrEee.Objects.Combat;
using FrEee.Utility;

namespace FrEee.Objects.Combat.Grid
{
	public class CombatantDestroyedEvent : BattleEvent
	{
		public CombatantDestroyedEvent(IBattle battle, ICombatant combatant, IntVector2 position)
			: base(battle, combatant, position, position)
		{
		}

	}
}