using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Objects.Civilization.Diplomacy.Clauses;

namespace FrEee.UI.Blazor.Components.Screens;

public class DiplomacyViewModel : ScreenViewModelBase
{
	public IEnumerable<Empire> OtherEmpires => Game.Current?.Empires.Where(e => e != Empire.Current) ?? Enumerable.Empty<Empire>();
	public Empire? SelectedEmpire { get; set; }
	public string MessageText { get; set; } = string.Empty;

	public void SendMessage()
	{
		MessageText = string.Empty;
	}
}
