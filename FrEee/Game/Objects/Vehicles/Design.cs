using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Vehicles;
using FrEee.Modding.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Vehicles
{
	/// <summary>
	/// A vehicle design.
	/// </summary>
	/// <typeparam name="T">The type of vehicle.</typeparam>
	public class Design<T> : IDesign, ITemplate<T> where T : Vehicle<T>, new()
	{
		public Design(Hull<T> hull)
		{
			Hull = hull;
			Components = new List<MountedComponentTemplate>();
		}

		public string Name { get; set; }

		/// <summary>
		/// The empire which created this design.
		/// </summary>
		public Empire Owner { get; set; }

		IHull IDesign.Hull { get { return Hull; } }

		/// <summary>
		/// The hull used in this design.
		/// </summary>
		public Hull<T> Hull { get; set; }

		/// <summary>
		/// The components used in this design.
		/// </summary>
		public IList<MountedComponentTemplate> Components
		{
			get;
			private set;
		}

		public T Instantiate()
		{
			var t = new T();
			t.Design = this;
			foreach (var mct in Components)
				t.Components.Add(mct.Instantiate());
			return t;
		}

		public VehicleTypes VehicleType
		{
			get
			{
				// TODO - implement vehicle types once we have actual vehicle classes
				return VehicleTypes.None;
			}
		}

		public string VehicleTypeName
		{
			get
			{
				// TODO - add spaces where necessary
				return VehicleType.ToString();
			}
		}

		/// <summary>
		/// The ship's role (design type in SE4).
		/// </summary>
		public string Role { get; set; }

		/// <summary>
		/// The turn this design was created (for our designs) or discovered (for alien designs).
		/// </summary>
		public int TurnNumber { get; set; }
	}
}