using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Vehicles;
using FrEee.Game.Setup;
using FrEee.Modding;
using FrEee.Modding.Templates;
using FrEee.Utility;
using FrEee.Utility.Extensions;

namespace FrEee.Wpf.Utility
{
	/// <summary>
	/// Utility methods for handling pictures.
	/// </summary>
	public static class Pictures
	{
		/// <summary>
		/// Picture cache for raw ImageSources on disk.
		/// </summary>
		private static IDictionary<string, ImageSource> fileCache = new Dictionary<string, ImageSource>();

		/// <summary>
		/// Picture cache for objects.
		/// </summary>
		private static IDictionary<object, ImageSource> objectPortraits = new Dictionary<object, ImageSource>();

		/// <summary>
		/// Generic pictures to use for space objects with missing pictures.
		/// </summary>
		private static IDictionary<Type, ImageSource> genericPictures = new Dictionary<Type, ImageSource>();

		private const int Dpi = 96;

		private static readonly PixelFormat PixelFormat = PixelFormats.Default;

		/// <summary>
		/// Draws some stuff.
		/// </summary>
		/// <param name="artist">The drawing actions to perform.</param>
		/// <returns></returns>
		public static ImageSource Render(this Action<DrawingContext> artist, int width, int? height = null)
		{
			if (height == null)
				height = width;
			var img = new RenderTargetBitmap(width, height.Value, Dpi, Dpi, PixelFormat);
			var dv = new DrawingVisual();
			var dc = dv.RenderOpen();
			artist(dc);
			dc.Close();
			img.Render(dv);
			return img;
		}

		static Pictures()
		{
			// TODO - moddable generic pics

			// star
			genericPictures.Add(typeof(Star), Render(dc =>
			{
				dc.DrawEllipse(Brushes.Yellow, new Pen(Brushes.White, 3), new Point(64, 64), 54, 54);
			}, 128, 128));

			// planet
			genericPictures.Add(typeof(Planet), Render(dc =>
			{
				dc.DrawEllipse(Brushes.Blue, new Pen(Brushes.White, 3), new Point(64, 64), 54, 54);
			}, 128, 128));

			// asteroid field
			genericPictures.Add(typeof(AsteroidField), Render(dc =>
			{
				dc.DrawEllipse(Brushes.Gray, null, new Point(32, 32), 25, 25);
				dc.DrawEllipse(Brushes.Gray, null, new Point(96, 48), 25, 25);
				dc.DrawEllipse(Brushes.Gray, null, new Point(48, 96), 25, 25);
			}, 128, 128));

			// storm
			genericPictures.Add(typeof(Storm), Render(dc =>
			{
				dc.DrawRoundedRectangle(Brushes.Purple, null, new Rect(0, 0, 128, 128), 32, 32);
			}, 128, 128));

			// warp point
			genericPictures.Add(typeof(WarpPoint), Render(dc =>
			{
				dc.DrawEllipse(null, new Pen(Brushes.Blue, 3), new Point(64, 64), 54, 54);
				dc.DrawEllipse(null, new Pen(Brushes.Blue, 3), new Point(64, 64), 34, 34);
				dc.DrawEllipse(null, new Pen(Brushes.Blue, 3), new Point(64, 64), 14, 14);
			}, 128, 128));

			// TODO - facility
			
			// TODO - component

			// TODO - hull/design/vehicle

			// TODO - fleet

			// TODO - mount, race, empire
		}

