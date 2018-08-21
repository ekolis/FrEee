﻿using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using System.Drawing;

namespace FrEee.Modding.Interfaces
{
	/// <summary>
	/// A location that may specify either a specific sector's coordinates, or a group of sectors, from which one is chosen randomly.
	/// </summary>
	public interface IStellarObjectLocation
	{
		/// <summary>
		/// The last coordinates chosen.
		/// Used for "Same As" locations.
		/// </summary>
		Point? LastResult { get; }

		ITemplate<StellarObject> StellarObjectTemplate { get; set; }

		/// <summary>
		/// Chooses a sector.
		/// </summary>
		/// <param name="radius">The star system.</param>
		/// <returns></returns>
		Point Resolve(StarSystem sys);
	}
}