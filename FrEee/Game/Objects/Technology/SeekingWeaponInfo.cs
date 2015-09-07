using FrEee.Modding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Modding.Interfaces;

namespace FrEee.Game.Objects.Technology
{
	[Serializable]
	public class SeekingWeaponInfo : WeaponInfo
	{
		/// <summary>
		/// The speed at which the seeker travels.
		/// </summary>
		public Formula<int> SeekerSpeed { get; set; }

		/// <summary>
		/// The durability of the seeker.
		/// </summary>
		public Formula<int> SeekerDurability { get; set; }

		public override Enumerations.WeaponTypes WeaponType
		{
			get
			{
				if (IsPointDefense)
					return Enumerations.WeaponTypes.SeekingPointDefense;
				else
					return Enumerations.WeaponTypes.Seeking;
			}
		}
	}
}
