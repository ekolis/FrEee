using FrEee.Game.Objects.Vehicles;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Technology
{
	/// <summary>
	/// A vehicle hull.
	/// </summary>
	/// <typeparam name="T">The type of vehicle.</typeparam>
	public class Hull<T> where T : Vehicle<T>, new()
	{
		// TODO - implement hull

		/// <summary>
		/// The cost to build this hull.
		/// </summary>
		public Resources Cost { get; set; }

		/// <summary>
		/// TODO - implement hull icons (will need knowledge of shipset!)
		/// </summary>
		public Image Icon { get { throw new NotImplementedException(); } }

		/// <summary>
		/// TODO - implement hull portraits (will need knowledge of shipset!)
		/// </summary>
		public Image Portrait { get { throw new NotImplementedException(); } }
	}
}
