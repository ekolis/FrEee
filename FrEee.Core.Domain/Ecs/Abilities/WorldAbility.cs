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
/// Marks an entity with <see cref="SemanticScope.World"/>
/// and provides any required data for the entity to be a world.
/// </summary>
public class WorldAbility(
	IEntity entity,
	IFormula<int> colonyCapacity
) : HolderAbility<ColonyAbility>(
	entity,
	AbilityRule.Find(SemanticScope.World.Name),
	scope: new LiteralFormula<string>(SemanticScope.World.Name),
	// TODO: what does world size mean? worlds don't take up space
	1.ToLiteralFormula(),
	new LiteralFormula<string>(SemanticScope.Colony.Name),
	colonyCapacity
)
{
	public WorldAbility
	(
		IEntity entity,
		AbilityRule rule,
		Formula<string>? description,
		IFormula[] values
	) : this(entity, values[0].ToFormula<int>())
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
}