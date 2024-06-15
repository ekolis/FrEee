using FrEee.Utility;
using FrEee.Extensions;
using System;
using System.Drawing;
using System.Windows.Forms;
using FrEee.Extensions;
using FrEee.Utility;

namespace FrEee.UI.WinForms.DataGridView;

public class DataGridViewProgressCell : DataGridViewCell
{
	public DataGridViewProgressCell()
	{
		BarColor = Color.Silver;
	}

	public Color BarColor { get; set; }
	public ProgressDisplayMode ProgressDisplayMode { get; set; }

	public override object Clone()
	{
		var cell = (DataGridViewProgressCell)base.Clone();
		cell.BarColor = BarColor;
		cell.ProgressDisplayMode = ProgressDisplayMode;
		return cell;
	}

	protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
	{
		base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

		var color = Selected ? cellStyle.SelectionBackColor : cellStyle.BackColor;
		graphics.FillRectangle(new SolidBrush(color), cellBounds);

		var progress = (Progress)value;
		if (progress != null)
		{
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
					// TODO - show incremental progress after delay?
					if (progress.IncrementalProgressBeforeDelay != 0)
						text = progress.Value.ToUnitString() + " / " + progress.Maximum.ToUnitString() + " (" + (progress.IncrementalProgressBeforeDelay >= 0 ? "+" + progress.IncrementalProgressBeforeDelay.ToUnitString() : progress.IncrementalProgressBeforeDelay.ToUnitString()) + " / turn)";
					else
						text = progress.Value.ToUnitString() + " / " + progress.Maximum.ToUnitString();
					break;

				case ProgressDisplayMode.Percentage:
					// TODO - show incremental progress after delay?
					if (progress.IncrementalProgressBeforeDelay != 0)
						text = ((double)progress.Value / (double)progress.Maximum * 100).ToString("f0") + "% (" + (progress.IncrementalProgressBeforeDelay >= 0 ? "+" + ((double)progress.IncrementalProgressBeforeDelay / (double)progress.Maximum * 100).ToString("f0") : ((double)progress.IncrementalProgressBeforeDelay / (double)progress.Maximum * 100).ToString("f0")) + " / turn)";
					else
						text = ((double)progress.Value / (double)progress.Maximum * 100).ToString("f0") + "%";
					break;

				case ProgressDisplayMode.Eta:
					if (progress.Eta == null)
						text = "Never";
					else
						text = progress.Eta + " turn" + (progress.Eta == 1 ? "" : "s");
					break;
			}
			graphics.DrawString(text, cellStyle.Font, brush, rect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
		}
		PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
	}
}