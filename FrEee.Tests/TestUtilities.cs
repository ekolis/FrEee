using FrEee.Objects.Civilization;
using FrEee.Modding;
using FrEee.Objects.GameState;
using FrEee.Modding.Loaders;
using FrEee.Vehicles;

namespace FrEee;

public static class TestUtilities
{
	public static Game CreateGalaxyWithMod(string? modPath = null)
	{
		Game gal = new();
		Mod.Current = new ModLoader().Load(modPath);
		return gal;
	}

	public static Empire CreateEmpire(string name = "Galactic Empire")
	 => new() { Name = name };

	public static IHull<T> CreateHull<T>(string name)
		where T : IVehicle
	{
		Hull<T> hull = new()
		{
			Name = name,
			ModID = name,
			ThrustPerMove = 1
		};
		Mod.Current.Hulls.Add(hull);
		return hull;
	}

	public static IHull<T> CreateHull<T>(this IDesign<T> design, string? name = null)
		where T : IVehicle
	{
		var hull = CreateHull<T>(name ?? design.BaseName);
		design.Hull = hull;
		return hull;
	}

	public static IDesign<T> CreateDesign<T>(Empire owner, string name = "Generic Design")
		where T : IVehicle
		=> new Design<T>()
		{
			BaseName = name,
			Owner = owner,
		};

	public static T CreateVehicle<T>(IDesign<T> design, Empire owner)
		where T : IVehicle
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
