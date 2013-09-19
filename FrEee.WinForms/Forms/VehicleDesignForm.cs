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
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Commands;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using System.Threading;
using System.Windows.Threading;

namespace FrEee.WinForms.Forms
{
	public partial class VehicleDesignForm : Form
	{
		public VehicleDesignForm()
		{
			InitializeComponent();
			ShowComponentDetails(null);

			try {this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon);} catch {}
		}

		public VehicleDesignForm(IHull<IVehicle> hull)
		{
			InitializeComponent();
			ShowComponentDetails(null);
			Design = FrEee.Game.Objects.Vehicles.Design.Create(hull);

			try {this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon);} catch {}
		}

		public VehicleDesignForm(IDesign design)
		{
			InitializeComponent();
			ShowComponentDetails(null);
			Design = design;

			try {this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon);} catch {}
		}

		private IDesign design;

		public IDesign Design
		{
			get { return design; }
			set
			{
				design = value;
				BindAll();
			}
		}

		private void BindDesignData()
		{
			// bind hull name
			btnHull.Text = Design == null || Design.Hull == null ? "(choose)" : Design.Hull.Name;

			// bind role
			// TODO - populate role dropdown from design types text file and other existing designs' roles
			ddlRole.Text = Design == null ? null : Design.Role;

			// bind name
			// TODO - populate name dropdown from design names text file
			ddlName.Text = Design == null ? null : Design.BaseName;

			// bind iteration
			txtIteration.Text = Design == null || Design.Iteration == 1 ? "" : Design.Iteration.ToRomanNumeral();

			// bind portrait
			picPortrait.Image = Design == null || Design.Hull == null ? null : Design.Hull.GetPortrait(Empire.Current.ShipsetPath);

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
				resCostMin.Amount = Design.Cost[Resource.Minerals];
				resCostOrg.Amount = Design.Cost[Resource.Organics];
				resCostRad.Amount = Design.Cost[Resource.Radioactives];
				txtSpeed.Text = Design.Speed.ToString() + " sectors/turn";
				txtSupplyStorage.Text = Design.GetAbilityValue("Supply Storage");
				txtSupplyUsage.Text = Design.SupplyUsagePerSector.ToString();
				if (Design.Speed == 0)
					txtRange.Text = "0 sectors";
				else if (Design.SupplyUsagePerSector == 0 || Design.HasAbility("Quantum Reactor"))
					txtRange.Text = "Unlimited";
				else
					txtRange.Text = (Design.GetAbilityValue("Supply Storage").ToInt() / Design.SupplyUsagePerSector) + " sectors";
				txtShields.Text = Design.ShieldHitpoints + " shields (+" + Design.ShieldRegeneration + " regen)";
				txtArmor.Text = Design.ArmorHitpoints + " armor";
				txtHull.Text = Design.HullHitpoints + " hull";
				if (Design.Accuracy > 0)
					txtAccuracy.Text = Design.Accuracy.ToString("+0") + "%";
				else
					txtAccuracy.Text = Design.Accuracy.ToString("0") + "%";
				if (Design.Evasion > 0)
					txtEvasion.Text = Design.Evasion.ToString("+0") + "%";
				else
					txtEvasion.Text = Design.Evasion.ToString("0") + "%";
				txtCargo.Text = Design.CargoCapacity.Kilotons();
			}

			// bind warnings
			lstWarnings.Items.Clear();
			if (Design != null)
			{
				foreach (var w in Design.Warnings)
					lstWarnings.Items.Add(w);
			}

			// show GUI
			ddlName.Enabled = Design != null;
			ddlRole.Enabled = Design != null;
			btnMount.Enabled = Design != null;
			btnClearMount.Enabled = Design != null;
		}

		private void BindAvailableComponents()
		{
			lstComponentsAvailable.Initialize(32, 32);
			if (Design != null)
			{
				IEnumerable<ComponentTemplate> comps = Empire.Current.UnlockedItems.OfType<ComponentTemplate>();

				// filter by vehicle type
				comps = comps.Where(comp => comp.VehicleTypes.HasFlag(Design.VehicleType));

				// filter by can-use-mount if the user wants to
				if (chkFilterByMount.Checked)
				{
					comps = comps.Where(comp => comp.CanUseMount(CurrentMount));
				}

				// filter by only-latest
				if (chkOnlyLatest.Checked)
					comps = comps.OnlyLatest(comp => comp.Family);

				var complist = comps.ToList();
				foreach (var comp in complist)
				{
					MountedComponentTemplate mct;
					if (comp.CanUseMount(CurrentMount))
						mct = new MountedComponentTemplate(comp, CurrentMount);
					else
						mct = new MountedComponentTemplate(comp);
					lstComponentsAvailable.AddItemWithImage(comp.Group, mct.Size.Kilotons() + " " + mct.Name, mct, mct.Icon);
				}
			}
		}

		private void BindInstalledComponents()
		{
			lstComponentsInstalled.Initialize(32, 32);
			if (Design != null)
			{
				foreach (var g in Design.Components.GroupBy(mct => mct))
					lstComponentsInstalled.AddItemWithImage(g.First().ComponentTemplate.Group, g.Count() + "x " + g.First().Size.Kilotons() + " " + g.First().ToString(), g.First(), g.First().Icon);
			}
		}

		private void BindAll()
		{
			BindDesignData();
			BindAvailableComponents();
			BindInstalledComponents();
		}

		private void btnHull_Click(object sender, EventArgs e)
		{
			var form = new HullPickerForm();
			if (Design != null && Design.Hull != null)
				form.SelectedVehicleType = Design.Hull.VehicleType;
			this.ShowChildForm(form);
			Cursor = Cursors.Default;
			if (form.DialogResult == DialogResult.OK)
			{
				if (Design != null && Design.Hull != null && form.Hull.VehicleType != Design.Hull.VehicleType)
				{
					// Changing vehicle types requires starting over, so warn the user
					if (MessageBox.Show("Changing the vehicle type requires starting over with your design. Abandon your old design?", "FrEee", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
					{
						var d = FrEee.Game.Objects.Vehicles.Design.Create(form.Hull.VehicleType);
						d.TurnNumber = Galaxy.Current.TurnNumber;
						d.Owner = Empire.Current;
						d.Hull = form.Hull;
						Design = d;
						BindAll();
					}
				}
				else if (Design != null)
				{
					Design.Hull = form.Hull;
					BindDesignData();
				}
				else
				{
					var d = FrEee.Game.Objects.Vehicles.Design.Create(form.Hull.VehicleType);
					d.TurnNumber = Galaxy.Current.TurnNumber;
					d.Owner = Empire.Current;
					d.Hull = form.Hull;
					Design = d;
					BindAll();
				}
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			abort = true;
			Close();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (TrySaveDesign())
			{
				abort = true;
				Close();
			}
		}

		private bool TrySaveDesign()
		{
			if (Design == null)
				return true; // allow player to cancel the dialog by pressing save if he hasn't started on a design yet
			if (Design.Warnings.Any())
			{
				MessageBox.Show("You cannot save your design while there are warnings.");
				return false;
			}
			else
			{
				// TODO - let player edit old designs only if they have never been added to a queue (like in SE4)?
				if (!Empire.Current.KnownDesigns.Contains(Design))
				{
					// add design here
					Empire.Current.KnownDesigns.Add(Design);

					// tell server to add design too so we can still see it next turn
					Empire.Current.Commands.Add(Design.CreateCreationCommand());
				}

				// done
				DialogResult = DialogResult.OK;
				return true;
			}
		}

		private Mount mount;

		private void btnMount_Click(object sender, EventArgs e)
		{
			if (Design == null || Design.Hull == null)
				return; // no need to pick mounts now

			var form = new MountPickerForm(Design.Hull);
			this.ShowChildForm(form);
			if (form.DialogResult == DialogResult.OK)
				CurrentMount = form.Mount;
		}

		private void chkOnlyLatest_CheckedChanged(object sender, EventArgs e)
		{
			BindAvailableComponents();
		}

		private void lstComponentsAvailable_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
		{
			if (e.Item == null)
				ShowComponentDetails(null);
			else
				ShowComponentDetails((MountedComponentTemplate)e.Item.Tag);
		}

		private void lstComponentsInstalled_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
		{
			if (e.Item == null)
				ShowComponentDetails(null);
			else
				ShowComponentDetails((MountedComponentTemplate)e.Item.Tag);
		}

		private void ShowComponentDetails(MountedComponentTemplate mct)
		{
			if (mct == null)
			{
				picDetailIcon.Image = null;
				txtDetailName.Text = "(no component)";
				txtDetailSize.Text = null;
				resDetailMin.Amount = 0;
				resDetailOrg.Amount = 0;
				resDetailRad.Amount = 0;
				txtDetailDescription.Text = null;
			}
			else
			{
				var v = mct;
				picDetailIcon.Image = v.ComponentTemplate.Icon;
				txtDetailName.Text = v.Name;
				txtDetailSize.Text = v.Size.Kilotons();
				resDetailMin.Amount = v.Cost[Resource.Minerals];
				resDetailOrg.Amount = v.Cost[Resource.Organics];
				resDetailRad.Amount = v.Cost[Resource.Radioactives];
				txtDetailDescription.Text = v.ComponentTemplate.Description;
			}
		}

		/// <summary>
		/// Are we closing the form no matter what, even if there's a design?
		/// </summary>
		private bool abort;

		private void VehicleDesignForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (Design != null && !abort)
			{
				var result = MessageBox.Show("Save your design before closing the ship designer?", "FrEee", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (result == DialogResult.Yes)
				{
					// try to save design and cancel if we can't
					if (!TrySaveDesign())
						e.Cancel = true;
				}
				else if (result == DialogResult.No)
				{
					// do nothing, let form close
				}
				else if (result == DialogResult.Cancel)
				{
					// cancel the close
					e.Cancel = true;
				}
			}
		}

		private void ddlRole_TextChanged(object sender, EventArgs e)
		{
			if (Design != null)
				Design.Role = ddlRole.Text;
		}

		private void ddlName_TextChanged(object sender, EventArgs e)
		{
			if (Design != null)
			{
				Design.BaseName = ddlName.Text;
				Design.Iteration = Empire.Current.KnownDesigns.Where(d => d != Design && d.Owner == Empire.Current && d.BaseName == ddlName.Text).Count() + 1;
				txtIteration.Text = Design.Iteration == 0 ? "" : Design.Iteration.ToRomanNumeral();
			}
		}

		private void btnClearMount_Click(object sender, EventArgs e)
		{
			CurrentMount = null;
		}

		/// <summary>
		/// The currently used mount for placing new components.
		/// </summary>
		public Mount CurrentMount
		{
			get
			{
				return mount;
			}
			set
			{
				mount = value;
				if (mount == null)
					btnMount.Text = "(none)";
				else
					btnMount.Text = mount.ShortName;
				BindAvailableComponents();
			}
		}

		private void chkFilterByMount_CheckedChanged(object sender, EventArgs e)
		{
			BindAvailableComponents();
		}

		private void lstComponentsAvailable_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				var item = lstComponentsAvailable.GetItemAt(e.X, e.Y);
				if (item != null)
				{
					var mct = (MountedComponentTemplate)item.Tag;
					Design.Components.Add(mct);
					BindInstalledComponents();
					BindDesignData();
				}
			}
			else if (e.Button == MouseButtons.Right)
			{
				// TODO - show mounted component template report
			}
		}

		private void lstComponentsInstalled_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				var item = lstComponentsInstalled.GetItemAt(e.X, e.Y);
				if (item != null)
				{
					var mct = (MountedComponentTemplate)item.Tag;
					Design.Components.Remove(mct);
					BindInstalledComponents();
					BindDesignData();
				}
			}
			else if (e.Button == MouseButtons.Right)
			{
				// TODO - show mounted component template report
			}
		}
	}
}
