using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Plugins;

/// <summary>
/// A plugin that can be dynamically loaded into the game.
/// </summary>
public interface IPlugin
{
	/// <summary>
	/// A unique ID to identify this plugin.
	/// </summary>
	public string ID { get; }

	/// <summary>
	/// The version of this plugin.
	/// </summary>
	public Version Version { get; }
}
