using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Diplomacy;
using FrEee.Objects.Civilization.Diplomacy.Clauses;
using FrEee.Objects.Space;
using FrEee.Objects.Technology;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Extensions;
using FrEee.UI.WinForms.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrEee.Objects.Civilization.Diplomacy.Messages;
using FrEee.Objects.Civilization.Diplomacy.Actions;
using FrEee.Modding.Abilities;
using FrEee.Gameplay.Commands;
using FrEee.Vehicles;
using FrEee.Vehicles.Types;

namespace FrEee.UI.WinForms.Forms;

public partial class DiplomacyForm : GameForm
{
	public DiplomacyForm(Empire targetEmpire)
	{
		InitializeComponent();
		try { base.Icon = new Icon(FrEee.UI.WinForms.Properties.Resources.FrEeeIcon); }
		catch { }
		TargetEmpire = targetEmpire;
		picPortrait.Image = TargetEmpire.Portrait;
		ddlMessageType.Items.Add("General Message");
		if (targetEmpire != Empire.Current)
		{
			ddlMessageType.Items.Add("Make Proposal");
			ddlMessageType.Items.Add("Break Treaty");
			ddlMessageType.Items.Add("Declare War");
		}
		ddlMessageType.SelectedIndex = 0;
		givePackage = new Package(Empire.Current, targetEmpire);
		receivePackage = new Package(targetEmpire, Empire.Current);
		foreach (AllianceLevel alliance in Enum.GetValues(typeof(AllianceLevel)))
		{
			if (alliance != AllianceLevel.None)
				ddlAlliance.Items.Add(new { Name = alliance.ToSpacedString(), Value = alliance });
		}
	}

	public DiplomacyForm(IMessage inReplyTo)
	{
		InitializeComponent();
		try { base.Icon = new Icon(FrEee.UI.WinForms.Properties.Resources.FrEeeIcon); }
		catch { }
		TargetEmpire = inReplyTo.Owner;
		picPortrait.Image = TargetEmpire?.Portrait;
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
		foreach (AllianceLevel alliance in Enum.GetValues(typeof(AllianceLevel)))
		{
			if (alliance != AllianceLevel.None)
				ddlAlliance.Items.Add(new { Name = alliance.ToSpacedString(), Value = alliance });
		}
	}

	public IMessage InReplyTo { get; private set; }
	public Empire TargetEmpire { get; private set; }
	private Package givePackage, receivePackage;

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnGive_Click(object sender, EventArgs e)
	{
		Transfer(treeWeHave, givePackage);
	}

	private void btnRequest_Click(object sender, EventArgs e)
	{
		Transfer(treeTheyHave, receivePackage);
	}

	private void btnReturn_Click(object sender, EventArgs e)
	{
		var node = treeTable.SelectedNode;
		if (node != null && node.Parent != null)
		{
			Package package = node.Parent.Text == "We Give" ? givePackage : receivePackage;
			if (node.Tag is Clause)
				package.TreatyClauses.Remove((Clause)node.Tag);
			else if (node.Tag is Planet)
				package.Planets.Remove((Planet)node.Tag);
			else if (node.Tag is IVehicle)
				package.Vehicles.Remove((IVehicle)node.Tag);
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
			else
				return;
			PopulateTable();
			if (package == givePackage)
				PopulateWeHave();
			else if (package == receivePackage)
				PopulateTheyHave();
		}
	}

	private void btnSend_Click(object sender, EventArgs e)
	{
		// create our message
		// TODO - autogenerate message text if none was specified
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

		// set it in reply to the incoming message (if any)
		msg.InReplyTo = InReplyTo;

		// create a command to send it
		var cmd = Services.Commands.Messages.SendMessage(msg);
		Empire.Current.Commands.Add(cmd);

		// all done!
		Close();
	}

	private void chkPercent_CheckedChanged(object sender, EventArgs e)
	{
		var node = treeTable.SelectedNode;
		if (node != null && node.Parent != null)
		{
			var amount = txtQuantityLevel.Text.ParseUnits();
			if (amount != null && amount > 0)
			{
				var package = ((string)node.Parent.Text).StartsWith("We") ? givePackage : receivePackage;
				if (node.Tag is TributeClause)
				{
					var clause = (TributeClause)node.Tag;
					clause.IsPercentage = chkPercent.Checked;
					PopulateTable();
				}
			}
		}
	}

