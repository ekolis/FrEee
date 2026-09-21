using FrEee.Modding;

namespace FrEee.UI.Blazor.Components.Screens;

public class ModPickerViewModel : ScreenViewModelBase
{
	public IEnumerable<ModInfo> Mods { get; set; } = new List<ModInfo>
	{
		new() { Name = "Stock (Default)", Path = null },
		new() { Name = "Star Trek Mod", Path = "Mods/StarTrek" }
	};
	public ModInfo? SelectedMod { get; set; }

	public void Select()
	{
		Close();
	}
}

public class ModInfo
{
	public string Name { get; set; } = string.Empty;
	public string? Path { get; set; }
}
