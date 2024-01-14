using FrEee.Extensions;
using FrEee.WinForms.DataGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FrEee.WinForms.Controls;

public partial class GameGridView : UserControl
{
	public GameGridView()
	{
		InitializeComponent();
		Data = Enumerable.Empty<object>();
		DataType = typeof(object);
		GridConfigs = new List<GridConfig>();
		CurrentGridConfig = new GridConfig();
		gridData.AutoGenerateColumns = false;
		foreach (Control c in Controls)
			c.MouseDown += c_MouseDown;
	}

	/// <summary>
	/// Any extra menu items which should be added to the end of the context menu.
	/// </summary>
	public IEnumerable<ToolStripMenuItem> AppendMenuItems { get; set; }

	/// <summary>
	/// Creates a default grid config.
	/// </summary>
	public Func<GridConfig> CreateDefaultGridConfig { get; set; }

	/// <summary>
	/// The current grid config.
	/// </summary>
	public GridConfig CurrentGridConfig { get; set; }

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
	/// Loads the current grid config from client settings.
	/// </summary>
	public Func<GridConfig> LoadCurrentGridConfig { get; set; }

	/// <summary>
	/// Loads the set of grid configs from client settings.
	/// </summary>
	public Func<ICollection<GridConfig>> LoadGridConfigs { get; set; }

	/// <summary>
	/// Any extra menu items which should be added to the beginning of the context menu.
	/// </summary>
	public IEnumerable<ToolStripMenuItem> PrependMenuItems { get; set; }

	/// <summary>
	/// Resets the saved grid configs to default settings.
	/// </summary>
	public Func<ICollection<GridConfig>> ResetGridConfigs { get; set; }

	public object SelectedItem
	{
		get
		{
			if (gridData.SelectedRows.Count == 1)
				return gridData.SelectedRows[0].DataBoundItem;
			return null;
		}
	}

	/// <summary>
	/// Show configuration options to the user?
	/// </summary>
	public bool ShowConfigs
	{
		get
		{
			return showConfigs;
		}
		set
		{
			showConfigs = value;
			if (showConfigs)
			{
				pnlConfigEdit.Visible = true;
				pnlConfigsBorder.Visible = true;
				gridData.Dock = DockStyle.None;
			}
			else
			{
				pnlConfigEdit.Visible = false;
				pnlConfigsBorder.Visible = false;
				gridData.Dock = DockStyle.Fill;
			}
		}
	}

	private int hoverColumn = -1;

	private int hoverRow = -1;

	private IComparable hoverValue = null;

	private bool showConfigs = true;

	/// <summary>
	/// Call this after setting up all the grid config loading functions.
	/// </summary>
	public void Initialize()
	{
		if (LoadGridConfigs != null)
			GridConfigs = LoadGridConfigs();
		if (GridConfigs == null && ResetGridConfigs != null)
			GridConfigs = ResetGridConfigs();
		if (LoadCurrentGridConfig != null)
			CurrentGridConfig = LoadCurrentGridConfig();
		if (CurrentGridConfig == null)
			throw new NullReferenceException("A current grid config is required for the GameGridView.");
		BindTabs();
		BindGrid(true);
	}

