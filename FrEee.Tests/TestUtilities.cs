using FrEee.Objects.Civilization;
using FrEee.Modding;
using FrEee.Objects.GameState;
using FrEee.Modding.Loaders;
using FrEee.Vehicles;
using FrEee.Utility;
using FrEee.Vehicles.Types;
using FrEee.Objects.Space;
using System.Linq;
using FrEee.Plugins;

namespace FrEee;

public static class TestUtilities
{
	public static Game Initialize(string? modPath = null)
	{
		PluginLibrary.Instance.LoadDefaultPlugins();
		Game game = new();
		Mod.Current = new ModLoader().Load(modPath, includeGuiPlugins: false);
		game.Galaxy = new();
		game.Setup = new();
		return game;
	}

	public static Game Initialize(Mod mod)
	{
		PluginLibrary.Instance.LoadDefaultPlugins();
		Game game = new();
		Mod.Current = mod;
		game.Galaxy = new();
		game.Setup = new();
		return game;
	}

	public static Empire CreateEmpire(string name = "Galactic Empire") =>
		new() { Name = name };

	public static IHull CreateHull(VehicleTypes vehicleType, string name = "Generic Hull")
	{
		var hull = Services.Hulls.CreateHull(vehicleType);
		hull.Name = name;
		hull.ModID = name;
		hull.ThrustPerMove = 1;
		Mod.Current.Hulls.Add(hull);
		Mod.Current.AssignID(hull, Mod.Current.Objects.Select(q => q.ModID).ToList());
		return hull;
	}

	public static IDesign CreateDesign(Empire owner, IHull hull, string name = "Generic Design")
	{
		var design = Services.Designs.CreateDesign(hull);
		design.BaseName = name;
		design.Owner = owner;
		return design;
	}

	public static IVehicle CreateVehicle(IDesign design, Empire owner)
	{
		var vehicle = design.Instantiate();
		vehicle.Owner = owner;
		return vehicle;
	}
}
