using System.Drawing;

namespace FrEee.UI.Blazor.Components;

public class GameGridColumn
{
	public string Header { get; set; } = string.Empty;
	public string PropertyName { get; set; } = string.Empty;
	public Func<object, object?>? ValueGetter { get; set; }
	public string? Width { get; set; }
}

public class GameGridViewModel : ViewModelBase
{
	public IEnumerable<object> Data { get; set; } = Enumerable.Empty<object>();
	public List<GameGridColumn> Columns { get; set; } = new();
	public object? SelectedItem { get; set; }
	public Color BackColor { get; set; } = Color.Black;
	public Color ForeColor { get; set; } = Color.White;
	public Color SelectedRowColor { get; set; } = Color.FromArgb(0, 0, 128);
	public Action<object?>? SelectedItemChanged { get; set; }
}
