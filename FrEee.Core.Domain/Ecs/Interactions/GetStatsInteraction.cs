using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Utility;

namespace FrEee.Ecs.Interactions
{
	public record GetStatsInteraction
	(
		IDictionary<string, Stat> Stats
	) : IInteraction
	{
		public void AddValue(string statName, decimal value)
		{
			var stat = Stats[statName];
            if (stat is null)
            {
				Stats[statName] = new Stat(statName, null, value);
            }
			else
			{
				stat.Values.Add(value);
			}
        }

		public void SetStackingRule(string statName, IStackingRule stackingRule)
		{
			if (Stats.TryGetValue(statName, out var stat))
			{
				stat.StackingRule = stackingRule;
			}
			else
			{
				Stats.Add(statName, new Stat(statName, stackingRule));
			}
		}
	}
}
