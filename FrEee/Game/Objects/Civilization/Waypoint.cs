using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Orders;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// A waypoint in space that can be used for navigation.
	/// </summary>
	public abstract class Waypoint : ILocated, IFoggable, IOwnable, INamed, IPromotable, IReferrable
	{
		protected Waypoint()
		{
			Owner = Empire.Current;
		}

		public long ID { get; set; }

		public bool IsDisposed { get; set; }

		public bool IsMemory { get; set; }

		public abstract string Name { get; }

		[DoNotSerialize]
		public Empire? Owner { get { return owner; } set { owner = value; } }

		public abstract Sector? Sector { get; set; }

		public abstract StarSystem? StarSystem { get; }

		public double Timestamp { get; set; }

		/// <summary>
		/// Number of vehicles whose orders were altered when this waypoint was deleted.
		/// </summary>
		[DoNotSerialize]
		internal int AlteredQueuesOnDelete { get; private set; }

		private GalaxyReference<Empire>? owner { get; set; }

		/// <summary>
		/// Only the waypoint's owner can see it.
		/// If the sector is null, it's invisible too.
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public Visibility CheckVisibility(Empire emp)
		{
			if (emp == Owner && Sector != null)
				return Visibility.Owned;
			return Visibility.Unknown;
		}

		public void Dispose()
		{
			if (IsDisposed)
				return;
			if (Owner != null)
			{
				Owner.Waypoints.Remove(this);
				for (int i = 0; i < Owner.NumberedWaypoints.Length; i++)
				{
					if (Owner.NumberedWaypoints[i] == this)
						Owner.NumberedWaypoints[i] = null;
				}
			}
			foreach (var sobj in Galaxy.Current.FindSpaceObjects<IMobileSpaceObject>())
			{
				// check if space object has orders to move to this waypoint
				// if so, delete that order and any future orders
				bool foundWaypoint = false;
				foreach (var order in sobj.Orders.ToArray())
				{
					if (order is WaypointOrder)
					{
						var wo = order as WaypointOrder;
						if (wo?.Target == this)
							foundWaypoint = true;
					}
					if (foundWaypoint)
						sobj.RemoveOrder(order);
				}
				if (foundWaypoint)
					AlteredQueuesOnDelete++;
			}
			IsDisposed = true;
			Galaxy.Current.UnassignID(this);
		}

		public bool IsObsoleteMemory(Empire emp) => false;

		public void Redact(Empire emp)
		{
			if (CheckVisibility(emp) == Visibility.Unknown)
				Dispose();
		}

		public virtual void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable>? done = null)
		{
			// doesn't use client objects, nothing to do here
		}

		public override string ToString() => Name;
	}
}
