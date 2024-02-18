using System;
using System.Collections.Generic;

namespace FrEee.WinForms.DataGridView;

/// <summary>
/// Configuration for a grid view.
/// </summary>
[Serializable]
public class GridConfig
{
	public GridConfig()
	{
		Columns = new List<GridColumnConfig>();
	}

	public IList<GridColumnConfig> Columns { get; private set; }
	public string Name { get; set; }
}