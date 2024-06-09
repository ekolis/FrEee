using FrEee.Objects.Civilization;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Extensions;
using FrEee.UI.WinForms.Interfaces;
using FrEee.UI.WinForms.Utility.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FrEee.Objects.Civilization.CargoStorage;
using FrEee.Objects.Vehicles;
using FrEee.Extensions;

namespace FrEee.UI.WinForms.Controls;

public partial class CargoList : UserControl, IBindable<ICargoContainer>, IBindable<CargoDelta>
{
	public CargoList()
	{
		InitializeComponent();
	}

	public CargoList(ICargoContainer cargoContainer)
	{
		InitializeComponent();
		CargoContainer = cargoContainer;
	}

	public CargoList(CargoDelta cargoDelta)
	{
		InitializeComponent();
		CargoDelta = cargoDelta;
	}

	/// <summary>
	/// The cargo container whose cargo to display.
	/// </summary>
	public ICargoContainer CargoContainer
	{
		get { return cargoContainer; }
		set
		{
			cargoContainer = value;
			cargoDelta = null;
			Bind();
		}
	}

	/// <summary>
	/// The cargo delta whose cargo to display.
	/// </summary>
	public CargoDelta CargoDelta
	{
		get { return cargoDelta; }
		set
		{
			cargoDelta = value;
			cargoContainer = null;
			Bind();
		}
	}

	public SelectionType CurrentSelectionType { get; private set; }
	public Race SelectedRace { get; private set; }
	public IUnit SelectedUnit { get; private set; }
	public IDesign<IUnit> SelectedUnitDesign { get; private set; }
	public string SelectedUnitRole { get; private set; }
	public VehicleTypes SelectedUnitType { get; private set; }
	public bool ShowAllUnitsAndPopulationAlways { get; set; }
	private ICargoContainer cargoContainer;

	private CargoDelta cargoDelta;

	public void Bind(ICargoContainer data)
	{
		CargoContainer = data;
		Bind();
	}

	public void Bind(CargoDelta data)
	{
		CargoDelta = data;
		Bind();
	}

