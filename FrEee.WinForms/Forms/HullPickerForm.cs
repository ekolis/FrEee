using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
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
	public partial class HullPickerForm : Form
	{
		public HullPickerForm()
		{
			InitializeComponent();
			ddlVehicleType.Items.Add(new { Name = "All", VehicleType = VehicleTypes.All });
			ddlVehicleType.Items.Add(new { Name = "Ships", VehicleType = VehicleTypes.Ship });
			ddlVehicleType.Items.Add(new { Name = "Bases", VehicleType = VehicleTypes.Base });
			ddlVehicleType.Items.Add(new { Name = "Fighters", VehicleType = VehicleTypes.Fighter });
			ddlVehicleType.Items.Add(new { Name = "Troops", VehicleType = VehicleTypes.Troop });
			ddlVehicleType.Items.Add(new { Name = "Satellites", VehicleType = VehicleTypes.Satellite });
			ddlVehicleType.Items.Add(new { Name = "Mines", VehicleType = VehicleTypes.Mine });
			ddlVehicleType.Items.Add(new { Name = "Drones", VehicleType = VehicleTypes.Drone });
			ddlVehicleType.Items.Add(new { Name = "Weapon Platforms", VehicleType = VehicleTypes.WeaponPlatform });
			ddlVehicleType.SelectedItem = ddlVehicleType.Items[0];
		}

		/// <summary>
		/// The hull that the user selected.
		/// </summary>
		public IHull Hull { get; private set; }

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

		public VehicleTypes SelectedVehicleType
		{
			get
			{
				if (ddlVehicleType.SelectedItem == null)
					return VehicleTypes.None;
				return ((dynamic)ddlVehicleType.SelectedItem).VehicleType;
			}
			set
			{
				ddlVehicleType.SelectedItem = ddlVehicleType.Items.Cast<dynamic>().SingleOrDefault(item => item.VehicleType == value);
				Bind();
			}
		}

		private void Bind()
		{
			var vt = (VehicleTypes)((dynamic)ddlVehicleType.SelectedItem).VehicleType;
			var hulls = Empire.Current.UnlockedItems.OfType<IHull>().Where(h => vt.HasFlag(h.VehicleType));
			iHullBindingSource.DataSource = hulls.ToArray();
			gridHulls.Visible = hulls.Any();
		}

		private void ddlVehicleType_SelectedIndexChanged(object sender, EventArgs e)
		{
			Bind();
		}

		private void gridHulls_SelectionChanged(object sender, EventArgs e)
		{
			if (gridHulls.SelectedRows.Count > 0)
				Hull = (IHull<IVehicle>)gridHulls.SelectedRows[0].DataBoundItem;
			else
				Hull = null;
		}

		private void gridHulls_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			// pick the hull and close the form
			Hull = (IHull)gridHulls.Rows[e.RowIndex].DataBoundItem;
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
