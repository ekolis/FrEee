using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Objects.Vehicles;
using FrEee.Utility;
using FrEee.Extensions;
using FrEee.UI.WinForms.Interfaces;
using FrEee.UI.WinForms.Utility.Extensions;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrEee.Objects.Civilization.Orders;
using FrEee.Modding.Abilities;
using FrEee.Gameplay.Commands.Orders;

namespace FrEee.UI.WinForms.Controls;

/// <summary>
/// A report on a ship or base.
/// </summary>
public partial class FleetReport : UserControl, IBindable<Fleet>
{
	public FleetReport()
	{
		InitializeComponent();
	}

	public FleetReport(Fleet fleet)
	{
		InitializeComponent();
		Fleet = fleet;
	}

	public Fleet Fleet { get { return fleet; } set { fleet = value; Bind(); } }
	private Fleet fleet;

	public void Bind(Fleet data)
	{
		Fleet = data;
		Bind();
	}

	public void Bind()
	{
		SuspendLayout();
		if (Fleet == null || Fleet.Owner == null)
			Visible = false;
		else
		{
			// pictures
			picOwnerFlag.Image = Fleet.Owner.Icon;
			picPortrait.Image = Fleet.Portrait;

			// timestamp
			txtAge.Text = Fleet.Timestamp.GetMemoryAgeDescription();
			txtAge.BackColor = txtAge.Text == "Current" ? Color.Transparent : Color.FromArgb(64, 64, 0);

			// name and stuff
			txtName.Text = Fleet.Name;
			txtCount.Text = Fleet.LeafVehicles.Count().ToString();
			var primaryClass = Fleet.LeafVehicles.GroupBy(v => v.Design).WithMax(g => g.Count()).WithMax(g => g.Key.Hull.Size).FirstOrDefault();
			if (primaryClass == null)
				txtClass.Text = "(none)";
			else
				txtClass.Text = primaryClass.Key.Name;
			txtHullSize.Text = Fleet.LeafVehicles.Sum(v => v.Design.Hull.Size).Kilotons();
			var topSpeed = Fleet.LeafVehicles.Min(v => v.Design.StrategicSpeed);
			progMovement.Maximum = topSpeed;
			progMovement.Value = Fleet.StrategicSpeed;

			// supplies and stuff
			progSupplies.Maximum = Fleet.SupplyStorage;
			progSupplies.Value = Fleet.SupplyRemaining;
			// will we even use ammo/fuel?
			progAmmunition.Visible = false;
			progFuel.Visible = false;

			// damage
			progShields.Maximum = Fleet.MaxShieldHitpoints;
			progShields.Value = Fleet.ShieldHitpoints;
			progArmor.Maximum = Fleet.MaxArmorHitpoints;
			progArmor.Value = Fleet.ArmorHitpoints;
			progHull.Maximum = Fleet.MaxHullHitpoints;
			progHull.Value = Fleet.HullHitpoints;

			// orders and stuff
			txtOrder.Text = Fleet.Orders.Any() ? Fleet.Orders.First().ToString() : "None";
			txtExperience.Text = "None"; // TODO - admiral XP
										 // TODO - show fleet to which this fleet belongs?

			// maintenance
			resMaintMin.Amount = Fleet.MaintenanceCost[Resource.Minerals];
			resMaintOrg.Amount = Fleet.MaintenanceCost[Resource.Organics];
			resMaintRad.Amount = Fleet.MaintenanceCost[Resource.Radioactives];

			// vehicle/subfleet summary
			lstVehicleSummary.Initialize(32, 32);
			foreach (var f in Fleet.Vehicles.OfType<Fleet>())
				lstVehicleSummary.AddItemWithImage("Subfleets", f.Name, f, f.Icon);
			foreach (var g in Fleet.Vehicles.OfType<SpaceVehicle>().GroupBy(v => v.Design))
				lstVehicleSummary.AddItemWithImage(g.Key.Role + "s", g.Count() + "x " + g.Key.Name, g.Key, g.Key.Icon);

			// cargo space free
			txtCargoSpaceFree.Text = string.Format("{0} / {1} free", (Fleet.CargoStorage - (Fleet.Cargo == null ? 0 : Fleet.Cargo.Size)).Kilotons(), Fleet.CargoStorage.Kilotons());

			// orders detail
			chkOnHold.Checked = Fleet.AreOrdersOnHold;
			chkRepeat.Checked = Fleet.AreRepeatOrdersEnabled;
			if (!Fleet.IsOurs)
			{
				chkOnHold.Visible = false;
				chkRepeat.Visible = false;
			}
			lstOrdersDetail.Items.Clear();
			foreach (var o in Fleet.Orders)
				lstOrdersDetail.Items.Add(o);

			// vehicle detail
			treeVehicles.Initialize(32);
			foreach (var v in Fleet.Vehicles)
				BuildVehicleTreeNode(treeVehicles, v);

			// abilities
			abilityTreeView.Abilities = Fleet.AbilityTree();
			abilityTreeView.IntrinsicAbilities = Fleet.IntrinsicAbilities;
		}
		ResumeLayout();
	}

