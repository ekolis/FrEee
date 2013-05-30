using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Technology
{
	public class SeekingWeaponInfo : WeaponInfo
	{
		/// <summary>
		/// The speed at which the seeker travels.
		/// </summary>
		int SeekerSpeed { get; set; }

		/// <summary>
		/// The durability of the seeker.
		/// </summary>
		int SeekerDurability { get; set; }
	}
}
