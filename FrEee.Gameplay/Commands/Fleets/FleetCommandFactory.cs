using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Gameplay.Commands.Designs;
using FrEee.Gameplay.Commands.Notes;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.GameState;
using FrEee.Objects.Space;
using FrEee.Objects.Vehicles;

namespace FrEee.Gameplay.Commands.Fleets;
public class FleetCommandFactory
	: IFleetCommandFactory
{
	public ICreateFleetCommand CreateFleet(Fleet fleet, Sector sector)
	{
		return new CreateFleetCommand(fleet, sector);
	}

	public ICommand DisbandFleet(Fleet fleet)
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

	public ICommand LeaveFleet(IMobileSpaceObject vehicle)
	{
		return new LeaveFleetCommand(vehicle);
	}
}
