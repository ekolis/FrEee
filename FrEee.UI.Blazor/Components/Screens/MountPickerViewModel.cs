using FrEee.Modding;
using FrEee.Objects.Technology;

namespace FrEee.UI.Blazor.Components.Screens;

public class MountPickerViewModel : ScreenViewModelBase
{
	public IEnumerable<Mount> Mounts => Mod.Current?.Mounts ?? Enumerable.Empty<Mount>();
	public Mount? SelectedMount { get; set; }

	public void Select()
	{
		Close();
	}
}
