using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;

#nullable enable

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

		/// <summary>
		/// Abilities granted by this trait.
		/// </summary>
		public IList<Ability> Abilities { get; private set; }

		public AbilityTargets AbilityTarget => AbilityTargets.Trait;

		public IEnumerable<IAbilityObject> Children
		{
			get { yield break; }
		}

		/// <summary>
		/// The cost of this trait, in empire points.
		/// </summary>
		public Formula<int>? Cost { get; set; }

		public Formula<string>? Description { get; set; }

		/// <summary>
		/// TODO - trait pictures
		/// </summary>
		public Image? Icon => null;

		public Image Icon32 => Icon.Resize(32);

		public IEnumerable<string> IconPaths
		{
			get
			{
				yield break;
			}
		}

		public IEnumerable<Ability> IntrinsicAbilities => Abilities;

		// TODO - disposable traits?
		public bool IsDisposed => false;

		public string? ModID { get; set; }

		public string? Name { get; set; }

		string? INamed.Name => Name;

		public IEnumerable<IAbilityObject> Parents
		{
			get
			{
				yield break;
			}
		}

		/// <summary>
		/// TODO - trait pictures
		/// </summary>
		public Image? Portrait => null;

		public IEnumerable<string> PortraitPaths
		{
			get
			{
				yield break;
			}
		}

		/// <summary>
		/// Traits that are required to choose this trait.
		/// </summary>
		public IList<Trait> RequiredTraits { get; private set; }

		public string ResearchGroup => "Trait";

		/// <summary>
		/// Traits that cannot be chosen alongside this trait.
		/// </summary>
		public IList<Trait> RestrictedTraits { get; private set; }

		/// <summary>
		/// Parameters from the mod meta templates.
		/// </summary>
		public IDictionary<string, object>? TemplateParameters { get; set; }

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

		public void Dispose()
		{
			// nothing to do
		}

		public override string ToString() => Name ?? string.Empty;
	}
}