	private void ddlAlliance_SelectedIndexChanged(object sender, EventArgs e)
	{
		var node = treeTable.SelectedNode;
		if (node != null && node.Parent != null)
		{
			var package = ((string)node.Parent.Text).StartsWith("We") ? givePackage : receivePackage;
			if (node.Tag is AllianceClause)
			{
				var clause = (AllianceClause)node.Tag;
				clause.AllianceLevel = (AllianceLevel)(((dynamic)ddlAlliance.SelectedItem).Value);
				PopulateTable();
			}
		}
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
			pnlQuantityLevel.Visible = false;
			ddlAlliance.Visible = false;
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
			pnlQuantityLevel.Visible = false;
			ddlAlliance.Visible = false;
			if (item == "Make Proposal")
			{
				givePackage = new Package(Empire.Current, TargetEmpire);
				receivePackage = new Package(TargetEmpire, Empire.Current);
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
			pnlQuantityLevel.Visible = false;
			ddlAlliance.Visible = false;
			var pm = (ProposalMessage)InReplyTo;
			// invert give/receive since we're on the target side of the proposal
			givePackage = pm.Proposal.ReceivePackage;
			receivePackage = pm.Proposal.GivePackage;
			PopulateTable();
		}
	}

	private void DiplomacyForm_Load(object sender, EventArgs e)
	{
		txtMessage.Focus();
	}

	private void picPortrait_MouseClick(object sender, MouseEventArgs e)
	{
		picPortrait.ShowFullSize(TargetEmpire.LeaderName + " of the " + TargetEmpire.Name);
	}

