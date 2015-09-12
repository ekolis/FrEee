using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace FrEee.Wpf.Rendering
{
	/// <summary>
	/// Renders an object.
	/// </summary>
	public interface IRenderer
	{
		ImageSource Render(object o, Size size, bool portrait);
	}

	/// <summary>
	/// Renders an object.
	/// </summary>
	public interface IRenderer<in T> : IRenderer
	{
		ImageSource Render(T o, Size size, bool portrait);
	}
}
