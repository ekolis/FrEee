using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game
{
	/// <summary>
	/// Something which can have abilities.
	/// </summary>
	public interface IAbilityObject
	{
		/// <summary>
		/// The abilities possessed by this object.
		/// </summary>
		IEnumerable<Ability> Abilities { get; }
	}
}
