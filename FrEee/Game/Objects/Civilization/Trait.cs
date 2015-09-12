using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// A trait that grants abilities to an empire or race.
	/// </summary>
	[Serializable]
	public class Trait : IModObject, IAbilityObject, IUnlockable
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

		public IEnumerable<IAbilityObject> Parents
		{
			get
			{
				yield break;
			}
		}

		public string ModID { get; set; }

		public void Dispose()
		{
			// nothing to do
		}

		public IList<Requirement<Empire>> UnlockRequirements
		{
			get
			{
				var list = new List<Requirement<Empire>>();
				foreach (var t in RequiredTraits)
					list.Add(new EmpireTraitRequirement(t, true));
				foreach (var t in RestrictedTraits)
					list.Add(new EmpireTraitRequirement(t, false));
				return list;
			}
		}

		public string ResearchGroup
		{
			get { return "Trait"; }
		}

		/// <summary>
		/// TODO - trait pictures
		/// </summary>
		public Image Portrait { get { return null; } }

		/// <summary>
		/// TODO - trait pictures
		/// </summary>
		public Image Icon { get { return null; } }

		public IEnumerable<string> IconPaths
		{
			get
			{
				yield break;
			}
		}

		public IEnumerable<string> PortraitPaths
		{
			get
			{
				yield break;
			}
		}

		public bool IsDisposed
		{
			// TODO - disposable traits?
			get { return false; }
		}
	}
}
