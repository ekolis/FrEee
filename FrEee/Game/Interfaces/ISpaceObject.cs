using System;
using System.Collections.Generic;
using System.Drawing;
using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Combat;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// An object that can exist independently in space.
	/// </summary>
	public interface ISpaceObject : IAbilityObject, IPictorial
	{
		/// <summary>
		/// The name of this space object.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Abilities intrinsically possessed by this space object.
		/// </summary>
		IList<Ability> IntrinsicAbilities { get; }

		/// <summary>
		/// The empire owning this space object.
		/// </summary>
		Empire Owner { get; }

		/// <summary>
		/// Checks visibility of this space object for the current player.
		/// </summary>
		/// <param name="galaxy">The galaxy, for context.</param>
		/// <param name="starSystem">The star system containing the space object.</param>
		/// <returns>The visibility rating.</returns>
		Visibility CheckVisibility(Galaxy galaxy, StarSystem starSystem);

		/// <summary>
		/// Removes any data from this space object that the current player cannot see.
		/// </summary>
		/// <param name="galaxy">The galaxy, for context.</param>
		/// <param name="starSystem">The star system containing the space object.</param>
		/// <param name="visibility">The visibility rating of this object.</param>
		/// <exception cref="ArgumentException">if the visibility parameter is Visibility.Unknown. (The object should be removed instead of redacted!)</exception>
		void Redact(Galaxy galaxy, StarSystem starSystem, Visibility visibility);

		/// <summary>
		/// Is this space object hostile to an empire?
		/// </summary>
		bool IsHostileTo(Empire emp);

		/// <summary>
		/// This space object's construction queue, if any.
		/// </summary>
		ConstructionQueue ConstructionQueue { get; }

		/// <summary>
		/// Can this space object be placed in a fleet?
		/// </summary>
		bool CanBeInFleet { get; }

		/// <summary>
		/// Cargo storage capacity.
		/// </summary>
		int CargoStorage { get; }

		/// <summary>
		/// Supply storage capacity.
		/// TODO - make supply a resource?
		/// </summary>
		int SupplyStorage { get; }

		/// <summary>
		/// Does this space object have infinite supplies?
		/// TODO - make supply a resource?
		/// </summary>
		bool HasInfiniteSupplies { get; }

		/// <summary>
		/// Can this space object fire on another space object?
		/// </summary>
		/// <param name="target"></param>
		/// <returns>true if the target is an enemy and this space object has weapons capable of targeting it</returns>
		bool CanTarget(ISpaceObject target);

		/// <summary>
		/// What type of object is this for weapon targeting purposes?
		/// TODO - move this into an IDamageable interface
		/// </summary>
		WeaponTargets WeaponTargetType { get; }

		/// <summary>
		/// Any undamaged weapons this space object is armed with.
		/// </summary>
		IEnumerable<Component> Weapons { get; }

		/// <summary>
		/// Inflicts damage on this space object.
		/// TODO - move this into an IDamageable interface
		/// </summary>
		/// <param name="damageType"></param>
		/// <param name="damage"></param>
		void TakeDamage(DamageType damageType, int damage);
	}
}
