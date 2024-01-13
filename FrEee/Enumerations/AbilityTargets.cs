using FrEee.Utility;
using System;

namespace FrEee.Enumerations
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
		Empire = 0x1,

		Race = 0x2,
		Trait = 0x4,
		Component = 0x8, // includes MountedComponentTemplate and ComponentTemplate
		Facility = 0x10, // includes FacilityTemplate

		// shared types
		Sector = 0x20,

		StarSystem = 0x40,
		Galaxy = 0x80,

		// stellar objects
		[Name("Asteroid")]
		[Name("Asteroids")]
		[CanonicalName("Asteroid Field")]
		AsteroidField = 0x100,

		Planet = 0x200,
		Star = 0x400,
		Storm = 0x800,

		[CanonicalName("Warp Point")]
		WarpPoint = 0x1000,

		// vehicles
		Base = 0x2000,

		Drone = 0x4000,
		Fighter = 0x8000,
		Mine = 0x10000,
		Satellite = 0x20000,
		Ship = 0x40000,
		Troop = 0x80000,
		WeaponPlatform = 0x100000,

		// fleets
		Fleet = 0x200000,

		// compound types
		[CanonicalName("Stellar Object")]
		StellarObject = AsteroidField | Planet | Star | Storm | WarpPoint,

		[CanonicalName("Space Vehicle")]
		SpaceVehicle = Base | Drone | Fighter | Mine | Satellite | Ship,

		[CanonicalName("Ground Vehicle")]
		GroundVehicle = WeaponPlatform | Troop,

		Unit = Drone | Fighter | Mine | Satellite | Troop | WeaponPlatform,
		Vehicle = Ship | Base | Unit,

		[CanonicalName("Space Object")]
		SpaceObject = StellarObject | SpaceVehicle,

		Part = Component | Facility,

		// invalid
		Invalid = 0x2000000,

		// everything!
		All = int.MaxValue & ~Invalid,
	}
}
