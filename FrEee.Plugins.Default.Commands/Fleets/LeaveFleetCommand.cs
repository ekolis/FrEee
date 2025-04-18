﻿using FrEee.Extensions;
using FrEee.Gameplay.Commands;
using FrEee.Gameplay.Commands.Fleets;
using FrEee.Objects.LogMessages;
using FrEee.Objects.Space;

namespace FrEee.Plugins.Default.Commands.Fleets;

/// <summary>
/// A command for a space vehicle to leave a fleet.
/// </summary>
public class LeaveFleetCommand : Command<IMobileSpaceObject>, ILeaveFleetCommand
{
	public LeaveFleetCommand(IMobileSpaceObject vehicle)
		: base(vehicle)
	{
	}

	public override void Execute()
	{
		// validation
		if (Executor.Container == null)
			Issuer.Log.Add(Executor.CreateLogMessage(Executor + " cannot leave its fleet because it is not currently in a fleet.", LogMessageType.Error));
		else
		{
			// remove from fleet
			var f = Executor.Container;
			f.Vehicles.Remove(Executor);
			Executor.Container = null;
			f.Sector.Place(Executor);
		}
	}
}