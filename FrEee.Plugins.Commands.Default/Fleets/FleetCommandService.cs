using System;
using FrEee.Gameplay.Commands.Fleets;
using FrEee.Objects.Space;

namespace FrEee.Plugins.Commands.Default.Fleets;
public class FleetCommandService
	: IFleetCommandService
{
	public string Package { get; } = IPlugin.DefaultPackage;
	public string Name { get; } = "FleetCommandService";
	public Version Version { get; } = IPlugin.DefaultVersion;

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
