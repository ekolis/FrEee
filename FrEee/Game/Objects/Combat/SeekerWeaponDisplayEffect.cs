using System;
using System.Drawing;

#nullable enable

namespace FrEee.Game.Objects.Combat
{
	[Serializable]
	public class SeekerWeaponDisplayEffect : WeaponDisplayEffect
	{
		public SeekerWeaponDisplayEffect(string name)
			: base(name)
		{
		}

		public override Point GlobalSpriteOffset => new Point();

		public override string GlobalSpriteSheetName => "Seekers";

		public override Point ShipsetSpriteOffset => new Point(40, 0);

		public override string ShipsetSpriteSheetName => "Main";

		public override Size SpriteSize => new Size(20, 20);
	}
}
