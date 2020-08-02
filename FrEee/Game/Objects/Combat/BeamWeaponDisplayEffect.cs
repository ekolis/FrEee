using System;
using System.Drawing;

#nullable enable

namespace FrEee.Game.Objects.Combat
{
	[Serializable]
	public class BeamWeaponDisplayEffect : WeaponDisplayEffect
	{
		public BeamWeaponDisplayEffect(string name)
			: base(name)
		{
		}

		public override Point GlobalSpriteOffset => new Point();

		public override string GlobalSpriteSheetName => "Beams";

		public override Point ShipsetSpriteOffset => new Point();

		public override string ShipsetSpriteSheetName => "Beams";

		public override Size SpriteSize => new Size(20, 20);
	}
}
