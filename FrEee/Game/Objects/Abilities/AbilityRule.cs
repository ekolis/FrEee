using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.Game.Interfaces;
using FrEee.Modding;
using FrEee.Game.Enumerations;

namespace FrEee.Game.Objects.Abilities
{
	/// <summary>
	/// A rule for grouping and stacking abilities.
	/// </summary>
	[Serializable]
	public class AbilityRule : INamed
	{
		public AbilityRule()
		{
			ValueRules = new List<AbilityValueRule>();
			Aliases = new HashSet<string>();
		}

		/// <summary>
		/// The name of the ability to which this rule applies.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Aliases for this ability's name.
		/// These must not be aliases or names of other abilities.
		/// </summary>
		public ICollection<string> Aliases { get; set; }

		/// <summary>
		/// Valid targets for this ability.
		/// </summary>
		public AbilityTargets Targets { get; set; }

		/// <summary>
		/// Can this ability target something?
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		public bool CanTarget(AbilityTargets target)
		{
			return Targets.HasFlag(target);
		}

		/// <summary>
		/// Does the specified name match this ability's name or aliases?
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public bool Matches(string name)
		{
			return name == Name || Aliases.Contains(name);
		}

		/// <summary>
		/// A default description for abilities which do not provide their own description.
		/// Can use, e.g. [%Amount1%] to specify the amount in the Value 1 field.
		/// </summary>
		public Formula<string> Description { get; set; }

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
			var ours = abilities.Where(a => a.Rule == this).ToArray();

			// group abilities
			var dict = new SafeDictionary<IList<string>, ICollection<Ability>>();
			var groupIndices = new List<int>();
			for (int i = 0; i < ValueRules.Count; i++)
			{
				if (ValueRules[i] == AbilityValueRule.Group)
					groupIndices.Add(i);
			}
			foreach (var a in ours)
			{
				// treat non-group indices as "equal" for grouping purposes
				var key = a.Values.Select((v, i) => groupIndices.Contains(i) ? v.Value : "").ToList();
				var existingKey = dict.Keys.SingleOrDefault(k => k.SequenceEqual(key));
				if (existingKey == null)
				{
					dict[key] = new List<Ability>();
					dict[key].Add(a);
				}
				else
					dict[existingKey].Add(a);
			}

			// stack abilities		
			var list = new List<Tuple<Ability, Ability>>();
			foreach (var group in dict.Values)
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
			result.Rule = abilities.First().Rule;
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
			return abilities.ToLookup(a => result, a => a);
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
