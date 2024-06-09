using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrEee.Extensions;
using FrEee.Extensions;

namespace FrEee.UI.WinForms.Controls;

public partial class LineGraph : Control
{
	public LineGraph()
	{
		InitializeComponent();
	}

	private bool roundMaxToMUltipleOfPowerOfTen;

	public bool RoundMaxToMultipleOfPowerOfTen
	{
		get => roundMaxToMUltipleOfPowerOfTen;
		set
		{
			roundMaxToMUltipleOfPowerOfTen = value;
			Invalidate();
		}
	}

	public ICollection<GraphSeries> Series
	{
		get { return series; }
		set
		{
			series = value;
			Invalidate();
		}
	}

	public string Title
	{
		get { return title; }
		set
		{
			title = value;
			Invalidate();
		}
	}

	private ICollection<GraphSeries> series = new List<GraphSeries>();

	private string title;

	protected override void OnPaint(PaintEventArgs pe)
	{
		base.OnPaint(pe);

		// get graphics
		var g = pe.Graphics;

		// compute boundaries
		var max = Series.Max(s => s.DataPoints.Max());
		var maxstr = max.ToUnitString(true);
		var horizontalMargin = g.MeasureString(maxstr, Font).Width * 2;
		var verticalMargin = Font.SizeInPoints * pe.Graphics.DpiY / 96f * 2f;
		var graphBounds = new RectangleF(horizontalMargin, verticalMargin, Width - horizontalMargin * 2, Height - verticalMargin * 2);
		var headerBounds = new RectangleF(horizontalMargin, 0, graphBounds.Width, verticalMargin);

		// draw title
		var sfCenter = new StringFormat();
		sfCenter.Alignment = StringAlignment.Center;
		sfCenter.LineAlignment = StringAlignment.Center;
		g.DrawString(Title, Font, Brushes.White, headerBounds, sfCenter);

		if (Series != null && Series.Any())
		{
			// draw Y-axis (round max up to a single digit multiple of a power of 10 for pretty display)
			if (RoundMaxToMultipleOfPowerOfTen)
			{
				var log = Math.Floor(Math.Log10(max));
				var pow = Math.Pow(10, log);
				var factor = max / pow;
				var rounded = Math.Ceiling(factor);
				max = rounded * pow;
			}
			var sfRight = new StringFormat();
			sfRight.Alignment = StringAlignment.Far;
			sfRight.LineAlignment = StringAlignment.Center;
			for (int i = 0; i <= 5; i++)
			{
				var val = max / 5d * i;
				var pos = graphBounds.Bottom - graphBounds.Height / 5f * i;
				var valstr = val.ToUnitString(true);
				var size = g.MeasureString(valstr, Font);
				var rect = new RectangleF(0, pos - size.Height / 2f, horizontalMargin, size.Height);
				g.DrawString(valstr, Font, Brushes.White, rect, sfRight);
			}

			// draw X-axis and data points
			var maxx = Series.Max(s => s.DataPoints.Count()) - 1;
			var sfTop = new StringFormat();
			sfTop.Alignment = StringAlignment.Center;
			sfTop.LineAlignment = StringAlignment.Near;
			float lastXpos = 0;
			float lastYpos = 0;
			foreach (var s in Series)
			{
				var pen = new Pen(s.Color);
				var brush = new SolidBrush(s.Color);
				for (int i = 0; i <= maxx; i++)
				{
					var xpos = graphBounds.Left + graphBounds.Width / (maxx == 0 ? 1 : maxx) * i;
					var size = g.MeasureString(i.ToString(), Font);
					var rect = new RectangleF(xpos - size.Width / 2f, graphBounds.Bottom, size.Width, size.Height);
					g.DrawString(i.ToString(), Font, Brushes.White, rect, sfTop);
					var val = s.DataPoints.ElementAt(i);
					var ypos = (float)(graphBounds.Bottom - graphBounds.Height * val / (double.IsNaN(max) ? 1 : max));
					if (i > 0)
						g.DrawLine(pen, lastXpos, lastYpos, xpos, ypos);
					else
						g.DrawString(s.Name, Font, brush, xpos, ypos);
					lastXpos = xpos;
					lastYpos = ypos;
				}
			}
		}
	}

	public class GraphSeries
	{
		public Color Color { get; set; }
		public string Name { get; set; }
		public IEnumerable<double> DataPoints { get; set; }
	}
}