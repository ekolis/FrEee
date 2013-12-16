using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee.Utility.Extensions;
using FrEee.WinForms.DataGridView;
using FrEee.WinForms.Objects;
using FrEee.Game.Objects.Space;

namespace FrEee.WinForms.Controls
{
	public partial class GameGridView : UserControl
	{
		public GameGridView()
		{
			InitializeComponent();
			Data = Enumerable.Empty<object>();
			DataType = typeof(object);
			GridConfigs = new List<GridConfig>();
			CurrentGridConfig = new GridConfig();
			CreateDefaultGridConfig = () => new GridConfig();
			LoadCurrentGridConfig = () => new GridConfig();
			ResetGridConfigs = () => new List<GridConfig>();
			gridData.AutoGenerateColumns = false;
		}

		/// <summary>
		/// Call this after setting up all the grid config loading functions.
		/// </summary>
		public void Initialize()
		{
			GridConfigs = LoadGridConfigs();
			CurrentGridConfig = LoadCurrentGridConfig();
			BindTabs();
			BindGrid(true);
		}

		/// <summary>
		/// The data to display in the grid.
		/// </summary>
		public IEnumerable<object> Data { get; set; }

		/// <summary>
		/// The type of data to display.
		/// </summary>
		public Type DataType { get; set; }

		/// <summary>
		/// The collection of grid configs that can be used and edited.
		/// </summary>
		public ICollection<GridConfig> GridConfigs { get; set; }

		/// <summary>
		/// The current grid config.
		/// </summary>
		public GridConfig CurrentGridConfig { get; set; }

		/// <summary>
		/// Creates a default grid config.
		/// </summary>
		public Func<GridConfig> CreateDefaultGridConfig { get; set; }

		/// <summary>
		/// Loads the current grid config from client settings.
		/// </summary>
		public Func<GridConfig> LoadCurrentGridConfig { get; set; }

		/// <summary>
		/// Loads the set of grid configs from client settings.
		/// </summary>
		public Func<ICollection<GridConfig>> LoadGridConfigs { get; set; }

		/// <summary>
		/// Resets the saved grid configs to default settings.
		/// </summary>
		public Func<ICollection<GridConfig>> ResetGridConfigs { get; set; }

		private void BindTabs()
		{
			pnlConfigs.Controls.Clear();

			// add buttons for each existing config
			foreach (var cfg in GridConfigs)
			{
				var btn = new GameButton();
				btn.Text = cfg.Name;
				btn.Tag = cfg;
				btn.Click += btnConfig_Click;
				pnlConfigs.Controls.Add(btn);
			}

			// add button for a new config
			{
				var btn = new GameButton();
				btn.Text = "(New Config)";
				btn.Click += btnConfig_Click;
				pnlConfigs.Controls.Add(btn);
			}

			// highlight the current config
			foreach (var b in pnlConfigs.Controls.OfType<GameButton>())
				b.BackColor = b.Tag == CurrentGridConfig ? Color.Navy : Color.Black;
		}

		void btnConfig_Click(object sender, EventArgs e)
		{
			var btn = (GameButton)sender;
			if (btn.Tag == null)
			{
				// create a new config based on the default and activate it
				var cfg = CreateDefaultGridConfig();
				GridConfigs.Add(cfg);
				CurrentGridConfig = cfg;
				btn.Tag = cfg;
				BindTabs();
				BindGrid(true);
				txtConfigName.Text = cfg.Name;
			}
			else
			{
				// activate the selected config
				var cfg = (GridConfig)btn.Tag;
				CurrentGridConfig = cfg;
				BindGrid(true);
				txtConfigName.Text = cfg.Name;
			}
			foreach (var b in pnlConfigs.Controls.OfType<GameButton>())
				b.BackColor = b.Tag == CurrentGridConfig ? Color.Navy : Color.Black;
		}

		private void BindGrid(bool refreshColumns)
		{
			if (refreshColumns)
			{
				gridData.Columns.Clear();
				// Don't display columns that have an exact filter; that would be pointless
				foreach (var col in CurrentGridConfig.Columns.Where(c => c.Filter != Filter.Exact))
				{
					var gridcol = (DataGridViewColumn)Activator.CreateInstance(col.ColumnType);
					gridcol.DataPropertyName = col.PropertyName;
					gridcol.HeaderText = col.HeaderText;
					gridcol.DefaultCellStyle.ForeColor = col.ForeColor;
					gridData.Columns.Add(gridcol);
				}
			}

			// do filtering
			var filteredData = Data;
			foreach (var col in CurrentGridConfig.Columns)
			{
				if (col.Filter == Filter.Exact)
					filteredData = filteredData.Where(p => col.FilterValue.CompareTo(p.GetPropertyValue(col.PropertyName)) == 0);
				else if (col.Filter == Filter.Minimum)
					filteredData = filteredData.Where(p => col.FilterValue.CompareTo(p.GetPropertyValue(col.PropertyName)) <= 0);
				else if (col.Filter == Filter.Maximum)
					filteredData = filteredData.Where(p => col.FilterValue.CompareTo(p.GetPropertyValue(col.PropertyName)) >= 0);
			}

			// do sorting
			var sortedData = filteredData.OrderBy(p => "");
			foreach (var col in CurrentGridConfig.Columns.OrderBy(c => c.SortPriority))
			{
				var gridCol = gridData.Columns.Cast<DataGridViewColumn>().SingleOrDefault(c => c.DataPropertyName == col.PropertyName);
				if (col.Sort == Sort.Ascending)
				{
					sortedData = sortedData.ThenBy(p => p.GetPropertyValue(col.PropertyName));
					if (gridCol != null && gridCol.SortMode != DataGridViewColumnSortMode.NotSortable)
						gridCol.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
				}
				else if (col.Sort == Sort.Descending)
				{
					sortedData = sortedData.ThenByDescending(p => p.GetPropertyValue(col.PropertyName));
					if (gridCol != null && gridCol.SortMode != DataGridViewColumnSortMode.NotSortable)
						gridCol.HeaderCell.SortGlyphDirection = SortOrder.Descending;
				}
				else
				{
					if (gridCol != null && gridCol.SortMode != DataGridViewColumnSortMode.NotSortable)
						gridCol.HeaderCell.SortGlyphDirection = SortOrder.None;
				}
			}

			// placing plain old objects in the grid doesn't seem to find their properties...
			var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(DataType));
			foreach (var item in sortedData)
				list.GetType().GetMethod("Add").Invoke(list, new object[] { item });
			gridData.DataSource = list;
		}

