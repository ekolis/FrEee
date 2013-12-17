using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Combat
{
	[Serializable]
	public class BeamWeaponDisplayEffect : WeaponDisplayEffect
	{
		public BeamWeaponDisplayEffect(string name)
			: base(name, "Beams", new Point(), "Beams", new Point(), new Size(20, 20))
		{

		}
	}
}
