using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Game.Objects.Abilities
{
	/// <summary>
	/// A special ability of some game object, or just a tag used by the AI or by modders.
	/// </summary>
	[Serializable]
	public class Ability : IContainable<IAbilityObject>, IReferrable
	{
		public Ability(IAbilityObject container)
		{
			Container = container;
			Values = new List<Formula<string>>();
		}

		public Ability(IAbilityObject container, AbilityRule rule, string description = null, params Formula<string>[] values)
		{
			Container = container;
			Rule = rule;
			Description = description;
			Values = values.ToList();
		}

		/// <summary>
		/// The ability rule which defines what ability this is.
		/// </summary>
		public AbilityRule Rule { get; set; }

		/// <summary>
		/// A description of the ability's effects.
		/// Can use, e.g. [%Amount1%] to specify the amount in the Value 1 field.
		/// </summary>
		public Formula<string> Description { get; set; }

		/// <summary>
		/// Extra data for the ability.
		/// </summary>
		public IList<Formula<string>> Values { get; set; }

		/// <summary>
		/// The first value of the ability. Not all abilities have values, so this might be null!
		/// </summary>
		public Formula<string> Value1
		{
			get
			{
				return Values.ElementAtOrDefault(0);
			}
		}

		/// <summary>
		/// The second value of the ability. Not all abilities have two values, so this might be null!
		/// </summary>
		public Formula<string> Value2
		{
			get
			{
				return Values.ElementAtOrDefault(1);
			}
		}

		public override string ToString()
		{
			// get basic description
			string result;
			if (Description != null && Description.Value != null)
				result = Description.Value;
			else if (Rule.Description != null)
				result = Rule.Description.Value;
			else
				result = Rule.Name + ": " + string.Join(", ", Values.Select(v => v.Value));
			
			// replace [%Amount1%] and such
			for (int i = 1; i <= Rule.ValueRules.Count && i <= Values.Count; i++)
				result = result.Replace("[%Amount" + i + "%]", Values[i - 1]);

			return result;
		}

		public IAbilityObject Container { get; private set; }

		/// <summary>
		/// Key for ability groups.
		/// </summary>
		public IEnumerable<string> Group
		{
			get
			{
				var list = new List<string>();
				for (int i = 0; i < Rule.ValueRules.Count; i++)
				{
					if (Rule.ValueRules.Count > i && Rule.ValueRules[i] == AbilityValueRule.Group)
						yield return Values[i];
					yield return "";
				}
			}
		}

		public long ID
		{
			get;
			set;
		}

		public bool IsDisposed
		{
			get;
			set;
		}

		public void Dispose()
		{
			if (IsDisposed)
				return;
			if (Container is IAbilityContainer)
				(Container as IAbilityContainer).Abilities.Remove(this);
			Galaxy.Current.UnassignID(this);
		}

		public Empire Owner
		{
			get
			{
				if (Container is IOwnable)
					return (Container as IOwnable).Owner;
				return null;
			}
		}
	}
}
