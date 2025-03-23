using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Plugins;

/// <summary>
/// Configuration to load a plugin.
/// </summary>
/// <param name="Package"></param>
/// <param name="Name"></param>
/// <param name="MinVersion"></param>
/// <param name="MaxVersion"></param>
public record PluginConfig(
	string Package,
	string Name,
	Version? MinVersion = null,
	Version? MaxVersion = null);
