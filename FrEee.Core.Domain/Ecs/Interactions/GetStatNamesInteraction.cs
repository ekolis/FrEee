using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Utility;

namespace FrEee.Ecs.Interactions
{
	[Obsolete("Stats should be gotten within actual abilities that do their actual actions on Execute method call.")]
	public record GetStatNamesInteraction
	(
		ISet<string> StatNames
	) : IInteraction
	{
		public void Execute()
		{
			// nothing to do
		}
	}
}
