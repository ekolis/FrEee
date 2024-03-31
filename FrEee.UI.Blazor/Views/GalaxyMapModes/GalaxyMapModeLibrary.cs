using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace FrEee.UI.Blazor.Views.GalaxyMapModes;

public class GalaxyMapModeLibrary
{
	private CompositionContainer container;

	public static GalaxyMapModeLibrary Instance { get; private set; } = new GalaxyMapModeLibrary();

	private GalaxyMapModeLibrary()
	{
		var catalog = new AggregateCatalog();
		catalog.Catalogs.Add(new AssemblyCatalog(typeof(IGalaxyMapMode).Assembly));
		container = new CompositionContainer(catalog);
		container.ComposeParts(this);
	}

	[ImportMany(typeof(IGalaxyMapMode))]
	public IEnumerable<IGalaxyMapMode> All { get; private set; }
}
