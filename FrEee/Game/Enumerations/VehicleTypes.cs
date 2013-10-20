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
		Base = 0x2,
		Ftr = 0x4,
		Fighter = Ftr,
		Sat = 0x8,
		Satellite = Sat,
		Mine = 0x10,
		Trp = 0x20,
		Troop = Trp,
		Drone = 0x40,
		WeapPlatform = 0x80,
		WeapPlat = WeapPlatform,
		WeaponPlatform = WeapPlatform,
		All = Ship | Base | Fighter | Satellite | Mine | Troop | Drone | WeaponPlatform,
		Invalid = 0x1000,
	}
}
