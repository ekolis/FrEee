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
        IList<decimal> Values
    )
    {
        public string Name => StatType.Name;

        public IStackingRule StackingRule => StatType.StackingRule;

        public decimal Value => StackingRule.Stack(Values);
    }
}
