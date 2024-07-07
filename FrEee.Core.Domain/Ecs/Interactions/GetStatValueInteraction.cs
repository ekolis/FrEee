using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Ecs.Stats;

namespace FrEee.Ecs.Interactions
{
	// TODO: get rid of GetStatValueInteraction, put stat calculations in actual interactions and give them an Execute method
    public record GetStatValueInteraction
	(
		Stat Stat
	) : IInteraction;
}
