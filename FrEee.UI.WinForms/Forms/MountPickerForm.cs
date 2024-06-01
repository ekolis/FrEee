using FrEee.Objects.Technology;
using FrEee.Modding;
using FrEee.Extensions;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FrEee.UI.WinForms.Forms;

public partial class MountPickerForm : GameForm
{
	public MountPickerForm(IHull hull)
	{
		InitializeComponent();
		this.hull = hull;
		Bind();

		try { this.Icon = new Icon(FrEee.UI.WinForms.Properties.Resources.FrEeeIcon); } catch { }
	}

	/// <summary>
	/// The mount that the user selected.
	/// </summary>
	public Mount Mount { get; private set; }

	/// <summary>
	/// The hull that the user is using.
	/// Some mounts have restrictions on what hulls can use them.
	/// So don't show any mounts that the hull can't use.
	/// </summary>
	private IHull hull;

	private void Bind()
	{
		var mounts = Mod.Current.Mounts.Where(m => hull.CanUseMount(m) && m.IsUnlocked());
		mountBindingSource.DataSource = mounts.ToArray();
		gridMounts.Visible = mounts.Any();
		// TODO - show mount stats somewhere...
	}

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

	private void gridMounts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		// pick the mount and close the form
		Mount = (Mount)gridMounts.Rows[e.RowIndex].DataBoundItem;
		DialogResult = DialogResult.OK;
		Close();
	}

	private void gridMounts_SelectionChanged(object sender, EventArgs e)
	{
		if (gridMounts.SelectedRows.Count > 0)
			Mount = (Mount)gridMounts.SelectedRows[0].DataBoundItem;
		else
			Mount = null;
	}
}