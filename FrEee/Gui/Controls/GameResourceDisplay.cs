using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee;

namespace FrEee.Gui.Controls
{
	public partial class GameResourceDisplay : UserControl
	{
		public GameResourceDisplay()
		{
			InitializeComponent();
		}

		private Color resourceColor;

		protected override void OnPaint(PaintEventArgs e)
		{
			lblAmount.ForeColor = ResourceColor;
			lblAmount.Text = Amount.ToUnitString();
			if (Change != null)
			{
				lblAmount.Text += " (";
				if (Change.Value >= 0)
					lblAmount.Text += "+";
				lblAmount.Text += Change.Value.ToUnitString();
				lblAmount.Text += ")";
			}
			base.OnPaint(e);
		}

		public Color ResourceColor
		{
			get { return resourceColor; }
			set
			{
				resourceColor = value;
				Invalidate();
			}
		}

		public int Amount { get; set; }

		public int? Change { get; set; }
	}
}
