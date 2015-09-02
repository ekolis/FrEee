using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using FrEee.Game.Objects.Space;
using FrEee.Wpf.Views.Shapes;

namespace FrEee.Wpf.Views.GalaxyMapViewRenderers
{
	/// <summary>
	/// A galaxy map view renderer that uses a pie chart.
	/// </summary>
	public abstract class PieRenderer : IGalaxyMapViewRenderer
	{
		public void Render(StarSystem sys, DrawingContext dc, Point p, double radius)
		{
			// find amounts
			var amounts = GetAmounts(sys).ToArray();

			// find relative amount here vs. max amount in any system to determine brightness of colors
			var amountHere = amounts.Sum(t => t.Item2);

			if (amountHere == 0)
			{
				// nothing, draw a gray outline
				dc.DrawEllipse(null, new Pen(Brushes.Gray, 1), p, radius, radius);
			}
			else
			{
				// draw pie chart
				double curAngle = 0d;
				foreach (var t in amounts.OrderBy(t => t.Item2))
				{
					var color = t.Item1;
					var amount = t.Item2;
					var arc = 360d * amount / amountHere;
					var brush = new SolidColorBrush(Color.FromArgb(GetAlpha(sys), color.R, color.G, color.B));
					var penBrush = new SolidColorBrush(color);
					var drawing = new GeometryDrawing(brush, new Pen(brush, 1), new PieSlice
					{
						Center = p,
						Radius = radius,
						StartDegrees = curAngle,
						DeltaDegrees = arc,
                    }.RenderedGeometry);
					dc.DrawDrawing(drawing);
					curAngle += arc;
				}
			}
		}

		/// <summary>
		/// Relative amounts of various colors to show in the chart.
		/// </summary>
		protected abstract IEnumerable<Tuple<Color, double>> GetAmounts(StarSystem sys);

		/// <summary>
		/// Alpha level for any given star system.
		/// </summary>
		protected abstract byte GetAlpha(StarSystem sys);

		public abstract string Name { get; }

		protected byte WeightAlpha(double amount, double max)
		{
			return (byte)(255 * amount / max);
		}
	}
}
