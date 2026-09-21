using FrEee.Objects.Civilization.CargoStorage;
using FrEee.Objects.Civilization;
using FrEee.Vehicles.Types;
using FrEee.Extensions;

namespace FrEee.UI.Blazor.Components;

public class CargoItemDisplay
{
	public string Name { get; set; } = string.Empty;
	public string Quantity { get; set; } = string.Empty;
	public string Size { get; set; } = string.Empty;
	public object? Tag { get; set; }
}

public class CargoListViewModel : ViewModelBase
{
	public Cargo? Cargo { get; set; }
	public CargoDelta? CargoDelta { get; set; }

	public List<CargoItemDisplay> Items
	{
		get
		{
			var list = new List<CargoItemDisplay>();
			if (Cargo is not null)
			{
				foreach (var pop in Cargo.Population)
				{
					list.Add(new CargoItemDisplay
					{
						Name = pop.Key.Name + " Population",
						Quantity = pop.Value.ToUnitString(),
						Size = (pop.Value * 1000).Kilotons(),
						Tag = pop.Key
					});
				}
				foreach (var unit in Cargo.Units)
				{
					list.Add(new CargoItemDisplay
					{
						Name = unit.Name,
						Quantity = "1",
						Size = unit.Design.Hull.Size.Kilotons(),
						Tag = unit
					});
				}
			}
			return list;
		}
	}
}
