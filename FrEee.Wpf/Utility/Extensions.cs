using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace FrEee.Wpf.Utility
{
	public static class Extensions
	{
		/// <summary>
		/// Computes the angle from one point to the other.
		/// Zero degrees is east, and positive is counterclockwise.
		/// </summary>
		/// <param name="p"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		public static double AngleTo(this Point p, Point target)
		{
			return Math.Atan2(target.Y - p.Y, target.X - p.X) * 180d / Math.PI;
		}

		public static Color ToWpfColor(this System.Drawing.Color c)
		{
			return Color.FromArgb(c.A, c.R, c.G, c.B);
		}

		/// <summary>
		/// http://stackoverflow.com/questions/2979834/how-to-draw-a-full-ellipse-in-a-streamgeometry-in-wpf
		/// </summary>
		/// <param name="ctx"></param>
		/// <param name="geo"></param>
		public static void DrawGeometry(this StreamGeometryContext ctx, Geometry geo)
		{
			var pathGeometry = geo as PathGeometry ?? PathGeometry.CreateFromGeometry(geo);
			foreach (var figure in pathGeometry.Figures)
				ctx.DrawFigure(figure);
		}

		/// <summary>
		/// http://stackoverflow.com/questions/2979834/how-to-draw-a-full-ellipse-in-a-streamgeometry-in-wpf
		/// </summary>
		/// <param name="ctx"></param>
		/// <param name="figure"></param>
		public static void DrawFigure(this StreamGeometryContext ctx, PathFigure figure)
		{
			ctx.BeginFigure(figure.StartPoint, figure.IsFilled, figure.IsClosed);
			foreach (var segment in figure.Segments)
			{
				var lineSegment = segment as LineSegment;
				if (lineSegment != null) { ctx.LineTo(lineSegment.Point, lineSegment.IsStroked, lineSegment.IsSmoothJoin); continue; }

				var bezierSegment = segment as BezierSegment;
				if (bezierSegment != null) { ctx.BezierTo(bezierSegment.Point1, bezierSegment.Point2, bezierSegment.Point3, bezierSegment.IsStroked, bezierSegment.IsSmoothJoin); continue; }

				var quadraticSegment = segment as QuadraticBezierSegment;
				if (quadraticSegment != null) { ctx.QuadraticBezierTo(quadraticSegment.Point1, quadraticSegment.Point2, quadraticSegment.IsStroked, quadraticSegment.IsSmoothJoin); continue; }

				var polyLineSegment = segment as PolyLineSegment;
				if (polyLineSegment != null) { ctx.PolyLineTo(polyLineSegment.Points, polyLineSegment.IsStroked, polyLineSegment.IsSmoothJoin); continue; }

				var polyBezierSegment = segment as PolyBezierSegment;
				if (polyBezierSegment != null) { ctx.PolyBezierTo(polyBezierSegment.Points, polyBezierSegment.IsStroked, polyBezierSegment.IsSmoothJoin); continue; }

				var polyQuadraticSegment = segment as PolyQuadraticBezierSegment;
				if (polyQuadraticSegment != null) { ctx.PolyQuadraticBezierTo(polyQuadraticSegment.Points, polyQuadraticSegment.IsStroked, polyQuadraticSegment.IsSmoothJoin); continue; }

				var arcSegment = segment as ArcSegment;
				if (arcSegment != null) { ctx.ArcTo(arcSegment.Point, arcSegment.Size, arcSegment.RotationAngle, arcSegment.IsLargeArc, arcSegment.SweepDirection, arcSegment.IsStroked, arcSegment.IsSmoothJoin); continue; }
			}
		}
	}
}
