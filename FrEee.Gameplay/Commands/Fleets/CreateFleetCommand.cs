using System.Collections.Generic;
using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Objects.Space;

namespace FrEee.Gameplay.Commands.Fleets;

/// <summary>
/// A command to create a new fleet.
/// </summary>
public class CreateFleetCommand : Command<Empire>, ICreateFleetCommand
{
	public CreateFleetCommand(Fleet fleet, Sector sector)
		: base(Empire.Current)
	{
		Fleet = fleet;
		Sector = sector;
	}

	public Fleet Fleet { get; set; }

	public override IEnumerable<IReferrable> NewReferrables
	{
		get
		{
			yield return Fleet;
		}
	}

	public Sector Sector { get; private set; }

	public override void Execute()
	{
		foreach (var v in Fleet.Vehicles)
			v.Container = null;
		Fleet.Vehicles.Clear(); // no cheating by spawning new vehicles!
		Fleet.Sector = Sector;

		// HACK - why is the fleet beign disposed?!
		Fleet.IsDisposed = false;
		Fleet.ID = 0;
		Game.Current.AssignID(Fleet);
	}

	public override void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
	{
		if (done == null)
			done = new HashSet<IPromotable>();
		if (!done.Contains(this))
		{
			done.Add(this);
			base.ReplaceClientIDs(idmap, done);
			Fleet.ReplaceClientIDs(idmap, done);
		}
	}
}