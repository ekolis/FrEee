using FrEee.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Utility.Extensions;
using FrEee.Modding;
using FrEee.Modding.Templates;
using FrEee.Game.Objects.Civilization;

namespace FrEee.WinForms.Forms
{
	public partial class VehicleDesignForm : Form
	{
		public VehicleDesignForm()
		{
			InitializeComponent();
		}

		private IDesign design;

		public IDesign Design
		{
			get { return design; }
			set
			{
				design = value;
				Bind();
			}
		}

		private void Bind()
		{
			// bind hull name
			btnHull.Text = Design == null || Design.Hull == null ? "(choose)" : Design.Hull.Name;

			// bind role
			// TODO - populate role dropdown from design types text file and other existing designs' roles
			ddlRole.Text = Design == null ? null : Design.Role;

			// bind name
			// TODO - populate name dropdown from design names text file
			ddlName.Text = Design == null ? null : Design.Name;

			// bind components available list
			lstComponentsAvailable.InitializeImageLists(32, 32);
			lstComponentsAvailable.Items.Clear();
			if (Design != null)
			{
				IEnumerable<ComponentTemplate> comps = Mod.Current.ComponentTemplates;

				// filter by vehicle type
				comps = comps.Where(comp => comp.VehicleTypes.HasFlag(Design.VehicleType));

				// TODO - filter by can-use-mount?

				// filter by only-latest
				if (chkOnlyLatest.Checked)
					comps = comps.GroupBy(comp => comp.Family).Select(g => g.OrderByDescending(comp => comp.RomanNumeral).First());

				var complist = comps.ToList();
				foreach (var comp in complist)
					lstComponentsAvailable.AddItemWithImage(comp.Group, comp.Name, comp.Icon, complist.IndexOf(comp).ToString());
			}

			// bind components installed list
			lstComponentsInstalled.InitializeImageLists(32, 32);
			lstComponentsInstalled.Items.Clear();
			if (Design != null)
			{
				foreach (var g in Design.Components.GroupBy(mct => mct))
					lstComponentsInstalled.AddItemWithImage(g.First().ComponentTemplate.Group, g.Count() + "x " + g.First().ComponentTemplate.Name, g.First().Icon, Design.Components.IndexOf(g.First()).ToString());
			}

			// bind portrait
			picPortrait.Image = Design == null ? null : Design.Hull.GetPortrait(Empire.Current.ShipsetPath);

			// bind stats
			if (Design == null)
			{
				foreach (Control ctl in pnlStats.Controls)
					ctl.Visible = false;
			}
			else
			{
				foreach (Control ctl in pnlStats.Controls)
					ctl.Visible = true;

				txtSpaceFree.Text = Design.SpaceFree.Kilotons() + " / " + Design.Hull.Size.Kilotons();
				resCostMin.Amount = Design.Cost["Minerals"];
				resCostOrg.Amount = Design.Cost["Organics"];
				resCostRad.Amount = Design.Cost["Radioactives"];
				txtSpeed.Text = Design.Speed.ToString() + " sectors/turn";
				txtSupplyStorage.Text = Design.GetAbilityValue("Supply Storage");
				txtSupplyUsage.Text = Design.SupplyUsage.ToString();
				txtRange.Text = (Design.GetAbilityValue("Supply Storage").ToInt() / Design.SupplyUsage) + " sectors";
				txtShields.Text = Design.ShieldHitpoints + " shields (+" + Design.ShieldRegeneration + " regen)";
				txtArmor.Text = Design.ArmorHitpoints + " armor";
				txtHull.Text = Design.HullHitpoints + " hull";
				txtAccuracy.Text = Design.Accuracy.ToString("+0") + "%";
				txtEvasion.Text = Design.Evasion.ToString("+0") + "%";
				txtCargo.Text = Design.CargoCapacity.Kilotons();
			}

			// bind warnings
			lstWarnings.Items.Clear();
			if (Design != null)
			{
				foreach (var w in Design.Warnings)
					lstWarnings.Items.Add(w);
			}
		}

		private void btnHull_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			var form = new HullPickerForm();
			form.ShowDialog();
			Cursor = Cursors.Default;
			if (form.DialogResult == DialogResult.OK)
			{
				if (Design != null && Design.Hull != null && form.Hull.VehicleType != Design.Hull.VehicleType)
				{
					// Changing vehicle types requires starting over, so warn the user
					if (MessageBox.Show("Changing the vehicle type requires starting over with your design. Abandon your old design?", "FrEee", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
					{
						Design = FrEee.Game.Objects.Vehicles.Design.Create(form.Hull.VehicleType);
						Design.Hull = form.Hull;
						Bind();
					}
				}
				else if (Design != null)
				{
					Design.Hull = form.Hull;
					Bind();
				}
				else
				{
					Design = FrEee.Game.Objects.Vehicles.Design.Create(form.Hull.VehicleType);
					Design.Hull = form.Hull;
					Bind();
				}
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			// TODO - implement save
		}

		private void btnMount_Click(object sender, EventArgs e)
		{
			// TODO - implement use mount
		}
	}
}
