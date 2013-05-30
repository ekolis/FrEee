using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Enumerations
{
	/// <summary>
	/// Used to limit what a weapon can fire at.
	/// </summary>
	[Flags]
	public enum WeaponTargets
	{
		None = 0x0,
		Ship = 0x1,
		Base = 0x2,
		Fighter = 0x4,
		Satellite = 0x8,
		Mine = 0x10,
		Troop = 0x20,
		Drone = 0x40,
		WeaponPlatform = 0x80,
		Planet = 0x100,
		Seeker = 0x200
	}
}
