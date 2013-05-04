using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game
{
	/// <summary>
	/// A warp point connecting two sectors.
	/// </summary>
	public class WarpPoint : StellarObject, ITemplate<WarpPoint>
	{
		/// <summary>
		/// The size of this warp point.
		/// </summary>
		public Size Size { get; set; }

		/// <summary>
		/// The sector that ships will appear in when they go through this warp point.
		/// </summary>
		public Sector Target { get; set; }

		/// <summary>
		/// Does this warp point have special abilities?
		/// </summary>
		public bool IsUnusual { get; set; }

		/// <summary>
		/// Just copy the warp point's data.
		/// </summary>
		/// <returns>A copy of the warp point.</returns>
		public new WarpPoint Instantiate()
		{
			return this.Clone();
		}
	}
}
