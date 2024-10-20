using FrEee.Objects.Civilization;
using FrEee.Objects.Vehicles;

namespace FrEee.Gameplay.Commands.Designs;

/// <summary>
/// A command to mark a design as obsolete or not obsolete.
/// </summary>
public interface ISetObsoleteFlagCommand : ICommand<IDesign>
{
	/// <summary>
	/// The design to set the flag on if it's already knwon by the server.
	/// </summary>
	IDesign Design { get; }

	/// <summary>
	/// The design to set the flag on if it's only in the library and not in the game or it's a brand new design.
	/// </summary>
	IDesign NewDesign { get; set; }

	/// <summary>
	/// The flag state to set.
	/// </summary>
	bool IsObsolete { get; set; }
}