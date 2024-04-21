namespace FrEee.UI.WinForms.DataGridView;

/// <summary>
/// Type of filter to use for a column.
/// </summary>
public enum Filter
{
	/// <summary>
	/// No filter.
	/// </summary>
	None,

	/// <summary>
	/// Filters to an exact value.
	/// </summary>
	Exact,

	/// <summary>
	/// Filters out a specific value.
	/// </summary>
	Different,

	/// <summary>
	/// Filters out all values less than a specific value.
	/// </summary>
	Minimum,

	/// <summary>
	/// Filters out all values greater than a specific value.
	/// </summary>
	Maximum
}