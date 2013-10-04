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
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Setup;
using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Vehicles;

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

			// TODO - moddable generic pics

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

			// hull/design/vehicle
			img = new Bitmap(128, 128);
			g = Graphics.FromImage(img);
			var top = new Point(64, 0);
			var bottomLeft = new Point(0, 128);
			var bottomRight = new Point(128, 128);
			var pts = new Point[] { top, bottomLeft, bottomRight };
			g.FillPolygon(Brushes.Gray, pts);
			genericPictures.Add(typeof(IHull), img);
			genericPictures.Add(typeof(IDesign), img);
			genericPictures.Add(typeof(IVehicle), img);

			// fleet
			img = new Bitmap(128, 128);
			g = Graphics.FromImage(img);
			top = new Point(64, 0);
			bottomLeft = new Point(32, 64);
			bottomRight = new Point(96, 64);
			pts = new Point[] { top, bottomLeft, bottomRight };
			g.FillPolygon(Brushes.Gray, pts);
			top = new Point(32, 128);
			bottomLeft = new Point(0, 192);
			bottomRight = new Point(64, 192);
			pts = new Point[] { top, bottomLeft, bottomRight };
			g.FillPolygon(Brushes.Gray, pts);
			top = new Point(96, 128);
			bottomLeft = new Point(64, 192);
			bottomRight = new Point(128, 192);
			pts = new Point[] { top, bottomLeft, bottomRight };
			g.FillPolygon(Brushes.Gray, pts);
			genericPictures.Add(typeof(Fleet), img);

			// TODO - mount, race, empire generic pics
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
			{
				var p = (Planet)sobj;
				if (p.Colony == null)
					p.DrawStatusIcons(img);
				else
					p.DrawPopulationBars(img);
			}

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
				Image portrait;
				double scale = 1d;
				if (sobj.StellarSize == StellarSize.Huge)
					scale = 1d;
				else if (sobj.StellarSize == StellarSize.Large)
					scale = 0.875d;
				else if (sobj.StellarSize == StellarSize.Medium)
					scale = 0.75d;
				else if (sobj.StellarSize == StellarSize.Small)
					scale = 0.625d;
				else if (sobj.StellarSize == StellarSize.Tiny)
					scale = 0.5d;

				if (sobj.PictureName == null)
					return GetGenericImage(sobj.GetType(), scale);

				if (Mod.Current.RootPath != null)
				{
					portrait =
						GetCachedImage(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Planets", sobj.PictureName)) ??
						GetCachedImage(Path.Combine("Pictures", "Planets", sobj.PictureName)) ??
						GetGenericImage(sobj.GetType(), scale);
				}
				else
				{
					// stock mod has no entry in Mods folder, and looking for a null path crashes Path.Combine
					portrait =
						GetCachedImage(Path.Combine("Pictures", "Planets", sobj.PictureName)) ??
						GetGenericImage(sobj.GetType(), scale);
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
			var thumb = portrait.GetThumbnailImage(32, 32, () => false, IntPtr.Zero);
			var g = Graphics.FromImage(thumb);
			var font = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular);
			var brush = Brushes.White;
			var sf = new StringFormat();
			sf.Alignment = StringAlignment.Far;
			sf.LineAlignment = StringAlignment.Far;
			if (f.RomanNumeral != 0)
				g.DrawString(f.RomanNumeral.ToRomanNumeral(), font, brush, new Point(32, 32), sf);
			return thumb;
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
			var thumb = portrait.GetThumbnailImage(32, 32, () => false, IntPtr.Zero);
			var g = Graphics.FromImage(thumb);
			var font = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular);
			var brush = Brushes.White;
			var sf = new StringFormat();
			sf.Alignment = StringAlignment.Far;
			sf.LineAlignment = StringAlignment.Far;
			if (c.RomanNumeral != 0)
				g.DrawString(c.RomanNumeral.ToRomanNumeral(), font, brush, new Point(32, 32), sf);
			return thumb;
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

		/// <summary>
		/// Gets the icon image for a mount.
		/// </summary>
		public static Image GetIcon(Mount m)
		{
			var portrait = GetPortrait(m);
			if (portrait == null)
				return null;
			return portrait.GetThumbnailImage(32, 32, () => false, IntPtr.Zero);
		}

		/// <summary>
		/// Gets the portrait image for a mount.
		/// </summary>
		public static Image GetPortrait(Mount m)
		{
			if (m.PictureName == null)
				return GetGenericImage(m.GetType());
			if (Mod.Current.RootPath != null)
			{
				return
					GetCachedImage(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Mounts", m.PictureName)) ??
					GetCachedImage(Path.Combine("Pictures", "Mounts", m.PictureName)) ??
					GetGenericImage(m.GetType());
			}
			else
			{
				// stock mod has no entry in Mods folder, and looking for a null path crashes Path.Combine
				return
					GetCachedImage(Path.Combine("Pictures", "Mounts", m.PictureName)) ??
					GetGenericImage(m.GetType());
			}
		}

		public static Image GetIcon(IHull<IVehicle> hull, string shipsetPath, int size = 32)
		{
			// allow for practically infinite variation in hull sizes within a confined range of image sizes using a log function
			var maxSize = (double)Mod.Current.Hulls.Max(h => h.Size);
			var ratio = maxSize / hull.Size;
			var scale = 1d / (1d + Math.Log10(ratio));

			if (shipsetPath == null)
				return GetGenericImage(hull.GetType()).Frame(scale).Resize(size);
			if (!hull.PictureNames.Any())
				return GetGenericImage(hull.GetType()).Frame(scale).Resize(size);
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
			return (GetCachedImage(paths) ?? GetGenericImage(hull.GetType(), scale)).Resize(size);
		}

		public static Image GetPortrait(IHull<IVehicle> hull, string shipsetPath)
		{
			if (shipsetPath == null)
				return GetGenericImage(hull.GetType());
			if (!hull.PictureNames.Any())
				return GetGenericImage(hull.GetType());
			var paths = new List<string>();

			// allow for practically infinite variation in hull sizes within a confined range of image sizes using a log function
			var maxSize = (double)Mod.Current.Hulls.Max(h => h.Size);
			var ratio = maxSize / hull.Size;
			var scale = 1d / (1d + Math.Log10(ratio));

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
			return GetCachedImage(paths) ?? GetGenericImage(hull.GetType(), scale);
		}

		/// <summary>
		/// Gets the icon image for a fleet.
		/// </summary>
		/// <param name="fleet"></param>
		/// <param name="shipsetPath"></param>
		/// <returns></returns>
		public static Image GetIcon(Fleet fleet, string shipsetPath, int size = 32)
		{
			if (shipsetPath == null)
				return GetGenericImage(fleet.GetType());
			var paths = new List<string>();

			if (Mod.Current.RootPath != null)
			{
				paths.Add(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, "Mini_Fleet"));
				paths.Add(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, shipsetPath + "_Mini_Fleet")); // for SE4 shipset compatibility
			}
			paths.Add(Path.Combine("Pictures", "Races", shipsetPath, "Mini_Fleet"));
			paths.Add(Path.Combine("Pictures", "Races", shipsetPath, shipsetPath + "_Mini_Fleet")); // for SE4 shipset compatibility

			return (GetCachedImage(paths) ?? GetGenericImage(fleet.GetType())).Resize(size);
		}

		/// <summary>
		/// Gets the portrait image for a fleet.
		/// </summary>
		/// <param name="fleet"></param>
		/// <param name="shipsetPath"></param>
		/// <returns></returns>
		public static Image GetPortrait(Fleet fleet, string shipsetPath)
		{
			if (shipsetPath == null)
				return GetGenericImage(fleet.GetType());
			var paths = new List<string>();
			
			if (Mod.Current.RootPath != null)
			{
				paths.Add(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, "Portrait_Fleet"));
				paths.Add(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, shipsetPath + "_Portrait_Fleet")); // for SE4 shipset compatibility
			}
			paths.Add(Path.Combine("Pictures", "Races", shipsetPath, "Portrait_Fleet"));
			paths.Add(Path.Combine("Pictures", "Races", shipsetPath, shipsetPath + "_Portrait_Fleet")); // for SE4 shipset compatibility

			return GetCachedImage(paths) ?? GetGenericImage(fleet.GetType());
		}

		/// <summary>
		/// Gets the icon image for a resource.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static Image GetIcon(Resource res)
		{
			if (Mod.Current.RootPath != null)
			{
				return
					GetCachedImage(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "UI", "Resources", res.PictureName)) ??
					GetCachedImage(Path.Combine("Pictures", "UI", "Resources", res.PictureName)) ??
					GetSolidColorImage(res.Color, 20);
			}
			else
			{
				// stock mod has no entry in Mods folder, and looking for a null path crashes Path.Combine
				return
					GetCachedImage(Path.Combine("Pictures", "UI", "Resources", res.PictureName)) ??
					GetSolidColorImage(res.Color, 20);
			}
		}

		/// <summary>
		/// Gets the population icon image for a race.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static Image GetIcon(Race race)
		{
			if (Mod.Current.RootPath != null)
			{
				return
					GetCachedImage(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", race.PopulationIconName, "Pop_Portrait")) ??
					GetCachedImage(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", race.PopulationIconName, race.PopulationIconName + "_Pop_Portrait")) ??
					GetCachedImage(Path.Combine("Pictures", "Races", race.PopulationIconName, "Pop_Portrait")) ??
					GetCachedImage(Path.Combine("Pictures", "Races", race.PopulationIconName, race.PopulationIconName + "_Pop_Portrait")) ??
					// fall back on portrait if icon not found
					GetCachedImage(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", race.PopulationIconName, "Race_Portrait")) ??
					GetCachedImage(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", race.PopulationIconName, race.PopulationIconName + "_Race_Portrait")) ??
					GetCachedImage(Path.Combine("Pictures", "Races", race.PopulationIconName, "Race_Portrait")) ??
					GetCachedImage(Path.Combine("Pictures", "Races", race.PopulationIconName, race.PopulationIconName + "_Race_Portrait")) ??
					GetGenericImage(race.GetType());
			}
			else
			{
				// stock mod has no entry in Mods folder, and looking for a null path crashes Path.Combine
				return
					GetCachedImage(Path.Combine("Pictures", "Races", race.PopulationIconName, "Pop_Portrait")) ??
					GetCachedImage(Path.Combine("Pictures", "Races", race.PopulationIconName, race.PopulationIconName + "_Pop_Portrait")) ??
					// fall back on portrait if icon not found
					GetCachedImage(Path.Combine("Pictures", "Races", race.PopulationIconName, "Race_Portrait")) ??
					GetCachedImage(Path.Combine("Pictures", "Races", race.PopulationIconName, race.PopulationIconName + "_Race_Portrait")) ??
					GetGenericImage(race.GetType());
			}
		}

		/// <summary>
		/// Gets the leader portrait image for a race.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static Image GetPortrait(Race race)
		{
			if (race.LeaderPortraitName == null)
				return null;

			if (Mod.Current.RootPath != null)
			{
				return
					GetCachedImage(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", race.LeaderPortraitName, "Race_Portrait")) ??
					GetCachedImage(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", race.LeaderPortraitName, race.LeaderPortraitName + "_Race_Portrait")) ??
					GetCachedImage(Path.Combine("Pictures", "Races", race.LeaderPortraitName, "Race_Portrait")) ??
					GetCachedImage(Path.Combine("Pictures", "Races", race.LeaderPortraitName, race.LeaderPortraitName + "_Race_Portrait")) ??
					// fall back on icon if portrait not found
					GetCachedImage(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", race.LeaderPortraitName, "Pop_Portrait")) ??
					GetCachedImage(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", race.LeaderPortraitName, race.LeaderPortraitName + "_Pop_Portrait")) ??
					GetCachedImage(Path.Combine("Pictures", "Races", race.LeaderPortraitName, "Pop_Portrait")) ??
					GetCachedImage(Path.Combine("Pictures", "Races", race.LeaderPortraitName, race.LeaderPortraitName + "_Pop_Portrait")) ??
					GetGenericImage(race.GetType());
			}
			else
			{
				// stock mod has no entry in Mods folder, and looking for a null path crashes Path.Combine
				return
					GetCachedImage(Path.Combine("Pictures", "Races", race.LeaderPortraitName, "Race_Portrait")) ??
					GetCachedImage(Path.Combine("Pictures", "Races", race.LeaderPortraitName, race.LeaderPortraitName + "_Race_Portrait")) ??
					// fall back on icon if portrait not found
					GetCachedImage(Path.Combine("Pictures", "Races", race.LeaderPortraitName, "Pop_Portrait")) ??
					GetCachedImage(Path.Combine("Pictures", "Races", race.LeaderPortraitName, race.LeaderPortraitName + "_Pop_Portrait")) ??
					GetGenericImage(race.GetType());
			}
		}

		/// <summary>
		/// Gets the insignia icon for an empire.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static Image GetIcon(Empire emp)
		{
			if (Mod.Current.RootPath != null)
			{
				return
					GetCachedImage(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", emp.InsigniaName, "Insignia")) ??
					GetCachedImage(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", emp.InsigniaName, emp.InsigniaName + "_Insignia")) ??
					GetCachedImage(Path.Combine("Pictures", "Races", emp.InsigniaName, "Insignia")) ??
					GetCachedImage(Path.Combine("Pictures", "Races", emp.InsigniaName, emp.InsigniaName + "_Insignia")) ??
					GetSolidColorImage(emp.Color);
			}
			else
			{
				// stock mod has no entry in Mods folder, and looking for a null path crashes Path.Combine
				return
					GetCachedImage(Path.Combine("Pictures", "Races", emp.InsigniaName, "Insignia")) ??
					GetCachedImage(Path.Combine("Pictures", "Races", emp.InsigniaName, emp.InsigniaName + "_Insignia")) ??
					GetSolidColorImage(emp.Color);
			}
		}

		/// <summary>
		/// Gets the leader portrait for an empire.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static Image GetPortrait(Empire emp)
		{
			if (Mod.Current.RootPath != null)
			{
				return
					GetCachedImage(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", emp.LeaderPortraitName, "Race_Portrait")) ??
					GetCachedImage(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", emp.LeaderPortraitName, emp.LeaderPortraitName + "_Race_Portrait")) ??
					GetCachedImage(Path.Combine("Pictures", "Races", emp.LeaderPortraitName, "Race_Portrait")) ??
					GetCachedImage(Path.Combine("Pictures", "Races", emp.LeaderPortraitName, emp.LeaderPortraitName + "_Race_Portrait")) ??
					// fall back on icon if portrait not found
					GetCachedImage(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", emp.LeaderPortraitName, "Pop_Portrait")) ??
					GetCachedImage(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", emp.LeaderPortraitName, emp.LeaderPortraitName + "_Pop_Portrait")) ??
					GetCachedImage(Path.Combine("Pictures", "Races", emp.LeaderPortraitName, "Pop_Portrait")) ??
					GetCachedImage(Path.Combine("Pictures", "Races", emp.LeaderPortraitName, emp.LeaderPortraitName + "_Pop_Portrait")) ??
					GetGenericImage(emp.GetType());
			}
			else
			{
				// stock mod has no entry in Mods folder, and looking for a null path crashes Path.Combine
				return
					GetCachedImage(Path.Combine("Pictures", "Races", emp.LeaderPortraitName, "Race_Portrait")) ??
					GetCachedImage(Path.Combine("Pictures", "Races", emp.LeaderPortraitName, emp.LeaderPortraitName + "_Race_Portrait")) ??
					// fall back on icon if portrait not found
					GetCachedImage(Path.Combine("Pictures", "Races", emp.LeaderPortraitName, "Pop_Portrait")) ??
					GetCachedImage(Path.Combine("Pictures", "Races", emp.LeaderPortraitName, emp.LeaderPortraitName + "_Pop_Portrait")) ??
					GetGenericImage(emp.GetType());
			}
		}

		/// <summary>
		/// Gets the insignia icon for an empire template.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static Image GetIcon(EmpireTemplate emp)
		{
			var insigniaName = emp.InsigniaName ?? emp.PrimaryRace.InsigniaName;
			if (insigniaName == null)
				GetSolidColorImage(emp.Color ?? emp.PrimaryRace.Color);
			if (Mod.Current.RootPath != null)
			{
				return
					GetCachedImage(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", insigniaName, "Insignia")) ??
					GetCachedImage(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", insigniaName, insigniaName + "_Insignia")) ??
					GetCachedImage(Path.Combine("Pictures", "Races", insigniaName, "Insignia")) ??
					GetCachedImage(Path.Combine("Pictures", "Races", insigniaName, insigniaName + "_Insignia")) ??
					GetSolidColorImage(emp.Color ?? emp.PrimaryRace.Color);
			}
			else
			{
				// stock mod has no entry in Mods folder, and looking for a null path crashes Path.Combine
				return
					GetCachedImage(Path.Combine("Pictures", "Races", insigniaName, "Insignia")) ??
					GetCachedImage(Path.Combine("Pictures", "Races", insigniaName, insigniaName + "_Insignia")) ??
					GetSolidColorImage(emp.Color ?? emp.PrimaryRace.Color);
			}
		}

		public static Image GetIcon(Seeker seeker)
		{
			if (Mod.Current.RootPath != null)
			{
				var fx = (SeekerWeaponDisplayEffect)seeker.WeaponInfo.DisplayEffect;
				return
					GetCachedImage(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", seeker.Owner.ShipsetPath, fx.Name)) ??
					GetCachedImage(Path.Combine("Pictures", "Races", seeker.Owner.ShipsetPath)) ??
					GetGenericImage(seeker.GetType());
			}
			else
			{
				// stock mod has no entry in Mods folder, and looking for a null path crashes Path.Combine
				var fx = (SeekerWeaponDisplayEffect)seeker.WeaponInfo.DisplayEffect;
				return
					GetCachedImage(Path.Combine("Pictures", "Races", seeker.Owner.ShipsetPath, fx.Name)) ??
					GetGenericImage(seeker.GetType());
			}
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

		public static Image GetCachedImage(params string[] paths)
		{
			return GetCachedImage((IEnumerable<string>)paths);
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

		public static Image GetModImage(string path)
		{
			if (Mod.Current.RootPath == null)
				return GetCachedImage(path);
			return GetCachedImage(new string[]
			{
				Path.Combine("Mods", Mod.Current.RootPath, path),
				path
			});
		}

		/// <summary>
		/// Gets a generic image for a type of object.
		/// </summary>
		/// <param name="type">The type of object.</param>
		/// <param name="scale">The scale factor. If less than 1, the image will be shrunk and "framed" with blank space.</param>
		/// <returns></returns>
		public static Image GetGenericImage(Type type, double scale = 1d)
		{
			if (genericPictures.ContainsKey(type))
				return genericPictures[type].Frame(scale);
			else
			{
				// check base type and interfaces
				if (type.BaseType != null && genericPictures.ContainsKey(type.BaseType))
					return genericPictures[type.BaseType].Frame(scale);

				foreach (var i in type.GetInterfaces())
				{
					if (genericPictures.ContainsKey(i))
						return genericPictures[i].Frame(scale);
				}

				// yay recursion
				Image img = null;
				if (type.BaseType != null)
					img = GetGenericImage(type.BaseType, scale);
				if (img != null)
					return img;
				foreach (var i in type.GetInterfaces())
				{
					img = GetGenericImage(i, scale);
					if (img != null)
						return img;
				}
			}
			return GetSolidColorImage(Color.Transparent);
		}

		public static Image GetSolidColorImage(Color color, int size = 32)
		{
			var img = new Bitmap(size, size);
			var g = Graphics.FromImage(img);
			g.Clear(color);
			return img;
		}

		/// <summary>
		/// Gets a generic unit image suitable for representing one or more units of a shipset.
		/// Right now this just gets the fighter group image.
		/// </summary>
		/// <param name="shipsetPath"></param>
		/// <returns></returns>
		public static Image GetUnitImage(string shipsetPath)
		{
			if (shipsetPath == null)
				return GetGenericImage(typeof(IUnit));
			var paths = new List<string>();

			if (Mod.Current.RootPath != null)
			{
				paths.Add(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, "Portrait_FighterGroup"));
				paths.Add(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, shipsetPath + "_Portrait_FighterGroup")); // for SE4 shipset compatibility
			}
			paths.Add(Path.Combine("Pictures", "Races", shipsetPath, "Portrait_FighterGroup"));
			paths.Add(Path.Combine("Pictures", "Races", shipsetPath, shipsetPath + "_Portrait_FighterGroup")); // for SE4 shipset compatibility

			return GetCachedImage(paths) ?? GetGenericImage(typeof(IUnit));
		}

		/// <summary>
		/// Lists available leader portraits.
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<string> ListLeaderPortraits()
		{
			var list = new List<string>();
			if (Mod.Current.RootPath != null)
			{
				if (!Directory.Exists(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races")))
					return list;
				foreach (var d in Directory.GetDirectories(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races")))
				{
					var name = Path.GetFileNameWithoutExtension(d);
					if (File.Exists(Path.Combine(d, "Race_Portrait.png")) ||
						File.Exists(Path.Combine(d, "Race_Portrait.bmp")) ||
						File.Exists(Path.Combine(d, "Pop_Portrait.png")) ||
						File.Exists(Path.Combine(d, "Pop_Portrait.bmp")) ||
						File.Exists(Path.Combine(d, name + "_Race_Portrait.png")) ||
						File.Exists(Path.Combine(d, name + "_Race_Portrait.bmp")) ||
						File.Exists(Path.Combine(d, name + "_Pop_Portrait.png")) ||
						File.Exists(Path.Combine(d, name + "_Pop_Portrait.bmp")))
						list.Add(name);
				}
			}
			if (!Directory.Exists(Path.Combine("Pictures", "Races")))
				return list.Distinct();
			foreach (var d in Directory.GetDirectories(Path.Combine("Pictures", "Races")))
			{
				var name = Path.GetFileNameWithoutExtension(d);
				if (File.Exists(Path.Combine(d, "Race_Portrait.png")) ||
					File.Exists(Path.Combine(d, "Race_Portrait.bmp")) ||
					File.Exists(Path.Combine(d, "Pop_Portrait.png")) ||
					File.Exists(Path.Combine(d, "Pop_Portrait.bmp")) ||
					File.Exists(Path.Combine(d, name + "_Race_Portrait.png")) ||
					File.Exists(Path.Combine(d, name + "_Race_Portrait.bmp")) ||
					File.Exists(Path.Combine(d, name + "_Pop_Portrait.png")) ||
					File.Exists(Path.Combine(d, name + "_Pop_Portrait.bmp")))
					list.Add(name);
			}
			return list.Distinct();
		}

		/// <summary>
		/// Lists available population icons.
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<string> ListPopulationIcons()
		{
			// they use the same sources
			return ListLeaderPortraits();
		}

		/// <summary>
		/// Lists available empire insignia.
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<string> ListInsignia()
		{
			var list = new List<string>();
			if (Mod.Current.RootPath != null)
			{
				if (!Directory.Exists(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races")))
					return list;
				foreach (var d in Directory.GetDirectories(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races")))
				{
					var name = Path.GetFileNameWithoutExtension(d);
					if (File.Exists(Path.Combine(d, "Insignia.png")) ||
						File.Exists(Path.Combine(d, "Insignia.bmp")) ||
						File.Exists(Path.Combine(d, name + "_Insignia.png")) ||
						File.Exists(Path.Combine(d, name + "_Insignia.bmp")))
						list.Add(name);
				}
			}
			if (!Directory.Exists(Path.Combine("Pictures", "Races")))
				return list.Distinct();
			foreach (var d in Directory.GetDirectories(Path.Combine("Pictures", "Races")))
			{
				var name = Path.GetFileNameWithoutExtension(d);
				if (File.Exists(Path.Combine(d, "Insignia.png")) ||
					File.Exists(Path.Combine(d, "Insignia.bmp")) ||
					File.Exists(Path.Combine(d, name + "_Insignia.png")) ||
					File.Exists(Path.Combine(d, name + "_Insignia.bmp")))
					list.Add(name);
			}
			return list.Distinct();
		}

		/// <summary>
		/// Lists available shipsets.
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<string> ListShipsets()
		{
			var list = new List<string>();
			if (Mod.Current.RootPath != null)
			{
				if (!Directory.Exists(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races")))
					return list;
				foreach (var d in Directory.GetDirectories(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races")))
					list.Add(Path.GetFileNameWithoutExtension(d));
			}
			if (!Directory.Exists(Path.Combine("Pictures", "Races")))
				return list.Distinct();
			foreach (var d in Directory.GetDirectories(Path.Combine("Pictures", "Races")))
				list.Add(Path.GetFileNameWithoutExtension(d));
			return list.Distinct();
		}

		/// <summary>
		/// Draws a red X across an image.
		/// </summary>
		/// <param name="img"></param>
		/// <returns></returns>
		public static Image CrossOut(Image img)
		{
			var img2 = (Image)img.Clone();
			var thickness = (img2.Width + img2.Height) / 16f;
			var g = Graphics.FromImage(img2);
			var pen = new Pen(Color.Red, thickness);
			g.DrawLine(pen, 0, 0, img2.Width, img2.Height);
			g.DrawLine(pen, 0, img2.Height, img2.Width, 0);
			return img2;
		}

		/// <summary>
		/// Scales an image and frames it in blank space.
		/// The image should be square.
		/// </summary>
		/// <param name="src">The source image.</param>
		/// <param name="scale">The scale factor.</param>
		/// <returns></returns>
		public static Image Frame(this Image src, double scale)
		{
			var scaled = src.Resize((int)Math.Ceiling(Math.Max(src.Width, src.Height) * scale));
			var result = new Bitmap(src.Width, src.Height);
			var g = Graphics.FromImage(result);
			g.DrawImage(scaled, (result.Width - scaled.Width) / 2, (result.Height - scaled.Height) / 2);
			return result;
		}
	}
}
