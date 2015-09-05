using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using FrEee.Game.Objects.Space;
using FrEee.Wpf.Utility;
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
				// nothing, draw a gray outline with a faint fill
				var grayTransparent = Color.FromArgb(GetAlpha(sys), Colors.Gray.R, Colors.Gray.G, Colors.Gray.B);
				dc.DrawEllipse(new SolidColorBrush(grayTransparent), new Pen(Brushes.Gray, 1), p, radius, radius);
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
					var pieSlice = new PieSlice
					{
						StartDegrees = curAngle,
						DeltaDegrees = arc,
					};
					dc.DrawGeometry(brush, new Pen(penBrush, 1), CreateGeometry(p, radius, curAngle, arc));
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

		private Geometry CreateGeometry(Point p, double radius, double startDegrees, double deltaDegrees)
		{
			// Create a StreamGeometry for describing the shape
			StreamGeometry geometry = new StreamGeometry();

			using (StreamGeometryContext context = geometry.Open())
			{
				var rad1 = Deg2Rad(startDegrees);
				var drad = Deg2Rad(deltaDegrees);
				var rad2 = rad1 + drad;
				var p1 = new Point((Math.Cos(rad1) + 1) * radius + p.X, -(Math.Sin(rad1) - 1) * radius + p.Y);
				var p2 = new Point((Math.Cos(rad2) + 1) * radius + p.X, -(Math.Sin(rad2) - 1) * radius + p.Y);

				context.BeginFigure(p, true, true);
				if (deltaDegrees >= 360)
				{
					context.DrawGeometry(new EllipseGeometry(new Rect(p.X - radius, p.Y - radius, radius * 2, radius * 2)));
				}
				else if (deltaDegrees >= 0)
				{
					context.LineTo(p1, true, true);
					context.ArcTo(p2, new Size(radius, radius), drad, deltaDegrees > 180, SweepDirection.Counterclockwise, true, true);
					context.Close();
				}
			}

			// Freeze the geometry for performance benefits
			geometry.Freeze();

			return geometry;
		}

		private double Deg2Rad(double degrees)
		{
			return degrees / 180d * Math.PI;
		}
	}
}
