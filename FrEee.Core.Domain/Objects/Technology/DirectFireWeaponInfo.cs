using FrEee.Modding;
using FrEee.Processes.Combat;
using System;

namespace FrEee.Objects.Technology;

[Serializable]
public class DirectFireWeaponInfo : WeaponInfo
{
	/// <summary>
	/// Accuracy bonus or penalty for this weapon.
	/// </summary>
	public Formula<int> AccuracyModifier { get; set; }

	public override WeaponTypes WeaponType
	{
		get
		{
			if (IsPointDefense)
				return WeaponTypes.DirectFirePointDefense;
			else
				return WeaponTypes.DirectFire;
		}
	}
}