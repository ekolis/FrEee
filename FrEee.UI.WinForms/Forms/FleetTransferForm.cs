
using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Objects.Vehicles;
using FrEee.Utility;
using FrEee.Extensions;
using FrEee.UI.WinForms.Controls;
using FrEee.UI.WinForms.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrEee.Gameplay.Commands;

namespace FrEee.UI.WinForms.Forms;

public partial class FleetTransferForm : GameForm
{
	public FleetTransferForm(Sector sector)
	{
		InitializeComponent();

		try { this.Icon = new Icon(FrEee.UI.WinForms.Properties.Resources.FrEeeIcon); }
		catch { }

		this.sector = sector;

		BindVehicles();
		BindFleets();
	}

	private Fleet SelectedFleet
	{
		get
		{
			if (treeFleets.SelectedNode == null)
				return null;
			else if (treeFleets.SelectedNode.Tag is Fleet)
				return treeFleets.SelectedNode.Tag as Fleet;
			else if (treeFleets.SelectedNode.Tag is IVehicle)
				return treeFleets.SelectedNode.Parent.Tag as Fleet;
			else
				return null; // invalid node
		}
	}

	private IMobileSpaceObject SelectedFleetSpaceObject
	{
		get
		{
			if (treeFleets.SelectedNode == null)
				return null;
			else if (treeFleets.SelectedNode.Tag is IMobileSpaceObject)
				return treeFleets.SelectedNode.Tag as IMobileSpaceObject;
			else
				return null; // invalid node
		}
	}

	private IEnumerable<IMobileSpaceObject> SelectedSpaceObjects
	{
		get
		{
			return FindNodeSpaceObjects(treeVehicles.SelectedNode);
		}
	}

	private bool abort = false;

	private bool changed = false;

	private List<ICommand> newCommands = new List<ICommand>();

	private List<Fleet> newFleets = new List<Fleet>();

	private Sector sector;

	/// <summary>
	/// Adds a vehicle or fleet to a fleet.
	/// </summary>
	/// <param name="vehicle"></param>
	/// <param name="fleet"></param>
	private void AddToFleet(IMobileSpaceObject vehicle, Fleet fleet)
	{
		DoAddToFleet(vehicle, fleet);

		BindVehicles();
		BindFleets(fleet);
		changed = true;
	}

	private void AddToFleet(IEnumerable<IMobileSpaceObject> sobjs, Fleet fleet)
	{
		if (sobjs == null)
			return;
		foreach (var sobj in sobjs)
			DoAddToFleet(sobj, fleet);

		BindVehicles();
		BindFleets(fleet);
		changed = true;
	}

	private void BindFleets(Fleet selected = null)
	{
		// build preliminary tree from existing fleets in sector
		treeFleets.Initialize(32);
		foreach (var f in sector.SpaceObjects.OfType<Fleet>().OwnedBy(Empire.Current))
			CreateNode(treeFleets, f);

		// create any new fleets
		foreach (var cmd in newCommands.OfType<CreateFleetCommand>())
			CreateNode(treeFleets, cmd.Fleet);

		// remove vehicles that are being removed from fleets
		foreach (var cmd in newCommands.OfType<LeaveFleetCommand>())
		{
			var node = FindNode(treeFleets, cmd.Executor);
			node.Remove();
		}
		foreach (var cmd in newCommands.OfType<DisbandFleetCommand>())
		{
			var node = FindNode(treeFleets, cmd.Executor);
			node.Remove();
		}

		// add vehicles that are being added to fleets
		foreach (var cmd in newCommands.OfType<JoinFleetCommand>())
		{
			var node = FindNode(treeFleets, cmd.Fleet);
			CreateNode(node, cmd.Executor);
		}

		// select the selected fleet in the GUI
		if (selected != null)
			treeFleets.SelectedNode = FindNode(treeFleets, selected);
		else if (treeFleets.Nodes.Count == 1)
			treeFleets.SelectedNode = treeFleets.Nodes[0]; // only one fleet? just select it anyway
	}

