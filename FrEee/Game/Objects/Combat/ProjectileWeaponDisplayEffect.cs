using System;
using System.Drawing;

#nullable enable

namespace FrEee.Game.Objects.Combat
{
	[Serializable]
	public class ProjectileWeaponDisplayEffect : WeaponDisplayEffect
	{
		public ProjectileWeaponDisplayEffect(string name)
			: base(name)
		{
		}

		public override Point GlobalSpriteOffset => new Point();

		public override string GlobalSpriteSheetName => "Torps";

		public override Point ShipsetSpriteOffset => new Point();

		public override string ShipsetSpriteSheetName => "Torps";

		public override Size SpriteSize => new Size(20, 20);
	}
}
