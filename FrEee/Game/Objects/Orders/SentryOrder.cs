using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility.Extensions;
using System.Drawing;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Enumerations;

namespace FrEee.Game.Objects.Orders
{
	/// <summary>
	/// An order for a mobile space object to hold position until enemies are sighted in the system.
	/// </summary>
	[Serializable]
	public class SentryOrder : IMobileSpaceObjectOrder<IMobileSpaceObject>
	{
		public SentryOrder()
		{
			Owner = Empire.Current;
		}

		public void Execute(IMobileSpaceObject sobj)
		{
			// if hostiles in system, we are done sentrying
			if (sobj.FindStarSystem().FindSpaceObjects<ISpaceObject>(s => s.IsHostileTo(sobj.Owner)).Any())
				IsComplete = true;
			
			// spend time
			sobj.TimeToNextMove += sobj.TimePerMove;
		}

		public bool IsComplete
		{
			get;
			private set;
		}

		public override string ToString()
		{
			return "Sentry";
		}

		public void Dispose()
		{
			// TODO - remove from queue, but we don't know which object we're on...
			Galaxy.Current.UnassignID(this);
		}

		private Reference<Empire> owner { get; set; }

		/// <summary>
		/// The empire which issued the order.
		/// </summary>
		[DoNotSerialize]
		public Empire Owner { get { return owner; } set { owner = value; } }

		/// <summary>
		/// Orders are visible only to their owners.
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public Visibility CheckVisibility(Empire emp)
		{
			if (emp == Owner)
				return Visibility.Visible;
			return Visibility.Unknown;
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap)
		{
			// This type does not use client objects, so nothing to do here.
		}

		public long ID { get; set; }
	}
}
