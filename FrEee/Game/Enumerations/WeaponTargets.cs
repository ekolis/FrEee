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
		Ships = Ship,
		Base = 0x2,
		Bases = Base,
		Fighter = 0x4,
		Fighters = Fighter,
		Ftr = Fighter,
		Satellite = 0x8,
		Satellites = Satellite,
		Sat = Satellite,
		Drone = 0x40,
		Drones = Drone,
		Planet = 0x100,
		Planets = Planet,
		Seeker = 0x200,
		Seekers = Seeker,
		Invalid = 0x400,
	}
}
