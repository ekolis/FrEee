using FrEee.Game.Objects.Combat;
using FrEee.Modding;
using FrEee.Utility;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Something which can take damage.
	/// </summary>
	public interface IDamageable
	{
		int ArmorHitpoints { get; }

		/// <summary>
		/// The chance for this object to be hit relative to another object that could also be hit by the same shot.
		/// </summary>
		int HitChance { get; }

		/// <summary>
		/// Current hitpoints.
		/// </summary>
		int Hitpoints { get; set; }

		int HullHitpoints { get; }

		/// <summary>
		/// Is this object destroyed?
		/// </summary>
		bool IsDestroyed { get; }

		int MaxArmorHitpoints { get; }

		int MaxHitpoints { get; }

		int MaxHullHitpoints { get; }

		int MaxNormalShields { get; }

		int MaxPhasedShields { get; }

		int MaxShieldHitpoints { get; }

		/// <summary>
		/// Normal shield points.
		/// </summary>
		int NormalShields { get; set; }

		/// <summary>
		/// Phased shield points.
		/// </summary>
		int PhasedShields { get; set; }

		int ShieldHitpoints { get; }

		/// <summary>
		/// Replenishes HP.
		/// If amount is are not specified, replenishes all HP.
		/// Otherwise repair points have a different effect on different objects.
		/// E.g. on a ship a repair point repairs 1 component while on a component a repair point replenishes 1 HP.
		/// </summary>
		/// <returns>The amount of unused repair points left over, or null for infinite.</returns>
		int? Repair(int? amount = null);

		/// <summary>
		/// Replenishes normal and phased shields.
		/// </summary>
		/// <param name="amount">How many shields to replenish, or null to replenish all of them.</param>
		void ReplenishShields(int? amount = null);

		/// <summary>
		/// Takes damage.
		/// </summary>
		/// <returns>Leftover damage.</returns>
		int TakeDamage(Hit hit, PRNG dice = null);
	}

	public interface IDamageableReferrable : IDamageable, IReferrable { }
}
