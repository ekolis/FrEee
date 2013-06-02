using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
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
	[Serializable]
	public abstract class Vehicle : INamed, IConstructable, IVehicle
	{
		public Vehicle()
		{
			Components = new List<Component>();
			ConstructionProgress = new Resources();
		}

		/// <summary>
		/// The name of this vehicle.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The design of this vehicle.
		/// </summary>
		public IDesign Design { get; set; }

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

		public Image Portrait
		{
			get { return Design.Hull.GetPortrait(Design.Owner.ShipsetPath); }
		}

		public abstract void Place(ISpaceObject target);

		/// <summary>
		/// The owner of this vehicle.
		/// </summary>
		public Empire Owner { get; set; }
	}
}
