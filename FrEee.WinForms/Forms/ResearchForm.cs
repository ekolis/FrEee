using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Commands;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
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
			txtPoints.Text = Empire.Current.Income["Research"].ToString();

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
		}

		private IEnumerable<Technology> allTechs;
		private Technology curTech;

		private void BindTechGrid()
		{
			allTechs = Empire.Current.AvailableTechnologies.Where(t => ((dynamic)ddlGroup.SelectedItem).GroupName == "" || t.Group == ((dynamic)ddlGroup.SelectedItem).GroupName).ToArray();
			technologyBindingSource.DataSource = allTechs.ToArray();
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
				var spent = allTechs.Sum(t => t.Spending.Value);
				lblSpending.Text = "Spending (" + (100 - spent) + "% unspent";
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
			BindTechGrid();
		}

		private bool abort;
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
			if (!abort)
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
			var cmd = new ResearchCommand(Empire.Current);
			cmd.Spending = Empire.Current.ResearchSpending;
			cmd.Queue = Empire.Current.ResearchQueue;
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
	}
}
