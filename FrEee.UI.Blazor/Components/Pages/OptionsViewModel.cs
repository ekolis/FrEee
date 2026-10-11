namespace FrEee.UI.Blazor.Components.Pages;

public class OptionsViewModel : PageViewModelBase
{
	public int Volume { get; set; } = 80;
	public bool Fullscreen { get; set; } = false;
	public bool AutoSave { get; set; } = true;

	public void Save()
	{
		Close();
	}
}
