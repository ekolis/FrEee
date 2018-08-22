﻿using FrEee.Game.Interfaces;
using FrEee.Utility.Extensions;

namespace FrEee.Game.Objects.Commands
{
	/// <summary>
	/// A command for a space vehicle to leave a fleet.
	/// </summary>
	public class LeaveFleetCommand : Command<IMobileSpaceObject>
	{
		public LeaveFleetCommand(IMobileSpaceObject vehicle)
			: base(vehicle)
		{
		}

		public override void Execute()
		{
			// validation
			if (Executor.Container == null)
				Issuer.Log.Add(Executor.CreateLogMessage(Executor + " cannot leave its fleet because it is not currently in a fleet."));
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
}