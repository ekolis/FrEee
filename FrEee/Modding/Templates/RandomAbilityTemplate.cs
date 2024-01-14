using FrEee.Interfaces;
using FrEee.Objects.Abilities;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Modding.Templates;

[Serializable]
public struct AbilityChance
{
	/// <summary>
	/// The ability.
	/// </summary>
	public Ability Ability { get; set; }

	/// <summary>
	/// The chance, in tenths of a percent.
	/// </summary>
	public int Chance { get; set; }
}

/// <summary>
/// Chooses a random ability, or none at all, based on a roll of a d1000.
/// </summary>
[Serializable]
public class RandomAbilityTemplate : ITemplate<Ability>, IModObject
{
	public RandomAbilityTemplate()
	{
		AbilityChances = new List<AbilityChance>();
	}

	/// <summary>
	/// Chances to get each ability.
	/// Chances are expressed in tenths of a percent.
	/// The total for all chances should add up to 1000 or less.
	/// </summary>
	public IList<AbilityChance> AbilityChances { get; private set; }

	public bool IsDisposed
	{
		get; private set;
	}

	public string ModID
	{
		get;
		set;
	}

	/// <summary>
	/// The name of this random ability template.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Parameters from the mod meta templates.
	/// </summary>
	public IDictionary<string, object> TemplateParameters { get; set; }

	public void Dispose()
	{
		// TODO - remove it from somewhere?
		IsDisposed = true;
	}

	public Ability Instantiate()
	{
		var num = RandomHelper.Next(Math.Max(1000, AbilityChances.Sum(ac => ac.Chance)));
		var howFar = 0;
		foreach (var ac in AbilityChances)
		{
			howFar += ac.Chance;
			if (num < howFar)
				return ac.Ability;
		}
		return null;
	}
}