	private void btnClearOrders_Click(object sender, System.EventArgs e)
	{
		foreach (var order in Fleet.Orders.ToArray())
		{
			var addCmd = Empire.Current.Commands.OfType<IAddOrderCommand>().SingleOrDefault(c => c.Order == order);
			if (addCmd == null)
			{
				// not a newly added order, so create a remove command to take it off the server
				var remCmd = DIRoot.OrderCommands.RemoveOrder(Fleet, order);
				Empire.Current.Commands.Add(remCmd);
				remCmd.Execute(); // show change locally
			}
			else
			{
				// a newly added order, so just get rid of the add command
				Empire.Current.Commands.Remove(addCmd);
				addCmd.Execute(); // show change locally
			}

			Bind();

			if (OrdersChanged != null)
				OrdersChanged();
		}
	}

	private void btnDeleteOrder_Click(object sender, System.EventArgs e)
	{
		var order = (IOrder)lstOrdersDetail.SelectedItem;
		if (order != null)
		{
			var addCmd = Empire.Current.Commands.OfType<IAddOrderCommand>().SingleOrDefault(c => c.Order == order);
			if (addCmd == null)
			{
				// not a newly added order, so create a remove command to take it off the server
				var remCmd = DIRoot.OrderCommands.RemoveOrder(Fleet, order);
				Empire.Current.Commands.Add(remCmd);
				remCmd.Execute(); // show change locally
			}
			else
			{
				// a newly added order, so just get rid of the add command
				Empire.Current.Commands.Remove(addCmd);
				addCmd.Execute(); // show change locally
			}

			Bind();

			if (OrdersChanged != null)
				OrdersChanged();
		}
	}

	private void btnOrderGoesDown_Click(object sender, System.EventArgs e)
	{
		var order = (IOrder)lstOrdersDetail.SelectedItem;
		if (order != null && Fleet.Orders.IndexOf(order) < Fleet.Orders.Count - 1)
		{
			var cmd = DIRoot.OrderCommands.RearrangeOrders(Fleet, order, 1);
			Empire.Current.Commands.Add(cmd);
			cmd.Execute(); // show change locally
			Bind();

			if (OrdersChanged != null)
				OrdersChanged();
		}
	}

	private void btnOrderGoesUp_Click(object sender, System.EventArgs e)
	{
		var order = (IOrder)lstOrdersDetail.SelectedItem;
		if (order != null && Fleet.Orders.IndexOf(order) > 0)
		{
			var cmd = new RearrangeOrdersCommand<Fleet>(
				Fleet, order, -1);
			Empire.Current.Commands.Add(cmd);
			cmd.Execute(); // show change locally
			Bind();

			if (OrdersChanged != null)
				OrdersChanged();
		}
		if (OrdersChanged != null)
			OrdersChanged();
	}

	private void btnOrderToBottom_Click(object sender, System.EventArgs e)
	{
		var order = (IOrder)lstOrdersDetail.SelectedItem;
		if (order != null)
		{
			var cmd = new RearrangeOrdersCommand<Fleet>(
				Fleet, order, Fleet.Orders.Count - Fleet.Orders.IndexOf(order) - 1);
			Empire.Current.Commands.Add(cmd);
			cmd.Execute(); // show change locally
			Bind();

			if (OrdersChanged != null)
				OrdersChanged();
		}
	}

