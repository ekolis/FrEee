using FrEee.Modding;
using FrEee.Objects.Civilization;

namespace FrEee.UI.Blazor.Components.Screens;

public class EmpireSetupViewModel : ScreenViewModelBase
{
	public Empire Empire { get; set; } = new();
	public string EmpireName { get; set; } = "Human Empire";
	public string LeaderName { get; set; } = "Emperor";
	public Culture? SelectedCulture { get; set; } = Mod.Current?.Cultures.FirstOrDefault();
	public Race PrimaryRace { get; set; } = new() { Name = "Human" };
	public TraitPickerViewModel TraitPickerVM { get; set; } = new();
	public AptitudePickerViewModel AptitudePickerVM { get; set; } = new();

	public void Save()
	{
		Empire.Name = EmpireName;
		Empire.LeaderName = LeaderName;
		Empire.Culture = SelectedCulture;
		Empire.PrimaryRace = PrimaryRace;
		Close();
	}
}
