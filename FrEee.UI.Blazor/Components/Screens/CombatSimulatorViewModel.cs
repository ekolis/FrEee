using FrEee.Objects.Civilization;
using FrEee.Processes.Combat;
using FrEee.Vehicles.Types;

namespace FrEee.UI.Blazor.Components.Screens;

public class CombatSimulatorViewModel : ScreenViewModelBase
{
	public List<ISpaceVehicle> Side1 { get; set; } = new();
	public List<ISpaceVehicle> Side2 { get; set; } = new();
	public IBattle? BattleResult { get; set; }

	public void StartSimulation()
	{
		// Simulate battle logic
	}
}
