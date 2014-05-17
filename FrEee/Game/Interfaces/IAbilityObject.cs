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
		/// Abilities possessed intrinsically by this object.
		/// </summary>
		IEnumerable<Ability> IntrinsicAbilities { get; }

		/// <summary>
		/// Child objects that can pass up abilities to this object.
		/// </summary>
		IEnumerable<IAbilityObject> Children { get; }

		/// <summary>
		/// Parent object from which this object can inherit abilities.
		/// </summary>
		IAbilityObject Parent { get; }

		/// <summary>
		/// The type of ability target that this object represents.
		/// </summary>
		AbilityTargets AbilityTarget { get; }
	}
}
