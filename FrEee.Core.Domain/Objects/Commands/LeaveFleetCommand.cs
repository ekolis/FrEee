using FrEee.Extensions;
using FrEee.Objects.Space;

namespace FrEee.Objects.Commands;

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
		if (Executor.Entity == null)
			Issuer.Log.Add(Executor.CreateLogMessage(Executor + " cannot leave its fleet because it is not currently in a fleet.", LogMessages.LogMessageType.Error));
		else
		{
			// remove from fleet
			var f = Executor.Entity;
			f.Vehicles.Remove(Executor);
			Executor.Entity = null;
			f.Sector.Place(Executor);
		}
	}
}