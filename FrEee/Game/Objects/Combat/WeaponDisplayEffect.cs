using FrEee.Game.Interfaces;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Combat
{
	/// <summary>
	/// The display effect to use for a weapon in combat.
	/// </summary>
	[Serializable]
	public abstract class WeaponDisplayEffect : IPictorial
	{
		protected WeaponDisplayEffect(string name, string shipsetSpriteSheetName, Point shipsetSpriteOffset, string globalSpriteSheetName, Point globalSpriteOffset, Size spriteSize)
		{
			Name = name;
			ShipsetSpriteSheetName = shipsetSpriteSheetName;
			ShipsetSpriteOffset = shipsetSpriteOffset;
			GlobalSpriteSheetName = globalSpriteSheetName;
			GlobalSpriteOffset = globalSpriteOffset;
			SpriteSize = spriteSize;
		}

		/// <summary>
		/// The name of the sprite sheet to use.
		/// </summary>
		public string ShipsetSpriteSheetName { get; private set; }

		/// <summary>
		/// The pixel offset to the first sprite in the shipset-specific sprite sheet.
		/// </summary>
		public Point ShipsetSpriteOffset { get; private set; }

		/// <summary>
		/// The name of the sprite sheet to use.
		/// </summary>
		public string GlobalSpriteSheetName { get; private set; }

		/// <summary>
		/// The pixel offset to the first sprite in the global sprite sheet.
		/// </summary>
		public Point GlobalSpriteOffset { get; private set; }

		/// <summary>
		/// The size of each sprite, in pixels.
		/// </summary>
		public Size SpriteSize { get; private set; }

		/// <summary>
		/// The name or index of the effect to use.
		/// </summary>
		public string Name { get; set; }

		public Image GlobalSpriteSheet
		{
			get
			{
				return Pictures.GetModImage(Path.Combine("Pictures", "Combat", GlobalSpriteSheetName));
			}
		}

		public Image LoadShipsetSpriteSheet(string shipset)
		{
			return Pictures.GetModImage(Path.Combine("Pictures", "Races", shipset, ShipsetSpriteSheetName));
		}

		public Image Icon
		{
			get { return GetIcon(null); }
		}

		public Image Portrait
		{
			get { return Icon; }
		}

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
	}
}
