using System;
using FrEee.Utility;

namespace FrEee.Enumerations
{
	/// <summary>
	/// Vehicle types. Used for restricting component placement.
	/// </summary>
	[Flags]
	public enum VehicleTypes
	{
		None = 0x0,

		[Name("Ships")]
		Ship = 0x1,

		[Name("Bases")]
		Base = 0x2,

		[Name("Fighters")]
		[Name("Ftr")]
		Fighter = 0x4,

		[Name("Satellites")]
		[Name("Sat")]
		Satellite = 0x8,

		[Name("Mines")]
		Mine = 0x10,

		[Name("Troops")]
		[Name("Troop")]
		[Name("Trp")]
		Troop = 0x20,

		[Name("Drones")]
		Drone = 0x40,

		[Name("WeapPlatform")]
		[Name("WeapPlatforms")]
		[Name("WeapPlat")]
		[Name("WeapPlats")]
		[Name("WeaponPlatforms")]
		[CanonicalName("Weapon Platform")]
		[Name("Weapon Platforms")]
		WeaponPlatform = 0x80,

		[Name("Any")]
		All = Ship | Base | Fighter | Satellite | Mine | Troop | Drone | WeaponPlatform,

		Invalid = 0x1000,
	}
}