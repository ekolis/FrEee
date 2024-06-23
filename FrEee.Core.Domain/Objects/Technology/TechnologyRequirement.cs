using FrEee.Objects.Civilization;
using FrEee.Modding;
using FrEee.Serialization;
using System;
using FrEee.Objects.GameState;
using FrEee.Serialization;

namespace FrEee.Objects.Technology;

/// <summary>
/// Requirement for a technology to be researched to a particular level.
/// </summary>
[Serializable]
public class TechnologyRequirement : Requirement<Empire>, IContainable<IResearchable>
{
	public TechnologyRequirement(IResearchable container, Technology tech, Formula<int> level)
		: base("Requires level " + level + " " + tech + ".")
	{
		Entity = container;
		Technology = tech;
		Level = level;
	}

	/// <summary>
	/// What researchable object "owns" this technology requirement.
	/// </summary>
	public IResearchable Entity
	{
		get;
		private set;
	}

	/// <summary>
	/// Technology requirements are always strict.
	/// </summary>
	public override bool IsStrict
	{
		get { return true; }
	}

	/// <summary>
	/// The level required.
	/// </summary>
	public Formula<int> Level { get; set; }

	private ModReference<Technology> technology { get; set; }

	/// <summary>
	/// The technology to be researched.
	/// </summary>
	[DoNotSerialize(false)]
	public Technology Technology { get => technology; set => technology = value; }

	/// <summary>
	/// Is this technology requirement met by a particular empire?
	/// </summary>
	/// <param name="emp">The empire being tested.</param>
	/// <returns>true if the requirement is met, otherwise false.</returns>
	public override bool IsMetBy(Empire emp)
	{
		return emp.ResearchedTechnologies[Technology] >= Level;
	}
}