using FrEee.Game.Interfaces;
using FrEee.Utility;

#nullable enable

namespace FrEee.Game.Objects.Combat.Grid
{
	public class CombatantDestroyedEvent : BattleEvent
	{
		public CombatantDestroyedEvent(IBattle battle, ICombatant combatant, IntVector2 position)
			: base(battle, combatant, position, position)
		{
		}

	}
}
