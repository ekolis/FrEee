using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Processes;
using FrEee.Utility;

namespace FrEee.Root;

/// <summary>
/// Configures FrEee.
/// </summary>
public static class Configuration
{
	/// <summary>
	/// Sets up any dependencies which need to be injected.
	/// </summary>
	public static void ConfigureDI()
	{
		DI.RegisterSingleton<ITurnProcessor, TurnProcessor>();

		// run this in the background, without awaiting it
		DI.Run();
	}
}
