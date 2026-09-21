using System.Drawing;

namespace FrEee.UI.Blazor.Components;

public class GamePanelViewModel : ViewModelBase
{
	public Color BackColor { get; set; } = Color.Black;
	public Color ForeColor { get; set; } = Color.White;
	public Color BorderColor { get; set; } = Color.CornflowerBlue;
	public int Padding { get; set; } = 3;
	public bool HasBorder { get; set; } = true;
}
