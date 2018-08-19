﻿using FrEee.Utility;
using System;

namespace FrEee.Game.Enumerations
{
    /// <summary>
    /// Used to limit what a weapon can fire at.
    /// </summary>
    [Flags]
    public enum WeaponTargets
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

        [Name("Drones")]
        Drone = 0x40,

        [Name("Planets")]
        Planet = 0x100,

        [Name("Seekers")]
        Seeker = 0x200,

        All = Ship | Base | Fighter | Satellite | Drone | Planet | Seeker,
        Invalid = 0x400,
    }
}
