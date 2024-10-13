using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.Space;

namespace FrEee.Gameplay.Commands.Fleets;

/// <summary>
/// Builds various types of commands used for managing fleets.
/// </summary>
public interface IFleetCommandFactory
{
    ICreateFleetCommand CreateFleet(Fleet fleet, Sector sector);

    ICommand DisbandFleet(Fleet fleet);

    IJoinFleetCommand JoinFleet(IMobileSpaceObject vehicle, Fleet fleet);

	IJoinFleetCommand JoinFleet(IMobileSpaceObject vehicle, ICreateFleetCommand command);

	ICommand LeaveFleet(IMobileSpaceObject vehicle);
}