using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Technology;
using FrEee.Utility;

namespace FrEee.Game.Objects.Combat.Grid
{
	public class WeaponFiresEvent : IBattleEvent
	{
		public WeaponFiresEvent(ICombatant combatant, IntVector2 here, ICombatant target, IntVector2 there, Component weapon, Hit hit, bool wasTargetDisarmed)
		{
			Combatant = combatant;
			Target = target;
			StartPosition = here;
			EndPosition = there;
			Weapon = weapon;
			Hit = hit;
			IsHit = hit != null;
			Damage = hit?.NominalDamage ?? 0;
			WasTargetDisarmed = wasTargetDisarmed;
		}

		public ICombatant Combatant { get; set; }

		public IntVector2 EndPosition { get; set; }
		public bool IsHit { get; set; }
		public IntVector2 StartPosition { get; set; }
		public ICombatant Target { get; set; }
		public Component Weapon { get; set; }
		public Hit Hit { get; set; }
		public int Damage { get; set; }

		public bool WasTargetDisarmed { get; set; }
	}
}