using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Interfaces;
using FrEee.Modding.Templates;
using FrEee.Modding;
using FrEee.Utility.Extensions;
using System.Linq;

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
			genericPictures.Add(typeof(FacilityTemplate), img);

			// component
			img = new Bitmap(128, 128);
			g = Graphics.FromImage(img);
			g.FillEllipse(new SolidBrush(Color.Silver), 10, 10, 108, 108);
			genericPictures.Add(typeof(ComponentTemplate), img);

			// hull
			img = new Bitmap(128, 128);
			g = Graphics.FromImage(img);
			g.FillEllipse(new SolidBrush(Color.Silver), 40, 0, 68, 68);
			g.FillRectangle(new SolidBrush(Color.Silver), 50, 50, 10, 50);
			g.FillRectangle(new SolidBrush(Color.Silver), 88, 50, 10, 50);
			genericPictures.Add(typeof(IHull<IVehicle>), img);
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
			var img = portrait.GetThumbnailImage(32, 32, () => false, IntPtr.Zero);

			if (sobj is Planet)
				((Planet)sobj).DrawPopulationBars(img);

			return img;
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
				if (sobj.PictureName == null)
					return GetGenericImage(sobj.GetType());

				Image portrait;
				if (Mod.Current.RootPath != null)
				{
					portrait =
						GetCachedImage(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Planets", sobj.PictureName)) ??
						GetCachedImage(Path.Combine("Pictures", "Planets", sobj.PictureName)) ??
						GetGenericImage(sobj.GetType());
				}
				else
				{
					// stock mod has no entry in Mods folder, and looking for a null path crashes Path.Combine
					portrait =
						GetCachedImage(Path.Combine("Pictures", "Planets", sobj.PictureName)) ??
						GetGenericImage(sobj.GetType());
				}

				// clone the image so we don't mess up the original cached version
				portrait = (Image)portrait.Clone();

				objectPortraits.Add(sobj, portrait);
			}

			return objectPortraits[sobj];
		}

		/// <summary>
		/// Gets the icon image for a facility.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static Image GetIcon(FacilityTemplate f)
		{
			var portrait = GetPortrait(f);
			if (portrait == null)
				return null;
			// TODO - draw level roman numeral on the icon
			return portrait.GetThumbnailImage(32, 32, () => false, IntPtr.Zero);
		}

		/// <summary>
		/// Gets the portrait image for a facility.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static Image GetPortrait(FacilityTemplate f)
		{
			if (f.PictureName == null)
				return GetGenericImage(f.GetType());
			if (Mod.Current.RootPath != null)
			{
				return
					GetCachedImage(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Facilities", f.PictureName)) ??
					GetCachedImage(Path.Combine("Pictures", "Facilities", f.PictureName)) ??
					GetGenericImage(f.GetType());
			}
			else
			{
				// stock mod has no entry in Mods folder, and looking for a null path crashes Path.Combine
				return
					GetCachedImage(Path.Combine("Pictures", "Facilities", f.PictureName)) ??
					GetGenericImage(f.GetType());
			}
		}

		/// <summary>
		/// Gets the icon image for a component.
		/// </summary>
		public static Image GetIcon(ComponentTemplate c)
		{
			var portrait = GetPortrait(c);
			if (portrait == null)
				return null;
			// TODO - draw level roman numeral on the icon
			return portrait.GetThumbnailImage(32, 32, () => false, IntPtr.Zero);
		}

		/// <summary>
		/// Gets the portrait image for a component.
		/// </summary>
		public static Image GetPortrait(ComponentTemplate c)
		{
			if (c.PictureName == null)
				return GetGenericImage(c.GetType());
			if (Mod.Current.RootPath != null)
			{
				return
					GetCachedImage(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Components", c.PictureName)) ??
					GetCachedImage(Path.Combine("Pictures", "Components", c.PictureName)) ??
					GetGenericImage(c.GetType());
			}
			else
			{
				// stock mod has no entry in Mods folder, and looking for a null path crashes Path.Combine
				return
					GetCachedImage(Path.Combine("Pictures", "Components", c.PictureName)) ??
					GetGenericImage(c.GetType());
			}
		}

		public static Image GetIcon(IHull<IVehicle> hull, string shipsetPath, int size = 32)
		{
			var paths = new List<string>();
			foreach (var s in hull.PictureNames)
			{
				if (Mod.Current.RootPath != null)
				{
					paths.Add(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, "Mini_" + s));
					paths.Add(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, shipsetPath + "_Mini_" + s)); // for SE4 shipset compatibility
				}
				paths.Add(Path.Combine("Pictures", "Races", shipsetPath, "Mini_" + s));
				paths.Add(Path.Combine("Pictures", "Races", shipsetPath, shipsetPath + "_Mini_" + s)); // for SE4 shipset compatibility
			}
			return (GetCachedImage(paths) ?? GetGenericImage(hull.GetType())).Resize(32);
		}

		public static Image GetPortrait(IHull<IVehicle> hull, string shipsetPath)
		{
			if (!hull.PictureNames.Any())
				return GetGenericImage(hull.GetType());
			var paths = new List<string>();
			foreach (var s in hull.PictureNames)
			{
				if (Mod.Current.RootPath != null)
				{
					paths.Add(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, "Portrait_" + s));
					paths.Add(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, shipsetPath + "_Portrait_" + s)); // for SE4 shipset compatibility
				}
				paths.Add(Path.Combine("Pictures", "Races", shipsetPath, "Portrait_" + s));
				paths.Add(Path.Combine("Pictures", "Races", shipsetPath, shipsetPath + "_Portrait_" + s)); // for SE4 shipset compatibility
			}
			return GetCachedImage(paths) ?? GetGenericImage(hull.GetType());
		}

		public static Image GetCachedImage(IEnumerable<string> paths)
		{
			foreach (var path in paths)
			{
				var img = GetCachedImage(path);
				if (img != null)
					return img;
			}
			return null;
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
				if (File.Exists(path))
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
				else
				{
					// TODO - log missing images
					fileCache[path] = null;
				}
			}
			return fileCache[path];
		}

		public static Image GetGenericImage(Type type)
		{
			if (genericPictures.ContainsKey(type))
				return genericPictures[type];
			else
			{
				// check base type and interfaces
				if (type.BaseType != null && genericPictures.ContainsKey(type.BaseType))
					return genericPictures[type.BaseType];

				foreach (var i in type.GetInterfaces())
				{
					if (genericPictures.ContainsKey(i))
						return genericPictures[i];
				}

				// yay recursion
				Image img = null;
				if (type.BaseType != null)
					img = GetGenericImage(type.BaseType);
				if (img != null)
					return img;
				foreach (var i in type.GetInterfaces())
				{
					img = GetGenericImage(i);
					if (img != null)
						return img;
				}
			}
			return null;
		}
	}
}
