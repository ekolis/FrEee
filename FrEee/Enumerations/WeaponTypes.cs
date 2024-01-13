using System;
using FrEee.Utility;

namespace FrEee.Enumerations
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
		[CanonicalName("Not a Weapon")]
		[Name("Not A Weapon")]
		[Name("None")]
		NotAWeapon = 0x1,

		/// <summary>
		/// Direct fire weapon.
		/// </summary>
		[CanonicalName("Direct Fire")]
		[Name("Direct-Fire")]
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
		[CanonicalName("Direct Fire Point Defense")]
		[Name("Direct-Fire Point-Defense")]
		[Name("Point-Defense")]
		[Name("Point Defense")]
		DirectFirePointDefense = 0x10,

		/// <summary>
		/// Weapon which fires automatically at incoming targets like a seeking weapon.
		/// </summary>
		[CanonicalName("Seeking Point Defense")]
		[Name("Seeking Point-Defense")]
		SeekingPointDefense = 0x20,

		/// <summary>
		/// Explodes when ramming an enemy or when being rammed.
		/// </summary>
		[CanonicalName("Warhead Point Defense")]
		[Name("Warhead Point-Defense")]
		WarheadPointDefense = 0x40,

		/// <summary>
		/// All types of weapons. Not nonweapons.
		/// </summary>
		[Name("Any")]
		All = DirectFire | Seeking | DirectFirePointDefense | SeekingPointDefense | WarheadPointDefense,

		/// <summary>
		/// Any component, including nonweapons.
		/// </summary>
		AnyComponent = All | NotAWeapon,
	}
}