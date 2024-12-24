using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Data;
public class DataTranslators
{
	private CompositionContainer container;

	private static DataTranslators instance = new DataTranslators();

	private DataTranslators()
	{
		var catalog = new AggregateCatalog();
		catalog.Catalogs.Add(new DirectoryCatalog("."));
		container = new CompositionContainer(catalog);
		container.ComposeParts(this);
	}

	[ImportMany(typeof(IDataTranslator))]
	private IEnumerable<IDataTranslator> all;

	public static IEnumerable<IDataTranslator> All => instance.all;

	/// <summary>
	/// Attempts to translate an object to a data representation.
	/// </summary>
	/// <param name="obj"></param>
	/// <returns>The data representation, or the original object if there is no available translator for the object type.</returns>
	public static object ToData(object obj)
	{
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
	/// <param name="obj"></param>
	/// <returns>The object, or the original data if there is no available translator for the data type.</returns>
	public static object FromData(object obj)
	{
		var translator = All.SingleOrDefault(q => q.DataType == obj.GetType());
		if (translator is null)
		{
			// can't translate, no translator found
			return obj;
		}
		else
		{
			return translator.FromData(obj);
		}
	}
}