		/*/// <summary>
		/// Gets the icon ImageSource for a stellar object.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static ImageSource GetIcon(StellarObject sobj)
		{
			var portrait = GetPortrait(sobj);
			if (portrait == null)
				return null;
			var img = portrait.Resize(32);

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
		/// Gets the portrait ImageSource for a stellar object.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static ImageSource GetPortrait(StellarObject sobj)
		{
			if (!objectPortraits.ContainsKey(sobj))
			{
				ImageSource portrait;
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
					return GetGenericImageSource(sobj.GetType(), scale);

				if (Mod.Current.RootPath != null)
				{
					portrait =
						GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Planets", sobj.PictureName), scale) ??
						GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Planets", sobj.PictureName), scale) ??
						GetGenericImageSource(sobj.GetType(), scale);
				}
				else
				{
					// stock mod has no entry in Mods folder, and looking for a null path crashes Path.Combine
					portrait =
						GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Planets", sobj.PictureName), scale) ??
						GetGenericImageSource(sobj.GetType(), scale);
				}

				// clone the ImageSource so we don't mess up the original cached version
				portrait = (ImageSource)portrait.Clone();

				objectPortraits.Add(sobj, portrait);
			}

			return objectPortraits[sobj];
		}

		/// <summary>
		/// Gets the icon ImageSource for a facility.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static ImageSource GetIcon(FacilityTemplate f)
		{
			var portrait = GetPortrait(f);
			if (portrait == null)
				return null;
			var thumb = portrait.Resize(32);
			var g = Graphics.FromImageSource(thumb);
			var font = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular);
			var brush = Brushes.White;
			var sf = new StringFormat();
			sf.Alignment = StringAlignment.Far;
			sf.LineAlignment = StringAlignment.Far;
			if (f.RomanNumeral != null && f.RomanNumeral != 0)
				g.DrawString(f.RomanNumeral.Value.ToRomanNumeral(), font, brush, new Point(32, 32), sf);
			return thumb;
		}

		/// <summary>
		/// Gets the portrait ImageSource for a facility.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static ImageSource GetPortrait(FacilityTemplate f)
		{
			if (f.PictureName == null)
				return GetGenericImageSource(f.GetType());
			if (Mod.Current.RootPath != null)
			{
				return
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Facilities", f.PictureName)) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Facilities", f.PictureName)) ??
					GetGenericImageSource(f.GetType());
			}
			else
			{
				// stock mod has no entry in Mods folder, and looking for a null path crashes Path.Combine
				return
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Facilities", f.PictureName)) ??
					GetGenericImageSource(f.GetType());
			}
		}

		/// <summary>
		/// Gets the icon ImageSource for a component.
		/// </summary>
		public static ImageSource GetIcon(ComponentTemplate c)
		{
			var portrait = GetPortrait(c);
			if (portrait == null)
				return null;
			var thumb = portrait.GetThumbnailImageSource(32, 32, () => false, IntPtr.Zero);
			var g = Graphics.FromImageSource(thumb);
			var font = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular);
			var brush = Brushes.White;
			var sf = new StringFormat();
			sf.Alignment = StringAlignment.Far;
			sf.LineAlignment = StringAlignment.Far;
			if (c.RomanNumeral != 0)
				g.DrawString(c.RomanNumeral.Value.ToRomanNumeral(), font, brush, new Point(32, 32), sf);
			return thumb;
		}

		/// <summary>
		/// Gets the portrait ImageSource for a component.
		/// </summary>
		public static ImageSource GetPortrait(ComponentTemplate c)
		{
			if (c.PictureName == null)
				return GetGenericImageSource(c.GetType());
			if (Mod.Current.RootPath != null)
			{
				return
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Components", c.PictureName)) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Components", c.PictureName)) ??
					GetGenericImageSource(c.GetType());
			}
			else
			{
				// stock mod has no entry in Mods folder, and looking for a null path crashes Path.Combine
				return
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Components", c.PictureName)) ??
					GetGenericImageSource(c.GetType());
			}
		}

		/// <summary>
		/// Gets the icon ImageSource for a mount.
		/// </summary>
		public static ImageSource GetIcon(Mount m)
		{
			var portrait = GetPortrait(m);
			if (portrait == null)
				return null;
			return portrait.GetThumbnailImageSource(32, 32, () => false, IntPtr.Zero);
		}

		/// <summary>
		/// Gets the portrait ImageSource for a mount.
		/// </summary>
		public static ImageSource GetPortrait(Mount m)
		{
			if (m.PictureName == null)
				return GetGenericImageSource(m.GetType());
			if (Mod.Current.RootPath != null)
			{
				return
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Mounts", m.PictureName)) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Mounts", m.PictureName)) ??
					GetGenericImageSource(m.GetType());
			}
			else
			{
				// stock mod has no entry in Mods folder, and looking for a null path crashes Path.Combine
				return
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Mounts", m.PictureName)) ??
					GetGenericImageSource(m.GetType());
			}
		}

		public static ImageSource GetIcon(IHull hull, string shipsetPath, int size = 32)
		{
			// allow for practically infinite variation in hull sizes within a confined range of ImageSource sizes using a log function
			var maxSize = (double)Mod.Current.Hulls.Max(h => h.Size);
			var ratio = maxSize / hull.Size;
			var scale = 1d / (1d + Math.Log10(ratio));

			if (shipsetPath == null)
				return GetGenericImageSource(hull.GetType()).Frame(scale).Resize(size);
			if (!hull.PictureNames.Any())
				return GetGenericImageSource(hull.GetType()).Frame(scale).Resize(size);
			var paths = new List<string>();
			foreach (var s in hull.PictureNames)
			{
				if (Mod.Current.RootPath != null)
				{
					paths.Add(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, "Mini_" + s));
					paths.Add(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, shipsetPath + "_Mini_" + s)); // for SE4 shipset compatibility
				}
				paths.Add(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", shipsetPath, "Mini_" + s));
				paths.Add(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", shipsetPath, shipsetPath + "_Mini_" + s)); // for SE4 shipset compatibility
			}
			return (GetCachedImageSource(paths) ?? GetGenericImageSource(hull.GetType(), scale)).Resize(size);
		}

		public static ImageSource GetPortrait(IHull hull, string shipsetPath)
		{
			if (shipsetPath == null)
				return GetGenericImageSource(hull.GetType());
			if (!hull.PictureNames.Any())
				return GetGenericImageSource(hull.GetType());
			var paths = new List<string>();

			// allow for practically infinite variation in hull sizes within a confined range of ImageSource sizes using a log function
			var maxSize = (double)Mod.Current.Hulls.Max(h => h.Size);
			var ratio = maxSize / hull.Size;
			var scale = 1d / (1d + Math.Log10(ratio));

			foreach (var s in hull.PictureNames)
			{
				if (Mod.Current.RootPath != null)
				{
					paths.Add(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, "Portrait_" + s));
					paths.Add(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, shipsetPath + "_Portrait_" + s)); // for SE4 shipset compatibility
				}
				paths.Add(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", shipsetPath, "Portrait_" + s));
				paths.Add(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", shipsetPath, shipsetPath + "_Portrait_" + s)); // for SE4 shipset compatibility
			}
			return GetCachedImageSource(paths) ?? GetGenericImageSource(hull.GetType(), scale);
		}

		/// <summary>
		/// Gets the icon ImageSource for a fleet.
		/// </summary>
		/// <param name="fleet"></param>
		/// <param name="shipsetPath"></param>
		/// <returns></returns>
		public static ImageSource GetIcon(Fleet fleet, string shipsetPath, int size = 32)
		{
			if (shipsetPath == null)
				return GetGenericImageSource(fleet.GetType());
			var paths = new List<string>();

			string ImageSourceName = "Fleet";
			if (fleet.LeafVehicles.All(v => v is Fighter))
				ImageSourceName = "FighterGroup";
			else if (fleet.LeafVehicles.All(v => v is Satellite))
				ImageSourceName = "SatelliteGroup";
			else if (fleet.LeafVehicles.All(v => v is Drone))
				ImageSourceName = "DroneGroup";
			else if (fleet.LeafVehicles.All(v => v is Mine))
				ImageSourceName = "MineGroup";

			if (Mod.Current.RootPath != null)
			{
				paths.Add(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, "Mini_" + ImageSourceName));
				paths.Add(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, shipsetPath + "_Mini_" + ImageSourceName)); // for SE4 shipset compatibility
			}
			paths.Add(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", shipsetPath, "Mini_" + ImageSourceName));
			paths.Add(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", shipsetPath, shipsetPath + "_Mini_" + ImageSourceName)); // for SE4 shipset compatibility

			return (GetCachedImageSource(paths) ?? GetGenericImageSource(fleet.GetType())).Resize(size);
		}

		/// <summary>
		/// Gets the portrait ImageSource for a fleet.
		/// </summary>
		/// <param name="fleet"></param>
		/// <param name="shipsetPath"></param>
		/// <returns></returns>
		public static ImageSource GetPortrait(Fleet fleet, string shipsetPath)
		{
			if (shipsetPath == null)
				return GetGenericImageSource(fleet.GetType());
			var paths = new List<string>();

			string ImageSourceName = "Fleet";
			if (fleet.LeafVehicles.All(v => v is Fighter))
				ImageSourceName = "FighterGroup";
			else if (fleet.LeafVehicles.All(v => v is Satellite))
				ImageSourceName = "SatelliteGroup";
			else if (fleet.LeafVehicles.All(v => v is Drone))
				ImageSourceName = "DroneGroup";
			else if (fleet.LeafVehicles.All(v => v is Mine))
				ImageSourceName = "MineGroup";

			if (Mod.Current.RootPath != null)
			{
				paths.Add(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, "Portrait_" + ImageSourceName));
				paths.Add(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, shipsetPath + "_Portrait_" + ImageSourceName)); // for SE4 shipset compatibility
			}
			paths.Add(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", shipsetPath, "Portrait_" + ImageSourceName));
			paths.Add(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", shipsetPath, shipsetPath + "_Portrait_" + ImageSourceName)); // for SE4 shipset compatibility

			return GetCachedImageSource(paths) ?? GetGenericImageSource(fleet.GetType());
		}

		/// <summary>
		/// Gets the icon ImageSource for a resource.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static ImageSource GetIcon(Resource res)
		{
			if (Mod.Current.RootPath != null)
			{
				return
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "UI", "Resources", res.PictureName)) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "UI", "Resources", res.PictureName)) ??
					GetSolidColorImageSource(res.Color, 20);
			}
			else
			{
				// stock mod has no entry in Mods folder, and looking for a null path crashes Path.Combine
				return
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "UI", "Resources", res.PictureName)) ??
					GetSolidColorImageSource(res.Color, 20);
			}
		}

		/// <summary>
		/// Gets the population icon ImageSource for a race.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static ImageSource GetIcon(this Race race)
		{
			if (Mod.Current.RootPath != null)
			{
				return
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races", race.PopulationIconName, "Pop_Portrait")) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races", race.PopulationIconName, race.PopulationIconName + "_Pop_Portrait")) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", race.PopulationIconName, "Pop_Portrait")) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", race.PopulationIconName, race.PopulationIconName + "_Pop_Portrait")) ??
					// fall back on portrait if icon not found
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races", race.PopulationIconName, "Race_Portrait")) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races", race.PopulationIconName, race.PopulationIconName + "_Race_Portrait")) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", race.PopulationIconName, "Race_Portrait")) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", race.PopulationIconName, race.PopulationIconName + "_Race_Portrait")) ??
					GetGenericImageSource(race.GetType());
			}
			else
			{
				// stock mod has no entry in Mods folder, and looking for a null path crashes Path.Combine
				return
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", race.PopulationIconName, "Pop_Portrait")) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", race.PopulationIconName, race.PopulationIconName + "_Pop_Portrait")) ??
					// fall back on portrait if icon not found
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", race.PopulationIconName, "Race_Portrait")) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", race.PopulationIconName, race.PopulationIconName + "_Race_Portrait")) ??
					GetGenericImageSource(race.GetType());
			}
		}

		/// <summary>
		/// Gets the population portrait ImageSource for a race.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static ImageSource GetPortrait(Race race)
		{
			return GetIcon(race);
		}

		/// <summary>
		/// Gets the insignia icon for an empire.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static ImageSource GetIcon(Empire emp)
		{
			if (Mod.Current.RootPath != null)
			{
				return
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races", emp.InsigniaName, "Insignia")) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races", emp.InsigniaName, emp.InsigniaName + "_Insignia")) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", emp.InsigniaName, "Insignia")) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", emp.InsigniaName, emp.InsigniaName + "_Insignia")) ??
					GetSolidColorImageSource(emp.Color);
			}
			else
			{
				// stock mod has no entry in Mods folder, and looking for a null path crashes Path.Combine
				return
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", emp.InsigniaName, "Insignia")) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", emp.InsigniaName, emp.InsigniaName + "_Insignia")) ??
					GetSolidColorImageSource(emp.Color);
			}
		}

		/// <summary>
		/// Gets the leader portrait for an empire.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static ImageSource GetPortrait(Empire emp)
		{
			if (Mod.Current.RootPath != null)
			{
				return
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races", emp.LeaderPortraitName, "Race_Portrait")) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races", emp.LeaderPortraitName, emp.LeaderPortraitName + "_Race_Portrait")) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", emp.LeaderPortraitName, "Race_Portrait")) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", emp.LeaderPortraitName, emp.LeaderPortraitName + "_Race_Portrait")) ??
					// fall back on icon if portrait not found
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races", emp.LeaderPortraitName, "Pop_Portrait")) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races", emp.LeaderPortraitName, emp.LeaderPortraitName + "_Pop_Portrait")) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", emp.LeaderPortraitName, "Pop_Portrait")) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", emp.LeaderPortraitName, emp.LeaderPortraitName + "_Pop_Portrait")) ??
					GetGenericImageSource(emp.GetType());
			}
			else
			{
				// stock mod has no entry in Mods folder, and looking for a null path crashes Path.Combine
				return
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", emp.LeaderPortraitName, "Race_Portrait")) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", emp.LeaderPortraitName, emp.LeaderPortraitName + "_Race_Portrait")) ??
					// fall back on icon if portrait not found
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", emp.LeaderPortraitName, "Pop_Portrait")) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", emp.LeaderPortraitName, emp.LeaderPortraitName + "_Pop_Portrait")) ??
					GetGenericImageSource(emp.GetType());
			}
		}

		/// <summary>
		/// Gets the insignia icon for an empire template.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns></returns>
		public static ImageSource GetIcon(EmpireTemplate emp)
		{
			var insigniaName = emp.InsigniaName;
			if (insigniaName == null)
				GetSolidColorImageSource(emp.Color);
			if (Mod.Current.RootPath != null)
			{
				return
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races", insigniaName, "Insignia")) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races", insigniaName, insigniaName + "_Insignia")) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", insigniaName, "Insignia")) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", insigniaName, insigniaName + "_Insignia")) ??
					GetSolidColorImageSource(emp.Color);
			}
			else
			{
				// stock mod has no entry in Mods folder, and looking for a null path crashes Path.Combine
				return
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", insigniaName, "Insignia")) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", insigniaName, insigniaName + "_Insignia")) ??
					GetSolidColorImageSource(emp.Color);
			}
		}

		public static ImageSource GetIcon(Seeker seeker)
		{
			if (Mod.Current.RootPath != null)
			{
				var fx = (SeekerWeaponDisplayEffect)seeker.WeaponInfo.DisplayEffect;
				return
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races", seeker.Owner.ShipsetPath, fx.Name)) ??
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", seeker.Owner.ShipsetPath)) ??
					GetGenericImageSource(seeker.GetType());
			}
			else
			{
				// stock mod has no entry in Mods folder, and looking for a null path crashes Path.Combine
				var fx = (SeekerWeaponDisplayEffect)seeker.WeaponInfo.DisplayEffect;
				return
					GetCachedImageSource(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", seeker.Owner.ShipsetPath, fx.Name)) ??
					GetGenericImageSource(seeker.GetType());
			}
		}*/

