using System;
using System.Collections.Generic;
using System.Linq;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.Game.Objects.Technology;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// A colony on a planet.
	/// </summary>
	 [Serializable] public class Colony : IAbilityObject
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
			get { return Facilities.SelectMany(f => f.Abilities).ToArray().Stack(); }
		}

		public ConstructionQueue ConstructionQueue
		{
			get;
			set;
		}
	}
}
