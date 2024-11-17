using System.Linq;
using FrEee.Objects.Space;

namespace FrEee.Gameplay.Commands.Fleets;

/// <summary>
/// A command to disband a fleet.
/// </summary>
public class DisbandFleetCommand : Command<Fleet>, IDisbandFleetCommand
{
	public DisbandFleetCommand(Fleet fleet)
		: base(fleet)
	{
	}

	public override void Execute()
	{
		foreach (var v in Executor.Vehicles.ToArray())
		{
			Executor.Vehicles.Remove(v);
			v.Container = null;
			Executor.Sector.Place(v);
		}
		Executor.Dispose();
	}
}