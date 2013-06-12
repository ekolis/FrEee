using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Technology
{
	[Serializable]
	public class WarheadWeaponInfo : WeaponInfo
	{
		public override Enumerations.WeaponTypes WeaponType
		{
			get
			{
				if (IsPointDefense)
					return Enumerations.WeaponTypes.WarheadPointDefense;
				else
					return Enumerations.WeaponTypes.Warhead;
			}
		}
	}
}
