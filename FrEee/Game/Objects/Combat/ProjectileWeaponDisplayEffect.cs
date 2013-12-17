using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Combat
{
	[Serializable]
	public class ProjectileWeaponDisplayEffect : WeaponDisplayEffect
	{
		public ProjectileWeaponDisplayEffect(string name)
			: base(name, "Torps", new Point(), "Torps", new Point(), new Size(20, 20))
		{

		}
	}
}