		public static ImageSource GetCachedImageSource(IEnumerable<string> paths, double scale = 1d)
		{
			foreach (var path in paths)
			{
				var img = GetCachedImageSource(path, scale);
				if (img != null)
					return img;
			}
			return null;
		}

		public static ImageSource GetCachedImageSource(string path, double scale = 1d)
		{
			if (string.IsNullOrEmpty(Path.GetExtension(path)))
			{
				// check PNG, then BMP, if no extension specified
				// don't scale BMP files (unless they're specifically requested as BMP with a scale factor)
				return GetCachedImageSource(path + ".png", scale) ?? GetCachedImageSource(path + ".bmp");
			}

			if (!fileCache.ContainsKey(path))
			{
				// make sure relative paths are relative to the executable, not some random working directory we might have started in (e.g. AutoPBW)
				if (path != Path.GetFullPath(path))
					path = Path.Combine(Path.GetFullPath(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)), path);

				if (File.Exists(path))
				{
					try
					{
						ImageSource img = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
						if (Path.GetExtension(path).ToLower() == ".bmp")
						{
							// make black pixels transparent if it's a bmp
							img = Render(dc =>
							{
								dc.PushOpacityMask(Brushes.Black);
								dc.DrawImage(img, new Rect(0, 0, img.Width, img.Height));
							}, (int)img.Width, (int)img.Height);
						}
						fileCache[path] = img;
					}
					catch (Exception ex)
					{
						Console.Error.WriteLine("Could not load " + Path.GetFullPath(path) + ": " + ex.Message);
						fileCache[path] = null;
					}
				}
				else
				{
					Console.Error.WriteLine("Could not find " + Path.GetFullPath(path) + ".");
					fileCache[path] = null;
				}
			}
			return fileCache[path].Frame(scale);
		}

