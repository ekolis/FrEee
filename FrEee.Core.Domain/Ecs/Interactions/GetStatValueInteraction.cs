using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Ecs.Stats;

namespace FrEee.Ecs.Interactions
{
    public record GetStatValueInteraction
	(
		Stat Stat
	) : IInteraction;
}
