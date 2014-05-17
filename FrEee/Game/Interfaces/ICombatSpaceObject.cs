using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A space object that can participate in combat.
	/// </summary>
	public interface ICombatSpaceObject : ICombatant, ISpaceObject
	{
	}
}
