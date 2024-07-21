using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Ecs.Stats
{
    /// <summary>
    /// A stat of some entity, such as cost or thrust points, which is granted by abilities.
    /// </summary>
    public record Stat
    (
        StatType StatType,
        IList<Modifier> Modifiers
    )
    {
        public string Name => StatType.Name;

        public decimal? Value
        {
            get
            {
                if (Modifiers.Any())
                {
                    var result = 0m;
					for (var priority = Operation.All.Min(it => it.Priority); priority <= Operation.All.Max(it => it.Priority); priority++)
                    {
                        foreach (var operation in Operation.All.Where(it => it.Priority == priority))
                        {
                            var modifiers = Modifiers.Where(it => it.Operation == operation);
                            result = operation.Aggregate(result, modifiers.Select(it => it.Value));
                        }
                    }
                    return result;
				}
                else
                {
                    return null;
                }
            }
        }
    }
}
