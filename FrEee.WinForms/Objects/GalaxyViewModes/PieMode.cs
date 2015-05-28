﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.WinForms.Utility.Extensions;

namespace FrEee.WinForms.Objects.GalaxyViewModes
{
	/// <summary>
	/// A galaxy view mode that uses a pie chart.
	/// </summary>
	public abstract class PieMode : IGalaxyViewMode
	{
		public void Draw(StarSystem sys, Graphics g, PointF pos, float size)
		{
			// find amounts
			var amounts = GetAmounts(sys).ToArray();

			// find relative amount here vs. max amount in any system to determine brightness of colors
			var amountHere = amounts.Sum(t => t.Item2);

			if (amountHere == 0)
			{
				// nothing, draw a gray outline
				g.DrawEllipse(Pens.Gray, pos, size);
			}
			else
			{
				// draw pie chart
				float curAngle = 0f;
				foreach (var t in amounts.OrderBy(t => t.Item2))
				{
					var color = t.Item1;
					var amount = t.Item2;
					var arc = 360f * amount / amountHere;
					g.FillPie(new SolidBrush(Color.FromArgb(GetAlpha(sys), color)), pos, size, curAngle, arc);
					g.DrawPie(new Pen(color), pos, size, curAngle, arc);
					curAngle += arc;
				}
			}
		}

		/// <summary>
		/// Relative amounts of various colors to show in the chart.
		/// </summary>
		protected abstract IEnumerable<Tuple<Color, float>> GetAmounts(StarSystem sys);

		/// <summary>
		/// Alpha level for any given star system.
		/// </summary>
		protected abstract int GetAlpha(StarSystem sys);

		public abstract string Name { get; }

		protected int WeightAlpha(float amount, float max)
		{
			return (int)(255 * amount / max);
		}
	}
}
