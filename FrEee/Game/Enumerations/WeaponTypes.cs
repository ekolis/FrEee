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
		/// Not a weapon.
		/// </summary>
		None = 0x0,
		/// <summary>
		/// Direct fire weapon.
		/// </summary>
		Direct = 0x1,
		/// <summary>
		/// Seeking weapon.
		/// </summary>
		Seeking = 0x2,
		/// <summary>
		/// Explodes when ramming an enemy.
		/// </summary>
		Warhead = 0x4,
		/// <summary>
		/// Weapon which fires automatically at incoming targets like a direct fire weapon.
		/// </summary>
		DirectPointDefense = 0x8,
		/// <summary>
		/// Weapon which fires automatically at incoming targets like a seeking weapon.
		/// </summary>
		SeekingPointDefense = 0x10,
		/// <summary>
		/// Explodes when ramming an enemy or when being rammed.
		/// </summary>
		WarheadPointDefense = 0x20,
		/// <summary>
		/// All types of weapons.
		/// </summary>
		All = Direct | Seeking | DirectPointDefense | SeekingPointDefense | WarheadPointDefense,
	}
}
