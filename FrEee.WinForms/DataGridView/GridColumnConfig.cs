using FrEee.Utility; using FrEee.Serialization;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FrEee.WinForms.DataGridView;

/// <summary>
/// Configuration for a grid column.
/// </summary>
[Serializable]
public class GridColumnConfig
{
	public GridColumnConfig(string propertyName, string headerText, Type columnType, Color foreColor, Format format = Format.Raw, Sort sort = Sort.None, int sortPriority = 0, Filter filter = Filter.None, IComparable filterValue = null)
	{
		PropertyName = propertyName;
		HeaderText = headerText;
		ColumnType = columnType;
		ForeColor = foreColor;
		Format = format;
		Sort = sort;
		SortPriority = sortPriority;
		Filter = filter;
		FilterValue = filterValue;
	}

	/// <summary>
	/// The type of data grid view column to use.
	/// </summary>
	[DoNotSerialize]
	public Type ColumnType
	{
		get
		{
			if (columnTypeName == null)
				return null;
			return Type.GetType(columnTypeName);
		}
		set
		{
			if (!typeof(DataGridViewColumn).IsAssignableFrom(value))
				throw new Exception("Grid column config's column type must be a DataGridViewColumn type.");
			columnTypeName = value.AssemblyQualifiedName;
		}
	}

	public string columnTypeName { get; set; }

	/// <summary>
	/// Type of filter to use on this column.
	/// </summary>
	public Filter Filter { get; set; }

	/// <summary>
	/// Value to filter by.
	/// </summary>
	public IComparable FilterValue { get; set; }

	/// <summary>
	/// The color to display text in.
	/// </summary>
	public Color ForeColor { get; set; }

	/// <summary>
	/// Format for the column.
	/// </summary>
	public Format Format { get; set; }

	/// <summary>
	/// The text for the column header.
	/// </summary>
	public string HeaderText { get; set; }

	/// <summary>
	/// The name of the data property.
	/// </summary>
	public string PropertyName { get; set; }

	/// <summary>
	/// How to sort the data.
	/// </summary>
	public Sort Sort { get; set; }

	/// <summary>
	/// Priority of sorting this column (lower is higher priority)
	/// </summary>
	public int SortPriority { get; set; }
}