using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Game.Interfaces;

namespace FrEee.Utility
{
	/// <summary>
	/// Utility methods for handling pictures.
	/// </summary>
	public static class Pictures
	{
		/// <summary>
		/// Picture cache for raw images on disk.
		/// </summary>
		private static IDictionary<string, Image> fileCache = new Dictionary<string, Image>();

		/// <summary>
		/// Picture cache for objects.
		/// </summary>
		private static IDictionary<object, Image> objectPortraits = new Dictionary<object, Image>();

		/// <summary>
		/// Generic pictures to use for space objects with missing pictures.
		/// </summary>
		private static IDictionary<Type, Image> genericPictures = new Dictionary<Type, Image>();

		static Pictures()
		{
			// Set up the generic images
			Image img;
			Graphics g;

			// star
			img = new Bitmap(128, 128);
			g = Graphics.FromImage(img);
			g.FillEllipse(new SolidBrush(Color.Yellow), 10, 10, 108, 108);
			g.DrawEllipse(new Pen(Color.White, 3), 10, 10, 108, 108);
			genericPictures.Add(typeof(Star), img);

			// planet
			img = new Bitmap(128, 128);
			g = Graphics.FromImage(img);
			g.FillEllipse(new SolidBrush(Color.Blue), 10, 10, 108, 108);
			g.DrawEllipse(new Pen(Color.White, 3), 10, 10, 108, 108);
			genericPictures.Add(typeof(Planet), img);

			// asteroid field
			img = new Bitmap(128, 128);
			g = Graphics.FromImage(img);
			g.FillEllipse(new SolidBrush(Color.Gray), 10, 10, 25, 25);
			g.FillEllipse(new SolidBrush(Color.Gray), 45, 15, 25, 25);
			g.FillEllipse(new SolidBrush(Color.Gray), 75, 90, 25, 25);
			g.FillEllipse(new SolidBrush(Color.Gray), 30, 70, 25, 25);
			g.FillEllipse(new SolidBrush(Color.Gray), 15, 60, 25, 25);
			genericPictures.Add(typeof(AsteroidField), img);

			// storm
			img = new Bitmap(128, 128);
			g = Graphics.FromImage(img);
			g.Clear(Color.Purple);
			genericPictures.Add(typeof(Storm), img);

			// warp point
			img = new Bitmap(128, 128);
			g = Graphics.FromImage(img);
			g.DrawEllipse(new Pen(Color.Blue, 3), 10, 10, 108, 108);
			g.DrawEllipse(new Pen(Color.Blue, 3), 20, 20, 88, 88);
			g.DrawEllipse(new Pen(Color.Blue, 3), 30, 20, 68, 68);
			genericPictures.Add(typeof(WarpPoint), img);

			// facility
			img = new Bitmap(128, 128);
			g = Graphics.FromImage(img);
			g.FillRectangle(new SolidBrush(Color.Silver), 10, 10, 108, 108);
			genericPictures.Add(typeof(Facility), img);
		}

		/// <summary>
		/// Gets the icon image for a stellar object.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static Image GetIcon(StellarObject sobj)
		{
			var portrait = GetPortrait(sobj);
			if (portrait == null)
				return null;
			return portrait.GetThumbnailImage(32, 32, () => false, IntPtr.Zero);
		}

		/// <summary>
		/// Gets the portrait image for a stellar object.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static Image GetPortrait(StellarObject sobj)
		{
			if (!objectPortraits.ContainsKey(sobj))
			{
				// TODO - check mod folders for images too
				var portrait =
					GetCachedImage(Path.Combine("Pictures", "Planets", sobj.PictureName + ".png")) ??
					GetCachedImage(Path.Combine("Pictures", "Planets", sobj.PictureName + ".bmp")) ??
					GetGenericImage(sobj.GetType());

				// clone the image so we don't mess up the original cached version
				portrait = (Image)portrait.Clone();

				if (sobj is Planet)
				{
					var planet = (Planet)sobj;
					if (planet.Colony != null)
					{
						// draw population bar
						var g = Graphics.FromImage(portrait);
						var rect = new Rectangle(portrait.Width / 2, portrait.Width / 16, portrait.Width / 2, portrait.Width / 4);
						var pen = new Pen(planet.Colony.Owner.Color, portrait.Width / 16);
						g.DrawRectangle(pen, rect);
						// TODO - fill population bar only partway if planet is not full, once colonies actually have population
						var brush = new SolidBrush(planet.Colony.Owner.Color);
						rect.Width /= 3;
						rect.Inflate(-portrait.Width / 32, -portrait.Height / 16);
						rect.X += portrait.Width / 32;
						g.FillRectangle(brush, rect);
						rect.X += portrait.Width / 6 - portrait.Width / 32;
						g.FillRectangle(brush, rect);
						rect.X += portrait.Width / 6 - portrait.Width / 32;
						g.FillRectangle(brush, rect);
					}
				}

				objectPortraits.Add(sobj, portrait);
			}

			return objectPortraits[sobj];
		}

		/// <summary>
		/// Gets the icon image for a facility.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static Image GetIcon(Facility f)
		{
			var portrait = GetPortrait(f);
			if (portrait == null)
				return null;
			return portrait.GetThumbnailImage(32, 32, () => false, IntPtr.Zero);
		}

		/// <summary>
		/// Gets the portrait image for a facility.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static Image GetPortrait(Facility f)
		{
			// TODO - check mod folders for images too
			return
				GetCachedImage(Path.Combine("Pictures", "Facilities", f.PictureName + ".png")) ??
				GetCachedImage(Path.Combine("Pictures", "Facilities", f.PictureName + ".bmp")) ??
				GetGenericImage(f.GetType());
		}

		public static Image GetCachedImage(string path)
		{
			if (string.IsNullOrEmpty(Path.GetExtension(path)))
			{
				// check PNG, then BMP, if no extension specified
				return GetCachedImage(path + ".png") ?? GetCachedImage(path + ".bmp");
			}

			if (!fileCache.ContainsKey(path))
			{
				try
				{
					fileCache[path] = Image.FromFile(path);
				}
				catch
				{
					// TODO - log missing images
					fileCache[path] = null;
				}
			}
			return fileCache[path];
		}

		private static Image GetGenericImage(Type type)
		{
			if (genericPictures.ContainsKey(type))
				return genericPictures[type];
			return null;
		}
	}
}
