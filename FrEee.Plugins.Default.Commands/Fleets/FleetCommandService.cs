using System;
using System.ComponentModel.Composition;
using FrEee.Gameplay.Commands.Designs;
using FrEee.Gameplay.Commands.Fleets;
using FrEee.Objects.Space;

namespace FrEee.Plugins.Default.Commands.Fleets;

[Export(typeof(IPlugin))]
public class FleetCommandService
	: Plugin<IFleetCommandService>, IFleetCommandService
{
	public override string Name { get; } = "FleetCommandService";

	public override IFleetCommandService Implementation => this;

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
