using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// A trait that grants abilities to an empire or race.
	/// </summary>
	[Serializable]
	public class Trait : IModObject, IAbilityObject
	{
		public Trait()
		{
			Abilities = new List<Ability>();
			RequiredTraits = new List<Trait>();
			RestrictedTraits = new List<Trait>();
		}

		public string Name
		{
			get;
			set;
		}

		public Formula<string> Description { get; set; }

		/// <summary>
		/// The cost of this trait, in empire points.
		/// </summary>
		public Formula<int> Cost { get; set; }

		/// <summary>
		/// Abilities granted by this trait.
		/// </summary>
		public IList<Ability> Abilities { get; private set; }

		/// <summary>
		/// Traits that are required to choose this trait.
		/// </summary>
		public IList<Trait> RequiredTraits { get; private set; }

		/// <summary>
		/// Traits that cannot be chosen alongside this trait.
		/// </summary>
		public IList<Trait> RestrictedTraits { get; private set; }

		public override string ToString()
		{
			return Name;
		}

		string INamed.Name
		{
			get { return Name; }
		}

		public AbilityTargets AbilityTarget
		{
			get { return AbilityTargets.Trait; }
		}

		public IEnumerable<Ability> IntrinsicAbilities
		{
			get { return Abilities; }
		}

		public IEnumerable<IAbilityObject> Children
		{
			get { yield break; }
		}

		public IAbilityObject Parent
		{
			get { return null; }
		}

		public string ModID { get; set; }
	}
}
