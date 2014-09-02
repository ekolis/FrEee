using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Orders;
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
	/// A command to create a new waypoint.
	/// </summary>
	public class CreateWaypointCommand : Command<Empire>
	{
		public CreateWaypointCommand(Waypoint waypoint)
			: base(Empire.Current)
		{
			Waypoint = waypoint;
		}

		/// <summary>
		/// The waypoint being created.
		/// </summary>
		public Waypoint Waypoint { get; set; }

		/// <summary>
		/// A hotkey from zero to nine used to access this waypoint, or null to create a waypoint with no hotkey.
		/// </summary>
		public int? Hotkey { get; private set; }

		public override void Execute()
		{
			if (!Executor.Waypoints.Contains(Waypoint))
				Executor.Waypoints.Add(Waypoint); // add new waypoint
		}

		public override IEnumerable<IReferrable> NewReferrables
		{
			get
			{
				foreach (var r in base.NewReferrables)
					yield return r;
				yield return Waypoint;
			}
		}
	}
}
