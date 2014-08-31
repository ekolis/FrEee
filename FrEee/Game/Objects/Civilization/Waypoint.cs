using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// A waypoint in space that can be used for navigation.
	/// </summary>
	public abstract class Waypoint : ILocated, IFoggable, IOwnable
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
			IsDisposed = true;
			Galaxy.Current.UnassignID(this);
		}

		public Empire Owner
		{
			get;
			private set;
		}
	}
}
