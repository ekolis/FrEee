using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FrEee.Utility;

namespace FrEee.Game.Objects.Abilities
{
	/// <summary>
	/// A rule for grouping and stacking abilities.
	/// </summary>
	public class AbilityRule
	{
		public AbilityRule()
		{
			StackingRules = new List<AbilityValueStackingRule>();
		}

		/// <summary>
		/// The name of the ability to which this rule applies.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The rule for grouping abilities.
		/// </summary>
		public AbilityGroupingRule GroupingRule { get; set; }

		/// <summary>
		/// The rules for stacking abilities.
		/// </summary>
		public IList<AbilityValueStackingRule> StackingRules { get; set; }

		/// <summary>
		/// Groups and stacks abilities.
		/// </summary>
		/// <param name="abilities"></param>
		/// <returns></returns>
		public IEnumerable<Ability> GroupAndStack(IEnumerable<Ability> abilities)
		{
			var ours = abilities.Where(a => a.Name == Name);
			if (!ours.Any())
				return abilities;

			var others = abilities.Where(a => a.Name != Name);

			// default grouping: one big group
			IEnumerable<IGrouping<string, Ability>> result = ours.GroupBy(a => "");

			// group abilities
			if (GroupingRule == AbilityGroupingRule.GroupByValue1)
				result = ours.GroupBy(a => a.Value1);
			else if (GroupingRule == AbilityGroupingRule.GroupByValue2)
				result = ours.GroupBy(a => a.Value2);

			// stack abilities		
			result = result.SelectMany(g => Stack(g).GroupBy(a => g.Key));

			return result.Flatten().Concat(others);
		}

		private IEnumerable<Ability> Stack(IEnumerable<Ability> abilities)
		{
			Ability result = new Ability();
			result.Name = abilities.First().Name;
			foreach (var abil in abilities)
			{
				for (int i = 0; i < abil.Values.Count; i++)
				{
					var rule = StackingRules.ElementAtOrDefault(i);
					if (rule == AbilityValueStackingRule.DoNotStack)
					{
						// don't stack when Do Not Stack rule is found unless it's the value we're grouping by
						if (i == 0 && GroupingRule != AbilityGroupingRule.GroupByValue1)
							return abilities;
						if (i == 1 && GroupingRule != AbilityGroupingRule.GroupByValue2)
							return abilities;
						if (i >= 2)
							return abilities;
					}
					// TODO - don't repeatedly convert to/from strings, just do it once outside the loop
					double oldval = result.Values.Count > i ? result.Values[i].ToDouble() : 0;
					double incoming = abil.Values.Count > i ? abil.Values[i].ToDouble() : 0;
					double newval = oldval;
					if (rule == AbilityValueStackingRule.Add)
						newval = oldval + incoming;
					else if (rule == AbilityValueStackingRule.TakeAverage)
						newval = oldval + incoming / abilities.Count();
					else if (rule == AbilityValueStackingRule.TakeHighest)
						newval = Math.Max(oldval, incoming);
					else if (rule == AbilityValueStackingRule.TakeLowest)
						newval = Math.Min(oldval, incoming);
					else if (GroupingRule == AbilityGroupingRule.GroupByValue1 && i == 0 || GroupingRule == AbilityGroupingRule.GroupByValue2 && i == 1)
						newval = incoming;
					if (result.Values.Count > i)
						result.Values[i] = newval.ToString(CultureInfo.InvariantCulture);
					else
					{
						while (result.Values.Count < i)
							result.Values.Add(null);
						result.Values.Add(newval.ToString(CultureInfo.InvariantCulture));
					}
				}
			}
			if (result.Values.Any())
				result.Description = result.Name + ": " + string.Join(", ", result.Values.ToArray());
			else
				result.Description = result.Name;
			return new Ability[] { result };
		}
	}
}
