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
			ValueRules = new List<AbilityValueRule>();
		}

		/// <summary>
		/// The name of the ability to which this rule applies.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The rules for grouping and stacking abilities.
		/// </summary>
		public IList<AbilityValueRule> ValueRules { get; set; }

		/// <summary>
		/// Groups and stacks abilities.
		/// </summary>
		/// <param name="abilities"></param>
		/// <returns></returns>
		public ILookup<Ability, Ability> GroupAndStack(IEnumerable<Ability> abilities, object stackingTo)
		{
			var ours = abilities.Where(a => a.Name == Name).ToArray();

			// group abilities
			IEnumerable<IGrouping<object, Ability>> grouped;
			var groupbys = ValueRules.Select(vr => vr == AbilityValueRule.Group ? true : false).ToArray();
			grouped = ours.GroupBy(a =>
				a.Values.Select((v, i) => new {
					Value = v, Index = i}).Join(
				groupbys.Select((b, i) => new {
					DoIt = b, Index = i}),
				item => item.Index, item => item.Index, (item, groupby) =>
					groupby.DoIt ? item.Value.Value : ""));
			
			// stack abilities		
			var list = new List<Tuple<Ability, Ability>>();
			foreach (var group in grouped)
			{
				var stacked = Stack(group, stackingTo);
				foreach (var stack in stacked)
				{
					foreach (var abil in stack)
						list.Add(Tuple.Create(stack.Key, abil));
				}
			}

			return list.ToLookup(t => t.Item1, t => t.Item2);
		}

		private ILookup<Ability, Ability> Stack(IEnumerable<Ability> abilities, object stackingTo)
		{
			if (abilities.Count() <= 1)
				return abilities.ToLookup(a => a, a => a);

			Ability result = new Ability(stackingTo);
			result.Name = abilities.First().Name;
			foreach (var abil in abilities)
			{
				for (int i = 0; i < abil.Values.Count; i++)
				{
					var rule = ValueRules.ElementAtOrDefault(i);
					// TODO - don't repeatedly convert to/from strings, just do it once outside the loop
					double? oldval = result.Values.Count > i ? (double?)result.Values[i].Value.ToDouble() : null;
					double incoming = abil.Values.Count > i ? abil.Values[i].Value.ToDouble() : 0;
					double newval = oldval ?? 0;
					if (rule == AbilityValueRule.Add)
						newval = (oldval ?? 0) + incoming;
					else if (rule == AbilityValueRule.TakeAverage)
						newval = (oldval ?? 0) + incoming / abilities.Count();
					else if (rule == AbilityValueRule.TakeHighest)
					{
						if (oldval == null)
							newval = incoming;
						else
							newval = Math.Max(oldval.Value, incoming);
					}
					else if (rule == AbilityValueRule.TakeLowest)
					{
						if (oldval == null)
							newval = incoming;
						else
							newval = Math.Min(oldval.Value, incoming);
					}
					else if (rule == AbilityValueRule.Group)
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
				result.Description = result.Name + ": " + string.Join(", ", result.Values.Select(v => v.Value).ToArray());
			else
				result.Description = result.Name;
			return abilities.ToLookup(a => result, a => a);
		}
	}

	 /// <summary>
	 /// Rules for grouping and stacking ability values within a group of similar abilities.
	 /// </summary>
	 public enum AbilityValueRule
	 {
		 /// <summary>
		 /// Do not group or stack abilities by this value.
		 /// Note that this does not necessarily mean that only one instance of the ability will apply!
		 /// To guarantee this, use TakeHighest, TakeAverage, or TakeLowest.
		 /// </summary>
		 None,
		 /// <summary>
		 /// Group the abilities by this value.
		 /// </summary>
		 Group,
		 /// <summary>
		 /// Add the values within the group. Only works properly for numeric values.
		 /// </summary>
		 Add,
		 /// <summary>
		 /// Take the highest value within the group. Only works properly for numeric values.
		 /// </summary>
		 TakeHighest,
		 /// <summary>
		 /// Take the average of the group values. Only works properly for numeric values.
		 /// </summary>
		 TakeAverage,
		 /// <summary>
		 /// Take the lowest value within the group. Only works properly for numeric values.
		 /// </summary>
		 TakeLowest
	 }
}
