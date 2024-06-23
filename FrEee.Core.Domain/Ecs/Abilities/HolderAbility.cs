using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Ecs.Abilities.Utility;
using FrEee.Modding;
using FrEee.Objects.Technology;
using FrEee.Utility;

namespace FrEee.Ecs.Abilities
{
    /// <summary>
    /// Holds entities that have a <see cref="SemanticScopeAbility"/> with the appopriate category.
    /// </summary>
    class HolderAbility(
		IEntity container,
		AbilityRule rule,
		Formula<string>? description,
		params IFormula[] values
	) : Ability(container, rule, description, values)
	{
		public HolderAbility
		(
			IEntity container,
			AbilityRule rule,
			IFormula<string> heldScope,
			IFormula<int> capacity
		) : this(container, rule, null, heldScope, capacity)
		{ }

		/// <summary>
		/// Scope describing what can be held.
		/// </summary>
		public IFormula<string> HeldScope { get; private set; } = values[0].ToFormula<string>();

		/// <summary>
		/// The maximum capacity in number or size of items which can be held.
		/// </summary>
		public IFormula<int> Capacity { get; private set; } = values[1].ToFormula<int>();

		/// <summary>
		/// The currently held abilities.
		/// </summary>
		// TODO: validate that held abilities have the right scope and fit within the specified capacity
		public IList<SemanticScopeAbility> HeldAbilities { get; private set; } = new List<SemanticScopeAbility>();

		/// <summary>
		/// The currently held entities.
		/// </summary>
		public IEnumerable<IEntity> HeldEntities => HeldAbilities.Select(q => q.Container);

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
				HeldAbilities = (IList<SemanticScopeAbility>)value["HeldAbilities"];
			}
		}

		public override void Interact(IInteraction interaction)
		{
			foreach (var held in HeldAbilities)
			{
				held.Interact(interaction);
			}
		}
	}
}
