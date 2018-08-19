﻿using System;
using System.Drawing;

namespace FrEee.Game.Objects.Combat
{
    [Serializable]
    public class BeamWeaponDisplayEffect : WeaponDisplayEffect
    {
        #region Public Constructors

        public BeamWeaponDisplayEffect(string name)
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
            get { return "Beams"; }
        }

        public override Point ShipsetSpriteOffset
        {
            get { return new Point(); }
        }

        public override string ShipsetSpriteSheetName
        {
            get { return "Beams"; }
        }

        public override Size SpriteSize
        {
            get { return new Size(20, 20); }
        }

        #endregion Public Properties
    }
}
