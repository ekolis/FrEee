using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Utility;

namespace FrEee.Ecs.Interactions
{
	public record GetStatNamesInteraction
	(
		ISet<string> StatNames
	) : IInteraction;
}
