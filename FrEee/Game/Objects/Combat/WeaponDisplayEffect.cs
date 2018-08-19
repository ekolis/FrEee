﻿using FrEee.Game.Interfaces;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace FrEee.Game.Objects.Combat
{
    /// <summary>
    /// The display effect to use for a weapon in combat.
    /// </summary>
    [Serializable]
    public abstract class WeaponDisplayEffect : IPictorial
    {
        #region Protected Constructors

        protected WeaponDisplayEffect(string name)
        {
            Name = name;
        }

        #endregion Protected Constructors

        #region Public Properties

        /// <summary>
        /// The pixel offset to the first sprite in the global sprite sheet.
        /// </summary>
        public abstract Point GlobalSpriteOffset { get; }

        public Image GlobalSpriteSheet
        {
            get
            {
                return Pictures.GetModImage(Path.Combine("Pictures", "Combat", GlobalSpriteSheetName));
            }
        }

        /// <summary>
        /// The name of the sprite sheet to use.
        /// </summary>
        public abstract string GlobalSpriteSheetName { get; }

        public Image Icon
        {
            get { return GetIcon(null); }
        }

        public IEnumerable<string> IconPaths
        {
            get
            {
                yield return Path.Combine("Races", "Default", Name);
                yield return Path.Combine("Races", "Default", "Default" + "_" + Name);
                yield return Path.Combine("Combat", Name);
            }
        }

        /// <summary>
        /// The name or index of the effect to use.
        /// </summary>
        public string Name { get; set; }

        public Image Portrait
        {
            get { return Icon; }
        }

        public IEnumerable<string> PortraitPaths
        {
            get
            {
                return IconPaths;
            }
        }

        /// <summary>
        /// The pixel offset to the first sprite in the shipset-specific sprite sheet.
        /// </summary>
        public abstract Point ShipsetSpriteOffset { get; }

        /// <summary>
        /// The name of the sprite sheet to use.
        /// </summary>
        public abstract string ShipsetSpriteSheetName { get; }

        /// <summary>
        /// The size of each sprite, in pixels.
        /// </summary>
        public abstract Size SpriteSize { get; }

        #endregion Public Properties

        #region Public Methods

        public Image GetIcon(string shipset)
        {
            // see if we have a positive number to use a sprite sheet
            int index = 0;
            int.TryParse(Name, out index);

            if (index > 0)
            {
                // use sprite sheets
                var shipsetSpriteSheet = LoadShipsetSpriteSheet(shipset);
                Image spriteSheet;
                Point offset;
                if (shipsetSpriteSheet != null)
                {
                    // crop shipset sprite sheet
                    spriteSheet = shipsetSpriteSheet;
                    offset = ShipsetSpriteOffset;
                }
                else if (GlobalSpriteSheet != null)
                {
                    // crop global sprite sheet
                    spriteSheet = GlobalSpriteSheet;
                    offset = GlobalSpriteOffset;
                }
                else
                {
                    // no sprite sheets found
                    return Pictures.GetModImage(
                        Path.Combine("Pictures", "Races", shipset, Name),
                        Path.Combine("Pictures", "Races", shipset, shipset + "_" + Name),
                        Path.Combine("Pictures", "Combat", Name));
                }

                // make sprite
                var spritesAcross = (spriteSheet.Width - offset.X) / SpriteSize.Width;
                var num = index - 1;
                var row = num / spritesAcross;
                var col = num % spritesAcross;
                var pos = new Point(offset.X + SpriteSize.Width * col, offset.Y + SpriteSize.Height * row);
                return spriteSheet.Crop(pos, SpriteSize);
            }
            else
            {
                // use individual sprites
                return Pictures.GetModImage(
                    Path.Combine("Pictures", "Races", shipset, Name),
                    Path.Combine("Pictures", "Races", shipset, shipset + "_" + Name),
                    Path.Combine("Pictures", "Combat", Name));
            }
        }

        public Image LoadShipsetSpriteSheet(string shipset)
        {
            return Pictures.GetModImage(Path.Combine("Pictures", "Races", shipset, ShipsetSpriteSheetName));
        }

        #endregion Public Methods
    }
}