	private void btnOrderToTop_Click(object sender, System.EventArgs e)
	{
		var order = (IOrder)lstOrdersDetail.SelectedItem;
		if (order != null)
		{
			var cmd = new RearrangeOrdersCommand<Fleet>(
				Fleet, order, -fleet.Orders.IndexOf(order));
			Empire.Current.Commands.Add(cmd);
			cmd.Execute(); // show change locally
			Bind();

			if (OrdersChanged != null)
				OrdersChanged();
		}
	}

	/// <summary>
	/// Recursively builds a tree node for a fleet or vehicle.
	/// </summary>
	/// <param name="tree">Should be a TreeView or TreeNode.</param>
	/// <param name="sobj">The fleet or vehicle.</param>
	private void BuildVehicleTreeNode(object tree, IMobileSpaceObject sobj)
	{
		TreeNode node;
		string CalculateSupplyStatus(int remaining, int storage)
		{
			if (remaining == 0)
				return "Supplies Empty";
			if (remaining < storage * 0.5)
				return "Low Supplies";
			return "";
		}
		string CalculateStatus(IMobileSpaceObject sobj2)
		{
			var s = "Speed " + sobj2.StrategicSpeed;
			var sup = CalculateSupplyStatus(sobj2.SupplyRemaining, sobj2.SupplyStorage);
			if (sup == null)
				return s;
			else
				return $"{s}, {sup}";
		}
		var namestr = $"{sobj.Name}: {CalculateStatus(sobj)}";
		if (tree is TreeView)
			node = ((TreeView)tree).AddItemWithImage(namestr, sobj, sobj.Icon);
		else if (tree is TreeNode)
			node = ((TreeView)tree).AddItemWithImage(namestr, sobj, sobj.Icon);
		else
			throw new ArgumentException("Tree for BuildVehicleTreeNode must be a TreeView or TreeNode.", "tree");
		if (sobj is Fleet)
		{
			foreach (var sobj2 in ((Fleet)sobj).Vehicles)
				BuildVehicleTreeNode(node, sobj2);
		}
	}

	private void lstVehicleSummary_MouseClick(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Right)
		{
			var item = lstVehicleSummary.GetItemAt(e.X, e.Y);
			if (item != null)
			{
				if (item.Tag is Fleet)
				{
					var f = (Fleet)item.Tag;
					FindForm().ShowChildForm(new FleetReport(f).CreatePopupForm(f.Name));
				}
				else if (item.Tag is IDesign)
				{
					var d = (IDesign)item.Tag;
					FindForm().ShowChildForm(new DesignReport(d).CreatePopupForm(d.Name));
				}
			}
		}
	}

	private void picPortrait_MouseClick(object sender, MouseEventArgs e)
	{
		picPortrait.ShowFullSize(Fleet.Name);
	}

	private void treeVehicles_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
	{
		if (e.Button == MouseButtons.Right)
		{
			if (e.Node.Tag is Fleet)
			{
				var f = (Fleet)e.Node.Tag;
				FindForm().ShowChildForm(new FleetReport(f).CreatePopupForm(f.Name));
			}
			else if (e.Node.Tag is SpaceVehicle)
			{
				var v = (SpaceVehicle)e.Node.Tag;
				FindForm().ShowChildForm(new SpaceVehicleReport(v).CreatePopupForm(v.Name));
			}
		}
	}

	public event OrdersChangedDelegate OrdersChanged;

	public delegate void OrdersChangedDelegate();

	private void chkRepeat_CheckedChanged(object sender, EventArgs e)
	{
		var cmd = Empire.Current.Commands.OfType<ToggleRepeatOrdersCommand>().SingleOrDefault(x => x.Executor == Fleet);
		if (cmd == null)
		{
			cmd = new ToggleRepeatOrdersCommand(Fleet, chkRepeat.Checked);
			Empire.Current.Commands.Add(cmd);
		}
		else
			cmd.IsToggleEnabled = chkRepeat.Checked;
		cmd.Execute();
	}

	private void chkOnHold_CheckedChanged(object sender, EventArgs e)
	{
		var cmd = Empire.Current.Commands.OfType<ToggleOrdersOnHoldCommand>().SingleOrDefault(x => x.Executor == Fleet);
		if (cmd == null)
		{
			cmd = new ToggleOrdersOnHoldCommand(Fleet, chkOnHold.Checked);
			Empire.Current.Commands.Add(cmd);
		}
		else
			cmd.IsToggleEnabled = chkOnHold.Checked;
		cmd.Execute();
	}
}