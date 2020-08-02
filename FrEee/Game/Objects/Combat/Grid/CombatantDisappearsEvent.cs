using FrEee.Game.Interfaces;
using FrEee.Utility;
using System;

#nullable enable

namespace FrEee.Game.Objects.Combat.Grid
{
	[Obsolete("This class is deprecated; use CombatantDestroyedEvent if a combatant is destroyed.")]
	public class CombatantDisappearsEvent : BattleEvent
	{
		public CombatantDisappearsEvent(IBattle battle, ICombatant combatant, IntVector2 position)
			: base(battle, combatant, position, position)
		{
		}

	}
}
