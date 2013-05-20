using System;
using System.Drawing;
using System.Windows.Forms;
using FrEee.Utility;
using FrEee.Utility.Extensions;

namespace FrEee.WinForms.Controls
{
	public partial class GameProgressBar : UserControl
	{
		public GameProgressBar()
		{
			InitializeComponent();
			this.SizeChanged += GameProgressBar_SizeChanged;
		}

		void GameProgressBar_SizeChanged(object sender, EventArgs e)
		{
			Invalidate();
		}

		private int value = 0;
		public int Value
		{
			get { return value; }
			set
			{
				this.value = value;
				Invalidate();
			}
		}

		private int maximum = 100;
		public int Maximum
		{
			get { return maximum; }
			set
			{
				maximum = value;
				Invalidate();
			}
		}

		private Color barColor = Color.Blue;
		public Color BarColor
		{
			get { return barColor; }
			set
			{
				barColor = value;
				Invalidate();
			}
		}

		public string LeftText { get { return lblLeft.Text; } set { lblLeft.Text = value; } }

		public string RightText { get { return lblRight.Text; } set { lblRight.Text = value; } }

		protected override void OnPaint(PaintEventArgs e)
		{
			lblCenter.Text = Value.ToUnitString() + "/" + Maximum.ToUnitString() + " (" + Math.Round(((double)Value / (double)Maximum * 100)) + "%)";
			lblCenter.Visible = lblLeft.Visible = lblRight.Visible = Width > 100;
			base.OnPaint(e);
			e.Graphics.Clear(BackColor);
			e.Graphics.FillRectangle(new SolidBrush(BarColor), 0, 0, Value * Width / Maximum, Height);
			if (BorderStyle == BorderStyle.FixedSingle)
				ControlPaint.DrawBorder(e.Graphics, ClientRectangle, BorderColor, ButtonBorderStyle.Solid);
		}

		private Color borderColor;
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
	}
}
