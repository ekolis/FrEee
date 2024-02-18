using FrEee.Modding;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms;

public partial class CultureComparisonForm : GameForm
{
	public CultureComparisonForm()
	{
		InitializeComponent();
		try { base.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); }
		catch { }
		foreach (DataGridViewColumn col in gridCultures.Columns)
		{
			col.DefaultCellStyle.BackColor = Color.Black;
			col.DefaultCellStyle.ForeColor = Color.White;
			if (gridCultures.Columns.IndexOf(col) > 0)
				col.Width = 50;
		}
		cultureBindingSource.DataSource = Mod.Current.Cultures;
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void gridCultures_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
	{
		if (e.ColumnIndex > 0)
		{
			var val = (int)e.Value;
			if (val > 0)
				e.CellStyle.ForeColor = Color.FromArgb(128, 255, 128);
			else if (val < 0)
				e.CellStyle.ForeColor = Color.FromArgb(255, 128, 128);
		}
	}
}