		private void gridData_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			// ignore silly errors
			e.ThrowException = false;
		}

		private void gridData_RowEnter(object sender, DataGridViewCellEventArgs e)
		{
			if (RowEnter != null)
				RowEnter(sender, e);
		}

		public event DataGridViewCellEventHandler RowEnter;

		private void gridData_RowLeave(object sender, DataGridViewCellEventArgs e)
		{
			if (RowLeave != null)
				RowEnter(sender, e);
		}

		public event DataGridViewCellEventHandler RowLeave;

		private void gridData_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			var propname = gridData.Columns[e.ColumnIndex].DataPropertyName;
			var propvals = Data.Select(o => new { Object = o, PropertyValue = o.GetPropertyValue(propname) });
			var col = CurrentGridConfig.Columns.Single(c => c.PropertyName == propname);

			// cycle sort options
			var minPriority = CurrentGridConfig.Columns.Min(c => c.SortPriority);
			if (col.Sort == Sort.None)
			{
				col.Sort = Sort.Ascending;
				col.SortPriority = minPriority - 1;
			}
			else if (col.Sort == Sort.Ascending)
			{
				col.Sort = Sort.Descending;
				col.SortPriority = minPriority - 1;
			}
			else if (col.Sort == Sort.Descending)
			{
				col.Sort = Sort.None;
				col.SortPriority = 0;
			}

			// normalize priorities so we don't get ridiculous negative numbers and cause an overflow when the player clicks 2 billion times
			minPriority = CurrentGridConfig.Columns.Min(c => c.SortPriority);
			foreach (var c in CurrentGridConfig.Columns.Where(c => c.Sort != Sort.None))
				c.SortPriority -= minPriority;

			BindGrid(false);
		}

		private void btnDeleteConfig_Click(object sender, EventArgs e)
		{
			GridConfigs.Remove(CurrentGridConfig);
			CurrentGridConfig = GridConfigs.FirstOrDefault();

			// don't let the player have no configs
			if (CurrentGridConfig == null)
			{
				var cfg = CreateDefaultGridConfig();
				CurrentGridConfig = cfg;
				GridConfigs.Add(cfg);
			}
			BindTabs();
		}

		private void txtConfigName_TextChanged(object sender, EventArgs e)
		{
			CurrentGridConfig.Name = txtConfigName.Text;
			BindTabs();
		}

		private void btnColumns_Click(object sender, EventArgs e)
		{
			// TODO - edit columns
		}

		private void btnReset_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Reset all grid configurations to default?", "Confirm Reset", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				GridConfigs = ResetGridConfigs();
				CurrentGridConfig = GridConfigs.FirstOrDefault();
				BindTabs();
				BindGrid(true);
			}
		}

		private void gridData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			// no idea why this happens...
			if (e.ColumnIndex >= CurrentGridConfig.Columns.Count)
				return;

			var col = CurrentGridConfig.Columns[e.ColumnIndex];
			switch (col.Format)
			{
				case Format.Units:
					if (e.Value is int)
						e.Value = ((int)e.Value).ToUnitString();
					else if (e.Value is long)
						e.Value = ((long)e.Value).ToUnitString();
					else if (e.Value is double)
						e.Value = ((double)e.Value).ToUnitString();
					e.FormattingApplied = true;
					break;
				case Format.UnitsBForBillions:
					if (e.Value is int)
						e.Value = ((int)e.Value).ToUnitString(true);
					else if (e.Value is long)
						e.Value = ((long)e.Value).ToUnitString(true);
					else if (e.Value is double)
						e.Value = ((double)e.Value).ToUnitString(true);
					e.FormattingApplied = true;
					break;
				case Format.Kilotons:
					if (e.Value is int)
						e.Value = ((int)e.Value).Kilotons();
					else if (e.Value is long)
						e.Value = ((long)e.Value).Kilotons();
					else if (e.Value is double)
						e.Value = ((double)e.Value).Kilotons();
					e.FormattingApplied = true;
					break;
			}
		}

		private void gridData_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
		{
			// TODO - set up context menu to filter on cell value
		}

		public object SelectedItem
		{
			get
			{
				if (gridData.SelectedRows.Count == 1)
					return gridData.SelectedRows[0].DataBoundItem;
				return null;
			}
		}

	}
}