	private void PopulateSomeoneHas(Empire emp, TreeView tree, Package package)
	{
		tree.Initialize(32);

		// treaty clauses
		var treatyNode = tree.AddItemWithImage("Treaty Clauses", "Treaty Clauses", emp.Icon);
		if (!package.TreatyClauses.OfType<AllianceClause>().Any())
		{
			var allianceNode = treatyNode.AddItemWithImage("Alliances", "Alliances", emp.Icon);
			foreach (AllianceLevel alliance in Enum.GetValues(typeof(AllianceLevel)))
			{
				if (alliance != AllianceLevel.None)
				{
					var clause = new AllianceClause(package.Owner, package.Recipient, alliance);
					allianceNode.AddItemWithImage(clause.BriefDescription, clause, null);
				}
			}
		}
		if (!package.TreatyClauses.OfType<CooperativeResearchClause>().Any())
		{
			var clause = new CooperativeResearchClause(package.Owner, package.Recipient);
			treatyNode.AddItemWithImage(clause.BriefDescription, clause, null);
		}
		var freeTradeNode = treatyNode.AddItemWithImage("Free Trade", "Free Trade", Resource.Minerals.Icon);
		foreach (var res in Resource.All.Where(res => res.IsGlobal && !package.TreatyClauses.OfType<FreeTradeClause>().Any(c => c.Resource == res)))
		{
			var clause = new FreeTradeClause(package.Owner, package.Recipient, res);
			freeTradeNode.AddItemWithImage(res.Name, clause, res.Icon);
		}
		var abilNode = treatyNode.AddItemWithImage("Ability Sharing", "Ability Sharing", null);
		foreach (var abil in Mod.Current.AbilityRules.Where(abil =>
			(abil.CanTarget(AbilityTargets.Sector) || abil.CanTarget(AbilityTargets.StarSystem) || abil.CanTarget(AbilityTargets.Galaxy)) &&
			!package.TreatyClauses.OfType<ShareAbilityClause>().Any(c => c.AbilityRule == abil)))
		{
			var clause = new ShareAbilityClause(package.Owner, package.Recipient, abil, SharingPriority.Medium);
			abilNode.AddItemWithImage(abil.Name, clause, null);
		}
		if (!package.TreatyClauses.OfType<ShareCombatLogsClause>().Any())
		{
			var clause = new ShareCombatLogsClause(package.Owner, package.Recipient);
			treatyNode.AddItemWithImage(clause.BriefDescription, clause, null);
		}
		if (!package.TreatyClauses.OfType<ShareDesignsClause>().Any())
		{
			var clause = new ShareDesignsClause(package.Owner, package.Recipient);
			treatyNode.AddItemWithImage(clause.BriefDescription, clause, null);
		}
		if (!package.TreatyClauses.OfType<ShareVisionClause>().Any())
		{
			var clause = new ShareVisionClause(package.Owner, package.Recipient);
			treatyNode.AddItemWithImage(clause.BriefDescription, clause, null);
		}
		var tributeNode = treatyNode.AddItemWithImage("Tribute", "Tribute", Resource.Minerals.Icon);
		foreach (var res in Resource.All.Where(res => res.IsGlobal && !package.TreatyClauses.OfType<FreeTradeClause>().Any(c => c.Resource == res)))
		{
			var clause = new TributeClause(package.Owner, package.Recipient, res, 10, true);
			tributeNode.AddItemWithImage(res.Name, clause, res.Icon);
		}

		// sort planets alphabetically
		var planetsNode = tree.AddItemWithImage("Planets", "Planets", Pictures.GetGenericImage(typeof(Planet)));
		foreach (var p in emp.ColonizedPlanets.Where(p => !package.Planets.Contains(p)).OrderBy(p => p.Name))
			planetsNode.AddItemWithImage(p.Name, p, p.Icon);

		// sort vehicles descending by size, then alphabetically
		// no trading units that are in cargo
		var vehiclesNode = tree.AddItemWithImage("Vehicles", "Vehicles", Pictures.GetVehicleTypeImage(emp.ShipsetPath, VehicleTypes.Ship));
		foreach (var v in emp.OwnedSpaceObjects.OfType<ISpaceVehicle>().Where(v => !package.Vehicles.Contains(v) && !(v is IUnit && ((IUnit)v).Container is ISpaceObject)).OrderByDescending(v => v.Design.Hull.Size).ThenBy(v => v.Name))
			vehiclesNode.AddItemWithImage(v.Name, v, v.Icon);

		// resources
		var resourcesNode = tree.AddItemWithImage("Resources", "Resources", Resource.Minerals.Icon);
		foreach (var r in Resource.All.Where(r => r.IsGlobal))
			resourcesNode.AddItemWithImage(emp.StoredResources[r].ToUnitString(true) + " " + r, r, r.Icon);

		// technology
		var techNode = tree.AddItemWithImage("Technology", "Technology", Resource.Research.Icon);
		foreach (var kvp in emp.ResearchedTechnologies.Where(kvp => kvp.Value > 0))
			techNode.AddItemWithImage(kvp.Key.Name + " level " + kvp.Value, kvp.Key, kvp.Key.Icon);

		// star charts
		var chartsNode = tree.AddItemWithImage("Star Charts", "Star Charts", Pictures.GetGenericImage(typeof(StarSystem)));
		foreach (var sys in emp.ExploredStarSystems.Where(sys => !package.StarCharts.Contains(sys)))
			chartsNode.AddItemWithImage(sys.Name, sys, sys.Icon);

		// comms channels
		var commsNode = tree.AddItemWithImage("Communications Channels", "Communications Channels", Pictures.GetGenericImage(typeof(Empire)));
		foreach (var ee in emp.EncounteredEmpires.Where(ee => !package.CommunicationChannels.Contains(ee)))
			commsNode.AddItemWithImage(ee.Name, ee, ee.Icon);
	}

	private void PopulateTable()
	{
		treeTable.Initialize(32);
		PopulateTable("We Give", givePackage);
		PopulateTable("We Receive", receivePackage);
		pnlQuantityLevel.Visible = false;
		ddlAlliance.Visible = false;
	}

	private void PopulateTable(string text, Package package)
	{
		var node = treeTable.AddItemWithImage(text, package, package.Owner.Icon);

		foreach (var c in package.TreatyClauses)
			node.AddItemWithImage(c.BriefDescription, c, null); // TODO - treaty clause icons?

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

		treeTable.ExpandAll();
	}

	private void PopulateTheyHave()
	{
		PopulateSomeoneHas(TargetEmpire, treeTheyHave, receivePackage);
	}

	private void PopulateWeHave()
	{
		PopulateSomeoneHas(Empire.Current, treeWeHave, givePackage);
	}

