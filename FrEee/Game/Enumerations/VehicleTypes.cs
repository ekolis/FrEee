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
		Fighter = 0x4,
		Satellite = 0x8,
		Mine = 0x10,
		Troop = 0x20,
		Drone = 0x40,
		WeaponPlatform = 0x80
	}
}
