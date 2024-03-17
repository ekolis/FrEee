using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace FrEee.UI.Blazor
{
	public static class WebExtensions
	{
		public static string ToRgba(this Color color)
		{
			var argb = color.ToArgb().ToString("x");
			return argb.Substring(2) + argb.Substring(0, 2);
		}
	}
}
