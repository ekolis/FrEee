using FrEee.Objects.Civilization.CargoStorage;

namespace FrEee.UI.Blazor.Components.Screens;

public class CargoTransferViewModel : ScreenViewModelBase
{
	public ICargoContainer? Source { get; set; }
	public ICargoContainer? Destination { get; set; }
	public CargoListViewModel SourceCargoVM { get; set; } = new();
	public CargoListViewModel DestinationCargoVM { get; set; } = new();

	public void Transfer()
	{
		Close();
	}
}
