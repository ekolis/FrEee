using FrEee.Objects.Space;
using System.Drawing;

namespace FrEee.UI.Blazor.Components;

public class StarSystemViewModel : ViewModelBase
{
	public StarSystem? StarSystem { get; set; }
	public Sector? SelectedSector { get; set; }
	public ISpaceObject? SelectedSpaceObject { get; set; }
	public bool DrawText { get; set; } = true;

	public Action<Sector>? SectorSelected { get; set; }
	public Action<ISpaceObject>? SpaceObjectSelected { get; set; }

	public void SelectSector(Sector? sector)
	{
		SelectedSector = sector;
		if (sector != null)
			SectorSelected?.Invoke(sector);
	}
}
