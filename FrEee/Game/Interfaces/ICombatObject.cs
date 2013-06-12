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
	/// A space object that fire weapons and be targeted by weapons.
	/// </summary>
	public interface ICombatObject : IPictorial, IOwnable, IDamageable
	{
		/// <summary>
		/// Can this object fire on another space object?
		/// </summary>
		/// <param name="target"></param>
		/// <returns>true if the target is an enemy and this space object has weapons capable of targeting it</returns>
		bool CanTarget(ICombatObject target);

		/// <summary>
		/// What type of object is this for weapon targeting purposes?
		/// </summary>
		WeaponTargets WeaponTargetType { get; }

		/// <summary>
		/// Any undamaged weapons this space object is armed with.
		/// </summary>
		IEnumerable<Component> Weapons { get; }
	}
}