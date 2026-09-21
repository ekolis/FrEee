using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Objects.Space;

namespace FrEee.UI.Blazor.Components.Screens;

public class PlanetListViewModel : ScreenViewModelBase
{
	public IEnumerable<Planet> ColonizedPlanets => Empire.Current?.ColonizedPlanets ?? Enumerable.Empty<Planet>();
	public Planet? SelectedPlanet { get; set; }
}
