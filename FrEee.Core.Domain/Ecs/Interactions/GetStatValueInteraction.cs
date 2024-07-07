using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Ecs.Stats;

namespace FrEee.Ecs.Interactions
{
	// TODO: get rid of GetStatValueInteraction, put stat calculations in actual interactions and give them an Execute method
	[Obsolete("Stats should be gotten within actual abilities that do their actual actions on Execute method call.")]
    public record GetStatValueInteraction
	(
		Stat Stat
	) : IInteraction
	{
		public void Execute()
		{
			// nothing to do
		}
	}
}
