using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Enumerations
{
	/// <summary>
	/// Vehicle types. Used for restricting component placement.
	/// </summary>
	[Flags]
	public enum VehicleTypes
	{
		None = 0x0,
		Ship = 0x1,
		Ships = Ship,
		Base = 0x2,
		Bases = Base,
		Ftr = 0x4,
		Fighter = Ftr,
		Fighters = Fighter,
		Sat = 0x8,
		Satellite = Sat,
		Satellites = Satellite,
		Mine = 0x10,
		Mines = Mine,
		Trp = 0x20,
		Troop = Trp,
		Troops = Troop,
		Drone = 0x40,
		Drones = Drone,
		WeapPlatform = 0x80,
		WeapPlatforms = WeapPlatform,
		WeapPlat = WeapPlatform,
		WeapPlats = WeapPlat,
		WeaponPlatform = WeapPlatform,
		WeaponPlatforms = WeaponPlatform,
		All = Ship | Base | Fighter | Satellite | Mine | Troop | Drone | WeaponPlatform,
		Invalid = 0x1000,
	}
}