	public void Bind()
	{
		tree.Initialize(32);
		var unitTypes = new List<VehicleTypes> { VehicleTypes.Drone, VehicleTypes.Fighter, VehicleTypes.Mine, VehicleTypes.Satellite, VehicleTypes.Troop, VehicleTypes.WeaponPlatform };
		if (CargoContainer != null)
		{
			var cargo = CargoContainer.Cargo;
			var used = cargo.Size;
			var total = CargoContainer.CargoStorage;
			var free = used - total;
			TreeNode typesNode;
			if (ShowAllUnitsAndPopulationAlways)
			{
				typesNode = tree.AddItemWithImage("Units - Types", "Types", Pictures.GetVehicleTypeImage(Empire.Current.ShipsetPath));
				foreach (var vt in unitTypes)
					typesNode.AddItemWithImage($"All {vt}s", vt, Pictures.GetVehicleTypeImage(Empire.Current.ShipsetPath, vt));
				typesNode.Expand();
			}
			if (CargoContainer.AllUnits.Any())
			{
				if (!ShowAllUnitsAndPopulationAlways)
				{
					typesNode = tree.AddItemWithImage("Units - Types", "Types", Pictures.GetVehicleTypeImage(Empire.Current.ShipsetPath));
					foreach (var ug in CargoContainer.AllUnits.GroupBy(u => u.Design.VehicleType))
						typesNode.AddItemWithImage($"All {ug.Key}s", ug.Key, Pictures.GetVehicleTypeImage(Empire.Current.ShipsetPath, ug.Key));
					typesNode.Expand();
				}
				foreach (var ug in CargoContainer.AllUnits.GroupBy(u => u.Design.VehicleType))
				{
					var typeNode = tree.GetAllNodes().First(x => x.Tag.ToString() == ug.Key.ToString());
					foreach (var ug2 in ug.GroupBy(u => u.Design))
					{
						var designNode = typeNode.AddItemWithImage(ug.Count() + "x \"" + ug2.Key.Name + "\" " + ug2.Key.VehicleTypeName + " (" + ug2.Key.Hull.Size.Kilotons() + " each)", ug.Key, ug.First().Icon);
						foreach (var u in ug2)
							designNode.AddItemWithImage(u.Name, u, u.Icon);
					}
				}
				var rolesNode = tree.AddItemWithImage("Units - Roles", "Roles", Pictures.GetVehicleTypeImage(Empire.Current.ShipsetPath));
				foreach (var ug in CargoContainer.AllUnits.GroupBy(u => u.Design.Role))
					rolesNode.AddItemWithImage(ug.Count() + "x " + ug.Key, ug.Key, ug.First().Icon);
				rolesNode.Expand();
			}
			TreeNode popNode = null;
			if (ShowAllUnitsAndPopulationAlways)
			{
				popNode = tree.AddItemWithImage(CargoContainer.AllPopulation.Sum(kvp => kvp.Value).ToUnitString(true) + " Total Population (" + ((long)(Mod.Current.Settings.PopulationSize * Mod.Current.Settings.PopulationFactor)).Kilotons() + " per " + Mod.Current.Settings.PopulationFactor.ToUnitString(true) + ")", "Population", Empire.Current.PrimaryRace.Icon);
			}
			if (CargoContainer.AllPopulation.Any())
			{
				if (popNode == null)
					popNode = tree.AddItemWithImage(CargoContainer.AllPopulation.Sum(kvp => kvp.Value).ToUnitString(true) + " Total Population (" + ((long)(Mod.Current.Settings.PopulationSize * Mod.Current.Settings.PopulationFactor)).Kilotons() + " per " + Mod.Current.Settings.PopulationFactor.ToUnitString(true) + ")", "Population", Empire.Current.PrimaryRace.Icon);
				foreach (var pop in CargoContainer.AllPopulation)
					popNode.AddItemWithImage(pop.Value.ToUnitString(true) + " " + pop.Key + " Population", pop.Key, pop.Key.Icon);
				popNode.Expand();
			}

			lblTonnage.Text = "Cargo Storage Free: " + CargoContainer.CargoStorageFree().Kilotons() + "\n" + "Population Storage Free: " + CargoContainer.PopulationStorageFree.ToUnitString(true);
		}
		else if (CargoDelta != null)
		{
			TreeNode typesNode;
			if (ShowAllUnitsAndPopulationAlways)
			{
				typesNode = tree.AddItemWithImage("Units - Types", "Types", Pictures.GetVehicleTypeImage(Empire.Current.ShipsetPath));
				foreach (var vt in unitTypes)
					typesNode.AddItemWithImage($"All {vt}s", vt, Pictures.GetVehicleTypeImage(Empire.Current.ShipsetPath, vt));
				typesNode.Expand();
			}
			else
			{
				if (CargoDelta.UnitTypeTonnage.Any())
				{
					typesNode = tree.AddItemWithImage("Units - Types", "Types", Pictures.GetVehicleTypeImage(Empire.Current.ShipsetPath));
					foreach (var ug in CargoDelta.UnitTypeTonnage)
						typesNode.AddItemWithImage(ug.Value.Kilotons("All") + " " + ug.Key.ToSpacedString() + "s", ug.Key, Pictures.GetVehicleTypeImage(Empire.Current.ShipsetPath, ug.Key));
					typesNode.Expand();
				}
			}

			if (CargoDelta.UnitRoleTonnage.Any())
			{
				var rolesNode = tree.AddItemWithImage("Units - Roles", "Roles", Pictures.GetVehicleTypeImage(Empire.Current.ShipsetPath));
				foreach (var ug in CargoDelta.UnitRoleTonnage)
					rolesNode.AddItemWithImage(ug.Value.Kilotons("All") + " " + ug.Key + "s", ug.Key, Pictures.GetVehicleTypeImage(Empire.Current.ShipsetPath));
				rolesNode.Expand();
			}
			if (CargoDelta.UnitDesignTonnage.Any())
			{
				var designsNode = tree.AddItemWithImage("Units - Designs", "Designs", Pictures.GetVehicleTypeImage(Empire.Current.ShipsetPath));
				foreach (var ug in CargoDelta.UnitDesignTonnage)
					designsNode.AddItemWithImage(ug.Value.Kilotons("All") + " \"" + ug.Key.Name + "\" " + ug.Key.VehicleType + "s + (" + ug.Key.Hull.Size.Kilotons() + " each)", ug.Key, ug.Key.Icon);
				designsNode.Expand();
			}
			if (CargoDelta.Units.Any())
			{
				var unitsNode = tree.AddItemWithImage("Units - Individual (" + CargoDelta.Units.Count + ")", "Units", Pictures.GetVehicleTypeImage(Empire.Current.ShipsetPath));
				foreach (var u in CargoDelta.Units)
					unitsNode.AddItemWithImage(u.Name, u, u.Icon);
				// don't expand the units node, there's probably tons of stuff there!
			}
			TreeNode popNode = null;
			if (ShowAllUnitsAndPopulationAlways || CargoDelta.AllPopulation)
			{
				popNode = tree.AddItemWithImage("All Population (" + ((long)(Mod.Current.Settings.PopulationSize * Mod.Current.Settings.PopulationFactor)).Kilotons() + " per " + Mod.Current.Settings.PopulationFactor.ToUnitString(true) + ")", "Population", Empire.Current.PrimaryRace.Icon);
				popNode.Expand();
			}
			if (CargoDelta.RacePopulation.Any() || CargoDelta.AnyPopulation != 0)
			{
				if (popNode == null)
					popNode = tree.AddItemWithImage("All Population (" + ((long)(Mod.Current.Settings.PopulationSize * Mod.Current.Settings.PopulationFactor)).Kilotons() + " per " + Mod.Current.Settings.PopulationFactor.ToUnitString(true) + ")", "Population", Empire.Current.PrimaryRace.Icon);
				foreach (var pop in CargoDelta.RacePopulation)
					popNode.AddItemWithImage(pop.Value.ToUnitString(true, 4, "All") + " " + pop.Key + " Population (" + ((long)(Mod.Current.Settings.PopulationSize * Mod.Current.Settings.PopulationFactor)).Kilotons() + " per " + Mod.Current.Settings.PopulationFactor.ToUnitString(true) + ")", pop.Key, pop.Key.Icon);
				if (CargoDelta.AnyPopulation != 0)
					popNode.AddItemWithImage(CargoDelta.AnyPopulation.ToUnitString(true, 4) + " Population of Any Race (" + ((long)(Mod.Current.Settings.PopulationSize * Mod.Current.Settings.PopulationFactor)).Kilotons() + " per " + Mod.Current.Settings.PopulationFactor.ToUnitString(true) + ")", "Total", Pictures.GetGenericImage(typeof(Race)));
				popNode.Expand();
			}

			// TODO - estimate tonnage based on available cargo, but we'd need the cargo transfer target for that...
			lblTonnage.Text = "Estimated Tonnage: " + CargoDelta.EstimatedTonnage.Kilotons("Unknown");
		}
	}

