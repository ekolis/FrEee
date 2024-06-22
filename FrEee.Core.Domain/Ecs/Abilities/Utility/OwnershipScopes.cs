using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Ecs.Abilities.Utility
{
	/// <summary>
	/// Scope of an ability in terms of the ownership of the entities it can affect.
	/// </summary>
	[Flags]
	public enum OwnershipScopes
	{
		Self = 0x1,
		Allies = 0x2,
		Neutral = 0x4,
		Enemies = 0x8,
		Unowned = 0x10,

		Default = Self,
		Friends = Self | Allies,
		Hostiles = Neutral | Enemies,
		Owned = Friends | Hostiles,
		All = Owned | Unowned,
	}
}
