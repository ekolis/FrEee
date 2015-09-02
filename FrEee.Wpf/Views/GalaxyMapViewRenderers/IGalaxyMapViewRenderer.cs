using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Wpf.ViewModels;

namespace FrEee.Wpf.Views.GalaxyMapViewRenderers
{
	/// <summary>
	/// Renders a galaxy map view using specific rules for rendering each star system.
	/// </summary>
	public interface IGalaxyMapViewRenderer : INamed
	{
		void Render(StarSystem sys, DrawingContext dc, Point p, double radius);
	}
}
