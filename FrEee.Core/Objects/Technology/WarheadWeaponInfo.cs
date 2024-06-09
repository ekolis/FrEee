using System;
using FrEee.Processes.Combat;

namespace FrEee.Objects.Technology;

[Serializable]
public class WarheadWeaponInfo : WeaponInfo
{
	public override WeaponTypes WeaponType
	{
		get
		{
			if (IsPointDefense)
				return WeaponTypes.WarheadPointDefense;
			else
				return WeaponTypes.Warhead;
		}
	}
}