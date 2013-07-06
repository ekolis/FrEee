using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Technology;
using FrEee.Modding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
	public partial class MountPickerForm : Form
	{
		public MountPickerForm(IHull hull)
		{
			InitializeComponent();
			this.hull = hull;
			Bind();

			this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon);
		}

		/// <summary>
		/// The hull that the user is using.
		/// Some mounts have restrictions on what hulls can use them.
		/// So don't show any mounts that the hull can't use.
		/// </summary>
		private IHull hull;

		/// <summary>
		/// The mount that the user selected.
		/// </summary>
		public Mount Mount { get; private set; }

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

		private void Bind()
		{
			var mounts = Empire.Current.UnlockedItems.OfType<Mount>().Where(m =>
				m.VehicleTypes.HasFlag(hull.VehicleType) &&
				(m.MinimumVehicleSize == null || m.MinimumVehicleSize <= hull.Size) &&
				(m.MaximumVehicleSize == null || m.MinimumVehicleSize >= hull.Size));
			mountBindingSource.DataSource = mounts.ToArray();
			gridMounts.Visible = mounts.Any();
			// TODO - show mount stats somewhere...
		}

		private void gridMounts_SelectionChanged(object sender, EventArgs e)
		{
			if (gridMounts.SelectedRows.Count > 0)
				Mount = (Mount)gridMounts.SelectedRows[0].DataBoundItem;
			else
				Mount = null;
		}

		private void gridMounts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			// pick the mount and close the form
			Mount = (Mount)gridMounts.Rows[e.RowIndex].DataBoundItem;
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
