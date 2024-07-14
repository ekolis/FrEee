using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FrEee.Ecs.Abilities.Utility;
using FrEee.Modding;
using FrEee.Objects.Technology;
using FrEee.Utility;

namespace FrEee.Ecs.Abilities;

/// <summary>
/// Holds entities that have a <see cref="SemanticScopeAbility"/> with the appopriate scope.
/// </summary>
public class HolderAbility<THeldAbility>(
	IEntity entity,
	AbilityRule rule,
	IFormula<string> scope,
	IFormula<int> size,
	IFormula<string> heldScope,
	IFormula<int> capacity
) : SemanticScopeAbility(entity, rule, scope: scope, size: size)
	where THeldAbility : SemanticScopeAbility
{
	public HolderAbility
	(
		IEntity entity,
		AbilityRule rule,
		Formula<string>? description,
		IFormula[] values
	) : this(
		entity,
		rule, 
		scope: values[0].ToStringFormula(),
		values[1].ToFormula<int>(),
		values[2].ToStringFormula(),
		values[3].ToFormula<int>()
	)
	{
		HeldAbilities = new List<THeldAbility>();
	}

	/// <summary>
	/// Scope describing what can be held.
	/// </summary>
	public IFormula<string> HeldScope { get; private set; } = heldScope;

	/// <summary>
	/// The maximum capacity in number or size of items which can be held.
	/// </summary>
	public IFormula<int> Capacity { get; private set; } = capacity;

	/// <summary>
	/// The currently held abilities.
	/// </summary>
	// TODO: validate that held abilities have the right scope and fit within the specified capacity
	public IList<THeldAbility> HeldAbilities { get; internal set; } = new List<THeldAbility>();

	/// <summary>
	/// The currently held entities.
	/// </summary>
	public IEnumerable<IEntity> HeldEntities => HeldAbilities?.Select(q => q.Container) ?? [];

	public override SafeDictionary<string, object> Data
	{
		get
		{
			var data = base.Data;
			data["HeldAbilities"] = HeldAbilities;
			return data;
		}
		set
		{
			base.Data = value;
			Capacity = Value1.ToFormula<int>();
			HeldAbilities = (IList<THeldAbility>)value["HeldAbilities"];
		}
	}

	public override void Interact(IInteraction interaction)
	{
		base.Interact(interaction);
		foreach (var heldAbility in HeldAbilities)
		{
			// TODO: implement ownership scopes for abilities
			heldAbility.Container.Interact(interaction);
		}
	}
}
