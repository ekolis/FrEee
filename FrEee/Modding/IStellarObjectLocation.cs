using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Modding
{
	/// <summary>
	/// A location that may specify either a specific sector's coordinates, or a group of sectors, from which one is chosen randomly.
	/// </summary>
	public interface IStellarObjectLocation
	{
		ITemplate<ISpaceObject> StellarObjectTemplate { get; set; }

		/// <summary>
		/// Chooses a sector.
		/// </summary>
		/// <param name="radius">The star system.</param>
		/// <returns></returns>
		Point Resolve(StarSystem sys);
	}
}
