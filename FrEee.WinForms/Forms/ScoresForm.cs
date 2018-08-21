using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;
using FrEee.WinForms.DataGridView;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
	public partial class ScoresForm : Form
	{
		public ScoresForm()
		{
			InitializeComponent();
		}

		private void ScoresForm_Load(object sender, EventArgs e)
		{
			// show graph
			// TODO - show other empires' scores in graph
			var max = Empire.Current.Scores.MaxOrDefault(kvp => kvp.Key);
			int s = 0;
			var dps = new List<double>();
			for (var i = 0; i <= max; i++)
			{
				if (Empire.Current.Scores[i] == null)
					dps.Add(s); // carry over score from previous turn
				else
				{
					dps.Add(Empire.Current.Scores[i].Value);
					s = Empire.Current.Scores[i].Value;
				}
			}
			graph.DataPoints = dps;

			// show grid
			grid.CurrentGridConfig = new GridConfig();
			grid.CurrentGridConfig.Columns.Add(new GridColumnConfig("Name", "Empire", typeof(DataGridViewTextBoxColumn), Color.White));
			grid.CurrentGridConfig.Columns.Add(new GridColumnConfig("Score", "Score", typeof(DataGridViewProgressColumn), Color.White, DataGridView.Format.UnitsBForBillions, DataGridView.Sort.Descending));
			grid.Data = Galaxy.Current.Empires;
			grid.Initialize();
		}
	}
}