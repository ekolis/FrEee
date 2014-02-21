using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Something which can have abilities and be owned.
	/// </summary>
	public interface IOwnableAbilityObject : IOwnable, IAbilityObject
	{
	}
}
