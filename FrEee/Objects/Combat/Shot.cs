using System.Collections.Generic;
using FrEee.Interfaces;
using FrEee.Objects.Technology;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using FrEee.Extensions;
using FrEee.Serialization;

namespace FrEee.Objects.Combat
{
	/// <summary>
	/// A weapon's fire, or another source of damage.
	/// </summary>
	public class Shot : IFormulaHost
	{
		public Shot(ICombatant attacker, Component weapon, IDamageable defender, int range)
		{
			Attacker = attacker;
			Weapon = weapon;
			Defender = defender;
			Range = range;
			DamageLeft = FullDamage;
		}

		[GameReference]
		public ICombatant Attacker { get; set; }

		public int DamageLeft { get; private set; }

		public DamageType DamageType
		{
			get
			{
				if (Weapon != null && Weapon.Template.ComponentTemplate.WeaponInfo != null)
					return Weapon.Template.ComponentTemplate.WeaponInfo.DamageType;
				return The.Mod.DamageTypes.FindByName("Normal") ?? new DamageType(); // TODO - moddable damage types for storms, etc.
			}
		}

		/// <summary>
		/// The specific target of this hit.
		/// </summary>
		[DoNotSerialize]
		public IDamageable Defender
		{
			get { return OurTarget ?? OurDamageableTarget; }
			set
			{
				if (value is IDamageableReferrable dr)
					OurTarget = dr;
				else
					OurDamageableTarget = value;
			}
		}

		/// <summary>
		/// Effective range for damage purposes, due to mount range modifiers
		/// </summary>
		public int EffectiveRange
		{
			get
			{
				var r = Range - (Weapon.Template.Mount == null ? 0 : Weapon.Template.Mount.WeaponRangeModifier.Evaluate(Weapon));
				if (r < 1)
					return 1;
				return r;
			}
		}

		public int FullDamage
		{
			get
			{
				if (Weapon == null || Range < Weapon.Template.WeaponMinRange || Range > Weapon.Template.WeaponMaxRange)
					return 0;
				return Weapon.Template.GetWeaponDamage(EffectiveRange); // TODO - use PRNG
			}
		}

		public IEnumerable<Hit> Hits { get; private set; }
		public int Range { get; set; }
		public IDamageableReferrable OurTarget { get; set; }
		private IDamageable OurDamageableTarget { get; set; }

		public IDictionary<string, object> Variables
		{
			get
			{
				return new Dictionary<string, object>
				{
					{ "range", Range},
				};
			}
		}

		// TODO - make this some sort of reference?
		public Component Weapon { get; set; }

		public int InflictDamage(IDamageable target, PRNG dice = null)
		{
			var hit = new Hit(this, target, DamageLeft);
			DamageLeft = target.TakeDamage(hit, dice);
			return DamageLeft;
		}

		public bool RollAccuracy(PRNG dice = null)
		{
			var accuracy = Weapon.Template.WeaponAccuracy + Attacker.Accuracy + The.Mod.Settings.WeaponAccuracyPointBlank - Range * The.Mod.Settings.WeaponAccuracyLossPerSquare;
			var evasion = 0;
			if (Defender is ITargetable t)
				evasion = t.Evasion;
			var netAccuracy = accuracy - evasion;
			if (netAccuracy > 99)
				netAccuracy = 99;
			if (netAccuracy < 1)
				netAccuracy = 1;
			return RandomHelper.Range(0, 99, dice) < netAccuracy;
		}

		public override string ToString()
		{
			return Attacker + "'s " + Weapon + " vs. " + Defender + " at range " + Range + " (" + DamageLeft + " damage left)";
		}
	}
}
