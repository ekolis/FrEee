using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee.WinForms.Utility.Extensions;
using FrEee.Utility.Extensions;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Technology;

namespace FrEee.WinForms.Forms
{
	public partial class DesignListForm : Form
	{
		public DesignListForm()
		{
			InitializeComponent();
			BindVehicleTypeList();
			BindDesignList();

			try {this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon);} catch {}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void BindVehicleTypeList()
		{
			ddlVehicleType.Items.Clear();
			ddlVehicleType.Items.Add(new { Name = "All", VehicleTypes = VehicleTypes.All });
			ddlVehicleType.Items.Add(new { Name = "Ships/Bases", VehicleTypes = VehicleTypes.Ship | VehicleTypes.Base });
			ddlVehicleType.Items.Add(new { Name = "Units", VehicleTypes = VehicleTypes.Fighter | VehicleTypes.Satellite | VehicleTypes.Drone | VehicleTypes.Troop | VehicleTypes.Mine | VehicleTypes.WeaponPlatform });
			ddlVehicleType.Items.Add(new { Name = "Space", VehicleTypes = VehicleTypes.Ship | VehicleTypes.Base | VehicleTypes.Fighter | VehicleTypes.Satellite | VehicleTypes.Drone | VehicleTypes.Mine });
			ddlVehicleType.Items.Add(new { Name = "Ground", VehicleTypes = VehicleTypes.Troop | VehicleTypes.WeaponPlatform });
			ddlVehicleType.SelectedItem = ddlVehicleType.Items[0];
		}

		private void BindDesignList()
		{
			var emp = Galaxy.Current.CurrentEmpire;
			IEnumerable<IDesign> designs = emp.KnownDesigns.OrderBy(d => d.Role).ThenBy(d => d.Name).ThenBy(d => d.Iteration);

			// filter by vehicle type
			var item = (dynamic)ddlVehicleType.SelectedItem;
			var vehicleTypeFilter = (VehicleTypes)item.VehicleTypes;
			designs = designs.Where(d => vehicleTypeFilter.HasFlag(d.VehicleType));

			// filter by ours/foreign (using an exclusive or)
			designs = designs.Where(d => d.Owner == emp ^ chkForeign.Checked);

			// filter by obsoleteness
			if (chkHideObsolete.Checked)
				designs = designs.Where(d => !d.IsObsolete);

			// display it!
			lstDesigns.Initialize(32, 32);
			foreach (var design in designs)
				lstDesigns.AddItemWithImage(design.Role, design.Name, design, design.Icon, design.Cost.Sum(kvp => kvp.Value).ToUnitString());
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
				designReport.Design = (IDesign)lstDesigns.SelectedItems[0].Tag;
			else
				designReport.Design = null;
		}

		private void btnCreate_Click(object sender, EventArgs e)
		{
			var form = new VehicleDesignForm();
			this.ShowChildForm(form);
			if (form.DialogResult == DialogResult.OK)
				BindDesignList();
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			if (lstDesigns.SelectedItems.Count == 1)
			{
				var old = (IDesign)lstDesigns.SelectedItems[0].Tag;
				var copy = old.CopyAndAssignNewID();
				copy.TurnNumber = Galaxy.Current.TurnNumber;
				copy.Owner = Empire.Current;
				copy.Iteration++;
				copy.VehiclesBuilt = 0;
				copy.Components.Clear();
				foreach (var mct in old.Components)
				{
					copy.Components.Add(new MountedComponentTemplate(copy, mct.ComponentTemplate, mct.Mount));
				}
				var form = new VehicleDesignForm();
				form.Design = copy;
				this.ShowChildForm(form);
				if (form.DialogResult == DialogResult.OK)
					BindDesignList();
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			if (lstDesigns.SelectedItems.Count == 1)
			{
				var d = (IDesign)lstDesigns.SelectedItems[0].Tag;
				if (d.Owner != Empire.Current)
				{
					MessageBox.Show("You cannot edit alien designs. You can, however, copy them.");
				}
				else
				{
					if (d.TurnNumber < Galaxy.Current.TurnNumber)
					{
						// TODO - let player edit old designs only if they have never been added to a queue (like in SE4)?
						MessageBox.Show("You cannot edit a design that was created on a prior turn.");
					}
					else
					{
						var form = new VehicleDesignForm();
						form.Design = d;
						this.ShowChildForm(form);
						if (form.DialogResult == DialogResult.OK)
							BindDesignList();
					}
				}
			}
		}

		private void btnUpgrade_Click(object sender, EventArgs e)
		{
			if (lstDesigns.SelectedItems.Count == 1)
			{
				var old = (IDesign)lstDesigns.SelectedItems[0].Tag;
				var copy = old.LatestVersion;
				var form = new VehicleDesignForm();
				form.Design = copy;
				this.ShowChildForm(form);
				if (form.DialogResult == DialogResult.OK)
					BindDesignList();
			}
		}

		private void btnObsolete_Click(object sender, EventArgs e)
		{
			if (lstDesigns.SelectedItems.Count >= 1)
			{
				foreach (IDesign d in lstDesigns.SelectedItems.Cast<ListViewItem>().Select(item => item.Tag))
				{
					d.IsObsolete = !d.IsObsolete;
					foreach (var cmd in Empire.Current.Commands.OfType<SetObsoleteFlagCommand>().Where(cmd => cmd.Design == d && cmd.IsObsolete != d.IsObsolete).ToArray())
						Empire.Current.Commands.Remove(cmd);
					if (!Empire.Current.Commands.OfType<SetObsoleteFlagCommand>().Where(cmd => cmd.Design == d && cmd.IsObsolete == d.IsObsolete).Any())
						Empire.Current.Commands.Add(new SetObsoleteFlagCommand(d, d.IsObsolete));
				}
				BindDesignList();
			}
		}

		private void btnSpaceSimulator_Click(object sender, EventArgs e)
		{
			this.ShowChildForm(new CombatSimulatorForm(false));
		}

		private void btnGroundSimulator_Click(object sender, EventArgs e)
		{
			this.ShowChildForm(new CombatSimulatorForm(true));
		}

        private void btnStrategy_Click(object sender, EventArgs e)
        {
            if (lstDesigns.SelectedItems.Count == 1)
            {
                var d = (IDesign)lstDesigns.SelectedItems[0].Tag;
                if (d.Owner != Empire.Current)
                {
                    MessageBox.Show("You cannot edit alien design Strategies.");
                }
                else
                {                   
                        var form = new MogreCombatRender.StrategiesDesigner.StratMainForm(d);
                        //form.Design = d;
                        this.ShowChildForm(form);
                        if (form.DialogResult == DialogResult.OK)
                            BindDesignList();                  
                }
            }
        }
	}
}
