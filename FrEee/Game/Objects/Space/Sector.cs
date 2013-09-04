using System;
using System.Collections.Generic;
using System.Linq;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using System.Drawing;
using FrEee.Game.Objects.Abilities;
using FrEee.Utility.Extensions;
using FrEee.Game.Enumerations;

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
		}

		public override string ToString()
		{
			var coords = Coordinates;
			return FindStarSystem() + " (" + coords.X + ", " + coords.Y + ")";
		}
		/// <summary>
		/// Aggregates abilities across a sector for an empire's space objects.
		/// </summary>
		/// <param name="emp"></param>
		/// <param name="name"></param>
		/// <param name="index"></param>
		/// <param name="filter"></param>
		/// <returns></returns>
		public string GetAbilityValue(Empire emp, string name, int index = 1, Func<Ability, bool> filter = null)
		{
			var abils = SpaceObjects.Where(o => o.Owner == emp).SelectMany(o => o.UnstackedAbilities).Where(a => a.Name == name && (filter == null || filter(a))).Stack();
			if (!abils.Any())
				return null;
			return abils.First().Values[index - 1];
		}

		/// <summary>
		/// Do any of the empire's space objects in this sector have an ability?
		/// </summary>
		/// <param name="emp"></param>
		/// <param name="name"></param>
		/// <param name="index"></param>
		/// <param name="filter"></param>
		/// <returns></returns>
		public bool HasAbility(Empire emp, string name, int index = 1, Func<Ability, bool> filter = null)
		{
			return SpaceObjects.Where(o => o.Owner == emp).SelectMany(o => o.UnstackedAbilities).Where(a => a.Name == name && (filter == null || filter(a))).Any();
		}


		public Visibility CheckVisibility(Empire emp)
		{
			return this.FindStarSystem().CheckVisibility(emp);
		}
	}
}
