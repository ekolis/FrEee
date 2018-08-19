using System;
using System.Drawing;

namespace FrEee.Game.Objects.Combat
{
    [Serializable]
    public class SeekerWeaponDisplayEffect : WeaponDisplayEffect
    {
        #region Public Constructors

        public SeekerWeaponDisplayEffect(string name)
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
            get { return "Seekers"; }
        }

        public override Point ShipsetSpriteOffset
        {
            get { return new Point(40, 0); }
        }

        public override string ShipsetSpriteSheetName
        {
            get { return "Main"; }
        }

        public override Size SpriteSize
        {
            get { return new Size(20, 20); }
        }

        #endregion Public Properties
    }
}
