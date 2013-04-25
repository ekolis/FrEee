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
	public partial class ResourceDisplay : UserControl
	{
		public ResourceDisplay()
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

		private int amount;
		public int Amount { get { return amount; } set { amount = value; Invalidate(); } }

		private int? change;
		public int? Change { get { return change; } set { change = value; Invalidate(); } }
	}
}
