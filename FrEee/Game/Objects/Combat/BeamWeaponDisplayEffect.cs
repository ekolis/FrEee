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
			: base(name)
		{

		}

		public override string ShipsetSpriteSheetName
		{
			get { return "Beams"; }
		}

		public override Point ShipsetSpriteOffset
		{
			get { return new Point(); }
		}

		public override string GlobalSpriteSheetName
		{
			get { return "Beams"; }
		}

		public override Point GlobalSpriteOffset
		{
			get { return new Point(); }
		}

		public override Size SpriteSize
		{
			get { return new Size(20, 20); }
		}
	}
}
