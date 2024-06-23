using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Ecs.Abilities.Utility;
using FrEee.Ecs.Interactions;
using FrEee.Extensions;
using FrEee.Modding;
using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using FrEee.Objects.LogMessages;
using FrEee.Objects.Space;
using FrEee.Objects.Technology;
using FrEee.Processes.Combat;
using FrEee.Utility;
using Microsoft.Scripting.Utils;

namespace FrEee.Ecs.Abilities
{
    /// <summary>
    /// Marks an entity as being owned by some empire.
    /// </summary>
    public class OwnableAbility(IEntity entity, AbilityRule rule, Formula<string>? description, params IFormula[] values)
		: Ability(entity, rule, description, values)
	{
		public OwnableAbility(IEntity entity, AbilityRule rule, Empire? owner)
			 : this(entity, rule, null, [])
		{
			Owner = owner;
		}

		private GalaxyReference<Empire> owner { get; set; }

		public new Empire? Owner
		{
			get => owner;
			set => owner = value;
		}

		public override SafeDictionary<string, object> Data
		{
			get
			{
				var data = base.Data;
				data["owner"] = owner;
				return data;
			}
			set
			{
				base.Data = value;
				owner = (GalaxyReference<Empire>)value["owner"];
			}
		}
	}
}
