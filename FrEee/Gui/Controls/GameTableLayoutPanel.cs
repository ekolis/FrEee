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
	public partial class GameTableLayoutPanel : TableLayoutPanel
	{
		public GameTableLayoutPanel()
		{
			InitializeComponent();
			DoubleBuffered = true;
		}
	}
}
