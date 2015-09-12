using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility;

namespace FrEee.Wpf.Rendering
{
	public static class Renderers
	{
		static Renderers()
		{
			new StellarObjectRenderer().Register();
			new VehicleRenderer().Register();
		}

		public static SafeDictionary<Type, IRenderer> All { get; private set; } = new SafeDictionary<Type, IRenderer>();

		public static ImageSource Render(object o, Size size, bool portrait)
		{
			if (o == null)
				return null;
			var t = o.GetType();
			while (t != null)
			{
				if (All[t] != null)
					return All[t].Render(o, size, portrait);
				t = t.BaseType;
			}
			return null;
		}

		public static ImageSource RenderIcon(object o)
		{
			return Render(o, new Size(64, 64), false);
		}

		public static ImageSource RenderPortrait(object o)
		{
			return Render(o, new Size(512, 512), true);
		}
	}
}