	private void BindVehicles(IMobileSpaceObject selected = null)
	{
		var vehicles = new HashSet<IVehicle>();

		// find vehicles in sector that are not fleets
		foreach (var v in sector.SpaceObjects.OfType<SpaceVehicle>().OwnedBy(Empire.Current))
			vehicles.Add(v);

		// add vehicles that are being removed from fleets (but not fleets themselves, those go in the fleets tree)
		foreach (var v in newCommands.OfType<LeaveFleetCommand>().Select(c => c.Executor).OfType<SpaceVehicle>())
			vehicles.Add(v);
		foreach (var v in newCommands.OfType<DisbandFleetCommand>().SelectMany(c => c.Executor.Vehicles.OfType<SpaceVehicle>()))
			vehicles.Add(v);

		// remove vehicles that are being added to fleets
		foreach (var v in newCommands.OfType<JoinFleetCommand>().Select(c => c.Executor).OfType<SpaceVehicle>())
			vehicles.Remove(v);

		// make a tree of vehicles
		treeVehicles.Initialize(32);
		foreach (var vtGroup in vehicles.GroupBy(v => v.Design.VehicleType))
		{
			var vtNode = treeVehicles.AddItemWithImage(vtGroup.Key.ToSpacedString(), vtGroup.Key, Pictures.GetVehicleTypeImage(Empire.Current.ShipsetPath, vtGroup.Key));
			foreach (var roleGroup in vtGroup.GroupBy(v => v.Design.Role))
			{
				var roleNode = vtNode.AddItemWithImage(roleGroup.Key, roleGroup.Key, Pictures.GetVehicleTypeImage(Empire.Current.ShipsetPath, vtGroup.Key));
				foreach (var designGroup in roleGroup.GroupBy(v => v.Design))
				{
					var designNode = roleNode.AddItemWithImage(designGroup.Key.Name, designGroup.Key, designGroup.Key.Icon);
					foreach (var vehicle in designGroup)
					{
						TreeNode vehicleNode;
						if (vehicle is IMobileSpaceObject sobj) // yay pattern matching! :D
							vehicleNode = designNode.AddItemWithImage(vehicle.Name + ": " + CalculateStatus(sobj), vehicle, vehicle.Icon);
						else
							vehicleNode = designNode.AddItemWithImage(vehicle.Name, vehicle, vehicle.Icon);
						if (vehicle == selected)
							treeVehicles.SelectedNode = vehicleNode;
					}
				}
			}
			if (vtNode.Nodes.Count == 0)
				vtNode.Remove();
		}

		// expand the treeeee!
		treeVehicles.ExpandAll();
	}

	private void btnAdd_Click(object sender, EventArgs e)
	{
		AddToFleet(SelectedSpaceObjects, SelectedFleet);
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		abort = true;
		Close();
	}

	private void btnCreate_Click(object sender, EventArgs e)
	{
		var fleet = new Fleet();
		fleet.Name = txtFleetName.Text;

		var cmd = new CreateFleetCommand(fleet, sector);
		newCommands.Add(cmd);
		newFleets.Add(fleet);

		BindFleets(fleet);

		changed = true;
	}

	private void btnDisband_Click(object sender, EventArgs e)
	{
		DisbandFleet(SelectedFleet);
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		Save();
		Close();
	}

	private void btnRemove_Click(object sender, EventArgs e)
	{
		if (SelectedFleetSpaceObject is Fleet && SelectedFleetSpaceObject.Container == null)
			DisbandFleet(SelectedFleetSpaceObject as Fleet);
		else
			RemoveFromFleet(SelectedFleetSpaceObject);
	}

	private string CalculateStatus(IMobileSpaceObject sobj2)
	{
		var s = "Speed " + sobj2.StrategicSpeed;
		var sup = CalculateSupplyStatus(sobj2.SupplyRemaining, sobj2.SupplyStorage);
		if (sup == null)
			return s;
		else
			return $"{s}, {sup}";
	}

	private string CalculateSupplyStatus(int remaining, int storage)
	{
		if (remaining == 0)
			return "Supplies Empty";
		if (remaining < storage * 0.5)
			return "Low Supplies";
		return "";
	}

	private TreeNode CreateNode(TreeView parent, IMobileSpaceObject v)
	{
		var node = parent.AddItemWithImage(v.Name, v, v.Icon);
		if (v is Fleet)
		{
			foreach (var sub in ((Fleet)v).Vehicles)
				CreateNode(node, sub);
		}
		return node;
	}

	private TreeNode CreateNode(TreeNode parent, IMobileSpaceObject v)
	{
		TreeNode node;
		node = parent.AddItemWithImage(v.Name + ": " + CalculateStatus(v), v, v.Icon);
		if (v is Fleet)
		{
			foreach (var sub in ((Fleet)v).Vehicles)
				CreateNode(node, sub);
		}
		return node;
	}

	/// <summary>
	/// Disbands a fleet.
	/// </summary>
	/// <param name="fleet"></param>
	private void DisbandFleet(Fleet fleet)
	{
		if (fleet == null)
			return;

		// confirm
		if (MessageBox.Show("Disband " + fleet + "?", "Confirm Disband", MessageBoxButtons.YesNo) == DialogResult.Yes)
		{
			if (!newFleets.Contains(fleet))
			{
				// create a disband command
				var cmd = new DisbandFleetCommand(fleet);
				newCommands.Add(cmd);
			}
			else
			{
				// delete any create/join/leave commands
				var cmd = newCommands.OfType<CreateFleetCommand>().Single(c => c.Fleet == fleet);
				newCommands.Remove(cmd);
				foreach (var c in newCommands.OfType<JoinFleetCommand>().Where(c => c.Fleet == fleet))
					newCommands.Remove(c);
				foreach (var c in newCommands.OfType<LeaveFleetCommand>().Where(c => c.Executor.Container == fleet))
					newCommands.Remove(c);
			}

			BindVehicles();
			BindFleets();

			changed = true;
		}
	}

