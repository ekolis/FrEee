using FrEee.Objects.Civilization;
using FrEee.Objects.Technology;
using FrEee.Utility;
using FrEee.Extensions;
using FrEee.UI.WinForms.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrEee.Objects.Civilization.Construction;
using FrEee.Objects.Vehicles;
using FrEee.Objects.GameState;
using FrEee.Objects.Space;
using FrEee.Gameplay.Commands.Orders;
using FrEee.Gameplay.Commands.Designs;

namespace FrEee.UI.WinForms.Forms;

public partial class DesignListForm : GameForm
{
	public DesignListForm()
	{
		InitializeComponent();
		BindVehicleTypeList();
		BindDesignList();

		try { this.Icon = new Icon(FrEee.UI.WinForms.Properties.Resources.FrEeeIcon); } catch { }
	}

	private void BindDesignList()
	{
		var emp = Game.Current.CurrentEmpire;
		IEnumerable<IDesign> designs = emp.KnownDesigns.OrderBy(d => d.Role).ThenBy(d => d.Name).ThenBy(d => d.Iteration);

		// filter by vehicle type
		var item = (dynamic)ddlVehicleType.SelectedItem;
		var vehicleTypeFilter = (VehicleTypes)item.VehicleTypes;
		designs = designs.Where(d => vehicleTypeFilter.HasFlag(d.VehicleType));

		// filter by ours/foreign (using an exclusive or)
		designs = designs.Where(d => d.Owner == emp ^ chkForeign.Checked && d.IsValidInMod);

		// filter by obsoleteness/lockedness (don't filter alien designs)
		if (chkHideObsolete.Checked)
			designs = designs.Where(d => d.Owner != Empire.Current || !d.IsObsolete && Empire.Current.HasUnlocked(d));

		// display it!
		lstDesigns.Initialize(32, 32);
		foreach (var design in designs)
		{
			var x = lstDesigns.AddItemWithImage(design.Role, design.Name, design, design.Icon, null, design.Cost.Sum(kvp => kvp.Value).ToUnitString());
			if (design.IsObsolete)
				x.ForeColor = Color.Gray;
			else if (design.IsObsolescent)
				x.ForeColor = Color.Yellow;
			else if (!Empire.Current.HasUnlocked(design))
				x.ForeColor = Color.Red;
		}
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

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnCopy_Click(object sender, EventArgs e)
	{
		if (lstDesigns.SelectedItems.Count == 1)
		{
			IDesign copy = CopyDesign((IDesign)lstDesigns.SelectedItems[0].Tag);
			var form = new VehicleDesignForm();
			form.Design = copy;
			this.ShowChildForm(form);
			if (form.DialogResult == DialogResult.OK)
				BindDesignList();
		}
	}

	private void btnCreate_Click(object sender, EventArgs e)
	{
		var form = new VehicleDesignForm();
		this.ShowChildForm(form);
		if (form.DialogResult == DialogResult.OK)
			BindDesignList();
	}

	private void btnDelete_Click(object sender, EventArgs e)
	{
		if (lstDesigns.SelectedItems.Count == 1)
		{
			var design = (IDesign)lstDesigns.SelectedItems[0].Tag;
			if (!design.IsNew || BuildingAnywhere(design))
			{
				MessageBox.Show("You can only delete designs that have been newly created or imported from your library.");
			}
			else if (MessageBox.Show("Delete " + design + " from your library?", "Delete Design", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				// design in the library is just a copy so we need to search for it
				Library.Delete<IDesign>(d =>
						d.BaseName == design.BaseName &&
						d.Hull == design.Hull &&
						d.Components.SequenceEqual(design.Components, new MountedComponentTemplate.SimpleEqualityComparer())
					);
				Empire.Current.KnownDesigns.Remove(design);
				design.Dispose();
				BindDesignList();
				designReport.Design = null;
			}
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
				if (d.TurnNumber < Game.Current.TurnNumber)
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

	private void btnGroundSimulator_Click(object sender, EventArgs e)
	{
		this.ShowChildForm(new CombatSimulatorForm(true));
	}

	private void btnObsolete_Click(object sender, EventArgs e)
	{
		if (lstDesigns.SelectedItems.Count >= 1)
		{
			foreach (IDesign d in lstDesigns.SelectedItems.Cast<ListViewItem>().Select(item => item.Tag))
			{
				d.IsObsolete = !d.IsObsolete;
				foreach (var cmd in Empire.Current.Commands.OfType<ISetObsoleteFlagCommand>().Where(cmd => (cmd.Design ?? cmd.NewDesign) == d && cmd.IsObsolete != d.IsObsolete).ToArray())
					Empire.Current.Commands.Remove(cmd);
				if (!Empire.Current.Commands.OfType<ISetObsoleteFlagCommand>().Where(cmd => cmd.Design == d && cmd.IsObsolete == d.IsObsolete).Any())
				{
					Empire.Current.Commands.Add(DIRoot.DesignCommands.SetObsoleteFlag(d, d.IsObsolete));
				}
			}
			BindDesignList();
		}
	}

	private void btnSpaceSimulator_Click(object sender, EventArgs e)
	{
		this.ShowChildForm(new CombatSimulatorForm(false));
	}

	private void btnStrategy_Click(object sender, EventArgs e)
	{
		/*if (lstDesigns.SelectedItems.Count == 1)
		{
			var d = (IDesign)lstDesigns.SelectedItems[0].Tag;
			if (d.Owner != Empire.Current)
			{
				MessageBox.Show("You cannot edit alien design strategies.");
			}
			else
			{
					var form = new MogreCombatRender.StrategiesDesigner.StratMainForm(d);
					//form.Design = d;
					this.ShowChildForm(form);
					if (form.DialogResult == DialogResult.OK)
						BindDesignList();
			}
		}*/
	}

	private void btnUpgrade_Click(object sender, EventArgs e)
	{
		if (lstDesigns.SelectedItems.Count == 1)
		{
			var old = (IDesign)lstDesigns.SelectedItems[0].Tag;
			var copy = old.Upgrade();
			var form = new VehicleDesignForm();
			form.Design = copy;
			this.ShowChildForm(form);
			if (form.DialogResult == DialogResult.OK)
				BindDesignList();
		}
	}

	/// <summary>
	/// Are we building this thingy anywhere?
	/// </summary>
	/// <param name="t"></param>
	/// <returns></returns>
	private bool BuildingAnywhere(IConstructionTemplate t)
	{
		return Galaxy.Current.FindSpaceObjects<IConstructor>().OwnedBy(Empire.Current).Any(o => o.ConstructionQueue != null && o.ConstructionQueue.Orders.Any(o2 => o2.Template == t));
	}

	private void chkForeign_CheckedChanged(object sender, EventArgs e)
	{
		BindDesignList();
	}

	private void chkHideObsolete_CheckedChanged(object sender, EventArgs e)
	{
		BindDesignList();
	}

	private IDesign CopyDesign(IDesign old)
	{
		var copy = old.CopyAndAssignNewID();
		copy.TurnNumber = Game.Current.TurnNumber;
		copy.Owner = Empire.Current;
		copy.Iteration++;
		copy.VehiclesBuilt = 0;
		copy.IsObsolete = false;

		// use real component templates and mounts from mod, not copies!
		copy.Components.Clear();
		foreach (var mct in old.Components)
		{
			// reuse templates so components appear "condensed" on vehicle designer
			var same = copy.Components.FirstOrDefault(x => x.ComponentTemplate == mct.ComponentTemplate && x.Mount == mct.Mount);
			if (same == null)
				copy.Components.Add(new MountedComponentTemplate(copy, mct.ComponentTemplate, mct.Mount));
			else
				copy.Components.Add(same);
		}

		return copy;
	}

	private void ddlVehicleType_SelectedIndexChanged(object sender, EventArgs e)
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

	private void btnExportAll_Click(object sender, EventArgs e)
	{
		var designs = Empire.Current.KnownDesigns.OwnedBy(Empire.Current);
		var lib = Library.Import<IDesign>(d => d.IsValidInMod);
		var count = 0;
		foreach (var d in designs)
		{
			if (!lib.Any(ld => ld.Equals(d)))
			{
				count++;
				Library.Export(d);
			}
		}
		Library.Save();
		MessageBox.Show($"{designs.Count()} designs exported to library ({count} new).");
	}
}
