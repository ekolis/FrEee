using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Commands
{
	/// <summary>
	/// A command to create a new fleet.
	/// </summary>
	public class CreateFleetCommand : Command<Empire>
	{
		public CreateFleetCommand(Empire issuer, Fleet fleet, Sector sector)
			: base(issuer, issuer)
		{
			Fleet = fleet;
			Sector = sector;
		}

		public override void Execute()
		{
			Fleet.Vehicles.Clear(); // no cheating by spawning new vehicles!
			Sector.Place(Fleet);
		}

		/// <summary>
		/// The fleet to create.
		/// </summary>
		public Fleet Fleet { get; set; }

		/// <summary>
		/// The sector to place the fleet in.
		/// </summary>
		public Sector Sector { get; private set; }

		public override IEnumerable<IReferrable> NewReferrables
		{
			get
			{
				yield return Fleet;
			}
		}

		public override void ReplaceClientIDs(IDictionary<long, long> idmap)
		{
			Fleet.ReplaceClientIDs(idmap);
		}
	}
}
