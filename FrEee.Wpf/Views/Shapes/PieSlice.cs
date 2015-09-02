using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FrEee.Wpf.Views.Shapes
{
	/// <summary>
	/// A pie slice, part of a pie chart.
	/// </summary>
	public class PieSlice : Shape
	{
		public Point Center
		{
			get { return (Point)GetValue(CenterProperty); }
			set { SetValue(CenterProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Center.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CenterProperty =
			DependencyProperty.Register("Center", typeof(Point), typeof(PieSlice), new PropertyMetadata(new Point()));



		[TypeConverter(typeof(LengthConverter))]
		public double Radius
		{
			get { return (double)GetValue(RadiusProperty); }
			set { SetValue(RadiusProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Radius.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty RadiusProperty =
			DependencyProperty.Register("Radius", typeof(double), typeof(PieSlice), new PropertyMetadata(10d));

		public double StartDegrees
		{
			get { return (double)GetValue(StartDegreesProperty); }
			set { SetValue(StartDegreesProperty, value); }
		}

		// Using a DependencyProperty as the backing store for StartAngle.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty StartDegreesProperty =
			DependencyProperty.Register("StartDegrees", typeof(double), typeof(PieSlice), new PropertyMetadata(0d));

		public double DeltaDegrees
		{
			get { return (double)GetValue(DeltaDegreesProperty); }
			set { SetValue(DeltaDegreesProperty, value); }
		}

		// Using a DependencyProperty as the backing store for AngleDelta.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty DeltaDegreesProperty =
			DependencyProperty.Register("DeltaDegrees", typeof(double), typeof(PieSlice), new PropertyMetadata(90d));

		protected override Geometry DefiningGeometry
		{
			get
			{
				// Create a StreamGeometry for describing the shape
				StreamGeometry geometry = new StreamGeometry();
				geometry.FillRule = FillRule.EvenOdd;

				using (StreamGeometryContext context = geometry.Open())
				{
					InternalDraw(context);
				}

				// Freeze the geometry for performance benefits
				geometry.Freeze();

				return geometry;
			}
		}

		private void InternalDraw(StreamGeometryContext context)
		{
			var center = new Point();
			var rad1 = Deg2Rad(StartDegrees);
			var drad = Deg2Rad(DeltaDegrees);
			var rad2 = rad1 + drad;
			var p1 = new Point(Math.Cos(rad1), -Math.Sin(rad1));
			var p2 = new Point(Math.Cos(rad2), -Math.Sin(rad2));

			context.BeginFigure(center, true, true);
			context.LineTo(p1, true, true);
			context.ArcTo(p2, new Size(Radius, Radius), drad, DeltaDegrees > 180, SweepDirection.Counterclockwise, true, true);
			context.Close();
		}

		private double Deg2Rad(double degrees)
		{
			return degrees / 180d * Math.PI;
		}
	}
}
