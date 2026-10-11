using FrEee.Objects.Civilization;

namespace FrEee.UI.Blazor.Components.Pages;

public class MinistersViewModel : PageViewModelBase
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
