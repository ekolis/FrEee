namespace FrEee.UI.Blazor.Components.Screens;

public class OptionsViewModel : ScreenViewModelBase
{
	public int Volume { get; set; } = 80;
	public bool Fullscreen { get; set; } = false;
	public bool AutoSave { get; set; } = true;

	public void Save()
	{
		Close();
	}
}