		public static ImageSource GetModImageSource(string path)
		{
			if (Mod.Current == null || Mod.Current.RootPath == null)
				return GetCachedImageSource(path);
			return GetCachedImageSource(new string[]
			{
				Path.Combine("Mods", Mod.Current.RootPath, path),
				path
			});
		}

		public static ImageSource GetModImageSource(params string[] paths)
		{
			if (Mod.Current.RootPath == null)
				return GetCachedImageSource(paths);
			var allpaths = new List<string>();
			foreach (var p in paths)
			{
				allpaths.Add(Path.Combine("Mods", Mod.Current.RootPath, p));
				allpaths.Add(p);
			}
			return GetCachedImageSource(allpaths);
		}

		/// <summary>
		/// Gets a generic ImageSource for a type of object.
		/// </summary>
		/// <param name="type">The type of object.</param>
		/// <param name="scale">The scale factor. If less than 1, the ImageSource will be shrunk and "framed" with blank space.</param>
		/// <returns></returns>
		public static ImageSource GetGenericImageSource(Type type, double scale = 1d)
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
				ImageSource img = null;
				if (type.BaseType != null)
					img = GetGenericImageSource(type.BaseType, scale);
				if (img != null)
					return img;
				foreach (var i in type.GetInterfaces())
				{
					img = GetGenericImageSource(i, scale);
					if (img != null)
						return img;
				}
			}
			return GetSolidColorImageSource(Colors.Transparent);
		}

		public static ImageSource GetSolidColorImageSource(Color color, int size = 32)
		{
			var brush = new SolidColorBrush(color);
			return Render(dc => dc.DrawRectangle(brush, null, new Rect(0, 0, size, size)), size);
		}
		[Obsolete("Temporary function until System.Drawing can be removed from core assembly")]
		public static ImageSource GetSolidColorImageSource(System.Drawing.Color color, int size = 32)
		{
			var wpfcolor = new Color();
			wpfcolor.R = color.R;
			wpfcolor.G = color.G;
			wpfcolor.B = color.B;
			wpfcolor.A = color.A;
			var brush = new SolidColorBrush(wpfcolor);
			return Render(dc => dc.DrawRectangle(brush, null, new Rect(0, 0, size, size)), size);
		}

		/// <summary>
		/// Gets a generic vehicle type ImageSource for a shipset.
		/// </summary>
		/// <param name="shipsetPath"></param>
		/// <returns></returns>
		public static ImageSource GetVehicleTypeImageSource(string shipsetPath, VehicleTypes vt = VehicleTypes.Fighter)
		{
			if (shipsetPath == null)
				return GetGenericImageSource(typeof(IUnit));
			var paths = new List<string>();

			string hullname;
			Type hulltype;
			switch (vt)
			{
				case VehicleTypes.Base:
					hullname = "BattleStation";
					hulltype = typeof(Base);
					break;
				case VehicleTypes.Drone:
					hullname = "Drone";
					hulltype = typeof(Drone);
					break;
				case VehicleTypes.Fighter:
					hullname = "FighterMedium";
					hulltype = typeof(Fighter);
					break;
				case VehicleTypes.Mine:
					hullname = "Mine";
					hulltype = typeof(Mine);
					break;
				case VehicleTypes.Satellite:
					hullname = "Satellite";
					hulltype = typeof(Satellite);
					break;
				case VehicleTypes.Ship:
					hullname = "Cruiser";
					hulltype = typeof(Ship);
					break;
				case VehicleTypes.Troop:
					hullname = "TroopMedium";
					hulltype = typeof(Troop);
					break;
				case VehicleTypes.WeaponPlatform:
					hullname = "WeapPlatformMedium";
					hulltype = typeof(WeaponPlatform);
					break;
				default:
					hullname = "Fighter";
					hulltype = typeof(Fighter);
					break;
			}

			if (Mod.Current.RootPath != null)
			{
				paths.Add(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, "Portrait_" + hullname));
				paths.Add(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, shipsetPath + "_Portrait_" + hullname)); // for SE4 shipset compatibility
			}
			paths.Add(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", shipsetPath, "Portrait_" + hullname));
			paths.Add(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races", shipsetPath, shipsetPath + "_Portrait_" + hullname)); // for SE4 shipset compatibility

			return GetCachedImageSource(paths) ?? GetGenericImageSource(hulltype);
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
				if (!Directory.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races")))
					return list;
				foreach (var d in Directory.GetDirectories(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races")))
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
			if (!Directory.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races")))
				return list.Distinct();
			foreach (var d in Directory.GetDirectories(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races")))
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
				if (!Directory.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races")))
					return list;
				foreach (var d in Directory.GetDirectories(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races")))
				{
					var name = Path.GetFileNameWithoutExtension(d);
					if (File.Exists(Path.Combine(d, "Insignia.png")) ||
						File.Exists(Path.Combine(d, "Insignia.bmp")) ||
						File.Exists(Path.Combine(d, name + "_Insignia.png")) ||
						File.Exists(Path.Combine(d, name + "_Insignia.bmp")))
						list.Add(name);
				}
			}
			if (!Directory.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races")))
				return list.Distinct();
			foreach (var d in Directory.GetDirectories(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races")))
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
				if (!Directory.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races")))
					return list;
				foreach (var d in Directory.GetDirectories(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", Mod.Current.RootPath, "Pictures", "Races")))
					list.Add(Path.GetFileNameWithoutExtension(d));
			}
			if (!Directory.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races")))
				return list.Distinct();
			foreach (var d in Directory.GetDirectories(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "Races")))
				list.Add(Path.GetFileNameWithoutExtension(d));
			return list.Distinct();
		}

		/// <summary>
		/// Draws a red X across an ImageSource.
		/// </summary>
		/// <param name="img"></param>
		/// <returns></returns>
		public static ImageSource CrossOut(ImageSource img)
		{
			return Render(dc =>
			{
				dc.DrawImage(img, new Rect());
				var thickness = (img.Width + img.Height) / 16f;
				var pen = new Pen(Brushes.Red, thickness);
				dc.DrawLine(pen, new Point(0, 0), new Point(img.Width, img.Height));
				dc.DrawLine(pen, new Point(0, img.Height), new Point(img.Width, 0));
			}, (int)img.Width, (int)img.Height);
		}

		/// <summary>
		/// Scales an ImageSource and frames it in blank space.
		/// The ImageSource should be square.
		/// </summary>
		/// <param name="src">The source ImageSource.</param>
		/// <param name="scale">The scale factor.</param>
		/// <returns></returns>
		public static ImageSource Frame(this ImageSource src, double scale)
		{
			if (src == null)
				return null;
			if (scale == 1d)
				return (ImageSource)src.Clone();
			var oldsize = Math.Max(src.Width, src.Height);
            var newsize = (int)Math.Ceiling(oldsize * scale);
			var delta = (newsize - oldsize) / 2;
			var scaled = src.Resize(new Size(newsize, newsize));
			return Render(dc => dc.DrawImage(scaled, new Rect(delta, delta, oldsize, oldsize)), newsize, newsize);
		}

		/// <summary>
		/// Crops an ImageSource to a specific size at a specific position.
		/// </summary>
		/// <param name="src">The source ImageSource.</param>
		/// <param name="upperLeft">The upper left corner of the crop rectangle.</param>
		/// <param name="size">The size of the crop rectangle.</param>
		/// <returns>The cropped ImageSource.</returns>
		public static ImageSource Crop(this ImageSource src, Point upperLeft, Size size)
		{
			return Render(dc => dc.DrawImage(src, new Rect(-upperLeft.X, -upperLeft.Y, src.Width, src.Height)), (int)size.Width, (int)size.Height);
		}

		public static ImageSource Resize(this ImageSource src, Size size)
		{
			return Render(dc => dc.DrawImage(src, new Rect(size)), (int)size.Width, (int)size.Height);
		}

		public static ImageSource Resize(this ImageSource src, double width, double? height = null)
		{
			if (height == null)
				height = width;
			return src.Resize(new Size(width, height.Value));
		}
	}
}
