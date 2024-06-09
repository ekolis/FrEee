using FrEee.Modding;
using FrEee.Objects.Combat;
using System;

namespace FrEee.Objects.Technology;

[Serializable]
public class SeekingWeaponInfo : WeaponInfo
{
	/// <summary>
	/// The durability of the seeker.
	/// </summary>
	public Formula<int> SeekerDurability { get; set; }

	/// <summary>
	/// The speed at which the seeker travels.
	/// </summary>
	public Formula<int> SeekerSpeed { get; set; }

	public override WeaponTypes WeaponType
	{
		get
		{
			if (IsPointDefense)
				return WeaponTypes.SeekingPointDefense;
			else
				return WeaponTypes.Seeking;
		}
	}
}