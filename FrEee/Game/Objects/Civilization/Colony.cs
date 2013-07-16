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
	[Serializable]
	public class Colony : IAbilityObject, IOwnable
	{
		public Colony()
		{
			Facilities = new List<Facility>();
			Population = new SafeDictionary<Race, long>();
			Cargo = new Cargo();
		}

		/// <summary>
		/// The empire which owns this colony.
		/// </summary>
		public Empire Owner { get; set; }

		/// <summary>
		/// The facilities on this colony.
		/// </summary>
		public ICollection<Facility> Facilities { get; set; }

		/// <summary>
		/// The population of this colony, by race.
		/// </summary>
		public SafeDictionary<Race, long> Population { get; private set; }

		public IEnumerable<Ability> Abilities
		{
			get { return UnstackedAbilities.Stack(); }
		}

		public IEnumerable<Ability> UnstackedAbilities
		{
			// TODO - take into account racial abilities if all races on colony share a trait
			get { return Facilities.SelectMany(f => f.Abilities).ToArray().Concat(Owner.Abilities); }
		}

		/// <summary>
		/// This colony's construction queue.
		/// </summary>
		public ConstructionQueue ConstructionQueue
		{
			get;
			set;
		}

		/// <summary>
		/// The cargo stored on this colony.
		/// </summary>
		public Cargo Cargo { get; set; }
	}
}
