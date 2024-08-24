using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace FrEee.UI.Blazor.Views.GalaxyMapModes;

public class GalaxyMapModeLibrary
{
	private CompositionContainer container;

	private static GalaxyMapModeLibrary Instance { get; set; } = new GalaxyMapModeLibrary();

	private GalaxyMapModeLibrary()
	{
		var catalog = new AggregateCatalog();
		catalog.Catalogs.Add(new AssemblyCatalog(typeof(IGalaxyMapMode).Assembly));
		container = new CompositionContainer(catalog);
		container.ComposeParts(this);
	}

	[ImportMany(typeof(IGalaxyMapMode))]
	public IEnumerable<IGalaxyMapMode> AllOfThem { get; private set; }

	public static IEnumerable<IGalaxyMapMode> All => Instance.AllOfThem;

	public static IGalaxyMapMode? Find<T>(string name = "")
		where T : IGalaxyMapMode
	{
		var results = All.OfType<T>();
		if (!string.IsNullOrWhiteSpace(name))
		{
			results = results.Where(q => q.Name.Contains(name));
		}
		return results.FirstOrDefault();
	}
}
