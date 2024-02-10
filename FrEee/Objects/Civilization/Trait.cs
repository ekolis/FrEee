using FrEee.Enumerations;
using FrEee.Objects.Abilities;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using FrEee.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using FrEee.Objects.GameState;

namespace FrEee.Objects.Civilization;

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

	public AbilityTargets AbilityTarget
	{
		get { return AbilityTargets.Trait; }
	}

	public IEnumerable<IAbilityObject> Children
	{
		get { yield break; }
	}

	/// <summary>
	/// The cost of this trait, in empire points.
	/// </summary>
	public Formula<int> Cost { get; set; }

	public Formula<string> Description { get; set; }

	/// <summary>
	/// TODO - trait pictures
	/// </summary>
	public Image Icon { get { return null; } }

	public Image Icon32 => Icon.Resize(32);

	public IEnumerable<string> IconPaths
	{
		get
		{
			yield break;
		}
	}

	public IEnumerable<Ability> IntrinsicAbilities
	{
		get { return Abilities; }
	}

	public bool IsDisposed
	{
		// TODO - disposable traits?
		get { return false; }
	}

	public string ModID { get; set; }

	public string Name
	{
		get;
		set;
	}

	string INamed.Name
	{
		get { return Name; }
	}

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
	public Image Portrait { get { return null; } }

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

	public string ResearchGroup
	{
		get { return "Trait"; }
	}

	/// <summary>
	/// Traits that cannot be chosen alongside this trait.
	/// </summary>
	public IList<Trait> RestrictedTraits { get; private set; }

	/// <summary>
	/// Parameters from the mod meta templates.
	/// </summary>
	public IDictionary<string, object> TemplateParameters { get; set; }

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

	public override string ToString()
	{
		return Name;
	}
}