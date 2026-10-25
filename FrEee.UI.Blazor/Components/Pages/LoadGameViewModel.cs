using Microsoft.AspNetCore.Components.Routing;

namespace FrEee.UI.Blazor.Components.Pages;

public class LoadGameViewModel
	: PageViewModelBase
{
	public ILookup<string, LoadableTurn> GameTurns { get; }
	
	public string? SelectedGameName { get; private set; }

	public LoadGameViewModel()
	{
		GameTurns = FindGameTurns();
	}
	
	public void SelectGame(string gameName)
	{
		SelectedGameName = gameName;
	}

	public void LoadTurn(LoadableTurn turn)
	{
		Navigation.NavigateTo(PagePaths.CreateMainGame(turn.Name, turn.TurnNumber, turn.PlayerNumber));
	}

	private ILookup<string, LoadableTurn> FindGameTurns()
	{
		// find all turns in the Savegame directory
		// TODO: since this is a web app, we'll need to filter by player for online play, which will require user accounts
		var turns = new List<LoadableTurn>();
		foreach (var gamFile in Directory.GetFiles("wwwroot/" + FrEeeConstants.SaveGameDirectory, "*" + FrEeeConstants.SaveGameExtension))
		{
			// skip files that aren't formatted like "game_1_0001.gam"
			// TODO: include host gam files
			var splitFileName = Path.GetFileNameWithoutExtension(gamFile).Split('_');
			if (splitFileName.Length != 3) continue;
			if (!int.TryParse(splitFileName[1].TrimStart("0"), out var turnNumber)) continue;
			if (!int.TryParse(splitFileName[2].TrimStart("0"), out var playerNumber)) continue;

			var turn = new LoadableTurn(splitFileName[0], turnNumber, playerNumber);
			turns.Add(turn);
		}
		
		// group turns by game
		var games = turns.ToLookup(t => t.Name);
		
		return games;
	}
	
	public record LoadableTurn(string Name, int TurnNumber, int PlayerNumber);
}
