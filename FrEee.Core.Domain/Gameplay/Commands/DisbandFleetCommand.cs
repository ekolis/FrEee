using FrEee.Objects.Space;
using System.Linq;

namespace FrEee.Gameplay.Commands;

/// <summary>
/// A command to disband a fleet.
/// </summary>
public class DisbandFleetCommand : Command<Fleet>
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