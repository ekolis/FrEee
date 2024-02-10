using FrEee.Objects.GameState;
using FrEee.WinForms.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms;

public partial class GenericPickerForm : GameForm
{
	public GenericPickerForm(IEnumerable<object> objects)
	{
		InitializeComponent();

		lstObjects.Initialize(32, 32);
		foreach (var obj in objects)
		{
			Image pic = null;
			if (obj is IPictorial)
				pic = (obj as IPictorial).Icon;
			lstObjects.AddItemWithImage(null, obj.ToString(), obj, pic);
		}
		lstObjects.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

		try { this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); } catch { }
	}

	public object SelectedObject { get; private set; }

	private void btnCancel_Click(object sender, EventArgs e)
	{
		DialogResult = DialogResult.Cancel;
		Close();
	}

	private void btnOk_Click(object sender, EventArgs e)
	{
		DialogResult = DialogResult.OK;
		Close();
	}

	private void lstObjects_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		var item = lstObjects.GetItemAt(e.X, e.Y);
		if (item != null)
		{
			SelectedObject = item.Tag;
			DialogResult = DialogResult.OK;
			Close();
		}
	}

	private void lstSpaceObjects_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
	{
		if (e.IsSelected)
			SelectedObject = e.Item.Tag;
		else
			SelectedObject = null;
	}
}