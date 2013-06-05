using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrEee.WinForms.DataGridView
{
	public class DataGridViewProgressCell : DataGridViewCell
	{
		public DataGridViewProgressCell()
		{
			BarColor = Color.Silver;
		}

		public ProgressDisplayMode ProgressDisplayMode { get; set; }

		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

			var color = Selected ? cellStyle.SelectionBackColor : cellStyle.BackColor;
			graphics.FillRectangle(new SolidBrush(color), cellBounds.X, cellBounds.Y, cellBounds.Width - 1, cellBounds.Height - 1);

			var progress = (Progress)value;			
			if (progress.Maximum != 0)
				graphics.FillRectangle(new SolidBrush(BarColor), cellBounds.X, cellBounds.Y, Math.Min(cellBounds.Width, progress.Value * cellBounds.Width / progress.Maximum), cellBounds.Height);
			Brush brush;
			if (BarColor.R + BarColor.G + BarColor.B > 128 * 3 && cellStyle.BackColor.R + cellStyle.BackColor.G + cellStyle.BackColor.B > 128 * 3)
				brush = new SolidBrush(Color.Black);
			else
				brush = new SolidBrush(Color.White);
			var rect = new Rectangle(cellBounds.Location, cellBounds.Size);
			rect.X += 5;
			rect.Y += 5;
			rect.Width -= 10;
			rect.Height -= 10;
			string text = "";
			switch (ProgressDisplayMode)
			{
				case ProgressDisplayMode.Raw:
					text = progress.Value.ToUnitString() + " / " + progress.Maximum.ToUnitString() + "(" + (progress.IncrementalProgress >= 0 ? "+" + progress.IncrementalProgress.ToUnitString() : progress.IncrementalProgress.ToUnitString()) + " / turn)";
					break;
				case ProgressDisplayMode.Percentage:
					text = ((double)progress.Value / (double)progress.Maximum).ToString("f0") + "% (" + (progress.IncrementalProgress >= 0 ? "+" + ((double)progress.Value / (double)progress.Maximum).ToString("f0") : ((double)progress.Value / (double)progress.Maximum).ToString("f0")) + " / turn)";
					break;
				case ProgressDisplayMode.Eta:
					if (progress.IncrementalProgress <= 0)
						text = "Never";
					else
						text = Math.Ceiling((double)(progress.Maximum - progress.Value) / (double)progress.IncrementalProgress) + " turns";
					break;
			}
			graphics.DrawString(text, cellStyle.Font, brush, rect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
		}

		public Color BarColor { get; set; }

		public override object Clone()
		{
			var cell = (DataGridViewProgressCell)base.Clone();
			cell.BarColor = BarColor;
			cell.ProgressDisplayMode = ProgressDisplayMode;
			return cell;
		}
	}
}
