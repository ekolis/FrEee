using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Enumerations
{
	/// <summary>
	/// Specifies what object types abilities can stack to.
	/// </summary>
	[Flags]
	public enum AbilityTargets
	{
		// none
		None = 0,

		// basic types
		Colony = 0x1,
		Empire = 0x2,
		Race = 0x4,
		Trait = 0x8,
		Component = 0x10, // includes MountedComponentTemplate and ComponentTemplate
		Facility = 0x20, // includes FacilityTemplate

		// shared types
		Sector = 0x40,
		StarSystem = 0x80,
		Galaxy = 0x100,

		// stellar objects
		AsteroidField = 0x200,
		Planet = 0x400,
		Star = 0x800,
		Storm = 0x1000,
		WarpPoint = 0x2000,

		// vehicles
		Base = 0x4000,
		Drone = 0x8000,
		Fighter = 0x10000,
		Mine = 0x20000,
		Satellite = 0x40000,
		Ship = 0x80000,
		Troop = 0x100000,
		WeaponPlatform = 0x200000,

		// fleets
		Fleet = 0x400000,

		// compound types
		StellarObject = AsteroidField | Planet | Star | Storm | WarpPoint,
		SpaceVehicle = Base | Drone | Fighter | Mine | Satellite | Ship,
		Unit = Drone | Fighter | Mine | Satellite | Troop | WeaponPlatform,
		Vehicle = Ship | Base | Unit,
		SpaceObject = StellarObject | SpaceVehicle,

		// invalid
		Invalid = 0x2000000,

		// everything!
		All = int.MaxValue & ~Invalid,
	}
}
