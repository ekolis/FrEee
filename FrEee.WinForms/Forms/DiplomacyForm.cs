using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Civilization.Diplomacy;
using FrEee.Game.Objects.Commands;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
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
	public partial class DiplomacyForm : Form
	{
		public DiplomacyForm(Empire targetEmpire)
		{
			InitializeComponent();
			try { base.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); }
			catch { }
			TargetEmpire = targetEmpire;
			picPortrait.Image = TargetEmpire.Portrait;
			ddlMessageType.Items.Add("General Message");
			ddlMessageType.Items.Add("Make Proposal");
			ddlMessageType.Items.Add("Break Treaty");
			ddlMessageType.Items.Add("Declare War");
			ddlMessageType.SelectedIndex = 0;
			givePackage = new Package(Empire.Current);
			receivePackage = new Package(targetEmpire);
		}

		public DiplomacyForm(IMessage inReplyTo)
		{
			InitializeComponent();
			try { base.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); }
			catch { }
			TargetEmpire = inReplyTo.Owner;
			picPortrait.Image = TargetEmpire.Portrait;
			InReplyTo = inReplyTo;
			txtInReplyTo.Text = InReplyTo.Text;
			if (InReplyTo is ProposalMessage)
			{
				var pm = (ProposalMessage)InReplyTo;
				// don't allow accepting tentative proposals
				ddlMessageType.Items.Add("Counter Proposal");
				if (!pm.Proposal.IsTentative)
					ddlMessageType.Items.Add("Accept Proposal");
				ddlMessageType.Items.Add("Reject Proposal");
				ddlMessageType.SelectedIndex = 0;
				PopulateTable();
			}
			else
			{
				ddlMessageType.Items.Add("General Message");
				ddlMessageType.SelectedIndex = 0;
			}
		}

		public Empire TargetEmpire { get; private set; }

		public IMessage InReplyTo { get; private set; }

		private void picPortrait_MouseClick(object sender, MouseEventArgs e)
		{
			picPortrait.ShowFullSize(TargetEmpire.LeaderName + " of the " + TargetEmpire.Name);
		}

		private void ddlMessageType_SelectedIndexChanged(object sender, EventArgs e)
		{
			var item = (string)ddlMessageType.SelectedItem;
			if (item == "General Message" ||
				item == "Break Treaty" ||
				item == "Declare War")
			{
				lblWeHave.Visible = false;
				treeWeHave.Visible = false;
				lblTheyHave.Visible = false;
				treeTheyHave.Visible = false;
				lblTable.Visible = false;
				treeTable.Visible = false;
				btnGive.Visible = false;
				btnRequest.Visible = false;
				btnReturn.Visible = false;
				chkTentative.Visible = false;
				lblQuantity.Visible = false;
				txtQuantity.Visible = false;
			}
			else if (item == "Make Proposal" ||
					item == "Counter Proposal")
			{
				lblWeHave.Visible = true;
				lblTheyHave.Visible = true;
				lblTable.Visible = true;
				treeWeHave.Visible = true;
				treeTheyHave.Visible = true;
				treeTable.Visible = true;
				btnGive.Visible = true;
				btnRequest.Visible = true;
				btnReturn.Visible = true;
				chkTentative.Visible = true;
				chkTentative.Enabled = true;
				lblQuantity.Visible = true;
				txtQuantity.Visible = true;
				if (item == "Make Proposal")
				{
					givePackage = new Package(Empire.Current);
					receivePackage = new Package(TargetEmpire);
				}
				else // Counter Proposal
				{
					var pm = (ProposalMessage)InReplyTo;
					// invert give/receive since we're on the target side of the proposal
					givePackage = pm.Proposal.ReceivePackage;
					receivePackage = pm.Proposal.GivePackage;
				}
				PopulateWeHave();
				PopulateTheyHave();
				PopulateTable();
			}
			else if (InReplyTo is ProposalMessage)
			{
				lblWeHave.Visible = false;
				lblTheyHave.Visible = false;
				lblTable.Visible = true;
				treeWeHave.Visible = false;
				treeTheyHave.Visible = false;
				treeTable.Visible = true;
				btnGive.Visible = false;
				btnRequest.Visible = false;
				btnReturn.Visible = false;
				chkTentative.Visible = true;
				chkTentative.Enabled = false;
				chkTentative.Checked = ((ProposalMessage)InReplyTo).Proposal.IsTentative;
				lblQuantity.Visible = false;
				txtQuantity.Visible = false;
				var pm = (ProposalMessage)InReplyTo;
				// invert give/receive since we're on the target side of the proposal
				givePackage = pm.Proposal.ReceivePackage;
				receivePackage = pm.Proposal.GivePackage;
				PopulateTable();
			}
		}

		private void PopulateWeHave()
		{
			PopulateSomeoneHas(Empire.Current, treeWeHave, givePackage);
		}

		private void PopulateTheyHave()
		{
			PopulateSomeoneHas(TargetEmpire, treeTheyHave, receivePackage);
		}

		private void PopulateSomeoneHas(Empire emp, TreeView tree, Package package)
		{
			treeTable.Initialize(32);

			// TODO - treaty elements
			var treatyNode = tree.AddItemWithImage("Treaty Elements", "Treaty Elements", emp.Icon);

			// sort planets alphabetically
			var planetsNode = tree.AddItemWithImage("Planets", "Planets", Pictures.GetGenericImage(typeof(Planet)));
			foreach (var p in emp.ColonizedPlanets.Where(p => !package.Planets.Contains(p)).OrderBy(p => p.Name))
				planetsNode.AddItemWithImage(p.Name, p, p.Icon);
			if (planetsNode.Nodes.Count == 0)
				planetsNode.Remove();

			// sort vehicles descending by size, then alphabetically
			// no trading units that are in cargo
			var vehiclesNode = tree.AddItemWithImage("Vehicles", "Vehicles", Pictures.GetVehicleTypeImage(emp.ShipsetPath, VehicleTypes.Ship));
			foreach (var v in emp.OwnedSpaceObjects.OfType<ISpaceVehicle>().Where(v => !package.Vehicles.Contains(v) && !(v is IUnit && ((IUnit)v).Container is ISpaceObject)).OrderByDescending(v => v.Design.Hull.Size).ThenBy(v => v.Name))
				vehiclesNode.AddItemWithImage(v.Name, v, v.Icon);
			if (vehiclesNode.Nodes.Count == 0)
				vehiclesNode.Remove();

			// resources
			var resourcesNode = tree.AddItemWithImage("Resources", "Resources", Resource.Minerals.Icon);
			foreach (var r in Resource.All.Where(r => r.IsGlobal))
				resourcesNode.AddItemWithImage(emp.StoredResources[r].ToUnitString(true) + " " + r, r, r.Icon);

			// technology
			var techNode = tree.AddItemWithImage("Technology", "Technology", Resource.Research.Icon);
			foreach (var kvp in emp.ResearchedTechnologies.Where(kvp => kvp.Value > 0))
				techNode.AddItemWithImage(kvp.Key.Name + " level " + kvp.Value, kvp.Key, kvp.Key.Icon);
			if (techNode.Nodes.Count == 0)
				techNode.Remove();

			// star charts
			var chartsNode = tree.AddItemWithImage("Star Charts", "Star Charts", Pictures.GetGenericImage(typeof(StarSystem)));
			foreach (var sys in emp.ExploredStarSystems.Where(sys => !package.StarCharts.Contains(sys)))
				chartsNode.AddItemWithImage(sys.Name, sys, sys.Icon);
			if (chartsNode.Nodes.Count == 0)
				chartsNode.Remove();

			// comms channels
			var commsNode = tree.AddItemWithImage("Communications Channels", "Communications Channels", Pictures.GetGenericImage(typeof(Empire)));
			foreach (var ee in emp.EncounteredEmpires.Where(ee => !package.CommunicationChannels.Contains(ee)))
				commsNode.AddItemWithImage(ee.Name, ee, ee.Icon);
			if (commsNode.Nodes.Count == 0)
				commsNode.Remove();
		}

		private void PopulateTable()
		{
			PopulateTable("We Give", givePackage);
			PopulateTable("We Receive", receivePackage);
		}

		private void PopulateTable(string text, Package package)
		{
			treeTable.Initialize(32);
			var node = treeTable.AddItemWithImage(text, package, package.Owner.Icon);

			// TODO - treaty elements

			foreach (var p in package.Planets.OrderBy(p => p.Name))
				node.AddItemWithImage(p.Name, p, p.Icon);

			foreach (var v in package.Vehicles.OrderByDescending(v => v.Design.Hull.Size).ThenBy(v => v.Name))
				node.AddItemWithImage(v.Name, v, v.Icon);

			foreach (var kvp in package.Resources.Where(kvp => kvp.Value != 0))
				node.AddItemWithImage(kvp.Value.ToUnitString(true) + " " + kvp.Key, kvp.Key, kvp.Key.Icon);

			foreach (var kvp in package.Technology)
				node.AddItemWithImage(kvp.Key.Name + " level " + kvp.Value, kvp.Key, kvp.Key.Icon);

			foreach (var sys in package.StarCharts)
				node.AddItemWithImage(sys.Name, sys, sys.Icon);

			foreach (var ee in package.CommunicationChannels)
				node.AddItemWithImage(ee.Name, ee, ee.Icon);
		}

		private Package givePackage, receivePackage;

		private void btnGive_Click(object sender, EventArgs e)
		{
			Transfer(treeWeHave, givePackage);
		}

		private void btnReturn_Click(object sender, EventArgs e)
		{
			var node = treeTable.SelectedNode;
			if (node != null && node.Parent != null)
			{
				Package package = node.Parent.Text == "We Give" ? givePackage : receivePackage;
				// TODO - treaty elements
				if (node.Tag is Planet)
					package.Planets.Remove((Planet)node.Tag);
				else if (node.Tag is Vehicle)
					package.Vehicles.Remove((Vehicle)node.Tag);
				else if (node.Tag is KeyValuePair<Resource, int>)
				{
					var kvp = (KeyValuePair<Resource, int>)node.Tag;
					package.Resources.Remove(kvp.Key);
				}
				else if (node.Tag is KeyValuePair<Technology, int>)
				{
					var kvp = (KeyValuePair<Technology, int>)node.Tag;
					package.Technology.Remove(kvp.Key);
				}
				else if (node.Tag is StarSystem)
					package.StarCharts.Remove((StarSystem)node.Tag);
				else if (node.Tag is Empire)
					package.CommunicationChannels.Remove((Empire)node.Tag);
			}
		}

		private void btnRequest_Click(object sender, EventArgs e)
		{
			Transfer(treeTheyHave, receivePackage);
		}

		private void Transfer(TreeView tree, Package package)
		{
			var node = tree.SelectedNode;
			if (node != null && node.Parent != null)
			{
				var type = (string)node.Parent.Tag;
				if (type == "Treaty Elements")
				{
					// TODO - treaty elements
				}
				else if (type == "Planets")
				{
					var p = (Planet)node.Tag;
					package.Planets.Add(p);

				}
				else if (type == "Vehicles")
				{
					var v = (Vehicle)node.Tag;
					package.Vehicles.Add(v);
				}
				else if (type == "Resources")
				{
					var amount = Parser.Units(txtQuantity.Text);
					if (amount == null)
						MessageBox.Show("Invalid quantity specified. Please specify a numeric quantity of resources, optionally using metric suffixes (e.g. K for thousands).");
					else if (amount < 0)
						MessageBox.Show("You cannot transfer negative resources.");
					else
					{
						var r = (Resource)node.Tag;
						package.Resources.Add(r, (int)amount.Value);
					}
				}
				else if (type == "Technology")
				{
					int level;
					if (!int.TryParse(txtQuantity.Text, out level))
						MessageBox.Show("Invalid level specified. Please specify a whole number.");
					else if (level <= 0)
						MessageBox.Show("Please specify a positive technology level.");
					else
					{
						var tech = (Technology)node.Tag;
						package.Technology.Add(tech, level);
					}
				}
				else if (type == "Star Charts")
				{
					var sys = (StarSystem)node.Tag;
					package.StarCharts.Add(sys);
				}
				else if (type == "Communications Channels")
				{
					var ee = (Empire)node.Tag;
					package.CommunicationChannels.Add(ee);
				}
				PopulateSomeoneHas(package.Owner, tree, package);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnSend_Click(object sender, EventArgs e)
		{
			// create our message
			var msgtype = (string)ddlMessageType.SelectedItem;
			IMessage msg;
			if (msgtype == "General Message")
			{
				var gm = new GeneralMessage(TargetEmpire);
				gm.Text = txtMessage.Text;
				msg = gm;
			}
			else if (msgtype == "Make Proposal" || msgtype == "Counter Proposal")
			{
				var pm = new ProposalMessage(TargetEmpire);
				pm.Text = txtMessage.Text;
				pm.Proposal.GivePackage = givePackage;
				pm.Proposal.ReceivePackage = receivePackage;
				msg = pm;
			}
			else if (msgtype == "Break Treaty")
			{
				var tm = new ActionMessage(TargetEmpire);
				tm.Action = new BreakTreatyAction(TargetEmpire);
				tm.Text = txtMessage.Text;
				msg = tm;
			}
			else if (msgtype == "Declare War")
			{
				var wm = new ActionMessage(TargetEmpire);
				wm.Action = new DeclareWarAction(TargetEmpire);
				wm.Text = txtMessage.Text;
				msg = wm;
			}
			else if (msgtype == "Accept Proposal")
			{
				var am = new ActionMessage(TargetEmpire);
				var pm = (ProposalMessage)InReplyTo;
				am.Action = new AcceptProposalAction(pm.Proposal);
				am.Text = txtMessage.Text;
				msg = am;
			}
			else if (msgtype == "Reject Proposal")
			{
				var rm = new ActionMessage(TargetEmpire);
				var pm = (ProposalMessage)InReplyTo;
				rm.Action = new RejectProposalAction(pm.Proposal);
				rm.Text = txtMessage.Text;
				msg = rm;
			}
			else
			{
				MessageBox.Show("Invalid message type " + msgtype + ". This is probably a bug...");
				return;
			}
			
			// create a command to send it
			var cmd = new SendMessageCommand(msg);
			Empire.Current.Commands.Add(cmd);

			// all done!
			Close();
		}
	}
}