	private void tree_AfterSelect(object sender, TreeViewEventArgs e)
	{
		CurrentSelectionType = SelectionType.None;
		SelectedUnitType = VehicleTypes.None;
		SelectedUnitRole = null;
		SelectedUnitDesign = null;
		SelectedUnit = null;
		SelectedRace = null;

		if (e.Node.Parent != null)
		{
			if (e.Node.Parent.Tag is string)
			{
				if ((string)e.Node.Parent.Tag == "Types")
				{
					CurrentSelectionType = SelectionType.UnitType;
					SelectedUnitType = (VehicleTypes)e.Node.Tag;
				}
				else if ((string)e.Node.Parent.Tag == "Roles")
				{
					CurrentSelectionType = SelectionType.UnitRole;
					SelectedUnitRole = (string)e.Node.Tag;
				}
				else if ((string)e.Node.Parent.Tag == "Designs")
				{
					CurrentSelectionType = SelectionType.UnitDesign;
					SelectedUnitDesign = (IDesign<IUnit>)e.Node.Tag;
				}
				else if ((string)e.Node.Parent.Tag == "Units")
				{
					CurrentSelectionType = SelectionType.Unit;
					SelectedUnit = (IUnit)e.Node.Tag;
				}
				else if ((string)e.Node.Parent.Tag == "Population")
				{
					CurrentSelectionType = SelectionType.Population;
					SelectedRace = (Race)e.Node.Tag;
				}
			}
			else if (e.Node.Tag is VehicleTypes)
			{
				CurrentSelectionType = SelectionType.UnitType;
				SelectedUnitType = (VehicleTypes)e.Node.Tag;
			}
			else if (e.Node.Tag is IDesign<IUnit>)
			{
				CurrentSelectionType = SelectionType.UnitDesign;
				SelectedUnitDesign = (IDesign<IUnit>)e.Node.Tag;
			}
			else if (e.Node.Tag is IUnit)
			{
				CurrentSelectionType = SelectionType.Unit;
				SelectedUnit = (IUnit)e.Node.Tag;
			}
			else if (e.Node.Tag is Race)
			{
				CurrentSelectionType = SelectionType.Population;
				SelectedRace = (Race)e.Node.Tag;
			}
		}
		else if (e.Node.Tag is string && (string)e.Node.Tag == "Population")
			CurrentSelectionType = SelectionType.Population;
	}

	public enum SelectionType
	{
		None,
		UnitType,
		UnitRole,
		UnitDesign,
		Unit,
		Population
	}
}
