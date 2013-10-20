using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Civilization.Diplomacy;
using FrEee.Game.Objects.Space;
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
			ddlMessageType.Items.Add("Propose Treaty");
			ddlMessageType.Items.Add("Break Treaty");
			ddlMessageType.Items.Add("Declare War");
			ddlMessageType.Items.Add("Propose Trade/Gift/Request");
			ddlMessageType.SelectedIndex = 0;
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
			if (InReplyTo is IProposalMessage)
			{
				// don't allow accepting tentative proposals
				if (!(InReplyTo is IProposalMessage<TradeProposal>) || !((IProposalMessage<TradeProposal>)InReplyTo).Proposal.IsTentative)
					ddlMessageType.Items.Add("Accept Proposal");
				ddlMessageType.Items.Add("Reject Proposal");
				ddlMessageType.SelectedIndex = -1;
			}
			else if (InReplyTo is IMessage<GeneralMessage>)
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
				btnCounter.Visible = false;
				chkTentative.Visible = false;
			}
			else if (item == "Propose Treaty")
			{
				lblWeHave.Visible = true;
				treeWeHave.Visible = true;
				lblTheyHave.Visible = false;
				treeTheyHave.Visible = false;
				lblTable.Visible = true;
				treeTable.Visible = true;
				btnGive.Visible = true;
				btnRequest.Visible = false;
				btnReturn.Visible = true;
				btnCounter.Visible = false;
				chkTentative.Visible = false;
				PopulateWeHaveTreatyElements();
			}
			else if (item == "Propose Trade/Gift/Request")
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
				btnCounter.Visible = false;
				chkTentative.Visible = true;
				chkTentative.Enabled = true;
				PopulateWeHaveTradeItems();
				PopulateTheyHaveTradeItems();
			}
			else if (InReplyTo is IProposalMessage<TreatyProposal>)
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
				btnCounter.Visible = true;
				chkTentative.Visible = false;
				PopulateTableTreatyElements();
			}
			else if (InReplyTo is IProposalMessage<TradeProposal>)
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
				btnCounter.Visible = true;
				chkTentative.Visible = true;
				chkTentative.Enabled = false;
				chkTentative.Checked = ((IProposalMessage<TradeProposal>)InReplyTo).Proposal.IsTentative;
				PopulateTableTreatyElements();
			}
		}

		private void PopulateWeHaveTreatyElements()
		{
			treeWeHave.Initialize(32);
			// TODO - treaty elements
		}

		private void PopulateTableTreatyElements()
		{
			treeTable.Initialize(32);
			// TODO - treaty elements
		}

		private void PopulateWeHaveTradeItems()
		{
			PopulateTradeItems(Empire.Current, treeWeHave);
		}

		private void PopulateTheyHaveTradeItems()
		{
			PopulateTradeItems(TargetEmpire, treeTheyHave);
		}

		private void PopulateTradeItems(Empire emp, TreeView tree)
		{
			treeTable.Initialize(32);

			// sort planets alphabetically
			var planetsNode = tree.AddItemWithImage("Planets", "Planets", Pictures.GetGenericImage(typeof(Planet)));
			foreach (var p in emp.ColonizedPlanets.OrderByDescending(p => p.Name))
				planetsNode.AddItemWithImage(p.Name, p, p.Icon);
			if (planetsNode.Nodes.Count == 0)
				planetsNode.Remove();

			// sort vehicles descending by size, then alphabetically
			// no trading units that are in cargo
			var vehiclesNode = tree.AddItemWithImage("Vehicles", "Vehicles", Pictures.GetVehicleTypeImage(emp.ShipsetPath, VehicleTypes.Ship));
			foreach (var v in emp.OwnedSpaceObjects.OfType<ISpaceVehicle>().Where(v => !(v is IUnit && ((IUnit)v).Container is ISpaceObject)).OrderByDescending(v => v.Size).ThenBy(v => v.Name))
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
			foreach (var sys in emp.ExploredStarSystems)
				chartsNode.AddItemWithImage(sys.Name, sys, sys.Icon);
			if (chartsNode.Nodes.Count == 0)
				chartsNode.Remove();

			// comms channels
			var commsNode = tree.AddItemWithImage("Communications Channels", "Communications Channels", Pictures.GetGenericImage(typeof(Empire)));
			foreach (var ee in emp.EncounteredEmpires)
				commsNode.AddItemWithImage(ee.Name, ee, ee.Icon);
			if (commsNode.Nodes.Count == 0)
				commsNode.Remove();
		}

		private void PopulateTableTradeItems()
		{
		}

		private void btnGive_Click(object sender, EventArgs e)
		{
			// TODO - give item
		}

		private void btnReturn_Click(object sender, EventArgs e)
		{
			// TODO - return
		}

		private void btnRequest_Click(object sender, EventArgs e)
		{
			// TODO - request item
		}

		private void btnCounter_Click(object sender, EventArgs e)
		{
			// TODO - counter proposal
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnSend_Click(object sender, EventArgs e)
		{
			// TODO - send message
		}
	}
}
