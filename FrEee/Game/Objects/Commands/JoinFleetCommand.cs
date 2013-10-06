using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Commands
{
	/// <summary>
	/// A command for a space vehicle to join a fleet.
	/// </summary>
	public class JoinFleetCommand : Command<IMobileSpaceObject>
	{
		public JoinFleetCommand(Empire issuer, IMobileSpaceObject vehicle, Fleet fleet)
			: base(issuer, vehicle)
		{
			Fleet = fleet;
		}

		public JoinFleetCommand(Empire issuer, IMobileSpaceObject vehicle, CreateFleetCommand cmd)
			: base(issuer, vehicle)
		{
			CreateFleetCommand = cmd;
		}

		private Reference<Fleet> fleet { get; set; }

		[DoNotSerialize]
		public Fleet Fleet { get { return fleet; } set { fleet = value; } }

		public CreateFleetCommand CreateFleetCommand { get; set; }

		public override void Execute()
		{
			// if it's a new fleet, find it
			if (CreateFleetCommand != null)
				Fleet = CreateFleetCommand.Fleet;

			// validation
			if (Target.Sector != Fleet.Sector)
				Issuer.Log.Add(Target.CreateLogMessage(Target + " cannot join " + Fleet + " because they are not in the same sector."));
			else
			{
				// remove from old fleet
				if (Target.Container != null)
					Target.Container.Vehicles.Remove(Target);

				// add to new fleet
				Fleet.Vehicles.Add(Target);
			}
		}
	}
}
