using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Extensions;
using FrEee.WinForms.DataGridView;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms;

public partial class ScoresForm : GameForm
{
	public ScoresForm()
	{
		InitializeComponent();
	}

	private void ScoresForm_Load(object sender, EventArgs e)
	{
		// show graph
		foreach (var emp in Galaxy.Current.Empires.ExceptSingle(null))
		{
			var dps = new List<double>();
			int s = 0;
			for (var i = 0; i <= Galaxy.Current.TurnNumber; i++)
			{
				if (emp.Scores[i] == null)
					dps.Add(s); // carry over score from previous turn
				else
				{
					dps.Add(emp.Scores[i].Value);
					s = emp.Scores[i].Value;
				}
			}
			graph.Series.Add(new Controls.LineGraph.GraphSeries
			{
				Name = emp.Name,
				Color = emp.Color,
				DataPoints = dps,
			});
		}

		// show grid
		grid.CurrentGridConfig = new GridConfig();
		grid.CurrentGridConfig.Columns.Add(new GridColumnConfig("Icon", "Insignia", typeof(DataGridViewImageColumn), Color.White));
		grid.CurrentGridConfig.Columns.Add(new GridColumnConfig("Name", "Empire", typeof(DataGridViewTextBoxColumn), Color.White));
		grid.CurrentGridConfig.Columns.Add(new GridColumnConfig("Score", "Score", typeof(DataGridViewTextBoxColumn), Color.White, DataGridView.Format.UnitsBForBillions, DataGridView.Sort.Descending));
		grid.Data = Galaxy.Current.Empires;
		grid.Initialize();
	}
}