	/// <summary>
	/// Adds to a fleet without refreshing the GUI.
	/// </summary>
	/// <param name="vehicle"></param>
	/// <param name="fleet"></param>
	private void DoAddToFleet(IMobileSpaceObject vehicle, Fleet fleet)
	{
		if (vehicle == null)
			return;
		if (fleet == null)
			return;

		JoinFleetCommand cmd;
		if (!newFleets.Contains(fleet))
		{
			// fleet already exists, we can add to it
			cmd = new JoinFleetCommand(vehicle, fleet);
		}
		else
		{
			// fleet is new, we need to reference it by its command
			cmd = new JoinFleetCommand(vehicle, newCommands.OfType<CreateFleetCommand>().Single(c => c.Fleet == fleet));
		}
		newCommands.Add(cmd);
	}

	private TreeNode FindNode(TreeView parent, IMobileSpaceObject v)
	{
		foreach (TreeNode n in parent.Nodes)
		{
			if (n.Tag == v)
				return n;
			var sub = FindNode(n, v);
			if (sub != null)
				return sub;
		}

		return null;
	}

	private TreeNode FindNode(TreeNode parent, IMobileSpaceObject v)
	{
		foreach (TreeNode n in parent.Nodes)
		{
			if (n.Tag == v)
				return n;
			var sub = FindNode(n, v);
			if (sub != null)
				return sub;
		}

		return null;
	}

	private IEnumerable<IMobileSpaceObject> FindNodeSpaceObjects(TreeNode node)
	{
		if (node == null)
			yield break;

		if (node.Tag is IMobileSpaceObject)
			yield return node.Tag as IMobileSpaceObject;

		foreach (TreeNode sub in node.Nodes)
		{
			foreach (var sobj in FindNodeSpaceObjects(sub))
				yield return sobj;
		}
	}

	private void FleetTransferForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (changed && !abort)
		{
			var result = MessageBox.Show("Save your changes?", "FrEee", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
			if (result == DialogResult.Yes)
				Save();
			else if (result == DialogResult.No)
				Reset();
			else if (result == DialogResult.Cancel)
				e.Cancel = true;
		}
	}

	/// <summary>
	/// Removes a vehicle or fleet from its fleet.
	/// </summary>
	/// <param name="sobj"></param>
	private void RemoveFromFleet(IMobileSpaceObject vehicle)
	{
		if (vehicle == null)
			return;
		if (vehicle.Container == null)
			return;

		var cmd = new LeaveFleetCommand(vehicle);
		newCommands.Add(cmd);
		BindVehicles(vehicle);
		var node = treeFleets.GetAllNodes().Single(x => x.Tag == vehicle);
		node.Remove();
		changed = true;
	}

	private void Reset()
	{
		foreach (var fleet in newFleets)
			fleet.Dispose();
		DialogResult = DialogResult.Cancel;
	}

	private void Save()
	{
		// save commands to plr-file in memory
		foreach (var cmd in newCommands)
			Empire.Current.Commands.Add(cmd);

		// execute commands immediately so they take effect on client
		foreach (var cmd in newCommands)
			cmd.Execute();

		// delete any empty fleets
		foreach (var f in sector.SpaceObjects.OfType<Fleet>().Where(f => !f.Vehicles.Any()).ToArray())
			f.Dispose();

		changed = false;
		DialogResult = DialogResult.OK;
	}

	private void treeFleets_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
	{
		// show report if you right click
		if (e.Button == MouseButtons.Right)
		{
			if (e.Node.Tag is SpaceVehicle)
			{
				var v = e.Node.Tag as SpaceVehicle;
				this.ShowPopupForm(new SpaceVehicleReport(v), v.Name);
			}
			else if (e.Node.Tag is Fleet)
			{
				var f = e.Node.Tag as Fleet;
				this.ShowPopupForm(new FleetReport(f), f.Name);
			}
			else if (e.Node.Tag is IDesign)
			{
				var d = e.Node.Tag as IDesign;
				this.ShowPopupForm(new DesignReport(d), d.Name);
			}
		}
	}

	private void treeFleets_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
	{
		if (e.Node.Tag is SpaceVehicle)
			RemoveFromFleet(e.Node.Tag as SpaceVehicle);
		else if (e.Node.Tag is Fleet)
		{
			// if it's a root fleet, disband it, otherwise remove it
			var f = e.Node.Tag as Fleet;
			if (f.Container == null)
				DisbandFleet(f);
			else
				RemoveFromFleet(f);
		}
	}

	private void treeVehicles_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
	{
		// show report if you right click
		if (e.Button == MouseButtons.Right)
		{
			if (e.Node.Tag is SpaceVehicle)
			{
				var v = e.Node.Tag as SpaceVehicle;
				this.ShowPopupForm(new SpaceVehicleReport(v), v.Name);
			}
			else if (e.Node.Tag is Fleet)
			{
				var f = e.Node.Tag as Fleet;
				this.ShowPopupForm(new FleetReport(f), f.Name);
			}
			else if (e.Node.Tag is IDesign)
			{
				var d = e.Node.Tag as IDesign;
				this.ShowPopupForm(new DesignReport(d), d.Name);
			}
		}
	}

	private void treeVehicles_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
	{
		AddToFleet(FindNodeSpaceObjects(e.Node), SelectedFleet);
	}
}