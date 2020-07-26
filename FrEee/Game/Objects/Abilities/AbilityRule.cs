using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#nullable enable

namespace FrEee.Game.Objects.Abilities
{
	/// <summary>
	/// A rule for grouping and stacking abilities.
	/// </summary>
	[Serializable]
	public class AbilityRule : IModObject
	{
		public AbilityRule()
		{
			ValueRules = new List<AbilityValueRule>();
			GroupRules = new List<AbilityValueRule>();
			Aliases = new HashSet<string>();
		}

		/// <summary>
		/// Aliases for this ability's name.
		/// These must not be aliases or names of other abilities.
		/// </summary>
		public ICollection<string> Aliases { get; set; }

		/// <summary>
		/// A default description for abilities which do not provide their own description.
		/// Can use, e.g. [%Amount1%] to specify the amount in the Value 1 field.
		/// </summary>
		public Formula<string>? Description { get; set; }

		/// <summary>
		/// The rules for stacking abilities after grouping.
		/// </summary>
		public IList<AbilityValueRule> GroupRules { get; set; }

		public bool IsActivatable
		{
			get
			{
				// TODO - scriptable ability rules
				return Matches("Emergency Resupply") ||
					Matches("Emergency Energy") ||
					Matches("Self-Destruct") ||
					Matches("Open Warp Point") ||
					Matches("Close Warp Point") ||
					Matches("Create Planet Size") ||
					Matches("Destroy Planet Size") ||
					Matches("Create Star") ||
					Matches("Destroy Star") ||
					Matches("Create Storm") ||
					Matches("Destroy Storm") ||
					Matches("Create Nebulae") ||
					Matches("Destroy Nebulae") ||
					Matches("Create Black Hole") ||
					Matches("Destroy Black Hole") ||
					Matches("Create Constructed Planet - Star") ||
					Matches("Create Constructed Planet - Planet") ||
					Matches("Create Constructed Planet - Storm") ||
					Matches("Create Constructed Planet - Warp Point") ||
					Matches("Create Constructed Planet - Asteroids") ||
					Matches("Create Constructed Planet - Space");
			}
		}

		public bool IsDisposed => false; // can't be disposed of

		public string? ModID { get; set; }

		/// <summary>
		/// The name of the ability to which this rule applies.
		/// </summary>
		public string? Name { get; set; }

		/// <summary>
		/// Valid targets for this ability.
		/// </summary>
		public AbilityTargets Targets { get; set; }

		/// <summary>
		/// Parameters from the mod meta templates.
		/// </summary>
		public IDictionary<string, object>? TemplateParameters { get; set; }

		/// <summary>
		/// The rules for grouping and stacking abilities.
		/// </summary>
		public IList<AbilityValueRule> ValueRules { get; set; }

		/// <summary>
		/// Finds an ability rule in the current mod.
		/// </summary>
		/// <param name="nameOrAlias">The name or alias.</param>
		/// <returns>The ability rule, or null if none matches.</returns>
		public static AbilityRule Find(string nameOrAlias) => Mod.Current.AbilityRules.Where(r => r.Matches(nameOrAlias)).SingleOrDefault();

		/// <summary>
		/// Can this ability target something?
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		public bool CanTarget(AbilityTargets target) => Targets.HasFlag(target);

		public void Dispose()
		{
			// nothing to do
		}

		/// <summary>
		/// Groups and stacks abilities.
		/// </summary>
		/// <param name="abilities"></param>
		/// <returns></returns>
		public ILookup<Ability, Ability> GroupAndStack(IEnumerable<Ability> abilities, IAbilityObject stackingTo)
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
			var stackedInGroups = new SafeDictionary<Ability, IEnumerable<Ability>>();
			foreach (var group in dict.Values)
			{
				var stacked = Stack(group, stackingTo, false);
				foreach (var stack in stacked)
					stackedInGroups.Add(stack.Key, stack);
			}

			var final = new List<Tuple<Ability, Ability>>();

			// stack grouped abilities if needed
			if (ValueRules.Any(r => r == AbilityValueRule.Group))
			{
				var groupLeaders = stackedInGroups.Select(g => g.Key);
				var stacked = Stack(groupLeaders, stackingTo, true);
				foreach (var stack in stacked)
				{
					foreach (var a in stack)
						final.Add(Tuple.Create(stack.Key, a));
				}
			}
			else
			{
				foreach (var group in stackedInGroups)
					final.Add(Tuple.Create(group.Key, group.Key));
			}

			return final.ToLookup(t => t.Item1, t => t.Item2);
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
		/// Does this rule's name or any of its aliases start with the specified prefix?
		/// </summary>
		/// <param name="prefix"></param>
		/// <returns></returns>
		public bool StartsWith(string prefix)
		{
			if (Name is null)
			{
				return false;
			}
			return Name.StartsWith(prefix) || Aliases.Any(a => a.StartsWith(prefix));
		}

		public override string ToString() => Name ?? string.Empty;

		private ILookup<Ability, Ability> Stack(IEnumerable<Ability> abilities, IAbilityObject stackingTo, bool groupStacking)
		{
			if (abilities.Count() <= 1)
				return abilities.ToLookup(a => a, a => a);

			var results = new SafeDictionary<Ability, Ability>(); // keys = original abilities, values = stacked abilities
			foreach (var abil in abilities)
			{
				for (int i = 0; i < abil.Values.Count; i++)
				{
					AbilityValueRule rule;
					if (groupStacking)
						rule = GroupRules.ElementAtOrDefault(i);
					else
						rule = ValueRules.ElementAtOrDefault(i);
					// TODO - don't repeatedly convert to/from strings, just do it once outside the loop
					double? oldval = null;
					if (results[abil] != null)
						oldval = results[abil].Values.Count > i ? (double?)results[abil].Values[i].Value.ToDouble() : null;
					else
					{
						var match = results.Values.Distinct().Where(a => a != null).SingleOrDefault(a => a.Rule == abil.Rule && a.Values.Select((val, idx) => rule != AbilityValueRule.Group && rule != AbilityValueRule.None || a.Values.Count >= abil.Values.Count && a.Values[idx] == abil.Values[idx]).All(b => b));

						if (match != null)
						{
							results[abil] = match;
							oldval = results[abil].Values.Count > i ? (double?)results[abil].Values[i].Value.ToDouble() : null;
						}
						else
							results[abil] = new Ability(stackingTo, abil.Rule);
					}
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
					else // group or none
						newval = incoming;
					if (results[abil].Values.Count > i)
						results[abil].Values[i] = newval.ToString(CultureInfo.InvariantCulture);
					else
					{
						while (results[abil].Values.Count < i)
							results[abil].Values.Add(null);
						results[abil].Values.Add(newval.ToString(CultureInfo.InvariantCulture));
					}
				}
			}
			foreach (var kvp in results)
			{
				if (results.Values.Where(a => a == kvp.Value).Count() == 1)
				{
					// ability is "stacked" alone, just use the original ability description
					results[kvp.Key].Description = kvp.Key.Description;
				}
			}
			return results.ToLookup(kvp => kvp.Value, kvp => kvp.Key);
		}
	}
}
