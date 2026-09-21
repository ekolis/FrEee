using System.Drawing;

namespace FrEee.UI.Blazor.Components;

public class GameTabItem
{
	public string Id { get; set; } = Guid.NewGuid().ToString();
	public string Text { get; set; } = string.Empty;
	public object? Tag { get; set; }
}

public class GameTabControlViewModel : ViewModelBase
{
	public Color BackColor { get; set; } = Color.Black;
	public Color TabBackColor { get; set; } = Color.Black;
	public Color TabForeColor { get; set; } = Color.CornflowerBlue;
	public Color SelectedTabBackColor { get; set; } = Color.CornflowerBlue;
	public Color SelectedTabForeColor { get; set; } = Color.Black;
	public Color TabBorderColor { get; set; } = Color.CornflowerBlue;

	public List<GameTabItem> Tabs { get; set; } = new();
	public int SelectedIndex { get; set; } = 0;

	public GameTabItem? SelectedTab => SelectedIndex >= 0 && SelectedIndex < Tabs.Count ? Tabs[SelectedIndex] : null;

	public Action<int>? SelectedIndexChanged { get; set; }
}
