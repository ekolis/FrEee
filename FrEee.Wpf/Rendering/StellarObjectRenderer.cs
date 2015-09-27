using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FrEee.Game.Objects.Space;
using FrEee.Wpf.Utility;

namespace FrEee.Wpf.Rendering
{
	public class StellarObjectRenderer : Renderer<StellarObject>
	{
		public override ImageSource Render(StellarObject o, Size size, bool portrait)
		{
			var img = portrait ? Pictures.GetCachedImageSource(o.PortraitPaths) : Pictures.GetCachedImageSource(o.IconPaths);
			if (img != null)
				return img.Resize(size);
			else
			{
				// generic gray rectangle if image can't be found
				var dv = new DrawingVisual();
				var dc = dv.RenderOpen();
				dc.DrawRectangle(Brushes.Gray, new Pen(Brushes.Gray, 1), new Rect(size));
				dc.Close();
				var bmp = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Default);
				bmp.Render(dv);
				return bmp;
			}
		}
	}
}
