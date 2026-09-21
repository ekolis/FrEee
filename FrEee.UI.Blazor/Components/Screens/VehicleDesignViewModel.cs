using FrEee.Modding;
using FrEee.Objects.Civilization;
using FrEee.Objects.Technology;
using FrEee.Vehicles;
using FrEee.Modding.Templates;

namespace FrEee.UI.Blazor.Components.Screens;

public class VehicleDesignViewModel : ScreenViewModelBase
{
	public string DesignName { get; set; } = "New Design";
	public string Role { get; set; } = "Warship";
	public IHull? SelectedHull { get; set; } = Mod.Current?.Hulls.FirstOrDefault();
	public List<MountedComponentTemplate> Components { get; set; } = new();

	public void AddComponent(ComponentTemplate template, Mount? mount = null)
	{
		Components.Add(new MountedComponentTemplate(null, template, mount));
	}

	public void RemoveComponent(MountedComponentTemplate comp)
	{
		Components.Remove(comp);
	}

	public void SaveDesign()
	{
		// Save vehicle design logic
		Close();
	}
}
