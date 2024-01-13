using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Objects.Commands;
using FrEee.Objects.Space;
using FrEee.Objects.Technology;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Extensions;
using FrEee.WinForms.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
	public partial class ResearchForm : GameForm
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
			ddlGroup.Items.Add(new { Text = "All" });
			ddlGroup.Items.Add(new { Text = "In Progress" });
			foreach (var group in Mod.Current.Technologies.Select(t => t.Group).Distinct())
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

			try { this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); }
			catch { }
		}

		private bool abort;

		private IEnumerable<Technology> allTechs;

		private Technology curTech;

		private bool hasChanged = false;

		private Dictionary<Technology, int> oldPcts;

		private List<Technology> oldQueue;

		private bool ready = false;

		private DataGridViewColumn sortColumn = null;

		private ListSortDirection sortDir = ListSortDirection.Ascending;

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
				sldSpending.Maximum = (int)(100 - spent + curTech.Spending.Value);
				sldSpending.Value = (int)curTech.Spending.Value;
				sldSpending.Enabled = true;
				if (curTech.Progress.Eta == null)
					lblResults.Text = "Expected Results (never)";
				else
					lblResults.Text = "Expected Results (in " + curTech.Progress.Eta + " turn" + (curTech.Progress.Eta == 1 ? "" : "s") + ")";
				lstResults.Initialize(32, 32);
				foreach (var result in curTech.ExpectedResults)
					lstResults.AddItemWithImage(result.ResearchGroup, result.Name, result, result.Icon);
			}
		}

		private void BindQueue()
		{
			lstQueue.Items.Clear();
			var idx = 0;
			var levels = new SafeDictionary<Technology, int>(Empire.Current.ResearchedTechnologies);
			Empire.Current.ComputeResearchProgress();
			foreach (var tech in Empire.Current.ResearchQueue)
			{
				levels[tech]++; // so we can research the same tech multiple times with the appropriate cost for each level
				var eta = Empire.Current.GetResearchProgress(tech, levels[tech]).Eta;
				if (eta == null)
				{
					if (tech.MaximumLevel == 1 && levels[tech] == 1)
						lstQueue.Items.Add(tech.Name + " (never)");
					else
						lstQueue.Items.Add(tech.Name + " L" + levels[tech] + " (never)");
				}
				else
				{
                    if (tech.MaximumLevel == 1 && levels[tech] == 1)
						lstQueue.Items.Add(tech.Name + " (" + eta + " turn" + (eta == 1 ? "" : "s") + ")");
					else
						lstQueue.Items.Add(tech.Name + " L" + levels[tech] + " (" + eta + " turn" + (eta == 1 ? "" : "s") + ")");
				}
				idx++;
			}
		}

		private void BindTechGrid()
		{
			allTechs = Empire.Current.AvailableTechnologies.Where(t =>
				{
					dynamic item = (dynamic)ddlGroup.SelectedItem;
					if (item.Text == "All")
						return true; // show all techs
					else if (item.Text == "In Progress")
						return t.Progress.Eta != null || t.Progress.Value > 0; // show techs in progress (either started or being researched)
					else
						return t.Group == item.GroupName; // show techs in selected group
				}).ToArray();
			RebindTechGrid();
		}

		private void btnAddToQueue_Click(object sender, EventArgs e)
		{
			TryAddTechToQueue(curTech);
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

		private void btnCancel_Click(object sender, EventArgs e)
		{
			abort = true;
			Cancel();
			Close();
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

		private void btnSave_Click(object sender, EventArgs e)
		{
			abort = true;
			Save();
			Close();
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

		private void btnTree_Click(object sender, EventArgs e)
		{
			this.ShowChildForm(new TechTreeForm());
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

		private void Cancel()
		{
			Empire.Current.ResearchSpending.Clear();
			foreach (var kvp in oldPcts)
				Empire.Current.ResearchSpending.Add(kvp);
			Empire.Current.ResearchQueue.Clear();
			foreach (var tech in oldQueue)
				Empire.Current.ResearchQueue.Add(tech);
		}

		private void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			BindTechGrid();
			gridTechs.ClearSelection();
			curTech = null;
			BindDetails();
		}

		private void gridQueues_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.RowIndex >= 0 && e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				var tech = (Technology)gridTechs.Rows[e.RowIndex].DataBoundItem;
				TryAddTechToQueue(tech);
			}
		}

		private void gridQueues_RowEnter(object sender, DataGridViewCellEventArgs e)
		{
			if (ready)
			{
				curTech = (Technology)gridTechs.Rows[e.RowIndex].DataBoundItem;
				BindDetails();
			}
		}

		private void gridTechs_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			ListSortDirection? dir = null;
			var col = gridTechs.Columns[e.ColumnIndex];
			if (sortColumn == col)
			{
				// already sorting by this column, change sort mode
				if (sortDir == ListSortDirection.Ascending)
					dir = ListSortDirection.Descending;
				else if (sortDir == ListSortDirection.Descending)
					dir = ListSortDirection.Ascending;
				else
					col = null; // unsort
			}
			if (dir == null)
			{
				// default sort order for each column
				if (col == colProgress || col == colSpending)
					dir = ListSortDirection.Descending;
				else
					dir = ListSortDirection.Ascending;
			}
			foreach (DataGridViewColumn c in gridTechs.Columns)
			{
				if (c == col)
				{
					if (dir == ListSortDirection.Ascending)
						c.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
					else if (dir == ListSortDirection.Descending)
						c.HeaderCell.SortGlyphDirection = SortOrder.Descending;
					else
						c.HeaderCell.SortGlyphDirection = SortOrder.None;
				}
				else
					c.HeaderCell.SortGlyphDirection = SortOrder.None;
			}

			sortDir = dir.Value;
			sortColumn = col;

			RebindTechGrid();
		}

		private void lstQueue_DoubleClick(object sender, EventArgs e)
		{
			if (lstQueue.SelectedIndex >= 0)
			{
				Empire.Current.ResearchQueue.RemoveAt(lstQueue.SelectedIndex);
				BindTechGrid();
				BindQueue();
				hasChanged = true;
			}
		}

		private void lstResults_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				var u = lstResults.SelectedItems.Cast<ListViewItem>().Select(i => i.Tag).Cast<IUnlockable>().SingleOrDefault();
				if (u != null)
					this.ShowChildForm(new TechTreeForm(u));
			}
		}

		private void RebindTechGrid()
		{
			var sorted = new List<Technology>(allTechs);

			// save selected tech since we're messing with the grid order
			var selTech = curTech;

			if (sortColumn == colName)
				sorted.Sort((t1, t2) => t1.Name.CompareTo(t2.Name));
			else if (sortColumn == colLevel)
				sorted.Sort((t1, t2) => t1.CurrentLevel.CompareTo(t2.CurrentLevel));
			else if (sortColumn == colNextLevelCost)
				sorted.Sort((t1, t2) => t1.NextLevelCost.CompareTo(t2.NextLevelCost));
			else if (sortColumn == colProgress)
			{
				// always show techs with no progress at the bottom
				var defaultValue = sortDir == ListSortDirection.Ascending ? double.MaxValue : double.MinValue;
				// sort first by ETA, then by RP required to complete but in the opposite order
				sorted = sorted.OrderBy(t => t.Progress.RawEta ?? defaultValue).ThenBy(t => t.Progress.Value - t.Progress.Maximum).ToList();
			}
			else if (sortColumn == colSpending)
				sorted.Sort((t1, t2) => t1.Spending.Value.CompareTo(t2.Spending.Value));

			if (sortDir == ListSortDirection.Descending && sortColumn != null)
				sorted.Reverse();

			technologyBindingSource.DataSource = sorted;
			technologyBindingSource.ResetBindings(false);

			// reselect previously selected tech
			curTech = selTech;
			foreach (DataGridViewRow row in gridTechs.Rows)
			{
				if (row.DataBoundItem == curTech)
				{
					row.Selected = true;
					gridTechs.FirstDisplayedScrollingRowIndex = row.Index;
				}
				else
					row.Selected = false;
			}
			BindDetails();
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

		private void ResearchForm_Load(object sender, EventArgs e)
		{
			curTech = null;
			BindTechGrid();
			BindDetails();
			ready = true;
		}

		private void ResearchForm_MouseEnter(object sender, EventArgs e)
		{
			ready = true;
		}

		private void Save()
		{
			var cmd = new ResearchCommand();
			cmd.Spending.Clear();
			foreach (var kvp in Empire.Current.ResearchSpending)
				cmd.Spending[kvp.Key] = kvp.Value;
			cmd.Queue.Clear();
			foreach (var tech in Empire.Current.ResearchQueue)
				cmd.Queue.Add(tech);
			Empire.Current.ResearchCommand = cmd;
		}

		private void sldSpending_Scroll(object sender, EventArgs e)
		{
			Empire.Current.ResearchSpending[curTech] = sldSpending.Value;
			RebindTechGrid();
			BindQueue();
			BindDetails();
			hasChanged = true;
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
					BindTechGrid();
					BindQueue();
					hasChanged = true;
				}
			}
		}
	}
}