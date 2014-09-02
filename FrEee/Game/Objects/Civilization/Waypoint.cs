using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Orders;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// A waypoint in space that can be used for navigation.
	/// </summary>
	public abstract class Waypoint : ILocated, IFoggable, IOwnable, INamed, IPromotable
	{
		protected Waypoint()
		{
			Owner = Empire.Current;
		}

		public abstract Sector Sector { get; protected set; }

		public abstract StarSystem StarSystem { get; }

		/// <summary>
		/// Only the waypoint's owner can see it.
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public Visibility CheckVisibility(Empire emp)
		{
			if (emp == Owner)
				return Visibility.Owned;
			return Visibility.Unknown;
		}

		public void Redact(Empire emp)
		{
			if (CheckVisibility(emp) == Visibility.Unknown)
				Dispose();
		}

		public bool IsMemory
		{
			get;
			set;
		}

		public double Timestamp
		{
			get;
			set;
		}

		public bool IsObsoleteMemory(Empire emp)
		{
			return false;
		}

		public long ID
		{
			get;
			set;
		}

		public bool IsDisposed
		{
			get;
			set;
		}

		public void Dispose()
		{
			if (IsDisposed)
				return;
			Owner.Waypoints.Remove(this);
			for (int i = 0; i < Owner.NumberedWaypoints.Length; i++)
			{
				if (Owner.NumberedWaypoints[i] == this)
					Owner.NumberedWaypoints[i] = null;
			}
			foreach (var sobj in Galaxy.Current.FindSpaceObjects<IMobileSpaceObject>())
			{
				// check if space object has orders to move to this waypoint
				// if so, delete that order and any future orders
				bool foundWaypoint = false;
				foreach (var order in sobj.Orders)
				{
					if (order is WaypointOrder<IMobileSpaceObject>)
					{
						var wo = order as WaypointOrder<IMobileSpaceObject>;
						if (wo.Target == this)
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

		public Empire Owner
		{
			get;
			private set;
		}

		public abstract string Name { get; }

		public override string ToString()
		{
			return Name;
		}

		/// <summary>
		/// Number of vehicles whose orders were altered when this waypoint was deleted.
		/// </summary>
		[DoNotSerialize]
		internal int AlteredQueuesOnDelete { get; private set; }

		public virtual void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			// doesn't use client objects, nothing to do here
		}
	}
}
