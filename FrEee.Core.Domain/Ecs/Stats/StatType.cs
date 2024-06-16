using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Ecs.Stats
{
    /// <summary>
    /// A type of stat that some <see cref="IEntity"/> can have.
    /// </summary>
    /// <param name="Name">The name of the stat.</param>
    /// <param name="StackingRule">Rule for stacking stat values.</param>
    public record StatType
    (
        string Name,
        IStackingRule StackingRule
    )
    {
        // TODO: make stacking rules moddable
        public static StatType WarpDamage { get; } = new("Warp Damage", new AdditionStackingRule());
    }
}
