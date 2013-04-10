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
	public partial class GameButton : Button
	{
		public GameButton()
		{
			InitializeComponent();
			UseVisualStyleBackColor = false;
			BackColor = Color.Black;
			ForeColor = Color.CornflowerBlue;
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}
	}
}
