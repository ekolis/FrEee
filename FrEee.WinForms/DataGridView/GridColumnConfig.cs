using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace FrEee.WinForms.DataGridView
{
	/// <summary>
	/// Configuration for a grid column.
	/// </summary>
	[Serializable]
	public class GridColumnConfig
	{
		public GridColumnConfig(string propertyName, string headerText, Type columnType, Color foreColor, Sort sort = Sort.None, int sortPriority = 0)
		{
			PropertyName = propertyName;
			HeaderText = headerText;
			ColumnType = columnType;
			ForeColor = foreColor;
			Sort = sort;
			SortPriority = sortPriority;
		}

		/// <summary>
		/// The name of the data property.
		/// </summary>
		public string PropertyName { get; set; }

		/// <summary>
		/// The text for the column header.
		/// </summary>
		public string HeaderText { get; set; }

		private Type columnType;

		/// <summary>
		/// The type of data grid view column to use.
		/// </summary>
		public Type ColumnType
		{
			get { return columnType; }
			set
			{
				if (!typeof(DataGridViewColumn).IsAssignableFrom(value))
					throw new Exception("Grid column config's column type must be a DataGridViewColumn type.");
				columnType = value;
			}
		}

		/// <summary>
		/// The color to display text in.
		/// </summary>
		public Color ForeColor { get; set; }

		/// <summary>
		/// How to sort the data.
		/// </summary>
		public Sort Sort { get; set; }

		/// <summary>
		/// Priority of sorting this column (lower is higher priority)
		/// </summary>
		public int SortPriority { get; set; }
	}
}
