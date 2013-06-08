using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Technology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A space object that fire weapons and be targeted by weapons.
	/// </summary>
	public interface ICombatObject : ISpaceObject
	{
		/// <summary>
		/// Can this object fire on another space object?
		/// </summary>
		/// <param name="target"></param>
		/// <returns>true if the target is an enemy and this space object has weapons capable of targeting it</returns>
		bool CanTarget(ISpaceObject target);

		/// <summary>
		/// What type of object is this for weapon targeting purposes?
		/// </summary>
		WeaponTargets WeaponTargetType { get; }

		/// <summary>
		/// Any undamaged weapons this space object is armed with.
		/// </summary>
		IEnumerable<Component> Weapons { get; }

		/// <summary>
		/// Inflicts damage on this space object.
		/// </summary>
		/// <param name="damageType"></param>
		/// <param name="damage"></param>
		/// <param name="battle">For logging</param>
		void TakeDamage(DamageType damageType, int damage, Battle battle);

		/// <summary>
		/// Is this object destroyed?
		/// </summary>
		bool IsDestroyed { get; }

		int NormalShields { get; set; }

		int PhasedShields { get; set; }

		int MaxNormalShields { get; }

		int MaxPhasedShields { get; }
	}
}