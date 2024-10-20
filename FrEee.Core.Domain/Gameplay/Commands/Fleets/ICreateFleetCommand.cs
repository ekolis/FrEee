using FrEee.Objects.Civilization;
using FrEee.Objects.Space;

namespace FrEee.Gameplay.Commands.Fleets;
public interface ICreateFleetCommand
	: ICommand<Empire>
{
	/// <summary>
	/// The fleet to create.
	/// </summary>
	Fleet Fleet { get; set; }

	/// <summary>
	/// The sector to place the fleet in.
	/// </summary>
	Sector Sector { get; }
}