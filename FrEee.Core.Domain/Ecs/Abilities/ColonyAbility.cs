using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FrEee.Objects.Civilization.Construction;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.GameState;
using FrEee.Processes.Combat;
using FrEee.Ecs;
using FrEee.Ecs.Abilities.Utility;
using FrEee.Objects.Technology;
using FrEee.Ecs.Stats;

namespace FrEee.Ecs.Abilities;

/// <summary>
/// Marks an entity with <see cref="SemanticScope.Colony"/>
/// and provides any required data for the entity to be a colony.
/// </summary>
public class ColonyAbility(
	IEntity entity,
	AbilityRule rule,
	Formula<string>? description,
	params IFormula[] values
) : SemanticScopeAbility(entity, rule, description, values)
{
	public ColonyAbility(
		IEntity entity
	) : this(
		entity,
		AbilityRule.Find(SemanticScope.Colony),
		null,
		new LiteralFormula<string>(SemanticScope.Colony),
		1.ToLiteralFormula()
	// TODO: maybe limit number/size of colonies on a planet rather than just having one?
	)
	{
	}

	public override SafeDictionary<string, object> Data
	{
		get
		{
			var dict = base.Data;
			return dict;
		}
		set
		{
			base.Data = value;
		}
	}

	public override string ToString()
	{
		return Name;
	}
}