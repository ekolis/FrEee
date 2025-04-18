using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace FrEee.Plugins;

/// <summary>
/// Manages FrEee's plugins.
/// </summary>
public class PluginLibrary
{
	private CompositionContainer container;

	private PluginLibrary()
	{
		// load plugins from DLLs in Plugins folder
		var catalog = new AggregateCatalog();
		var pluginsDir = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Plugins");
		catalog.Catalogs.Add(new DirectoryCatalog(pluginsDir));
		container = new CompositionContainer(catalog);
		container.ComposeParts(this);
	}

	public static PluginLibrary Instance { get; } = new PluginLibrary();

	/// <summary>
	/// All available plugins.
	/// </summary>
	[ImportMany(typeof(IPlugin))]
	public IEnumerable<IPlugin> All { get; private set; }

	private IList<IPlugin> loaded = [];

	/// <summary>
	/// Any plugins that are loaded.
	/// </summary>
	public IEnumerable<IPlugin> Loaded => loaded;

	/// <summary>
	/// Configuration for the GUI.
	/// </summary>
	private Action? GuiConfig;

	public void Load(IPlugin plugin)
	{
		DI.RegisterSingleton(plugin.ExtensionPoint, plugin);
		loaded.Add(plugin);
	}

	/// <summary>
	/// Loads the default plugins into the game.
	/// </summary>
	public void LoadDefaultPlugins(Action? guiConfig = null)
	{
		// reset in case DI was already running (e.g. unit test suite)
		DI.Reset();

		// load the latest verion of all default plugins
		foreach (var group in All
			.Where(q => q.Package == Plugin.DefaultPackage)
			.GroupBy(q => q.Name))
		{
			var plugin = group.MaxBy(q => q.Version);
			if (plugin is not null)
			{
				Load(plugin);
			}
		}

		// addtional configuration for the GUI or whatever
		GuiConfig = guiConfig;
		GuiConfig?.Invoke();

		// export the config
		//var configs = ExportConfiguration();
		//var json = JsonConvert.SerializeObject(configs);
		//File.WriteAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Plugins.json"), json);

		// run this in the background, without awaiting it
		DI.Run();

		// TODO: verify that all plugins are loaded
		//VerifyAll();
	}

	/// <summary>
	/// Loads plugins based on configuration.
	/// </summary>
	/// <remarks>
	/// Call <see cref="LoadDefaultPlugins(Action?)"/> from the GUI first to load the GUI plugins.
	/// </remarks>
	public void LoadConfiguredPlugins(IEnumerable<PluginConfig> configs)
	{
		// reset in case DI was already running (e.g. unit test suite)
		DI.Reset();

		// load the plugins
		foreach (var config in configs)
		{
			var plugin = All.Where(q =>
				q.Package == config.Package && q.Name == config.Name
				&& (config.MinVersion is null || q.Version >= config.MinVersion)
				&& (config.MaxVersion is null || q.Version <= config.MaxVersion))
				.MaxBy(q => q.Version);
			if (plugin is not null)
			{
				Load(plugin);
			}
			else
			{
				throw new InvalidOperationException($"Plugin {config.Package}:{config.Name} with version between {config.MinVersion} and {config.MaxVersion} was not found.");
			}
		}

		// addtional configuration for the GUI or whatever
		GuiConfig?.Invoke();

		// run this in the background, without awaiting it
		DI.Run();
	}

	/// <summary>
	/// Exports the currently loaded plugins' configuration as a list of <see cref="PluginConfig"/> records.
	/// </summary>
	/// <remarks>
	/// Does not specify version numbers.
	/// </remarks>
	/// <returns></returns>
	public IEnumerable<PluginConfig> ExportConfiguration()
	{
		return Loaded.Select(q => new PluginConfig(q.Package, q.Name));
	}
}