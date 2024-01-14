using FrEee.Interfaces;
using FrEee.Utility;
using System.Linq;

namespace FrEee.Objects.Combat.Grid;

public class CombatantAppearsEvent : BattleEvent
{
	public CombatantAppearsEvent(IBattle battle, ICombatant combatant, IntVector2 position)
		: base(battle, combatant, position, position)
	{
		IsUnarmed = !(Combatant is Seeker) && !Combatant.Weapons.Any();
	}

	public bool IsUnarmed { get; set; }
}