using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FrEee.Utility;
using FrEee.Utility.Extensions;

namespace FrEee.Game.Objects.Abilities
{
	/// <summary>
	/// A rule for grouping and stacking abilities.
	/// </summary>
	 [Serializable] public class AbilityRule
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
		public ILookup<Ability, Ability> GroupAndStack(IEnumerable<Ability> abilities)
		{
			var ours = abilities.Where(a => a.Name == Name).ToArray();

			// group abilities
			IEnumerable<IGrouping<string, Ability>> grouped;
			if (GroupingRule == AbilityGroupingRule.GroupByValue1)
				grouped = ours.GroupBy(a => a.Value1);
			else if (GroupingRule == AbilityGroupingRule.GroupByValue2)
				grouped = ours.GroupBy(a => a.Value2);
			else
				grouped = ours.GroupBy(a => "");

			// stack abilities		
			var list = new List<Tuple<Ability, Ability>>();
			foreach (var group in grouped)
			{
				var stacked = Stack(group);
				foreach (var stack in stacked)
				{
					foreach (var abil in stack)
						list.Add(Tuple.Create(stack.Key, abil));
				}
			}

			return list.ToLookup(t => t.Item1, t => t.Item2);
		}

		private ILookup<Ability, Ability> Stack(IEnumerable<Ability> abilities)
		{
			if (abilities.Count() <= 1)
				return abilities.ToLookup(a => a, a => a);

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
							return abilities.ToLookup(a => a, a => a);
						if (i == 1 && GroupingRule != AbilityGroupingRule.GroupByValue2)
							return abilities.ToLookup(a => a, a => a);
						if (i >= 2)
							return abilities.ToLookup(a => a, a => a);
					}
					// TODO - don't repeatedly convert to/from strings, just do it once outside the loop
					double? oldval = result.Values.Count > i ? (double?)result.Values[i].ToDouble() : null;
					double incoming = abil.Values.Count > i ? abil.Values[i].ToDouble() : 0;
					double newval = oldval ?? 0;
					if (rule == AbilityValueStackingRule.Add)
						newval = (oldval ?? 0) + incoming;
					else if (rule == AbilityValueStackingRule.TakeAverage)
						newval = (oldval ?? 0) + incoming / abilities.Count();
					else if (rule == AbilityValueStackingRule.TakeHighest)
					{
						if (oldval == null)
							newval = incoming;
						else
							newval = Math.Max(oldval.Value, incoming);
					}
					else if (rule == AbilityValueStackingRule.TakeLowest)
					{
						if (oldval == null)
							newval = incoming;
						else
							newval = Math.Min(oldval.Value, incoming);
					}
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
			return abilities.ToLookup(a => result, a => a);
		}
	}
}
