using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game
{
	/// <summary>
	/// A sector in a star system. Can contain space objects.
	/// </summary>
	public class Sector
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
