using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Plugins;

public interface IPlugin
{
	/// <summary>
	/// A package name to identify this and related plugins.
	/// </summary>
	/// <remarks>
	/// For instance, all default plugins have the default package name (<see cref="Plugin.DefaultPackage)"/>).
	/// </remarks>
	public string Package { get; }

	/// <summary>
	/// The extension point (interface) to which this plugin should attach.
	/// </summary>
	public Type ExtensionPoint { get; }

	/// <summary>
	/// The name of this plugin. Must be unique within its package, apart from different versions.
	/// </summary>
	public string Name { get; }

	/// <summary>
	/// The version of this plugin. Must be unique for a particular combination of package and name.
	/// </summary>
	public Version Version { get; }

	/// <summary>
	/// A unique ID to identify this plugin.
	/// </summary>
	public string ID => $"{Package}:{Name}:{Version}";
}


/// <summary>
/// A plugin that can be dynamically loaded into the game.
/// </summary>
public interface IPlugin<out T>
	: IPlugin
{
	/// <summary>
	/// The plugin's implementation of the extension point interface.
	/// </summary>
	public T Implementation { get; }
}