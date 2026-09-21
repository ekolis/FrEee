using System.Drawing;

namespace FrEee.UI.Blazor.Components;

public class GameTableLayoutPanelViewModel : ViewModelBase
{
	public int Columns { get; set; } = 2;
	public int Rows { get; set; } = 1;
	public string ColumnWidths { get; set; } = "1fr 1fr";
	public string RowHeights { get; set; } = "auto";
	public int Gap { get; set; } = 4;
	public Color BackColor { get; set; } = Color.Transparent;
}
