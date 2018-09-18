using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System.Collections.Generic;

namespace FrEee.Game.Objects.Commands
{
	/// <summary>
	/// A command for a space vehicle to join a fleet.
	/// </summary>
	public class JoinFleetCommand : Command<IMobileSpaceObject>
	{
		public JoinFleetCommand(IMobileSpaceObject vehicle, Fleet fleet)
			: base(vehicle)
		{
			Fleet = fleet;
		}

		public JoinFleetCommand(IMobileSpaceObject vehicle, CreateFleetCommand cmd)
			: base(vehicle)
		{
			CreateFleetCommand = cmd;
		}

		public CreateFleetCommand CreateFleetCommand { get; set; }

		[DoNotSerialize]
		public Fleet Fleet
		{
			get
			{
				return fleet == null || fleet.ID <= 0 ? (Fleet)CreateFleetCommand.Fleet : (Fleet)fleet;
			}
			set
			{
				Galaxy.Current.AssignID(value);
				fleet = value;
			}
		}

		private GalaxyReference<Fleet> fleet { get; set; }

		public override void Execute()
		{
			// if it's a new fleet, find it
			if (CreateFleetCommand != null)
				Fleet = CreateFleetCommand.Fleet;

			// validation
			if (Fleet.Sector != null && Executor.Sector != Fleet.Sector)
				Issuer.Log.Add(Executor.CreateLogMessage(Executor + " cannot join " + Fleet + " because they are not in the same sector."));
			else if (Fleet.Owner != null && Fleet.Owner != Issuer && CreateFleetCommand == null)
				Issuer.Log.Add(Executor.CreateLogMessage(Executor + " cannot join " + Fleet + " because this fleet does not belong to us."));
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
}