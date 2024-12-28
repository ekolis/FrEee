using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FrEee.Utility;

namespace FrEee.Data;
public class DataTranslators
{
	private static DataTranslators? instance;

	public static void Load()
	{
		instance = new DataTranslators();

		// https://stackoverflow.com/questions/4020532/mef-unable-to-load-one-or-more-of-the-requested-types-retrieve-the-loaderexce
		var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		var di = new DirectoryInfo(path);

		var dlls = di.GetFileSystemInfos("FrEee.*.dll");
		AggregateCatalog agc = new();

		foreach (var fi in dlls)
		{
			try
			{
				// find the assembly in the file and create a catalog for it
				var assembly = Assembly.LoadFile(fi.FullName);
				var ac = new AssemblyCatalog(assembly);
				var parts = ac.Parts.ToArray(); // throws ReflectionTypeLoadException 
				agc.Catalogs.Add(ac);

				// formally load the assembly so it can be used later
				SafeType.RegisterAssembly(assembly);
			}
			catch (ReflectionTypeLoadException ex)
			{
				// do nothing, it was a bad DLL
			}
		}

		CompositionContainer cc = new CompositionContainer(agc);
		cc.ComposeParts(instance);
	}

	[ImportMany(typeof(IDataTranslator))]
	private IEnumerable<IDataTranslator> all;

	public static IEnumerable<IDataTranslator> All
	{
		get
		{
			if (instance is null)
			{
				Load();
			}
			return instance.all;
		}
	}

	/// <summary>
	/// Attempts to translate an object to a data representation.
	/// </summary>
	/// <param name="obj"></param>
	/// <returns>The data representation, or the original object if there is no available translator for the object type.</returns>
	public static object ToData(object obj)
	{
		if (obj is null)
		{
			return null;
		}

		var translator = All.SingleOrDefault(q => q.ObjectType == obj.GetType());
		if (translator is null)
		{
			// can't translate, no translator found
			return obj;
		}
		else
		{
			return translator.ToData(obj);
		}
	}

	/// <summary>
	/// Attempts to translate data to an object.
	/// </summary>
	/// <param name="data"></param>
	/// <returns>The object, or the original data if there is no available translator for the data type.</returns>
	public static object FromData(object data)
	{
		if (data is null)
		{
			return null;
		}

		var translator = All.SingleOrDefault(q => q.DataType == data.GetType());
		if (translator is null)
		{
			// can't translate, no translator found
			return data;
		}
		else
		{
			return translator.FromData(data);
		}
	}

	/// <summary>
	/// Gets the type used to represent a type as data.
	/// </summary>
	/// <param name="objType"></param>
	/// <returns></returns>
	public static Type GetDataType(Type objType)
	{
		var translator = All.SingleOrDefault(q => q.ObjectType == objType);
		return translator?.DataType ?? objType;
	}

	public static bool CanTranslateToData(Type objType)
	{
		var translator = All.SingleOrDefault(q => q.ObjectType == objType);
		return translator is not null;
	}
}
