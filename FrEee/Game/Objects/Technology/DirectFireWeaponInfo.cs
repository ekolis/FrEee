using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Technology
{
	public class DirectFireWeaponInfo : WeaponInfo
	{
		/// <summary>
		/// Accuracy bonus or penalty for this weapon.
		/// </summary>
		public int AccuracyModifier { get; set; }
	}
}
