using FrEee.Game.Interfaces;
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
	}
}