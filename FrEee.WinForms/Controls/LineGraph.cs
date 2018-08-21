using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FrEee.WinForms.Controls
{
	public partial class LineGraph : Control
	{
		public LineGraph()
		{
			InitializeComponent();
		}

		public IEnumerable<double> DataPoints
		{
			get { return dataPoints; }
			set
			{
				dataPoints = value;
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

		private IEnumerable<double> dataPoints;

		private string title;

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);

			// compute boundaries
			var horizontalMargin = Font.SizeInPoints * pe.Graphics.DpiX / 96f * 5f;
			var verticalMargin = Font.SizeInPoints * pe.Graphics.DpiY / 96f * 2f;
			var graphBounds = new RectangleF(horizontalMargin, verticalMargin, Width - horizontalMargin * 2, Height - verticalMargin * 2);
			var headerBounds = new RectangleF(horizontalMargin, 0, graphBounds.Width, verticalMargin);

			// get graphics
			var g = pe.Graphics;

			// get brush
			var brush = new SolidBrush(ForeColor);

			// draw title
			var sfCenter = new StringFormat();
			sfCenter.Alignment = StringAlignment.Center;
			sfCenter.LineAlignment = StringAlignment.Center;
			g.DrawString(Title, Font, brush, headerBounds, sfCenter);

			if (DataPoints != null && DataPoints.Any())
			{
				// draw Y-axis (round max up to a single digit multiple of a power of 10 for pretty display)
				var max = DataPoints.Max();
				var log = Math.Floor(Math.Log10(max));
				var pow = Math.Pow(10, log);
				var factor = max / pow;
				var rounded = Math.Ceiling(factor);
				max = rounded * pow;
				var sfRight = new StringFormat();
				sfRight.Alignment = StringAlignment.Far;
				sfRight.LineAlignment = StringAlignment.Center;
				for (int i = 0; i <= 5; i++)
				{
					var val = max / 5d * i;
					var pos = graphBounds.Bottom - graphBounds.Height / 5f * i;
					var size = g.MeasureString(val.ToString(), Font);
					var rect = new RectangleF(0, pos - size.Height / 2f, horizontalMargin, size.Height);
					g.DrawString(val.ToString(), Font, brush, rect, sfRight);
				}

				// draw X-axis and data points
				var maxx = DataPoints.Count() - 1;
				var sfTop = new StringFormat();
				sfTop.Alignment = StringAlignment.Center;
				sfTop.LineAlignment = StringAlignment.Near;
				float lastXpos = 0;
				float lastYpos = 0;
				var pen = new Pen(brush);
				for (int i = 0; i <= maxx; i++)
				{
					var xpos = graphBounds.Left + graphBounds.Width / (maxx == 0 ? 1 : maxx) * i;
					var size = g.MeasureString(i.ToString(), Font);
					var rect = new RectangleF(xpos - size.Width / 2f, graphBounds.Bottom, size.Width, size.Height);
					g.DrawString(i.ToString(), Font, brush, rect, sfTop);
					var val = DataPoints.ElementAt(i);
					var ypos = (float)(graphBounds.Bottom - graphBounds.Height * val / (double.IsNaN(max) ? 1 : max));
					if (i > 0)
						g.DrawLine(pen, lastXpos, lastYpos, xpos, ypos);
					lastXpos = xpos;
					lastYpos = ypos;
				}
			}
		}
	}
}