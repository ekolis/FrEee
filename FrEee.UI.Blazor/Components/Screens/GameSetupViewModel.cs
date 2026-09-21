using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Processes.Setup;
using FrEee.Modding;

namespace FrEee.UI.Blazor.Components.Screens;

public class GameSetupViewModel : ScreenViewModelBase
{
	public GameSetup Setup { get; set; } = new();
	public string GameName { get; set; } = "New Game";
	public int StarSystems { get; set; } = 25;
	public int GalaxyWidth { get; set; } = 10;
	public int GalaxyHeight { get; set; } = 10;
	public List<Empire> Empires { get; set; } = new();

	public void AddEmpire()
	{
		var emp = new Empire { Name = $"Empire {Empires.Count + 1}" };
		Empires.Add(emp);
	}

	public void RemoveEmpire(Empire emp)
	{
		Empires.Remove(emp);
	}

	public void StartGame()
	{
		// Game initialization logic
		Close();
	}
}
