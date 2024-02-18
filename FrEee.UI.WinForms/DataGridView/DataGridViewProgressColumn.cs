using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FrEee.UI.WinForms.DataGridView;

/// <summary>
/// Data grid view column which shows progress.
/// </summary>
public class DataGridViewProgressColumn : DataGridViewColumn
{
	public DataGridViewProgressColumn()
	{
		CellTemplate = new DataGridViewProgressCell();
		MinimumWidth = 100;
	}

	/// <summary>
	/// The color of the progress bar.
	/// </summary>
	[
		Category("Appearance"),
		Description("The color of the progress bar.")
	]
	public Color BarColor
	{
		get
		{
			return barColor;
		}
		set
		{
			((DataGridViewProgressCell)CellTemplate).BarColor = value;
			barColor = value;
		}
	}

	public override DataGridViewCell CellTemplate
	{
		get
		{
			return base.CellTemplate;
		}
		set
		{
			DataGridViewProgressCell cell = value as DataGridViewProgressCell;
			if (value != null && cell == null)
				throw new InvalidCastException("Value provided for CellTemplate must be of type DataGridViewProgressCell or derive from it.");
			base.CellTemplate = value;
		}
	}

	/// <summary>
	/// The type of text to display for progress.
	/// </summary>
	[
		Category("Appearance"),
		DefaultValue(ProgressDisplayMode.Raw),
		Description("The type of text to display for progress.")
	]
	public ProgressDisplayMode ProgressDisplayMode
	{
		get
		{
			return progressDisplayMode;
		}
		set
		{
			((DataGridViewProgressCell)CellTemplate).ProgressDisplayMode = value;
			progressDisplayMode = value;
		}
	}

	private Color barColor;

	private ProgressDisplayMode progressDisplayMode;

	public override object Clone()
	{
		var copy = (DataGridViewProgressColumn)base.Clone();
		copy.BarColor = BarColor;
		copy.ProgressDisplayMode = progressDisplayMode;
		return copy;
	}
}