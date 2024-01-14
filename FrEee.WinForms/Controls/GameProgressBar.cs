using FrEee.Utility; using FrEee.Serialization;
using FrEee.Extensions;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FrEee.WinForms.Controls;

public partial class GameProgressBar : UserControl
{
	public GameProgressBar()
	{
		InitializeComponent();
		this.SizeChanged += GameProgressBar_SizeChanged;
		Padding = new Padding(5, 5, 5, 5);
	}

	public Color BarColor
	{
		get { return barColor; }
		set
		{
			barColor = value;
			Invalidate();
		}
	}

	/// <summary>
	/// Color of the border for BorderStyle.FixedSingle mode.
	/// </summary>
	public Color BorderColor
	{
		get { return borderColor; }
		set
		{
			borderColor = value;
			Invalidate();
		}
	}

	public long IncrementalProgress
	{
		get { return incrementalProgress; }
		set
		{
			incrementalProgress = value;
			Invalidate();
		}
	}

	public string LeftText { get { return leftText; } set { leftText = value; Invalidate(); } }

	public long Maximum
	{
		get { return maximum; }
		set
		{
			maximum = value;
			Invalidate();
		}
	}

	public Progress Progress
	{
		get
		{
			return new Progress(Value, Maximum, IncrementalProgress);
		}
		set
		{
			Value = value.Value;
			Maximum = value.Maximum;
			IncrementalProgress = value.IncrementalProgressBeforeDelay;
		}
	}

	public ProgressDisplayType ProgressDisplayType
	{
		get { return displayType; }
		set
		{
			displayType = value;
			Invalidate();
		}
	}

	public string RightText { get { return rightText; } set { rightText = value; Invalidate(); } }

	public long Value
	{
		get { return value; }
		set
		{
			this.value = value;
			Invalidate();
		}
	}

	private Color barColor = Color.Blue;

	private Color borderColor;

	private ProgressDisplayType displayType = ProgressDisplayType.Percentage;

	private long incrementalProgress = 0;

	private string leftText, rightText;

	private long maximum = 100;

	private long value = 0;

	protected override void OnPaint(PaintEventArgs e)
	{
		string centerText;
		switch (ProgressDisplayType)
		{
			case ProgressDisplayType.None:
				centerText = "";
				break;

			case ProgressDisplayType.Percentage:
				centerText = Math.Round(((double)Value / (double)Maximum * 100)) + "%";
				break;

			case ProgressDisplayType.Numeric:
				centerText = Value.ToUnitString(true) + " / " + Maximum.ToUnitString(true);
				break;

			case ProgressDisplayType.Both:
				centerText = Math.Round(((double)Value / (double)Maximum * 100)) + "% (" + Value.ToUnitString(true) + " / " + Maximum.ToUnitString(true) + ")";
				break;

			default:
				centerText = "";
				break;
		}
		base.OnPaint(e);
		e.Graphics.Clear(BackColor);
		if (Maximum != 0)
		{
			e.Graphics.FillRectangle(new SolidBrush(BarColor), 0, 0, Value * Width / Maximum, Height);
			e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(128, BarColor)), Value * Width / Maximum, 0, IncrementalProgress * Width / Maximum, Height);
		}
		if (BorderStyle == BorderStyle.FixedSingle)
			ControlPaint.DrawBorder(e.Graphics, ClientRectangle, BorderColor, ButtonBorderStyle.Solid);
		Brush brush;
		if (BarColor.R + BarColor.G + BarColor.B > 128 * 3 && BackColor.R + BackColor.G + BackColor.B > 128 * 3)
			brush = new SolidBrush(Color.Black);
		else
			brush = new SolidBrush(Color.White);
		var rect = new Rectangle(0, 0, Width, Height);
		rect.X += Padding.Left;
		rect.Y += Padding.Top;
		rect.Width -= Padding.Left + Padding.Right;
		rect.Height -= Padding.Top + Padding.Bottom;
		e.Graphics.DrawString(LeftText, Font, brush, rect, new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
		e.Graphics.DrawString(RightText, Font, brush, rect, new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center });
		e.Graphics.DrawString(centerText, Font, brush, rect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
	}

	private void GameProgressBar_SizeChanged(object sender, EventArgs e)
	{
		Invalidate();
	}
}

[Flags]
public enum ProgressDisplayType
{
	None = 0,
	Percentage = 1,
	Numeric = 2,
	Both = 3,
}