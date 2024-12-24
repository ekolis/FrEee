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
		catalog.Catalogs.Add(new AssemblyCatalog(typeof(IDataTranslator).Assembly));
		container = new CompositionContainer(catalog);
		container.ComposeParts(this);
	}

	[ImportMany(typeof(IDataTranslator))]
	private IEnumerable<IDataTranslator> all;

	public static IEnumerable<IDataTranslator> All => instance.all;
}
