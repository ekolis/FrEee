using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Game.Objects.Abilities
{
	/// <summary>
	/// A special ability of some game object, or just a tag used by the AI or by modders.
	/// </summary>
	[Serializable]
	public class Ability : IContainable<IAbilityObject>, IReferrable, IModObject, IDataObject, IFormulaHost
	{
		public Ability(IAbilityObject container)
		{
			Container = container;
			Values = new List<Formula<string>>();
		}

		public Ability(IAbilityObject container, AbilityRule rule, string description = null, params object[] values)
		{
			Container = container;
			Rule = rule;
			Description = description;
			Values = new List<Formula<string>>();
			foreach (var val in values)
			{
				if (val is IFormula)
					Values.Add((val as IFormula).ToStringFormula());
				else
					Values.Add(new LiteralFormula<string>(val.ToString()));
			}
		}

		[DoNotCopy]
		public IAbilityObject Container { get; private set; }

		public virtual SafeDictionary<string, object> Data
		{
			get
			{
				var dict = new SafeDictionary<string, object>();
				dict[nameof(rule)] = rule;
				dict[nameof(Description)] = Description;
				dict[nameof(Values)] = Values;
				dict[nameof(Container)] = Container;
				dict[nameof(ID)] = ID;
				dict[nameof(IsDisposed)] = IsDisposed;
				dict[nameof(ModID)] = ModID;
				return dict;
			}

			set
			{
				rule = value[nameof(rule)].Default<ModReference<AbilityRule>>();
				Description = value[nameof(Description)].Default<Formula<string>>();
				Values = value[nameof(Values)].Default<IList<Formula<string>>>();
				Container = value[nameof(Container)].Default<IAbilityObject>();
				ID = value[nameof(ID)].Default<long>();
				IsDisposed = value[nameof(IsDisposed)].Default<bool>();
				ModID = value[nameof(ModID)].Default<string>();
			}
		}

		/// <summary>
		/// A description of the ability's effects.
		/// Can use, e.g. [%Amount1%] to specify the amount in the Value 1 field.
		/// </summary>
		public Formula<string> Description { get; set; }

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

		public long ID { get; set; }

		public bool IsDisposed { get; set; }

		public string ModID { get; set; }

		// TODO - should abilities even have names?
		public string Name => null;

		public Empire Owner => (Container as IOwnable)?.Owner;

		/// <summary>
		/// The ability rule which defines what ability this is.
		/// </summary>
		[DoNotCopy]
		public AbilityRule Rule { get => rule; set => rule = value; }

		/// <summary>
		/// Parameters from the mod meta templates.
		/// </summary>
		public IDictionary<string, object> TemplateParameters { get; set; }

		/// <summary>
		/// The first value of the ability. Not all abilities have values, so this might be null!
		/// </summary>
		public Formula<string> Value1 => Values.ElementAtOrDefault(0);

		/// <summary>
		/// The second value of the ability. Not all abilities have two values, so this might be null!
		/// </summary>
		public Formula<string> Value2 => Values.ElementAtOrDefault(1);

		/// <summary>
		/// Extra data for the ability.
		/// </summary>
		public IList<Formula<string>> Values { get; set; }

		public IDictionary<string, object> Variables
		{
			get
			{
				var dict = new Dictionary<string, object>();
				for (var i = 0; i < Values.Count; i++)
					dict.Add("Amount" + (i + 1), Values[i]);
				if (Rule.Matches("Shield Generation") || Rule.Matches("Phased Shield Generation") || Rule.Matches("Planet - Shield Generation"))
					dict.Add("ShieldPointsGenerated", Value1.ToInt()); // TODO - take into account mounts that affect shields
				return dict;
			}
		}

		private ModReference<AbilityRule> rule { get; set; }

		public void Dispose()
		{
			if (IsDisposed)
				return;
			if (Container is IAbilityContainer abilityContainer)
				abilityContainer.Abilities.Remove(this);
			Galaxy.Current.UnassignID(this);
		}

		public override string ToString()
		{
			// get basic description
			string result;
			if (Description != null)
				result = Description.Evaluate(this, TemplateParameters);
			else if (Rule.Description != null)
				result = Rule.Description.Evaluate(this, TemplateParameters);
			else
				result = Rule.Name + ": " + string.Join(", ", Values.Select(v => v.Value));

			return result;
		}
	}
}
