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
		public CreateFleetCommand(Empire issuer, string fleetName, Sector sector)
			: base(issuer, issuer)
		{
			Sector = sector;
		}

		/// <summary>
		/// The name to assign to the new fleet.
		/// </summary>
		public string FleetName { get; set; }

		/// <summary>
		/// The sector to create the fleet in.
		/// </summary>
		public Sector Sector { get; set; }
		
		public override void Execute()
		{
			// create fleet
			Fleet = new Fleet();
			Fleet.Name = FleetName;
			Sector.Place(Fleet);
		}

		/// <summary>
		/// The newly created fleet, or null if it's not yet been created.
		/// </summary>
		[DoNotSerialize]
		public Fleet Fleet { get; private set; }
	}
}
