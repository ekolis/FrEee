using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Ecs.Abilities.Utility
{
	/// <summary>
	/// Scope of an ability in terms of the locations of the entities it can affect.
	/// </summary>
	public enum SpatialScope
	{
		Entity = 1,
		SpaceObject = 2,
		Sector = 3,
		StarSystem = 4,
		Galaxy = 5,

		Default = SpaceObject,
	}
}
