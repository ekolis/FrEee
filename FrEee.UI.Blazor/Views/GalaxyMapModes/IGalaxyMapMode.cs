using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.Space;

namespace FrEee.UI.Blazor.Views.GalaxyMapModes;

/// <summary>
/// Rendering modes for the galaxy map.
/// </summary>
public interface IGalaxyMapMode
{
	/// <summary>
	/// The name of this mode.
	/// </summary>
	string Name { get; }

	/// <summary>
	/// Gets a <see cref="PieChartViewModel{T}"/> to represent a star system.
	/// </summary>
	/// <param name="starSystem">The star system to represent.</param>
	/// <returns>The view model.</returns>
	PieChartViewModel<int> GetStarSystemViewModel(StarSystem starSystem);
}
