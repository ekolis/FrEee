using FrEee.Utility;
using System.Linq;
using FrEee.Utility;

namespace FrEee.Objects.Combat.Grid;

public class CombatantAppearsEvent : BattleEvent
{
	public CombatantAppearsEvent(IBattle battle, ICombatant combatant, Vector2<int> position)
		: base(battle, combatant, position, position)
	{
		IsUnarmed = !(Combatant is Seeker) && !Combatant.Weapons.Any();
	}

	public bool IsUnarmed { get; set; }
}