using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Technology;
using FrEee.Utility;
using FrEee.Utility.Extensions;

namespace FrEee.Game.Objects.Combat.Grid
{
	public class WeaponFiresEvent : BattleEvent
	{
		public WeaponFiresEvent(Battle battle, ICombatant combatant, IntVector2 here, ICombatant target, IntVector2 there, Component weapon, Hit hit, bool wasTargetDisarmed)
			: base(battle, combatant, here, there)
		{
			Target = target;
			Weapon = weapon;
			Hit = hit;
			IsHit = hit != null;
			Damage = hit?.NominalDamage ?? 0;
			WasTargetDisarmed = wasTargetDisarmed;
		}
		public bool IsHit { get; set; }

		private GalaxyReference<ICombatant> target { get; set; }

		public ICombatant Target
		{
			get => target?.Value ?? Battle?.StartCombatants?[target.ID];
			set => target = value.ReferViaGalaxy();
		}

		private GalaxyReference<Component> weapon { get; set; }

		public Component Weapon
		{
			get => weapon?.Value ?? Battle?.StartWeapons?[weapon.ID];
			set => weapon = value.ReferViaGalaxy();
		}

		public Hit Hit { get; set; }
		public int Damage { get; set; }

		public bool WasTargetDisarmed { get; set; }
	}
}