using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.Space;

namespace FrEee.Ecs.Interactions
{
	public record WarpInteraction
	(
		IMobileSpaceObject WarpingVehicle,
		WarpPoint WarpPoint,
		Sector Destination
	) : IInteraction
	{
		public void Execute()
		{
			// TODO: warp interaction execute, see WarpDamageAbility
		}
	}
}
