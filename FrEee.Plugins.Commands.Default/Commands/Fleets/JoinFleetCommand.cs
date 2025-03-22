using System.Collections.Generic;
using FrEee.Extensions;
using FrEee.Objects.GameState;
using FrEee.Objects.LogMessages;
using FrEee.Objects.Space;
using FrEee.Serialization;

namespace FrEee.Gameplay.Commands.Fleets;

/// <summary>
/// A command for a space vehicle to join a fleet.
/// </summary>
public class JoinFleetCommand : Command<IMobileSpaceObject>, IJoinFleetCommand
{
	public JoinFleetCommand(IMobileSpaceObject vehicle, Fleet fleet)
		: base(vehicle)
	{
		Fleet = fleet;
	}

	public JoinFleetCommand(IMobileSpaceObject vehicle, ICreateFleetCommand cmd)
		: base(vehicle)
	{
		CreateFleetCommand = cmd;
	}

	public ICreateFleetCommand CreateFleetCommand { get; set; }

	[DoNotSerialize]
	public Fleet Fleet
	{
		get
		{
			return fleet == null || fleet.ID <= 0 ? CreateFleetCommand.Fleet : (Fleet)fleet;
		}
		set
		{
			if (!value.HasValidID())
			{
				// HACK - why is the fleet beign disposed?!
				value.IsDisposed = false;
				value.ID = 0;
				Game.Current.AssignID(value);
			}
			fleet = value;
		}
	}

	private GameReference<Fleet> fleet { get; set; }

	public override void Execute()
	{
		// if it's a new fleet, find it
		if (CreateFleetCommand != null)
			Fleet = CreateFleetCommand.Fleet;

		// validation
		if (Fleet.Sector != null && Executor.Sector != Fleet.Sector)
			Issuer.Log.Add(Executor.CreateLogMessage(Executor + " cannot join " + Fleet + " because they are not in the same sector.", LogMessageType.Warning));
		else if (Fleet.Owner != null && Fleet.Owner != Issuer && CreateFleetCommand == null)
			Issuer.Log.Add(Executor.CreateLogMessage(Executor + " cannot join " + Fleet + " because this fleet does not belong to us.", LogMessageType.Warning));
		else
		{
			// remove from old fleet
			if (Executor.Container != null)
				Executor.Container.Vehicles.Remove(Executor);

			if (Fleet.Sector == null)
				Fleet.Sector = Executor.Sector;

			// add to new fleet
			Fleet.Vehicles.Add(Executor);
			Executor.Container = Fleet;
		}
	}

	public override void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
	{
		if (done == null)
			done = new HashSet<IPromotable>();
		if (!done.Contains(this))
		{
			done.Add(this);
			base.ReplaceClientIDs(idmap, done);
			fleet.ReplaceClientIDs(idmap, done);
		}
	}
}