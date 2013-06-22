using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Technology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A space object that may fire weapons and can targeted by weapons.
	/// </summary>
	public interface ICombatObject : IPictorial, IOwnable, IDamageable
	{
		/// <summary>
		/// Can this object fire on another combat object?
		/// </summary>
		/// <param name="target"></param>
		/// <returns>true if the target is an enemy and this combat object has weapons capable of targeting it</returns>
		bool CanTarget(ICombatObject target);

		/// <summary>
		/// What type of object is this for weapon targeting purposes?
		/// </summary>
		WeaponTargets WeaponTargetType { get; }

		/// <summary>
		/// Any undamaged weapons this combat object is armed with.
		/// </summary>
		IEnumerable<Component> Weapons { get; }

		bool IsHostileTo(Empire emp);

		/// <summary>
		/// Accuracy rating of this combat object.
		/// </summary>
		int Accuracy { get; }

		/// <summary>
		/// Evasion rating of this combat object.
		/// </summary>
		int Evasion { get; }
	}
}