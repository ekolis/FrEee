﻿using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Technology;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System.Collections.Generic;

namespace FrEee.Game.Objects.Combat
{
	/// <summary>
	/// A weapon's fire, or another source of damage.
	/// </summary>
	public class Shot : IFormulaHost
	{
		#region Public Constructors

		public Shot(ICombatant attacker, Component weapon, IDamageable defender, int range)
		{
			Attacker = attacker;
			Weapon = weapon;
			Defender = defender;
			Range = range;
			DamageLeft = FullDamage;
		}

		#endregion Public Constructors

		#region Public Properties

		public GalaxyReference<ICombatant> attacker { get; set; }

		[DoNotSerialize]
		public ICombatant Attacker { get { return attacker == null ? null : attacker.Value; } set { attacker = value == null ? null : value.ReferViaGalaxy(); } }

		public int DamageLeft { get; private set; }

		public DamageType DamageType
		{
			get
			{
				if (Weapon != null && Weapon.Template.ComponentTemplate.WeaponInfo != null)
					return Weapon.Template.ComponentTemplate.WeaponInfo.DamageType;
				return Mod.Current.DamageTypes.FindByName("Normal") ?? new DamageType(); // TODO - moddable damage types for storms, etc.
			}
		}

		[DoNotSerialize]
		public IDamageable Defender { get { return target == null ? null : target.Value; } set { target = value == null ? null : value.ReferViaGalaxy(); } }

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
				var dict = Variables;
				dict["range"] = EffectiveRange;
				return Weapon.Template.GetWeaponDamage(dict); // TODO - use PRNG
			}
		}

		public IEnumerable<Hit> Hits { get; private set; }
		public int Range { get; set; }
		public GalaxyReference<IDamageable> target { get; set; }

		public IDictionary<string, object> Variables
		{
			get
			{
				return new Dictionary<string, object>
				{
					{ "weapon", Weapon},
					{ "attacker", Defender},
					{ "defender", Defender},
					{ "range", Range},
				};
			}
		}

		public GalaxyReference<Component> weapon { get; set; }

		[DoNotSerialize]
		public Component Weapon { get { return weapon; } set { weapon = value; } }

		#endregion Public Properties

		#region Public Methods

		public int InflictDamage(IDamageable target, PRNG dice = null)
		{
			var hit = new Hit(this, target, DamageLeft);
			DamageLeft = target.TakeDamage(hit, dice);
			return DamageLeft;
		}

		public bool RollAccuracy(PRNG dice = null)
		{
			var accuracy = Weapon.Template.WeaponAccuracy + Attacker.Accuracy + Mod.Current.Settings.WeaponAccuracyPointBlank - Range * Mod.Current.Settings.WeaponAccuracyLossPerSquare;
			int evasion = 0;
			if (Defender is ITargetable t)
				evasion = t.Evasion;
			var netAccuracy = accuracy - evasion;
			if (netAccuracy > 99)
				netAccuracy = 99;
			if (netAccuracy < 1)
				netAccuracy = 1;
			return RandomHelper.Range(1, 100) < netAccuracy;
		}

		public override string ToString()
		{
			return Attacker + "'s " + Weapon + " vs. " + Defender + " at range " + Range + " (" + DamageLeft + " damage left)";
		}

		#endregion Public Methods
	}
}