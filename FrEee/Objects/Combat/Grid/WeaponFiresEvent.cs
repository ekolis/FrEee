using FrEee.Interfaces;
using FrEee.Objects.Technology;
using FrEee.Objects.Combat;
using FrEee.Utility;
using FrEee.Extensions;
using FrEee.Serialization; using FrEee.Serialization.Attributes;

namespace FrEee.Objects.Combat.Grid
{
	public class WeaponFiresEvent : BattleEvent
	{
		public WeaponFiresEvent(Battle battle, ICombatant attacker, IntVector2 here, ICombatant target, IntVector2 there, Component weapon, Hit hit, bool wasTargetDisarmed)
			: base(battle, attacker, here, there)
		{
			Attacker = attacker;
			Target = target;
			Weapon = weapon;
			Hit = hit;
			IsHit = hit != null;
			Damage = hit?.NominalDamage ?? 0;
			WasTargetDisarmed = wasTargetDisarmed;
		}
		public bool IsHit { get; set; }

		private ICombatant OurAttacker { get; set; }

		private ICombatant OurTarget { get; set; }

		[DoNotSerialize]
		public ICombatant Attacker
		{
			get => OurAttacker ?? Battle?.StartCombatants?[OurAttacker.ID];
			set => OurAttacker = value;
		}

		[DoNotSerialize]
		public ICombatant Target
		{
			get => OurTarget ?? Battle?.StartCombatants?[OurTarget.ID];
			set => OurTarget = value;
		}

		// TODO - make this some sort of reference?
		public Component Weapon { get; set; }

		public Hit Hit { get; set; }
		public int Damage { get; set; }

		public bool WasTargetDisarmed { get; set; }
	}
}
