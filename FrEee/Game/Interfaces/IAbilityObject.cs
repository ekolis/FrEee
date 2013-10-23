using System.Collections.Generic;
using FrEee.Game.Objects.Abilities;
using FrEee.Modding.Interfaces;
using FrEee.Game.Enumerations;

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

		/// <summary>
		/// The type of ability target that this object represents.
		/// </summary>
		AbilityTargets AbilityTarget { get; }
	}
}
