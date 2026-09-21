using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Vehicles.Types;

namespace FrEee.UI.Blazor.Components.Screens;

public class FleetTransferViewModel : ScreenViewModelBase
{
	public Fleet? SourceFleet { get; set; }
	public Fleet? TargetFleet { get; set; }
	public ISpaceVehicle? SelectedVehicle { get; set; }

	public void TransferVehicle(ISpaceVehicle vehicle, Fleet from, Fleet to)
	{
		from.Vehicles.Remove(vehicle);
		to.Vehicles.Add(vehicle);
	}
}
