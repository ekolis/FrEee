using FrEee.Modding;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game
{
	/// <summary>
	/// A star. Normally found at the center of a star system.
	/// </summary>
	public class Star : StellarObject, ITemplate<Star>
	{
		/// <summary>
		/// The size of this star.
		/// </summary>
		public Size Size { get; set; }

		/// <summary>
		/// The brightness of this star. (For flavor)
		/// </summary>
		public string Brightness { get; set; }

		/// <summary>
		/// The color of this star. (For flavor)
		/// </summary>
		public string Color { get; set; }

		/// <summary>
		/// The age of this star. (For flavor)
		/// </summary>
		public string Age { get; set; }

		/// <summary>
		/// Description of this star. (For flavor)
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Just copy the star's data.
		/// </summary>
		/// <returns>A copy of the star.</returns>
		public new Star Instantiate()
		{
			return this.Clone();
		}
	}
}
