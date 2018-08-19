using System;
using System.Drawing;

namespace FrEee.Game.Objects.Combat
{
    [Serializable]
    public class ProjectileWeaponDisplayEffect : WeaponDisplayEffect
    {
        #region Public Constructors

        public ProjectileWeaponDisplayEffect(string name)
            : base(name)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override Point GlobalSpriteOffset
        {
            get { return new Point(); }
        }

        public override string GlobalSpriteSheetName
        {
            get { return "Torps"; }
        }

        public override Point ShipsetSpriteOffset
        {
            get { return new Point(); }
        }

        public override string ShipsetSpriteSheetName
        {
            get { return "Torps"; }
        }

        public override Size SpriteSize
        {
            get { return new Size(20, 20); }
        }

        #endregion Public Properties
    }
}
