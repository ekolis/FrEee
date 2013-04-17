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
	public class WarpPoint : ISpaceObject
	{
		/// <summary>
		/// The name of this warp point.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Warp points cannot be obscured by fog of war.
		/// </summary>
		public bool CanBeFogged { get { return false; } }

		/// <summary>
		/// The size of this warp point.
		/// </summary>
		public Size Size { get; set; }

		/// <summary>
		/// Index of the picture to use to represent this storm.
		/// </summary>
		public int PictureNumber { get; set; }

		/// <summary>
		/// A picture used to represent this storm on the map.
		/// </summary>
		public Image Icon
		{
			get
			{
				return Pictures.GetStellarObjectIcon(PictureNumber);
			}
		}

		/// <summary>
		/// A picture used to represent this storm in reports.
		/// </summary>
		public Image Portrait
		{
			get
			{
				return Pictures.GetStellarObjectPortrait(PictureNumber);
			}
		}

		/// <summary>
		/// The sector that ships will appear in when they go through this warp point.
		/// </summary>
		public Sector Target { get; set; }

		/// <summary>
		/// Does this warp point have special abilities?
		/// </summary>
		public bool IsUnusual { get; set; }
	}
}