	private void Transfer(TreeView tree, Package package)
	{
		var node = tree.SelectedNode;
		if (node != null && node.Parent != null)
		{
			var type = (string)node.Parent.Tag;
			if (node.Tag is Clause)
			{
				var c = (Clause)node.Tag;
				package.TreatyClauses.Add(c);
			}
			else if (type == "Planets")
			{
				var p = (Planet)node.Tag;
				package.Planets.Add(p);
			}
			else if (type == "Vehicles")
			{
				var v = (IVehicle)node.Tag;
				package.Vehicles.Add(v);
			}
			else if (type == "Resources")
			{
				var res = (Resource)node.Tag;
				package.Resources.Add(res, Math.Min(10000, package.Owner.StoredResources[res]));
			}
			else if (type == "Technology")
			{
				var tech = (Technology)node.Tag;
				package.Technology.Add(tech, package.Owner.ResearchedTechnologies[tech]);
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
			else
				return;
			PopulateSomeoneHas(package.Owner, tree, package);
			PopulateTable();
		}
	}

	private void treeTable_AfterSelect(object sender, TreeViewEventArgs e)
	{
		// see if we need to display any detail editor
		if (e.Node != null && e.Node.Parent != null)
		{
			var package = ((string)e.Node.Parent.Text).StartsWith("We") ? givePackage : receivePackage;
			if (e.Node.Tag is Resource)
			{
				// display resource quantity editor
				pnlQuantityLevel.Visible = true;
				txtQuantityLevel.Text = package.Resources[(Resource)e.Node.Tag].ToUnitString(true);
				lblQuantityLevel.Text = "Quantity";
				chkPercent.Visible = false;
				ddlAlliance.Visible = false;
			}
			else if (e.Node.Tag is Technology)
			{
				// display tech level editor
				pnlQuantityLevel.Visible = true;
				txtQuantityLevel.Text = package.Technology[(Technology)e.Node.Tag].ToString();
				lblQuantityLevel.Text = "Level";
				chkPercent.Visible = false;
				ddlAlliance.Visible = false;
			}
			else if (e.Node.Tag is AllianceClause)
			{
				// display alliance clause editor
				pnlQuantityLevel.Visible = false;
				ddlAlliance.Visible = true;
				var alliance = ((AllianceClause)e.Node.Tag).AllianceLevel;
				ddlAlliance.SelectedItem = ddlAlliance.Items.Cast<dynamic>().Single(d => d.Value == alliance);
			}
			else if (e.Node.Tag is TributeClause)
			{
				// display tribute clause editor
				pnlQuantityLevel.Visible = true;
				var clause = (TributeClause)e.Node.Tag;
				lblQuantityLevel.Text = "Quantity";
				txtQuantityLevel.Text = clause.Quantity.ToUnitString(true);
				chkPercent.Visible = true;
				chkPercent.Checked = clause.IsPercentage;
				ddlAlliance.Visible = false;
			}
			else
			{
				// hide editor
				pnlQuantityLevel.Visible = false;
				ddlAlliance.Visible = false;
			}
		}
		else
		{
			// hide editor
			pnlQuantityLevel.Visible = false;
			ddlAlliance.Visible = false;
		}
	}

	private void treeTable_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
			btnReturn_Click(treeTable, e);
	}

	private void treeTheyHave_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
			btnRequest_Click(treeTheyHave, e);
	}

	private void treeWeHave_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
			btnGive_Click(treeWeHave, e);
	}

	private void txtQuantityLevel_TextChanged(object sender, EventArgs e)
	{
		var node = treeTable.SelectedNode;
		if (node != null && node.Parent != null)
		{
			var amount = txtQuantityLevel.Text.ParseUnits();
			if (amount != null && amount > 0)
			{
				var package = ((string)node.Parent.Text).StartsWith("We") ? givePackage : receivePackage;
				if (node.Tag is Resource)
				{
					var r = (Resource)treeTable.SelectedNode.Tag;
					package.Resources[r] = (int)amount;
					PopulateTable();
				}
				else if (node.Tag is Technology)
				{
					var tech = (Technology)treeTable.SelectedNode.Tag;
					package.Technology[tech] = (int)amount;
					PopulateTable();
				}
				else if (node.Tag is TributeClause)
				{
					var clause = (TributeClause)node.Tag;
					clause.Quantity = (int)amount;
					PopulateTable();
				}
			}
		}
	}
}