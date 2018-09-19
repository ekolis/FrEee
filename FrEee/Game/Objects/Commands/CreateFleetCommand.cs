using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using System.Collections.Generic;

namespace FrEee.Game.Objects.Commands
{
	/// <summary>
	/// A command to create a new fleet.
	/// </summary>
	public class CreateFleetCommand : Command<Empire>
	{
		public CreateFleetCommand(Fleet fleet, Sector sector)
			: base(Empire.Current)
		{
			Fleet = fleet;
			Sector = sector;
		}

		/// <summary>
		/// The fleet to create.
		/// </summary>
		public Fleet Fleet { get; set; }

		public override IEnumerable<IReferrable> NewReferrables
		{
			get
			{
				yield return Fleet;
			}
		}

		/// <summary>
		/// The sector to place the fleet in.
		/// </summary>
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
			Galaxy.Current.AssignID(Fleet);
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
}