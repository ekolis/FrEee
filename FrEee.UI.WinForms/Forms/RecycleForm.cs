using FrEee.Objects.Civilization;
using FrEee.Objects.Commands;
using FrEee.Objects.Space;
using FrEee.Objects.Technology;
using FrEee.Objects.Vehicles;
using FrEee.Extensions;
using FrEee.UI.WinForms.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.Civilization.CargoStorage;
using FrEee.Objects.Civilization.Orders.RecycleBehaviors;
using FrEee.Extensions;

namespace FrEee.UI.WinForms.Forms;

/// <summary>
/// Form where the player can choose recycling actions such as scrapping and mothballing.
/// </summary>
public partial class RecycleForm : GameForm
{
	public RecycleForm(Sector sector)
	{
		try { this.Icon = new Icon(FrEee.UI.WinForms.Properties.Resources.FrEeeIcon); }
		catch { }

		InitializeComponent();

		Sector = sector;

		Text = "Recycle in " + Sector;

		Bind();
	}

	/// <summary>
	/// The sector in which we are recycling.
	/// </summary>
	public Sector Sector { get; private set; }

	/// <summary>
	/// Finds all facilities that are selected.
	/// </summary>
	private IEnumerable<Facility> SelectedFacilities
	{
		get
		{
			return treeVehicles.GetAllNodes().Where(n => n.Tag is Facility && n.Checked).Select(n => n.Tag as Facility);
		}
	}

	/// <summary>
	/// Finds all units in cargo that are selected.
	/// </summary>
	private IEnumerable<IUnit> SelectedUnitsInCargo
	{
		get
		{
			return treeVehicles.GetAllNodes().Where(n => n.Tag is IUnit && n.Checked).Select(n => n.Tag as IUnit).Where(u => u.Container != null);
		}
	}

	/// <summary>
	/// Finds all space vehicles that are selected (including units in space, but not units in cargo).
	/// </summary>
	private IEnumerable<SpaceVehicle> SelectedVehiclesInSpace
	{
		get
		{
			return treeVehicles.GetAllNodes()
				.Where(n =>
					n.Tag is SpaceVehicle && n.Checked)
				.Select(n =>
					n.Tag as SpaceVehicle)
				.Where(v =>
					!(v is IUnit && ((IUnit)v).Container != null))
				.ExceptNull();
		}
	}

	private bool doneCleanup = false;

	private ICollection<ICommand> newCommands = new HashSet<ICommand>();

	private void AddCommand(ICommand cmd)
	{
		newCommands.Add(cmd);
		Empire.Current.Commands.Add(cmd);
	}

	/// <summary>
	/// Binds the controls on the form.
	/// </summary>
	private void Bind()
	{
		treeVehicles.Initialize(32);
		foreach (var p in Sector.SpaceObjects.OfType<Planet>().Where(p => p.Owner == Empire.Current))
		{
			// planets with our colonies
			var pnode = treeVehicles.AddItemWithImage(p.Name, p, p.Icon);
			// TODO: flesh out FacilityAbility so any entity can be a facility, not just a Facility object
			foreach (var ft in p.Colony.Facilities.Cast<Facility>().GroupBy(f => f.Template))
			{
				// facility templates
				var ftnode = pnode.AddItemWithImage(ft.Count() + "x " + ft.Key.Name, ft.Key, ft.Key.Icon);
				foreach (var f in ft)
				{
					// facilities
					var fnode = ftnode.AddItemWithImage(f.Name, f, f.Icon);
					var orders = Empire.Current.Commands.OfType<AddOrderCommand>()
						.Where(x => x.Order is RecycleFacilityOrCargoOrder o && x.Executor == p && o.Target == f)
						.Select(x => ((RecycleFacilityOrCargoOrder)x.Order).Behavior.Verb);
					if (orders.Any())
					{
						fnode.Text += $" ({string.Join(", ", orders)})";
						var n = fnode.Parent;
						while (n != null)
						{
							if (!n.Text.EndsWith("*"))
								n.Text += " *";
							n = n.Parent;
						}
					}
				}
			}

			// cargo of planets
			BindUnitsIn(p, pnode);
		}
		foreach (var v in Sector.SpaceObjects.OfType<SpaceVehicle>().Where(v => v.Owner == Empire.Current))
		{
			// our space vehicles
			var vnode = treeVehicles.AddItemWithImage(v.Name, v, v.Icon);
			if (v is ICargoContainer cc)
				BindUnitsIn(cc, vnode);
			var orders = Empire.Current.Commands.OfType<AddOrderCommand>()
				.Where(x => x.Order is RecycleVehicleInSpaceOrder && x.Executor == v)
				.Select(x => ((RecycleVehicleInSpaceOrder)x.Order).Behavior.Verb);
			if (orders.Any())
			{
				vnode.Text += $" ({string.Join(", ", orders)})";
				var n = vnode.Parent;
				while (n != null)
				{
					if (!n.Text.EndsWith("*"))
						n.Text += " *";
					n = n.Parent;
				}
			}
		}
	}

