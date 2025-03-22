using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Gameplay.Commands.Designs;
using FrEee.Gameplay.Commands.Fleets;
using FrEee.Gameplay.Commands.Messages;
using FrEee.Gameplay.Commands.Ministers;
using FrEee.Gameplay.Commands.Notes;
using FrEee.Gameplay.Commands.Orders;
using FrEee.Gameplay.Commands.Projects;
using FrEee.Gameplay.Commands.Waypoints;
using FrEee.Persistence;
using FrEee.Plugins.Commands.Default.Designs;
using FrEee.Plugins.Commands.Default.Fleets;
using FrEee.Plugins.Commands.Default.Messages;
using FrEee.Plugins.Commands.Default.Ministers;
using FrEee.Plugins.Commands.Default.Notes;
using FrEee.Plugins.Commands.Default.Orders;
using FrEee.Plugins.Commands.Default.Projects;
using FrEee.Plugins.Commands.Default.Waypoints;
using FrEee.Plugins.Persistence.Default;
using FrEee.Plugins.Processes.Default;
using FrEee.Plugins.Processes.Default.Combat.Grid;
using FrEee.Plugins.Processes.Default.Construction;
using FrEee.Plugins.Vehicles.Default;
using FrEee.Processes;
using FrEee.Processes.Combat;
using FrEee.Processes.Combat.Grid;
using FrEee.Serialization.Stringifiers;
using FrEee.Utility;
using FrEee.Vehicles;
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

	public void Load(IPlugin plugin)
	{
		DI.RegisterSingleton(plugin.ExtensionPoint, plugin);
		loaded.Add(plugin);
	}

	/// <summary>
	/// Loads the default plugins into the game.
	/// </summary>
	public void LoadDefaultPlugins(Action? additionalConfig = null)
	{
		// TODO: load dependencies from configuration file in mod data so we can really modularize this thing!

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

		/*// processes
		DI.RegisterSingleton<ITurnProcessor, TurnProcessor>();
		DI.RegisterSingleton<IBattleService, BattleService>();
		DI.RegisterSingleton<IConstructionQueueService, ConstructionQueueService>();

		// gameplay
		DI.RegisterSingleton<IDesignCommandService, DesignCommandService>();
		DI.RegisterSingleton<IFleetCommandService, FleetCommandService>();
		DI.RegisterSingleton<IMessageCommandService, MessageCommandService>();
		DI.RegisterSingleton<IMinisterCommandService, MinisterCommandService>();
		DI.RegisterSingleton<INoteCommandService, NoteCommandService>();
		DI.RegisterSingleton<IOrderCommandService, OrderCommandService>();
		DI.RegisterSingleton<IProjectCommandService, ProjectCommandService>();
		DI.RegisterSingleton<IWaypointCommandService, WaypointCommandService>();

		// vehicles
		DI.RegisterSingleton<IHullService, HullService>();
		DI.RegisterSingleton<IDesignService, DesignService>();
		DI.RegisterSingleton<IVehicleService, VehicleService>();

		// persistence
		DI.RegisterSingleton<IGamePersister, GamePersister>();
		DI.RegisterSingleton<ICommandPersister, CommandPersister>();
		DI.RegisterSingleton<IGameSetupPersister, GameSetupPersister>();
		DI.RegisterSingleton<IEmpireTemplatePersister, EmpireTemplatePersister>();

		// libraries
		DI.RegisterSingleton<ILibrary<IDesign>, DesignLibrary>();*/

		// addtional configuration for the GUI or whatever
		additionalConfig?.Invoke();

		// run this in the background, without awaiting it
		DI.Run();
	}
}