﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using FrEee.Game.Objects.Civilization;
using FrEee.Modding;
using static FrEee.Wpf.Utility.Pictures;

namespace FrEee.Wpf.ViewModels
{
	public class EmpireViewModel
	{
		public EmpireViewModel()
		{

		}

		public EmpireViewModel(Empire e)
		{
			Model = e;
		}

		private Empire Model;

		public ImageSource Insignia
		{
			get
			{
				var exefolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
				var emp = Model;
                if (Mod.Current.RootPath != null)
				{
					return
						GetCachedImageSource(Path.Combine(exefolder, "Mods", Mod.Current.RootPath, "Pictures", "Races", emp.InsigniaName, "Insignia")) ??
						GetCachedImageSource(Path.Combine(exefolder, "Mods", Mod.Current.RootPath, "Pictures", "Races", emp.InsigniaName, emp.InsigniaName + "_Insignia")) ??
						GetCachedImageSource(Path.Combine(exefolder, "Pictures", "Races", emp.InsigniaName, "Insignia")) ??
						GetCachedImageSource(Path.Combine(exefolder, "Pictures", "Races", emp.InsigniaName, emp.InsigniaName + "_Insignia")) ??
						GetSolidColorImageSource(emp.Color);
				}
				else
				{
					// stock mod has no entry in Mods folder, and looking for a null path crashes Path.Combine
					return
						GetCachedImageSource(Path.Combine(exefolder, "Pictures", "Races", emp.InsigniaName, "Insignia")) ??
						GetCachedImageSource(Path.Combine(exefolder, "Pictures", "Races", emp.InsigniaName, emp.InsigniaName + "_Insignia")) ??
						GetSolidColorImageSource(emp.Color);
				}
			}
		}

		public ResourceProgressViewModel ResourceProgress
		{
			get
			{
				return new ResourceProgressViewModel(new FrEee.Utility.ResourceProgress(Model.StoredResources, Model.ResourceStorage, Model.NetIncomeLessConstruction));
			}
		}
	}
}