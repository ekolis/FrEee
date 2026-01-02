using FrEee.Objects.Space;

namespace FrEee.UI.Blazor.Components.GalaxyMapModes;

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
	/// <param name="starSystemClicked">Action to take when the star system is clicked.</param>
	/// <returns>The view model.</returns>
	PieChartViewModel<int> GetStarSystemViewModel(StarSystem starSystem, Action<StarSystem> starSystemClicked);
}
