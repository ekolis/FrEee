using System.Drawing;

namespace FrEee.UI.Blazor.Components;

public class GameButtonViewModel : ViewModelBase
{
	public string Text { get; set; } = string.Empty;
	public bool Enabled { get; set; } = true;
	public Image? Image { get; set; }
	public ImageDisplayViewModel ImageVM => new() { Image = Image };
	public Color BackColor { get; set; } = Color.Black;
	public Color ForeColor { get; set; } = Color.CornflowerBlue;
	public Action? OnClick { get; set; }
}