	private void atLeastToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (hoverColumn >= 0)
		{
			CurrentGridConfig.Columns[hoverColumn].Filter = Filter.Minimum;
			CurrentGridConfig.Columns[hoverColumn].FilterValue = hoverValue;
		}
		BindGrid(false);
	}

	private void atMostToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (hoverColumn >= 0)
		{
			CurrentGridConfig.Columns[hoverColumn].Filter = Filter.Maximum;
			CurrentGridConfig.Columns[hoverColumn].FilterValue = hoverValue;
		}
		BindGrid(false);
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
		var filteredData = Data.ExceptSingle(null);
		foreach (var col in CurrentGridConfig.Columns)
		{
			if (col.Filter == Filter.Exact)
				filteredData = filteredData.Where(p => col.FilterValue.CompareTo(p.GetPropertyValue(col.PropertyName)) == 0);
			if (col.Filter == Filter.Different)
				filteredData = filteredData.Where(p => col.FilterValue.CompareTo(p.GetPropertyValue(col.PropertyName)) != 0);
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
				sortedData = sortedData.ThenBy(p => p.GetComparablePropertyValue(col.PropertyName));
				if (gridCol != null && gridCol.SortMode != DataGridViewColumnSortMode.NotSortable)
					gridCol.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
			}
			else if (col.Sort == Sort.Descending)
			{
				sortedData = sortedData.ThenByDescending(p => p.GetComparablePropertyValue(col.PropertyName));
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

	private void BindTabs()
	{
		pnlConfigs.Controls.Clear();

		// add buttons for each existing config
		if (GridConfigs == null)
			GridConfigs = new List<GridConfig>();
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

	private void btnColumns_Click(object sender, EventArgs e)
	{
		// TODO - edit columns
	}

	private void btnConfig_Click(object sender, EventArgs e)
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

	private void c_MouseDown(object sender, MouseEventArgs e)
	{
		OnMouseDown(e);
	}

	private void clearAllFiltersToolStripMenuItem_Click(object sender, EventArgs e)
	{
		foreach (var c in CurrentGridConfig.Columns)
		{
			c.Filter = Filter.None;
			c.FilterValue = null;
		}
	}

	private void differentFromToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (hoverColumn >= 0)
		{
			CurrentGridConfig.Columns[hoverColumn].Filter = Filter.Different;
			CurrentGridConfig.Columns[hoverColumn].FilterValue = hoverValue;
		}
		BindGrid(false);
	}

	private void exactlyToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (hoverColumn >= 0)
		{
			CurrentGridConfig.Columns[hoverColumn].Filter = Filter.Exact;
			CurrentGridConfig.Columns[hoverColumn].FilterValue = hoverValue;
		}
		BindGrid(false);
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

	private void gridData_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (e.RowIndex >= 0) // don't allow selection of header row, it crashes and makes no sense anyway
		{
			// select moused row, even if not left button
			gridData.ClearSelection();
			gridData.Rows[e.RowIndex].Selected = true;
		}
	}

	private void gridData_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
	{
		contextMenu.Items.Clear();
		PrependMenuItems.SafeForeach(i => contextMenu.Items.Add(i));
		contextMenu.Items.Add(noFilterToolStripMenuItem);
		contextMenu.Items.Add(exactlyToolStripMenuItem);
		contextMenu.Items.Add(differentFromToolStripMenuItem);
		contextMenu.Items.Add(atLeastToolStripMenuItem);
		contextMenu.Items.Add(atMostToolStripMenuItem);
		AppendMenuItems.SafeForeach(i => contextMenu.Items.Add(i));

		if (e.RowIndex < 0 || e.ColumnIndex < 0)
		{
			hoverValue = null;
			noFilterToolStripMenuItem.Visible = false;
			exactlyToolStripMenuItem.Visible = false;
			differentFromToolStripMenuItem.Visible = false;
			atLeastToolStripMenuItem.Visible = false;
			atMostToolStripMenuItem.Visible = false;
		}
		else
		{
			var cell = gridData.Rows[e.RowIndex].Cells[e.ColumnIndex];
			hoverValue = cell.Value as IComparable;
			noFilterToolStripMenuItem.Visible = cell.Value is IComparable;
			exactlyToolStripMenuItem.Visible = cell.Value is IComparable;
			differentFromToolStripMenuItem.Visible = cell.Value is IComparable;
			atLeastToolStripMenuItem.Visible = cell.Value is IComparable;
			atMostToolStripMenuItem.Visible = cell.Value is IComparable;
			hoverColumn = e.ColumnIndex;
		}

		hoverRow = e.RowIndex;
	}

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

	private void gridData_DataError(object sender, DataGridViewDataErrorEventArgs e)
	{
		// ignore silly errors
		e.ThrowException = false;
	}

	private void gridData_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		OnMouseDoubleClick(e);
	}

	private void gridData_RowEnter(object sender, DataGridViewCellEventArgs e)
	{
		if (RowEnter != null)
			RowEnter(sender, e);
	}

	private void gridData_RowLeave(object sender, DataGridViewCellEventArgs e)
	{
		if (RowLeave != null)
			RowEnter(sender, e);
	}

	private void noFilterToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (hoverColumn >= 0)
		{
			CurrentGridConfig.Columns[hoverColumn].Filter = Filter.None;
			CurrentGridConfig.Columns[hoverColumn].FilterValue = null;
		}
		BindGrid(false);
	}

	private void txtConfigName_TextChanged(object sender, EventArgs e)
	{
		CurrentGridConfig.Name = txtConfigName.Text;
		BindTabs();
	}

	public event DataGridViewCellEventHandler RowEnter;

	public event DataGridViewCellEventHandler RowLeave;
}