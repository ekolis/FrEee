using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.UI.Blazor.Components.Reports;
using FrEee.Vehicles.Types;

namespace FrEee.UI.Blazor.Components.Pages;

public class ShipListViewModel : PageViewModelBase
{
	public IEnumerable<ISpaceVehicle> Vehicles => Empire.Current?.OwnedSpaceObjects.OfType<ISpaceVehicle>() ?? Enumerable.Empty<ISpaceVehicle>();
	public ISpaceVehicle? SelectedVehicle { get; set; }
	public SpaceVehicleReportViewModel SpaceVehicleReportViewModel => new() { Vehicle = SelectedVehicle };
}