	/// <summary>
	/// Binds the tree nodes for a planet's cargo or a ship's cargo.
	/// </summary>
	/// <param name="cc"></param>
	/// <param name="ccnode"></param>
	private void BindUnitsIn(ICargoContainer cc, TreeNode ccnode)
	{
		foreach (var ur in cc.Cargo.Units.GroupBy(u => u.Design.Role))
		{
			// unit roles
			var urnode = ccnode.AddItemWithImage(ur.Count() + "x " + ur.Key, ur.Key, ur.Majority(u => u.Icon));
			foreach (var ud in ur.GroupBy(u => u.Design))
			{
				// unit designs
				var udnode = urnode.AddItemWithImage(ud.Count() + "x " + ud.Key.Name, ud.Key, ud.Key.Icon);
				foreach (var u in ud)
				{
					// units
					var unode = udnode.AddItemWithImage(u.Name, u, u.Icon);
					var orders = Empire.Current.Commands.OfType<AddOrderCommand>()
						.Where(x => x.Order is RecycleFacilityOrCargoOrder o && x.Executor == cc && o.Target == u)
						.Select(x => ((RecycleFacilityOrCargoOrder)x.Order).Behavior.Verb);
					if (orders.Any())
					{
						unode.Text += $" ({string.Join(", ", orders)})";
						var n = unode.Parent;
						while (n != null)
						{
							if (!n.Text.EndsWith("*"))
								n.Text += " *";
							n = n.Parent;
						}
					}
				}
			}
		}
	}

	private void btnAnalyze_Click(object sender, EventArgs e)
	{
		// TODO - analyze ships/units/facilities (sure, why not let you analyze units/facilities?)
		MessageBox.Show("Sorry, analyzing ships/units/facilities is not yet implemented.");
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		foreach (var cmd in newCommands)
			Empire.Current.Commands.Remove(cmd);
		doneCleanup = true;
		Close();
	}

	private void btnMothball_Click(object sender, EventArgs e)
	{
		if (SelectedFacilities.Any())
			MessageBox.Show("Facilities cannot be mothballed."); // TODO - allow mothballing facilities once they can cost maintenance?
		else if (SelectedVehiclesInSpace.OfType<IUnit>().Any() || SelectedUnitsInCargo.Any())
			MessageBox.Show("Units cannot be mothballed."); // TODO - allow mothballing units once they can cost maintenance?
		else
		{
			// TODO - mothballing ships
			MessageBox.Show("Sorry, mothballing/unmothballing ships is not yet implemented.");
		}
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		foreach (var cmd in newCommands)
			cmd.Execute();
		doneCleanup = true;
		Close();
	}

	private void btnRefit_Click(object sender, EventArgs e)
	{
		if (SelectedFacilities.Any())
			MessageBox.Show("Facilities cannot be refit. Use the upgrade option on the construction queue screen instead.");
		else
		{
			// TODO - refit ships/units (sure, why not let you refit units?)
			MessageBox.Show("Sorry, refitting ships/units is not yet implemented.");
		}
	}

	private void btnRevert_Click(object sender, EventArgs e)
	{
		// TODO - revert recycle commands
		MessageBox.Show("Sorry, reverting recycle commands is not yet implemented.");
	}

	private void btnScrap_Click(object sender, EventArgs e)
	{
		foreach (var f in SelectedFacilities)
			AddCommand(new AddOrderCommand(f.Container, new RecycleFacilityOrCargoOrder(new ScrapBehavior(), f)));
		foreach (var v in SelectedVehiclesInSpace)
			AddCommand(new AddOrderCommand(v, new RecycleVehicleInSpaceOrder(new ScrapBehavior())));
		foreach (var u in SelectedUnitsInCargo)
			AddCommand(new AddOrderCommand((IMobileSpaceObject)u.Container, new RecycleFacilityOrCargoOrder(new ScrapBehavior(), u)));
		Bind();
	}

	private void btnUnmothball_Click(object sender, EventArgs e)
	{
		if (SelectedFacilities.Any())
			MessageBox.Show("Facilities cannot be mothballed."); // TODO - allow mothballing facilities once they can cost maintenance?
		else if (SelectedVehiclesInSpace.OfType<IUnit>().Any() || SelectedUnitsInCargo.Any())
			MessageBox.Show("Units cannot be mothballed."); // TODO - allow mothballing units once they can cost maintenance?
		else
		{
			// TODO - unmothballing ships
			MessageBox.Show("Sorry, mothballing/unmothballing ships is not yet implemented.");
		}
	}

	// https://stackoverflow.com/questions/5740585/how-to-automatically-select-or-deselect-the-all-child-nodes-whenever-its-parent
	private void CheckTreeViewNode(TreeNode node, Boolean isChecked)
	{
		foreach (TreeNode item in node.Nodes)
		{
			item.Checked = isChecked;

			if (item.Nodes.Count > 0)
			{
				this.CheckTreeViewNode(item, isChecked);
			}
		}
	}

	private void RecycleForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (!doneCleanup && newCommands.Any())
		{
			// TODO - summarize changes in dialog in more detail
			var choice = MessageBox.Show("Save changes? " + newCommands.Count + " orders were issued.", "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
			if (choice == DialogResult.Yes)
			{
				// save any changes
				foreach (var cmd in newCommands)
					cmd.Execute();
			}
			else if (choice == DialogResult.No)
			{
				// cancel any changes
				foreach (var cmd in newCommands)
					Empire.Current.Commands.Remove(cmd);
			}
			else if (choice == DialogResult.Cancel)
				e.Cancel = true; // don't close the form yet
		}
	}

	private void treeVehicles_AfterCheck(object sender, TreeViewEventArgs e)
	{
		// recursively check/uncheck descendant nodes
		CheckTreeViewNode(e.Node, e.Node.Checked);
	}
}