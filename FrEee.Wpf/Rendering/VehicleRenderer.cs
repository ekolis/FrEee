using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Wpf.Utility;

namespace FrEee.Wpf.Rendering
{
	public class VehicleRenderer : Renderer<Vehicle>
	{
		public override ImageSource Render(Vehicle o, Size size, bool portrait)
		{
			var img = portrait ? Pictures.GetCachedImageSource(o.PortraitPaths) : Pictures.GetCachedImageSource(o.IconPaths);
			if (img != null)
				return img.Resize(size);
			else
			{
				// generic gray triangle if image can't be found
				var dv = new DrawingVisual();
				var dc = dv.RenderOpen();
				var geometry = new StreamGeometry();
				var ctx = geometry.Open();
				ctx.BeginFigure(new Point(size.Width / 2d, 0), true, true);
				ctx.LineTo(new Point(size.Width, size.Height), true, true);
				ctx.LineTo(new Point(0, size.Height), true, true);
				ctx.Close();
				dc.DrawGeometry(new SolidColorBrush(o.Owner.Color.ToWpfColor()), null, geometry);
				dc.Close();
				var bmp = new RenderTargetBitmap(512, 512, 96, 96, PixelFormats.Default);
				bmp.Render(dv);
				return bmp;
			}
		}
	}
}
