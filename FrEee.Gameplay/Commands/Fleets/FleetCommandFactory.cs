using FrEee.Objects.Space;

namespace FrEee.Gameplay.Commands.Fleets;
public class FleetCommandFactory
	: IFleetCommandFactory
{
	public ICreateFleetCommand CreateFleet(Fleet fleet, Sector sector)
	{
		return new CreateFleetCommand(fleet, sector);
	}

	public IDisbandFleetCommand DisbandFleet(Fleet fleet)
	{
		return new DisbandFleetCommand(fleet);
	}

	public IJoinFleetCommand JoinFleet(IMobileSpaceObject vehicle, Fleet fleet)
	{
		return new JoinFleetCommand(vehicle, fleet);
	}

	public IJoinFleetCommand JoinFleet(IMobileSpaceObject vehicle, ICreateFleetCommand command)
	{
		return new JoinFleetCommand(vehicle, command);
	}

	public ILeaveFleetCommand LeaveFleet(IMobileSpaceObject vehicle)
	{
		return new LeaveFleetCommand(vehicle);
	}
}
