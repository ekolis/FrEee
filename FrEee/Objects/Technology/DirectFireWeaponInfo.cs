using FrEee.Modding;
using System;

namespace FrEee.Objects.Technology;

[Serializable]
public class DirectFireWeaponInfo : WeaponInfo
{
	/// <summary>
	/// Accuracy bonus or penalty for this weapon.
	/// </summary>
	public Formula<int> AccuracyModifier { get; set; }

	public override Enumerations.WeaponTypes WeaponType
	{
		get
		{
			if (IsPointDefense)
				return Enumerations.WeaponTypes.DirectFirePointDefense;
			else
				return Enumerations.WeaponTypes.DirectFire;
		}
	}
}