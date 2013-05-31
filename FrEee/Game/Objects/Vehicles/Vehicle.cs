using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Vehicles
{
	/// <summary>
	/// A ship, base, or unit.
	/// </summary>
	public abstract class Vehicle<T> : INamed, IConstructable where T : Vehicle<T>, new()
	{
		public Vehicle()
		{
			Components = new List<Component>();
		}

		/// <summary>
		/// The name of this vehicle.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The design of this vehicle.
		/// </summary>
		public Design<T> Design { get; set; }

		/// <summary>
		/// The components on this vehicle.
		/// </summary>
		public IList<Component> Components { get; private set; }

		public bool RequiresColonyQueue
		{
			get { return false; }
		}

		public abstract bool RequiresSpaceYardQueue { get; }

		public Resources Cost
		{
			get
			{
				return Design.Hull.Cost + Components.Select(c => c.Template.Cost).Aggregate((c1, c2) => c1 + c2);
			}
		}

		public Resources ConstructionProgress
		{
			get;
			set;
		}

		public Image Icon
		{
			get { return Design.Hull.GetIcon(Design.Owner.ShipsetPath); }
		}

		public void Place(ISpaceObject target)
		{
			var search = Galaxy.Current.FindSpaceObjects<ISpaceObject>(sobj => sobj == target);
			if (!search.Any() || !search.First().Any() || !search.First().First().Any())
				throw new Exception("Can't place newly constructed vehicle near " + target + " because the target is not in any known sector.");
			var sys = search.First().Key.Item;
			var coords = search.First().First().First().Key;
			var sector = sys.GetSector(coords);
			sector.SpaceObjects.Add(target);
		}
	}
}
