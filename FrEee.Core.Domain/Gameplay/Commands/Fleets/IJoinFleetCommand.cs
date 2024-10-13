using FrEee.Objects.Space;

namespace FrEee.Gameplay.Commands.Fleets;
public interface IJoinFleetCommand
	: ICommand<IMobileSpaceObject>
{
	/// <summary>
	/// The command that creates the fleet to join (if the fleet is newly created on the client side).
	/// </summary>
	ICreateFleetCommand CreateFleetCommand { get; set; }

	/// <summary>
	/// The fleet to join.
	/// </summary>
	Fleet Fleet { get; set; }
}