using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrEee.WinForms.DataGridView
{
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

		public string Name { get; set; }

		public IList<GridColumnConfig> Columns { get; private set; }
	}
}
