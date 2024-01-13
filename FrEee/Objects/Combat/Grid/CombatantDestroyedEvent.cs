using FrEee.Interfaces;
using FrEee.Utility; using FrEee.Serialization;

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