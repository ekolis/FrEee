using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game
{
	/// <summary>
	/// A colony on a planet.
	/// </summary>
	public class Colony : IAbilityObject
	{
		public Colony()
		{
			Facilities = new List<Facility>();
		}

		/// <summary>
		/// The empire which owns this colony.
		/// </summary>
		public Empire Owner { get; set; }

		/// <summary>
		/// The facilities on this colony.
		/// </summary>
		public ICollection<Facility> Facilities { get; set; }

		public IEnumerable<Ability> Abilities
		{
			get { return Facilities.SelectMany(f => f.Abilities); }
		}
	}
}
