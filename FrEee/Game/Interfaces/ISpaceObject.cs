using System;
using System.Collections.Generic;
using System.Drawing;
using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;

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
	}
}
