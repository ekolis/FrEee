using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Commands;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Utility.Extensions;
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
	public partial class FleetTransferForm : Form
	{
		public FleetTransferForm(Sector sector)
		{
			InitializeComponent();

			try { this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); }
			catch { }

			this.sector = sector;

			BindVehicles();
			BindFleets();
		}

		private List<ICommand> newCommands = new List<ICommand>();
		private List<Fleet> newFleets = new List<Fleet>();

		private void BindVehicles(IVehicle selected = null)
		{
			var vehicles = new HashSet<IVehicle>();

			// find vehicles in sector that are not fleets
			foreach (var v in sector.SpaceObjects.OfType<SpaceVehicle>())
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
							var vehicleNode = designNode.AddItemWithImage(vehicle.Name, vehicle, vehicle.Icon);
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

		private void BindFleets(Fleet selected = null)
		{
			// build preliminary tree from existing fleets in sector
			treeFleets.Initialize(32);
			foreach (var f in sector.SpaceObjects.OfType<Fleet>())
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
			var node = parent.AddItemWithImage(v.Name, v, v.Icon);
			if (v is Fleet)
			{
				foreach (var sub in ((Fleet)v).Vehicles)
					CreateNode(node, sub);
			}
			return node;
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

		private Sector sector;

		private void btnOK_Click(object sender, EventArgs e)
		{
			Save();
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			abort = true;
			Close();
		}

		private void Save()
		{
			foreach (var cmd in newCommands)
				Empire.Current.Commands.Add(cmd);
			foreach (var cmd in newCommands)
				cmd.Execute();
		}

		private void Reset()
		{
			foreach (var fleet in newFleets)
				fleet.Dispose();
		}

		private bool abort = false;
		private bool changed = false;

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

		private void btnCreate_Click(object sender, EventArgs e)
		{
			var fleet = new Fleet();
			fleet.Name = txtFleetName.Text;

			var cmd = new CreateFleetCommand(fleet, sector);
			newCommands.Add(cmd);

			BindFleets();

			changed = true;
		}

		private void btnDisband_Click(object sender, EventArgs e)
		{
			if (treeFleets.SelectedNode != null && treeFleets.SelectedNode.Tag is Fleet)
			{
				var fleet = (Fleet)treeFleets.SelectedNode.Tag;
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

		private void btnRemove_Click(object sender, EventArgs e)
		{
			if (treeFleets.SelectedNode != null && treeFleets.SelectedNode.Parent != null)
			{
				var vehicle = (IMobileSpaceObject)treeFleets.SelectedNode.Tag;
				var cmd = new LeaveFleetCommand(vehicle);
				newCommands.Add(cmd);
				BindVehicles();
				BindFleets();
				changed = true;
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			if (treeVehicles.SelectedNode != null && treeVehicles.SelectedNode.Tag is IMobileSpaceObject && treeFleets.SelectedNode != null)
			{
				var fleet = (Fleet)treeFleets.SelectedNode.Tag;
				var vehicle = (IMobileSpaceObject)treeVehicles.SelectedNode.Tag;
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
				BindVehicles();
				BindFleets(fleet);
				changed = true;
			}
		}
	}
}
