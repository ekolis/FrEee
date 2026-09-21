using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Vehicles.Types;

namespace FrEee.UI.Blazor.Components.Screens;

public class ShipListViewModel : ScreenViewModelBase
{
	public IEnumerable<ISpaceVehicle> Vehicles => Empire.Current?.OwnedSpaceObjects.OfType<ISpaceVehicle>() ?? Enumerable.Empty<ISpaceVehicle>();
	public ISpaceVehicle? SelectedVehicle { get; set; }
}
