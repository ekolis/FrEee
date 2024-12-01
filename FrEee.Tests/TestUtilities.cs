using FrEee.Objects.Civilization;
using FrEee.Modding;
using FrEee.Objects.GameState;
using FrEee.Modding.Loaders;
using FrEee.Vehicles;
using FrEee.Utility;
using FrEee.Vehicles.Types;
using FrEee.Objects.Space;
using System.Linq;

namespace FrEee;

public static class TestUtilities
{
	public static Game CreateGame(string? modPath = null)
	{
		Game game = new();
		Mod.Current = new ModLoader().Load(modPath);
		game.Galaxy = new Galaxy();
		return game;
	}

	public static Empire CreateEmpire(string name = "Galactic Empire")
	 => new() { Name = name };

	public static IHull CreateHull(VehicleTypes vehicleType, string name = "Generic Hull")
	{
		var hull = DIRoot.Hulls.Build(vehicleType);
		hull.Name = name;
		hull.ModID = name;
		hull.ThrustPerMove = 1;
		Mod.Current.Hulls.Add(hull);
		Mod.Current.AssignID(hull, Mod.Current.Objects.Select(q => q.ModID).ToList());
		return hull;
	}

	public static IDesign CreateDesign(Empire owner, IHull hull, string name = "Generic Design")
	{
		var design = DIRoot.Designs.Build(hull);
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

	///// <summary>
	///// Use as first line in ad hoc tests (needed by XNA specifically)
	///// </summary>
	///// https://stackoverflow.com/questions/4337201/net-nunit-test-assembly-getentryassembly-is-null
	//public static void SetEntryAssembly()
	//{
	//	SetEntryAssembly(Assembly.GetCallingAssembly());
	//}

	///// <summary>
	///// Allows setting the Entry Assembly when needed.
	///// Use AssemblyUtilities.SetEntryAssembly() as first line in XNA ad hoc tests
	///// </summary>
	///// <param name="assembly">Assembly to set as entry assembly</param>
	///// https://stackoverflow.com/questions/4337201/net-nunit-test-assembly-getentryassembly-is-null
	//public static void SetEntryAssembly(Assembly assembly)
	//{
	//	AppDomainManager manager = new AppDomainManager();
	//	FieldInfo entryAssemblyfield = manager.GetType().GetField("m_entryAssembly", BindingFlags.Instance | BindingFlags.NonPublic);
	//	entryAssemblyfield.SetValue(manager, assembly);

	//	AppDomain domain = AppDomain.CurrentDomain;
	//	FieldInfo domainManagerField = domain.GetType().GetField("_domainManager", BindingFlags.Instance | BindingFlags.NonPublic);
	//	domainManagerField.SetValue(domain, manager);
	//}
}
