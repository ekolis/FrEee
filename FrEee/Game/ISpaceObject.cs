using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FrEee.Game
{
	/// <summary>
	/// An object that can exist independently in space.
	/// </summary>
	public interface ISpaceObject
	{
		/// <summary>
		/// The name of this space object.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Can this space object be obscured by fog of war?
		/// </summary>
		bool CanBeFogged { get; }

		/// <summary>
		/// A picture used to represent this space object on the map.
		/// </summary>
		Image Icon { get; }

		/// <summary>
		/// A picture used to represent this space object in reports.
		/// </summary>
		Image Portrait { get; }

		/// <summary>
		/// Abilities intrinsically possessed by this space object.
		/// </summary>
		IList<Ability> IntrinsicAbilities { get; }

		/// <summary>
		/// All abilities possessed by this space object, whether intrinsic or inherited.
		/// </summary>
		IEnumerable<Ability> Abilities { get; }
	}
}
