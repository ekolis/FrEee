using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Utility; using Newtonsoft.Json;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
				Executor.Container.Vehicles.Remove(Executor);
			}
		}
	}
}
