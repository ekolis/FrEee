using FrEee.Utility;
using System;
using FrEee.Utility;

namespace FrEee.Objects.Combat.Grid;

[Obsolete("This class is deprecated; use CombatantDestroyedEvent if a combatant is destroyed.")]
public class CombatantDisappearsEvent : BattleEvent
{
	public CombatantDisappearsEvent(IBattle battle, ICombatant combatant, Vector2<int> position)
		: base(battle, combatant, position, position)
	{
	}

}