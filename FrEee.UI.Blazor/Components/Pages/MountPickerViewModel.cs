using FrEee.Modding;
using FrEee.Objects.Technology;

namespace FrEee.UI.Blazor.Components.Pages;

public class MountPickerViewModel : PageViewModelBase
{
	public IEnumerable<Mount> Mounts => Mod.Current?.Mounts ?? Enumerable.Empty<Mount>();
	public Mount? SelectedMount { get; set; }

	public void Select()
	{
		Close();
	}
}
