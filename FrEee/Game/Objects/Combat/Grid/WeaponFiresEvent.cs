using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Technology;
using FrEee.Utility;
using FrEee.Utility.Extensions;

#nullable enable

namespace FrEee.Game.Objects.Combat.Grid
{
	public class WeaponFiresEvent : BattleEvent
	{
		public WeaponFiresEvent(Battle battle, ICombatant attacker, IntVector2 here, ICombatant target, IntVector2 there, Component weapon, Hit? hit, bool wasTargetDisarmed)
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

		private GalaxyReference<ICombatant?>? attacker { get; set; }

		private GalaxyReference<ICombatant?>? target { get; set; }

		[DoNotSerialize]
		public ICombatant? Attacker
		{
			get
			{
				if (attacker?.Value != null)
				{
					return attacker.Value;
				}
				else if (attacker?.ID != null)
				{
					return Battle?.StartCombatants?[attacker.ID];
				}
				return null;
			}

			set => attacker = value.ReferViaGalaxy();
		}

		[DoNotSerialize]
		public ICombatant? Target
		{
			get
			{
				if (target?.Value != null)
				{
					return target.Value;
				}
				else if (target?.ID != null)
				{
					return Battle?.StartCombatants?[target.ID];
				}
				return null;
			}

			set => target = value.ReferViaGalaxy();
		}

		// TODO - make this some sort of reference?
		public Component Weapon { get; set; }

		public Hit? Hit { get; set; }
		public int Damage { get; set; }

		public bool WasTargetDisarmed { get; set; }
	}
}
