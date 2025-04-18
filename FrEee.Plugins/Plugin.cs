using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Plugins;

public abstract class Plugin
	: IPlugin
{
	/// <summary>
	/// The default package name.
	/// </summary>
	public static string DefaultPackage { get; } = "Default";

	/// <summary>
	/// The default version number.
	/// </summary>
	public static Version DefaultVersion { get; } = new Version(0, 0, 10);

	public virtual string Package { get; } = DefaultPackage;
	public abstract string Name { get; }
	public virtual Version Version { get; } = DefaultVersion;
	public abstract Type ExtensionPoint { get; }
}

public abstract class Plugin<T>
	: Plugin, IPlugin<T>
{
	public override Type ExtensionPoint { get; } = typeof(T);

	public abstract T Implementation { get; }
}