using FrEee.Objects.Civilization;
using FrEee.Objects.Technology;
using FrEee.Modding;

namespace FrEee.UI.Blazor.Components.Screens;

public class ResearchViewModel : ScreenViewModelBase
{
	public Empire? Empire => Empire.Current;
	public IEnumerable<Technology> AvailableTechs => Mod.Current?.Technologies ?? Enumerable.Empty<Technology>();
	public Technology? SelectedTech { get; set; }

	public void SetPrimaryResearch(Technology tech)
	{
		// Set primary tech
	}
}
