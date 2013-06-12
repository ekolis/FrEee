using System;
using System.Collections.Generic;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using System.Drawing;

namespace FrEee.Game.Objects.Space
{
	/// <summary>
	/// A sector in a star system. Can contain space objects.
	/// </summary>
	[Serializable]
	public class Sector : IReferrable
	{
		public Sector()
		{
			SpaceObjects = new HashSet<ISpaceObject>();
			if (Galaxy.Current != null) 
				Galaxy.Current.Register(this);
		}

		/// <summary>
		/// The space objects contained in this sector.
		/// </summary>
		public ISet<ISpaceObject> SpaceObjects { get; private set; }

		public int ID
		{
			get;
			set;
		}

		public Empire Owner
		{
			get { return null; }
		}

		public StarSystem FindStarSystem()
		{
			foreach (var ssl in Galaxy.Current.StarSystemLocations)
			{
				if (ssl.Item.Contains(this))
					return ssl.Item;
			}
			return null;
		}

		public Point Coordinates
		{
			get
			{
				var sys = FindStarSystem();
				if (sys == null)
					throw new Exception("Can't find sector coordinates because it does not belong to a known star system.");
				return sys.FindSector(this);
			}
		}

		public void Dispose()
		{
			Galaxy.Current.Unregister(this);
			foreach (var emp in Galaxy.Current.Empires)
				Galaxy.Current.Unregister(this, emp);
		}
	}
}
