using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Combat
{
	[Serializable]
	public class SeekerWeaponDisplayEffect : WeaponDisplayEffect
	{
		public SeekerWeaponDisplayEffect(string name)
			: base(name, "Main", new Point(), "Seekers", new Point(40, 0), new Size(20, 20))
		{

		}
	}
}
