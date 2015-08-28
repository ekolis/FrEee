using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using DynamicViewModel;
using FrEee.Modding;
using FrEee.Utility;
using static FrEee.Wpf.Utility.Pictures;

namespace FrEee.Wpf.ViewModels
{
	public class ResourceViewModel : DynamicViewModel<Resource>
	{
		public ResourceViewModel(Resource x = null)
			: base(typeof(Resource), x)
		{
		}

		public ImageSource Icon
		{
			get
			{
				var exefolder = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
                if (Mod.Current.RootPath != null)
				{
					return
						GetCachedImageSource(Path.Combine(exefolder, "Mods", Mod.Current.RootPath, "Pictures", "UI", "Resources", Model.PictureName)) ??
						GetCachedImageSource(Path.Combine(exefolder, "Pictures", "UI", "Resources", Model.PictureName)) ??
						GetSolidColorImageSource(Model.Color, 20);
				}
				else
				{
					// stock mod has no entry in Mods folder, and looking for a null path crashes Path.Combine
					return
						GetCachedImageSource(Path.Combine(exefolder, "Pictures", "UI", "Resources", Model.PictureName)) ??
						GetSolidColorImageSource(Model.Color, 20);
				}
			}
		}
	}
}
