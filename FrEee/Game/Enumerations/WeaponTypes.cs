using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Enumerations
{
	/// <summary>
	/// Types of weapons.
	/// </summary>
	[Flags]
	public enum WeaponTypes
	{
		/// <summary>
		/// No weapon type specified.
		/// </summary>
		None = 0x0,
		/// <summary>
		/// Not a weapon.
		/// </summary>
		NotAWeapon = 0x1,
		/// <summary>
		/// Direct fire weapon.
		/// </summary>
		DirectFire = 0x2,
		/// <summary>
		/// Seeking weapon.
		/// </summary>
		Seeking = 0x4,
		/// <summary>
		/// Explodes when ramming an enemy.
		/// </summary>
		Warhead = 0x8,
		/// <summary>
		/// Weapon which fires automatically at incoming targets like a direct fire weapon.
		/// </summary>
		DirectFirePointDefense = 0x10,
		/// <summary>
		/// Weapon which fires automatically at incoming targets like a seeking weapon.
		/// </summary>
		SeekingPointDefense = 0x20,
		/// <summary>
		/// Explodes when ramming an enemy or when being rammed.
		/// </summary>
		WarheadPointDefense = 0x40,
		/// <summary>
		/// All types of weapons. Not nonweapons.
		/// </summary>
		All = DirectFire | Seeking | DirectFirePointDefense | SeekingPointDefense | WarheadPointDefense,
		/// <summary>
		/// Any component, including nonweapons.
		/// </summary>
		AnyComponent = All | NotAWeapon,
	}
}
