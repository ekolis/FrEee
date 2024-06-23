using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Utility;

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

        public static StatType SpaceYardRateMinerals { get; } = new("Space Yard Rate Minerals", new MaximumStackingRule());
		public static StatType SpaceYardRateOrganics { get; } = new("Space Yard Rate Radioactives", new MaximumStackingRule());
		public static StatType SpaceYardRateRadioactives { get; } = new("Space Yard Rate Radioactives", new MaximumStackingRule());
        public static StatType SpaceYardRate(Resource resource) => resource.Name switch
        {
            "Minerals" => SpaceYardRateMinerals,
			"Organics" => SpaceYardRateOrganics,
			"Radioactives" => SpaceYardRateRadioactives,
            _ => throw new ArgumentException($"Can't get space yard rate for resource {resource.Name}. Must be Minerals, Organics, or Radioactives.")
		};
		public static StatType SpaceYardRate(int resourceNumber) => resourceNumber switch
		{
			1 => SpaceYardRateMinerals,
			2 => SpaceYardRateOrganics,
			3 => SpaceYardRateRadioactives,
			_ => throw new ArgumentException($"Can't get space yard rate for resource number {resourceNumber}. Must be between 1 and 3.")
		};

		public static StatType FacilitySize { get; } = new("Facility Size", new AdditionStackingRule());
	}
}
