using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
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
	public partial class DesignListForm : Form
	{
		public DesignListForm()
		{
			InitializeComponent();
			BindVehicleTypeList();
			BindDesignList();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void BindVehicleTypeList()
		{
			ddlVehicleType.Items.Clear();
			ddlVehicleType.Items.Add(new { Name = "All", Value = VehicleTypes.All });
			ddlVehicleType.Items.Add(new { Name = "Ships/Bases", Value = VehicleTypes.Ship | VehicleTypes.Base });
			ddlVehicleType.Items.Add(new { Name = "Units", Value = VehicleTypes.Fighter | VehicleTypes.Satellite | VehicleTypes.Drone | VehicleTypes.Troop | VehicleTypes.Mine | VehicleTypes.WeaponPlatform });
			ddlVehicleType.Items.Add(new { Name = "Space", Value = VehicleTypes.Ship | VehicleTypes.Base |  VehicleTypes.Fighter | VehicleTypes.Satellite | VehicleTypes.Drone | VehicleTypes.Mine });
			ddlVehicleType.Items.Add(new { Name = "Ground", Value = VehicleTypes.Troop | VehicleTypes.WeaponPlatform });
			ddlVehicleType.SelectedIndex = 0;
		}

		private void BindDesignList()
		{
			var emp = Galaxy.Current.CurrentEmpire;
			IEnumerable<IDesign> designs = emp.KnownDesigns;

			// filter by vehicle type
			var vehicleTypeFilter = (VehicleTypes)ddlVehicleType.SelectedValue;
			designs = designs.Where(d => vehicleTypeFilter.HasFlag(d.VehicleType));

			// filter by ours/foreign (using an exclusive or)
			designs = designs.Where(d => d.Owner == emp ^ !chkForeign.Checked);

			// filter by obsoleteness
			if (chkHideObsolete.Checked)
				designs = designs.Where(d => !d.IsObsolete);
		}

		private void ddlVehicleType_SelectedIndexChanged(object sender, EventArgs e)
		{
			BindDesignList();
		}

		private void chkHideObsolete_CheckedChanged(object sender, EventArgs e)
		{
			BindDesignList();
		}

		private void chkForeign_CheckedChanged(object sender, EventArgs e)
		{
			BindDesignList();
		}

		private void lstDesigns_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstDesigns.SelectedItems.Count > 0)
				designReport.Design = (IDesign)lstDesigns.SelectedItems[0];
			else
				designReport.Design = null;
		}
	}
}
