using FrEee.Interfaces;
using FrEee.WinForms.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms;

public partial class SpaceObjectPickerForm : GameForm
{
	public SpaceObjectPickerForm(IEnumerable<ISpaceObject> objects)
	{
		InitializeComponent();

		lstSpaceObjects.Initialize(32, 32);
		foreach (var sobj in objects)
			lstSpaceObjects.AddItemWithImage(null, sobj.Name, sobj, sobj.Icon);
		lstSpaceObjects.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

		try { this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); } catch { }
	}

	public ISpaceObject SelectedSpaceObject { get; private set; }

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

	private void lstSpaceObjects_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
	{
		if (e.IsSelected)
			SelectedSpaceObject = (ISpaceObject)e.Item.Tag;
		else
			SelectedSpaceObject = null;
	}

	private void lstSpaceObjects_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		var item = lstSpaceObjects.GetItemAt(e.X, e.Y);
		if (item != null)
		{
			SelectedSpaceObject = (ISpaceObject)item.Tag;
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}