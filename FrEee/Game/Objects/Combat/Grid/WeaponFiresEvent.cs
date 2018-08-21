using FrEee.Game.Interfaces;
using FrEee.Utility;

namespace FrEee.Game.Objects.Combat.Grid
{
	public class WeaponFiresEvent : IBattleEvent
	{
		public WeaponFiresEvent(ICombatant combatant, IntVector2 here, ICombatant target, IntVector2 there, bool isHit)
		{
			Combatant = combatant;
			Target = target;
			StartPosition = here;
			EndPosition = there;
			IsHit = isHit;
		}

		public ICombatant Combatant { get; set; }

		public IntVector2 EndPosition { get; set; }
		public bool IsHit { get; set; }
		public IntVector2 StartPosition { get; set; }
		public ICombatant Target { get; set; }
	}
}