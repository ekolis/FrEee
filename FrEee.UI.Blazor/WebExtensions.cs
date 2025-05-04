using System.Drawing;

namespace FrEee.UI.Blazor
{
	public static class WebExtensions
	{
		public static string ToRgba(this Color color)
		{
			var argb = color.ToArgb().ToString("x8");
			return argb.Substring(2) + argb.Substring(0, 2);
		}
	}
}
