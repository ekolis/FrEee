using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
	}
}
