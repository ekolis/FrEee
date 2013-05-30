using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Technology
{
	[Serializable]
	public class SeekingWeaponInfo : WeaponInfo
	{
		/// <summary>
		/// The speed at which the seeker travels.
		/// </summary>
		public int SeekerSpeed { get; set; }

		/// <summary>
		/// The durability of the seeker.
		/// </summary>
		public int SeekerDurability { get; set; }
	}
}
