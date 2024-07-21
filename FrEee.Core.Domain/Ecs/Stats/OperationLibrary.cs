using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Serialization.Stringifiers;

namespace FrEee.Ecs.Stats
{
	public class OperationLibrary
	{
		private static CompositionContainer container;

		public static OperationLibrary Instance { get; private set; } = new OperationLibrary();

		private OperationLibrary()
		{
			var catalog = new AggregateCatalog();
			catalog.Catalogs.Add(new AssemblyCatalog(typeof(IOperation).Assembly));
			container = new CompositionContainer(catalog);
			container.ComposeParts(this);
		}

		[ImportMany(typeof(IOperation))]
		public IEnumerable<Operation> All { get; private set; }

		public Operation Find(string name) => All.SingleOrDefault(q => q.Name == name);
	}
}
