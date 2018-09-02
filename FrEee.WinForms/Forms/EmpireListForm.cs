using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Commands;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Utility.Extensions;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
	public partial class EmpireListForm : GameForm
	{
		public EmpireListForm()
		{
			InitializeComponent();
			try { base.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); }
			catch { }
			BindEmpires();
		}

		private Empire empire;

		private void BindEmpire(Empire emp, TabPage tab = null)
		{
			empire = emp;
			if (tab != null)
				tabs.SelectedTab = tab;
			else if (emp == Empire.Current && tabs.SelectedTab == tabDiplomacy)
				tabs.SelectedTab = tabBudget;
			else if (emp != Empire.Current && tabs.SelectedTab == tabBudget)
				tabs.SelectedTab = tabDiplomacy;

			report.Empire = emp;

			if (emp == null)
			{
				txtTreaty.Text = "N/A";
				txtTreaty.ForeColor = Color.White;
			}
			else
			{
				if (emp == Empire.Current)
				{
					txtTreaty.Text = "Self";
					txtTreaty.ForeColor = Color.CornflowerBlue;
				}
				else
				{
					var treaty = Empire.Current.GetTreaty(emp);
					var giving = treaty.Where(c => c.Giver == Empire.Current);
					var receiving = treaty.Where(c => c.Receiver == Empire.Current);
					var givingTexts = giving.Select(c => c.ToString());
					var receivingTexts = receiving.Select(c => c.ToString());
					if (!treaty.Any())
					{
						txtTreaty.Text = "None";
						txtTreaty.ForeColor = Color.Yellow;
					}
					else if (!giving.Any())
					{
						txtTreaty.Text = "Receiving " + string.Join(", ", receivingTexts.ToArray());
						txtTreaty.ForeColor = Color.Green;
					}
					else if (!receiving.Any())
					{
						txtTreaty.Text = "Giving " + string.Join(", ", givingTexts.ToArray());
						txtTreaty.ForeColor = Color.LightGray;
					}
					else
					{
						var mutualTexts = givingTexts.Join(receivingTexts, s => s, s => s, (g, r) => g);
						var givingOnlyTexts = givingTexts.Except(mutualTexts);
						var receivingOnlyTexts = receivingTexts.Except(mutualTexts);
						if (givingOnlyTexts.Any() && receivingTexts.Any())
							txtTreaty.Text = "Trading " + string.Join(", ", givingOnlyTexts.ToArray()) + " for " + string.Join(", ", receivingOnlyTexts.ToArray());
						else if (givingOnlyTexts.Any())
							txtTreaty.Text = "Giving " + string.Join(", ", givingOnlyTexts.ToArray());
						else if (receivingOnlyTexts.Any())
							txtTreaty.Text = "Receiving " + string.Join(", ", receivingOnlyTexts.ToArray());
						if (mutualTexts.Any())
						{
							if (givingOnlyTexts.Any() || receivingOnlyTexts.Any())
								txtTreaty.Text = "Mutual " + string.Join(", ", mutualTexts.ToArray()) + "; " + txtTreaty.Text;
							else
								txtTreaty.Text = "Mutual " + string.Join(", ", mutualTexts.ToArray());
						}
						txtTreaty.ForeColor = Color.White;
					}
				}

				// budget
				if (emp == Empire.Current)
					rqdConstruction.ResourceQuantity = emp.ConstructionSpending;
				else
					// assume other empires' construction queues are running at full capacity
					rqdConstruction.ResourceQuantity = emp.ConstructionQueues.Sum(rq => rq.Rate);
				rqdExtraction.ResourceQuantity = emp.ColonyIncome + emp.RemoteMiningIncome + emp.RawResourceIncome;
				rqdIncome.ResourceQuantity = emp.GrossDomesticIncome;
				rqdMaintenance.ResourceQuantity = emp.Maintenance;
				rqdNet.ResourceQuantity = emp.NetIncomeLessConstruction;
				rqdStorage.ResourceQuantity = emp.ResourceStorage;
				var spoilageOrDeficit = new ResourceQuantity();
				var newResources = emp.StoredResources + emp.NetIncomeLessConstruction;
				foreach (var r in Resource.All)
				{
					if (newResources[r] > emp.ResourceStorage[r])
						spoilageOrDeficit[r] = newResources[r] - emp.ResourceStorage[r];
					else if (newResources[r] < 0)
						spoilageOrDeficit[r] = newResources[r];
					else
						spoilageOrDeficit[r] = 0;
				}
				rqdSpoiledDeficit.ResourceQuantity = spoilageOrDeficit;
				rqdStored.ResourceQuantity = emp.StoredResources;
				rqdTrade.ResourceQuantity = emp.TradeIncome;
				rqdTributesIn.ResourceQuantity = new ResourceQuantity(); // TODO - show tributes
				rqdTributesOut.ResourceQuantity = new ResourceQuantity(); // TODO - show tributes
				rqExpenses.ResourceQuantity = rqdConstruction.ResourceQuantity + rqdMaintenance.ResourceQuantity + rqdTributesOut.ResourceQuantity;
				lblBudgetWarning.Visible = emp != Empire.Current;

				// message log
				var msgs = Empire.Current.IncomingMessages.Where(m => m.Owner == emp).Union(Empire.Current.SentMessages.Where(m => m.Recipient == emp)).Union(Empire.Current.Commands.OfType<SendMessageCommand>().Select(cmd => cmd.Message));
				lstMessages.Initialize(64, 64);
				foreach (var msg in msgs.OrderByDescending(m => m.TurnNumber))
					lstMessages.AddItemWithImage(msg.TurnNumber.ToStardate(), "", msg, msg.Owner.Portrait, msg.Owner == Empire.Current ? "Us" : msg.Owner.Name, msg.Recipient == Empire.Current ? "Us" : msg.Recipient.Name, msg.Text);

				// player info
				txtName.Text = emp.PlayerInfo.Name;
				lnkPbw.Text = emp.PlayerInfo.Pbw;
				lnkEmail.Text = emp.PlayerInfo.Email;
				txtIrc.Text = emp.PlayerInfo.Irc;
				txtDiscord.Text = emp.PlayerInfo.Discord;
				lnkWebsite.Text = emp.PlayerInfo.Website;
				txtNotes.Text = emp.PlayerInfo.Notes;
			}
		}

		private void BindEmpires()
		{
			lstEmpires.Initialize(128, 128);
			foreach (var emp in Galaxy.Current.Empires.ExceptSingle((Empire)null))
			{
				var item = lstEmpires.AddItemWithImage(null, emp.Name, emp, emp.Portrait);
				if (emp == Empire.Current)
					item.Selected = true;
			}
		}

		private void btnAvoidSystems_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Sorry, marking systems to avoid is not yet implemented.");
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnCompose_Click(object sender, EventArgs e)
		{
			if (this.ShowChildForm(new DiplomacyForm(empire)) == DialogResult.OK)
				BindEmpire(empire, tabDiplomacy);
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			var msgs = lstMessages.SelectedItems.Cast<ListViewItem>().Select(item => (IMessage)item.Tag);
			if (msgs.Any())
			{
				string confirm;
				if (msgs.Count() == 1)
					confirm = "Delete this message?";
				else
					confirm = "Delete these " + msgs.Count() + " messages?";
				if (MessageBox.Show(confirm, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					// delete messages from inbox/outbox/sentbox
					foreach (var msg in msgs)
					{
						if (Empire.Current.IncomingMessages.Contains(msg) || Empire.Current.SentMessages.Contains(msg))
						{
							var cmd = new DeleteMessageCommand(msg);
							Empire.Current.Commands.Add(cmd);
							cmd.Execute();
						}
						var sendCommand = Empire.Current.Commands.OfType<SendMessageCommand>().SingleOrDefault(cmd => cmd.Message == msg);
						if (sendCommand != null)
							Empire.Current.Commands.Remove(sendCommand);
					}

					// refresh
					BindEmpire(empire, tabDiplomacy);
				}
			}
			else
				MessageBox.Show("Please select one or more messages to delete before clicking the delete button.");
		}

		private void btnMinisters_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Sorry, control of AI ministers is not yet implemented.");
		}

		private void btnReply_Click(object sender, EventArgs e)
		{
			var item = lstMessages.SelectedItems.Count != 1 ? null : lstMessages.SelectedItems[0];
			if (item != null)
				ReplyToMessage((IMessage)item.Tag);
		}

		private void btnScores_Click(object sender, EventArgs e)
		{
			this.ShowChildForm(new ScoresForm());
		}

		private void btnWaypoints_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Sorry, setting waypoints is not yet implemented.");
		}

		private void lstEmpires_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			var item = e.Item;
			if (e.IsSelected)
				BindEmpire((Empire)e.Item.Tag);
			else
				BindEmpire(null);
		}

		private void lstMessages_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				var item = lstMessages.SelectedItems.Count == 1 ? lstMessages.SelectedItems[0] : null;
				if (item != null)
					ReplyToMessage((IMessage)item.Tag);
			}
		}

		private void lstMessages_SizeChanged(object sender, EventArgs e)
		{
			lstMessages.Columns[3].Width = Math.Max(100, lstMessages.Width - lstMessages.Columns.Cast<ColumnHeader>().Take(3).Sum(c => c.Width));
		}

		private void ReplyToMessage(IMessage msg)
		{
			if (msg.Recipient == Empire.Current)
			{
				if (this.ShowChildForm(new DiplomacyForm(msg)) == DialogResult.OK)
					BindEmpire(empire, tabDiplomacy);
			}
			else
				MessageBox.Show("You cannot reply to outgoing messages.");
		}

		private void lnkPbw_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start($"http://pbw.spaceempires.net/profile/{lnkPbw.Text}");
		}

		private void lnkEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start($"mailto:{lnkEmail.Text}");
		}

		private void lnkWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			// prevent attacks by players who set their website to "deltree /y c:\" :)
			if (lnkWebsite.Text.StartsWith("http://") || lnkWebsite.Text.StartsWith("https://"))
			{
				Process.Start(lnkWebsite.Text);
			}
		}
	}
}