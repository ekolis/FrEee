using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.WinForms.DataGridView;
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
		}

		private void BindTechGrid()
		{
			technologyBindingSource.DataSource = Galaxy.Current.Mod.Technologies.Where(t => ((dynamic)ddlGroup.SelectedItem).GroupName == "" || t.Group == ((dynamic)ddlGroup.SelectedItem).GroupName).ToArray();
		}

		private void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			BindTechGrid();
		}
	}
}
