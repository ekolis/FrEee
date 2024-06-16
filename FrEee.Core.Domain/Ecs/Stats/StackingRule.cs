using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Extensions;

namespace FrEee.Ecs.Stats
{
    public interface IStackingRule
    {
        decimal Stack(IEnumerable<decimal> values);
    }

    public class AdditionStackingRule
        : IStackingRule
    {
        public decimal Stack(IEnumerable<decimal> values)
            => values.Sum();
    }

    public class MaximumStackingRule
        : IStackingRule
    {
        public decimal Stack(IEnumerable<decimal> values)
            => values.MaxOrDefault();
    }

    public class MinimumStackingRule
        : IStackingRule
    {
        public decimal Stack(IEnumerable<decimal> values)
            => values.MinOrDefault();
    }

    public class AverageStackingRule
        : IStackingRule
    {
        public decimal Stack(IEnumerable<decimal> values)
            => values.AverageOrDefault();
    }
}
