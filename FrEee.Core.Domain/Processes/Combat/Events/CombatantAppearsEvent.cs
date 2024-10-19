using FrEee.Utility;
using System.Linq;

namespace FrEee.Processes.Combat.Events;

public class CombatantAppearsEvent : BattleEvent
{
	public CombatantAppearsEvent(IBattle battle, ICombatant combatant, Vector2<int> position)
		: base(battle, combatant, position, position)
	{
		IsUnarmed = !(Combatant is Seeker) && !Combatant.Weapons.Any();
	}

	public bool IsUnarmed { get; set; }
}