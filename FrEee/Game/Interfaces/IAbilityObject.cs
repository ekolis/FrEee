using System.Collections.Generic;
using FrEee.Game.Objects.Abilities;

namespace FrEee.Game.Interfaces
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

		/// <summary>
		/// The abilities possessed by this object, before stacking.
		/// </summary>
		IEnumerable<Ability> UnstackedAbilities { get; }
	}
}
