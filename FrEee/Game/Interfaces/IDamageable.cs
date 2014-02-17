using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Combat;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Something which can take damage.
	/// </summary>
	public interface IDamageable
	{
		/// <summary>
		/// Current hitpoints.
		/// </summary>
		int Hitpoints { get; set; }

		/// <summary>
		/// Normal shield points.
		/// </summary>
		int NormalShields { get; set; }

		/// <summary>
		/// Phased shield points.
		/// </summary>
		int PhasedShields { get; set; }

		int MaxHitpoints { get; }

		int MaxNormalShields { get; }

		int MaxPhasedShields { get; }

		int ShieldHitpoints { get; }

		int ArmorHitpoints { get; }

		int HullHitpoints { get; }

		int MaxShieldHitpoints { get; }

		int MaxArmorHitpoints { get; }

		int MaxHullHitpoints { get; }

		/// <summary>
		/// Replenishes normal and phased shields.
		/// </summary>
		void ReplenishShields();

		/// <summary>
		/// Replenishes HP.
		/// If amount is are not specified, replenishes all HP.
		/// Otherwise repair points have a different effect on different objects.
		/// E.g. on a ship a repair point repairs 1 component while on a component a repair point replenishes 1 HP.
		/// </summary>
		/// <returns>The amount of unused repair points left over, or null for infinite.</returns>
		int? Repair(int? amount = null);

		/// <summary>
		/// Takes damage.
		/// </summary>
		/// <param name="dmgType"></param>
		/// <param name="damage"></param>
		/// <param name="dice"></param>
		/// <returns>Leftover damage.</returns>
		int TakeDamage(DamageType dmgType, int damage, PRNG dice = null);

		/// <summary>
		/// Is this object destroyed?
		/// </summary>
		bool IsDestroyed { get; }

		/// <summary>
		/// The chance for this object to be hit relative to another object that could also be hit by the same shot.
		/// </summary>
		int HitChance { get; }
	}
}
