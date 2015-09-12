using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace FrEee.Wpf.Rendering
{
	public abstract class Renderer<T> : IRenderer<T>
	{
		public void Register()
		{
			Renderers.All[typeof(T)] = this;
		}

		public ImageSource Render(object o, Size size, bool portrait)
		{
			if (o is T)
				return Render((T)o, size, portrait);
			else
				throw new InvalidOperationException($"{this} + cannot render {o}.");
		}

		public abstract ImageSource Render(T o, Size size, bool portrait);
	}
}
