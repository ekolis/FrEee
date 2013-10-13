using System;
using System.Collections.Generic;
using System.Linq;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Space;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// A colony on a planet.
	/// </summary>
	[Serializable]
	public class Colony : IAbilityObject, IOwnable, IFoggable, IContainable<Planet>
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
		[RequiresVisibility(Visibility.Scanned)]
		public ICollection<Facility> Facilities { get; set; }

		/// <summary>
		/// The population of this colony, by race.
		/// </summary>
		[RequiresVisibility(Visibility.Scanned)]
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
		[RequiresVisibility(Visibility.Owned)]
		public ConstructionQueue ConstructionQueue
		{
			get;
			set;
		}

		/// <summary>
		/// The cargo stored on this colony.
		/// </summary>
		[RequiresVisibility(Visibility.Scanned)]
		public Cargo Cargo { get; set; }

		public Visibility CheckVisibility(Empire emp)
		{
			// should be visible, assuming the planet is visible - we don't have colony cloaking at the moment...
			if (emp == Owner)
				return Visibility.Owned;
			else
				// colonies cannot be scanned, though that would be a cool special ability
				return Visibility.Visible;
		}

		public long ID
		{
			get;
			set;
		}

		public void Dispose()
		{
			if (Container != null)
				Container.Colony = null;
			Galaxy.Current.UnassignID(this);
			IsKnownToBeDestroyed = true;
			this.UpdateEmpireMemories();
		}

		public Planet Container
		{
			get
			{
				return Galaxy.Current.FindSpaceObjects<Planet>().Flatten().Flatten().SingleOrDefault(p => p.Colony == this);
			}
		}

		public bool IsMemory
		{
			get;
			set;
		}

		public bool IsKnownToBeDestroyed
		{
			get;
			set;
		}

		/// <summary>
		/// Colonies can never be stored in mods.
		/// </summary>
		public bool IsModObject
		{
			get { return false; }
		}
	}
}
