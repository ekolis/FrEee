﻿using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace FrEee.Serialization.Stringifiers;

public class StringifierLibrary
{
	private CompositionContainer container;

	public static StringifierLibrary Instance { get; private set; } = new StringifierLibrary();

	private StringifierLibrary()
	{
		var catalog = new AggregateCatalog();
		catalog.Catalogs.Add(new AssemblyCatalog(typeof(IStringifier).Assembly));
		container = new CompositionContainer(catalog);
		container.ComposeParts(this);
	}

	[ImportMany(typeof(IStringifier))]
	public IEnumerable<IStringifier> All { get; private set; }
}
