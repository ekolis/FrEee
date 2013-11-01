using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Commands;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.WinForms.DataGridView;
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
	public partial class ResearchForm : Form
	{
        
		public ResearchForm()
		{
			InitializeComponent();

			// show RP available
			resRes.Amount = Empire.Current.NetIncome[Resource.Research];
			if (Empire.Current.BonusResearch != 0)
				resRes.Change = Empire.Current.BonusResearch;

			// bind group dropdown and tech grid
			ddlGroup.Items.Clear();
			ddlGroup.Items.Add(new { Text = "All", GroupName = "" });
			foreach (var group in Galaxy.Current.Mod.Technologies.Select(t => t.Group).Distinct())
				ddlGroup.Items.Add(new { Text = group, GroupName = group });
			ddlGroup.SelectedItem = ddlGroup.Items.Cast<object>().First();

			// save off old research priorities in case user cancels
			oldPcts = new Dictionary<Technology, int>();
			foreach (var kvp in Empire.Current.ResearchSpending)
				oldPcts.Add(kvp.Key, kvp.Value);
			oldQueue = new List<Technology>();
			foreach (var tech in Empire.Current.ResearchQueue)
				oldQueue.Add(tech);

			// bind queue
			BindQueue();

			try {this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon);} catch {}
		}

		private IEnumerable<Technology> allTechs;
		private Technology curTech;

		private void BindTechGrid()
		{
			allTechs = Empire.Current.AvailableTechnologies.Where(t => ((dynamic)ddlGroup.SelectedItem).GroupName == "" || t.Group == ((dynamic)ddlGroup.SelectedItem).GroupName).ToArray();
			technologyBindingSource.DataSource = allTechs.ToArray();
		}

		private void RebindTechGrid()
		{
			technologyBindingSource.ResetBindings(false);
		}

		private void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			BindTechGrid();
		}

		private void gridQueues_RowEnter(object sender, DataGridViewCellEventArgs e)
		{
			curTech = (Technology)gridQueues.Rows[e.RowIndex].DataBoundItem;
			BindDetails();
		}

		private void BindDetails()
		{
			if (curTech == null)
			{
				txtTechName.Text = "(No Technology)";
                txtTechDiscription.Text = "(No Technology)";                
				lblSpending.Text = "Spending";
				sldSpending.Maximum = 100;
				sldSpending.Value = 0;
				sldSpending.Enabled = false;
				lblResults.Text = "Expected Results";
				lstResults.Items.Clear();
			}
			else
			{
				txtTechName.Text = curTech.Name;
                txtTechDiscription.Text = curTech.Description; 
				var spent = allTechs.Sum(t => t.Spending.Value);
				lblSpending.Text = "Spending (" + (100 - spent) + "% unspent)";
				sldSpending.Maximum = 100 - spent + curTech.Spending.Value;
				sldSpending.Value = curTech.Spending.Value;
				sldSpending.Enabled = true;
				if (curTech.Progress.Eta == null)
					lblResults.Text = "Expected Results (never)";
				else
					lblResults.Text = "Expected Results (in " + curTech.Progress.Eta + " turns)";
				lstResults.Initialize(32, 32);
				foreach (var result in curTech.ExpectedResults)
					lstResults.AddItemWithImage(result.ResearchGroup, result.Name, result, result.Icon);
			}
		}

		private void sldSpending_Scroll(object sender, EventArgs e)
		{
			Empire.Current.ResearchSpending[curTech] = sldSpending.Value;
			RebindTechGrid();
			BindQueue();
			BindDetails();
            hasChanged = true;
		}

		private bool abort;
        private bool hasChanged = false;
		private Dictionary<Technology, int> oldPcts;
		private List<Technology> oldQueue;

		private void btnSave_Click(object sender, EventArgs e)
		{
			abort = true;
			Save();
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			abort = true;
			Cancel();
			Close();
		}

		private void ResearchForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!abort && hasChanged)
			{
				switch (MessageBox.Show("Save your changes?", "FrEee", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
				{
					case DialogResult.Yes:
						Save();
						break;
					case DialogResult.No:
						Cancel();
						break;
					case DialogResult.Cancel:
						e.Cancel = true;
						break;
				}
			}
		}

		private void Save()
		{
			var cmd = new ResearchCommand();
			cmd.ClearSpending();
			foreach (var kvp in Empire.Current.ResearchSpending)
				cmd.SetSpending(kvp.Key, kvp.Value);
			cmd.ClearQueue();
			foreach (var tech in Empire.Current.ResearchQueue)
				cmd.AddToQueue(tech);
			Empire.Current.ResearchCommand = cmd;
		}

		private void Cancel()
		{
			Empire.Current.ResearchSpending.Clear();
			foreach (var kvp in oldPcts)
				Empire.Current.ResearchSpending.Add(kvp);
			Empire.Current.ResearchQueue.Clear();
			foreach (var tech in oldQueue)
				Empire.Current.ResearchQueue.Add(tech);
		}

		private void BindQueue()
		{           
			lstQueue.Items.Clear();
			var idx = 0;
			var levels = new Dictionary<Technology, int>(Empire.Current.ResearchedTechnologies);
			Empire.Current.ComputeResearchProgress();
			foreach (var tech in Empire.Current.ResearchQueue)
			{
				levels[tech]++; // so we can research the same tech multiple times with the appropriate cost for each level
				var eta = Empire.Current.GetResearchProgress(tech, levels[tech]).Eta;
				if (eta == null)
					lstQueue.Items.Add(tech.Name + " L" + levels[tech] + " (never)");
				else
					lstQueue.Items.Add(tech.Name + " L" + levels[tech] + " (" + eta + " turns)");
				idx++;
			}
		}

		private void btnAddToQueue_Click(object sender, EventArgs e)
		{
			TryAddTechToQueue(curTech);
		}

		private void btnTop_Click(object sender, EventArgs e)
		{
			var selIdx = lstQueue.SelectedIndex;
			if (selIdx >= 0)
			{
				var selTech = Empire.Current.ResearchQueue[selIdx];
				Empire.Current.ResearchQueue.RemoveAt(selIdx);
				Empire.Current.ResearchQueue.Insert(0, selTech);
				BindQueue();
                hasChanged = true;
			}
		}

		private void btnBottom_Click(object sender, EventArgs e)
		{
			var selIdx = lstQueue.SelectedIndex;
			if (selIdx >= 0)
			{
				var selTech = Empire.Current.ResearchQueue[selIdx];
				Empire.Current.ResearchQueue.RemoveAt(selIdx);
				Empire.Current.ResearchQueue.Add(selTech);
				BindQueue();
                hasChanged = true;
			}
		}

		private void btnUp_Click(object sender, EventArgs e)
		{
			var selIdx = lstQueue.SelectedIndex;
			if (selIdx >= 0)
			{
				var selTech = Empire.Current.ResearchQueue[selIdx];
				Empire.Current.ResearchQueue.RemoveAt(selIdx);
				Empire.Current.ResearchQueue.Insert(Math.Max(0, selIdx - 1), selTech);
				BindQueue();
                hasChanged = true;
			}
		}

		private void btnDown_Click(object sender, EventArgs e)
		{
			var selIdx = lstQueue.SelectedIndex;
			if (selIdx >= 0)
			{
				var selTech = Empire.Current.ResearchQueue[selIdx];
				Empire.Current.ResearchQueue.RemoveAt(selIdx);
				Empire.Current.ResearchQueue.Insert(Math.Min(Empire.Current.ResearchQueue.Count, selIdx + 1), selTech);
				BindQueue();
                hasChanged = true;
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			Empire.Current.ResearchQueue.Clear();
			BindQueue();
            hasChanged = true;
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			var selIdx = lstQueue.SelectedIndex;
			if (selIdx >= 0)
			{
				var selTech = Empire.Current.ResearchQueue[selIdx];
				Empire.Current.ResearchQueue.RemoveAt(selIdx);
				BindQueue();
                hasChanged = true;
			}
		}

		private void gridQueues_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				var tech = (Technology)gridQueues.Rows[e.RowIndex].DataBoundItem;
				TryAddTechToQueue(tech);
			}
		}

		private void TryAddTechToQueue(Technology tech)
		{
			if (tech != null)
			{
				if (Empire.Current.ResearchedTechnologies[tech] + Empire.Current.ResearchQueue.Where(t => t == tech).Count() >= tech.MaximumLevel)
					MessageBox.Show("The maximum level for " + tech + " is " + tech.MaximumLevel + ".", "Cannot Research Further");
				else
				{
					Empire.Current.ResearchQueue.Add(tech);
					BindQueue();
					hasChanged = true;
				}
			}
		}

		private void lstQueue_DoubleClick(object sender, EventArgs e)
		{
			if (lstQueue.SelectedIndex >= 0)
			{
				Empire.Current.ResearchQueue.RemoveAt(lstQueue.SelectedIndex);
				BindQueue();
				hasChanged = true;
			}
		}
	}
}
