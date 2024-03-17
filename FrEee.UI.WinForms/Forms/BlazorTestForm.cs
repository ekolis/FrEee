using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrEee.UI.WinForms.Controls;
using FrEee.UI.WinForms.Controls.Blazor;

namespace FrEee.UI.WinForms.Forms
{
	public partial class BlazorTestForm : Form
	{
		public BlazorTestForm()
		{
			InitializeComponent();
		}

		private T ShowControl<T>() where T : Control, new()
		{
			panel.Controls.Clear();
			var c = new T();
			c.Dock = DockStyle.Fill;
			panel.Controls.Add(c);
			return c;
		}

		private void btnProgressBar_Click(object sender, EventArgs e)
		{
			ShowControl<GameProgressBar>();
		}

		private void btnResourceDisplay_Click(object sender, EventArgs e)
		{
			ShowControl<ResourceDisplay>();
		}
	}
}
