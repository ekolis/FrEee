using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.Space;

namespace FrEee.Gameplay.Commands.Fleets;

/// <summary>
/// Creates various types of commands used for managing fleets.
/// </summary>
public interface IFleetCommandService
{
    ICreateFleetCommand CreateFleet(Fleet fleet, Sector sector);

    IDisbandFleetCommand DisbandFleet(Fleet fleet);

    IJoinFleetCommand JoinFleet(IMobileSpaceObject vehicle, Fleet fleet);

	IJoinFleetCommand JoinFleet(IMobileSpaceObject vehicle, ICreateFleetCommand command);

	ILeaveFleetCommand LeaveFleet(IMobileSpaceObject vehicle);
}