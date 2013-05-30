using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Technology
{
	/// <summary>
	/// Information about a weapon component.
	/// </summary>
	[Serializable]
	public abstract class WeaponInfo
	{
		/// <summary>
		/// What can this weapon target?
		/// </summary>
		public WeaponTargets Targets { get; set; }

		/// <summary>
		/// If this is not null, it will be displayed in place of a simple list of things the weapon can target.
		/// </summary>
		public string TargetDescription { get; set; }

		/// <summary>
		/// Damage, indexed by range. The first entry (index 0) is used for "infinite range" seekers.
		/// </summary>
		public int[] Damage { get; set; }

		/// <summary>
		/// The type of damage inflicted by this weapon.
		/// </summary>
		public DamageType DamageType { get; set; }

		/// <summary>
		/// The number of turns after firing that the weapon must wait to fire again. (1 means the weapon can fire every turn.)
		/// </summary>
		public int ReloadRate { get; set; }

		/// <summary>
		/// The display effect used for this weapons.
		/// </summary>
		public WeaponDisplayEffect DisplayEffect { get; set; }

		/// <summary>
		/// Does this weapon automatically fire at targets that come into range?
		/// </summary>
		public bool IsPointDefense { get; set; }

		/// <summary>
		/// The sound file to play when this weapon fires.
		/// </summary>
		public string Sound { get; set; }

		/// <summary>
		/// Not sure what this is for, maybe the AI?
		/// </summary>
		public string Family { get; set; }
	}
}
