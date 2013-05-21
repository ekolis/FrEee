using System;
using System.Collections.Generic;
using FrEee.Game.Interfaces;

namespace FrEee.Game.Objects.Space
{
	/// <summary>
	/// A sector in a star system. Can contain space objects.
	/// </summary>
	 [Serializable] public class Sector
	{
		public Sector()
		{
			SpaceObjects = new HashSet<ISpaceObject>();
		}

		/// <summary>
		/// The space objects contained in this sector.
		/// </summary>
		public ISet<ISpaceObject> SpaceObjects { get; private set; }
	}
}
