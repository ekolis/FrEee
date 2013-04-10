using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrEee.Gui.Controls
{
	public partial class GamePanel : Panel
	{
		public GamePanel()
		{
			InitializeComponent();
			this.SizeChanged += GamePanel_SizeChanged;
			BackColor = Color.Black;
			ForeColor = Color.White;
			BorderColor = Color.CornflowerBlue;
		}

		void GamePanel_SizeChanged(object sender, EventArgs e)
		{
			Invalidate();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
			if (BorderStyle == BorderStyle.FixedSingle)
				ControlPaint.DrawBorder(pe.Graphics, ClientRectangle, BorderColor, ButtonBorderStyle.Solid);
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
