using FrEee.Objects.Civilization;

namespace FrEee.UI.Blazor.Components.Screens;

public class MinistersViewModel : ScreenViewModelBase
{
	public Empire? Empire => Empire.Current;
	public bool AutoColonize { get; set; } = true;
	public bool AutoBuild { get; set; } = false;
	public bool AutoResearch { get; set; } = false;

	public void Save()
	{
		Close();
	}
}
