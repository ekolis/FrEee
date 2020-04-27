using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Technology;
using FrEee.Modding;
using System;
using System.Reflection;

namespace FrEee.Tests
{
	public static class TestUtilities
	{
		public static IHull<T> CreateHull<T>(string name)
			where T : IVehicle
		{
			IHull<T> hull = new Hull<T>();
			hull.Name = name;
			hull.ModID = name;
			Mod.Current.Hulls.Add(hull);
			Mod.Current.Register(hull);
			return hull;
		}

		public static IHull<T> CreateHull<T>(this IDesign<T> design, string name = null)
			where T : IVehicle
		{
			var hull = CreateHull<T>(name ?? design.BaseName);
			design.Hull = hull;
			return hull;
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
}